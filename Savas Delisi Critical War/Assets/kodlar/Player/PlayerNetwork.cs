//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable; //Replace default Hashtables with Photon hashtables

public class PlayerNetwork : Photon.MonoBehaviour
{

	//This is main script that coordinate our player in game, it decides which component to enable/disable
	//Also it sync's our data with remote instances over network

	public SoldierAnimation soldierAnimation;
	public Renderer soldierBody;
	public Material teamALook;
	public Material teamBLook;
	public Material fpsHandMaterial;
	public PlayerWeapons playerWeapons;
	public GameObject firstPersonView;

	public Transform nameLabelTransform;
	public GUIText nameLabel;
	public GUIText nameLabelShadow;

	public AudioSource playerAudio;
	public AudioSource walkingAudio;

	public AudioClip hitSound;
	public AudioClip[] walkingSounds;
	public AudioClip[] ladderSounds;

	public Collider[] headHitBoxes;
	public Collider[] torsoHitBoxes;
	public Collider[] limbsHitBoxes;

	Vector3 playerPos = Vector3.zero;
	[HideInInspector]
	public Vector3 aimPos = Vector3.zero;
	Vector3 smoothAimPos = Vector3.zero;
	float positionSmoother = 17.5f;
	float d;

	[HideInInspector]
	public FPSController fpsController;
	FPSMouseLook localMouseLook;
	[HideInInspector]
	public FPSMouseLook cameraMouseLook;
	[HideInInspector]
	public Transform thisT;
	//[HideInInspector]
	//public RoomController rc;
	[HideInInspector]
	public int playerID;
	[HideInInspector]
	public bool playerKilled = false;
	
	int currentWeaponIndex = -1;
	int previousWeaponIndex = -1;
	bool isFiringRemote = false;
	int playerTeam;
	Camera mainCamera;
	Transform mainCameraT;
	Vector3 screenPos; //For name label display
	float offset;
	//Save references incase we kill this player
	string lastWeaponName = "";
	int lastBodyPart = -1;
	
	//Position Interpolation Sync
	double interpolationBackTime = 0.37d; 
	
	internal struct State
    {
		internal double timestamp;
		internal Vector3 pos;
		internal Quaternion rot;
	}

	//double currentTime;
	
	// We store twenty states with "playback" information
	State[] m_BufferedState = new State[20];
	// Keep track of what slots are used
	int m_TimestampCount;
	int movementStateLocal = -1;
	int receivedMovementState = -1;
	float stepLength; //Used for playing walking sounds
	
	// Use this for initialization
	void Start ()
    {
		photonView.synchronization = ViewSynchronization.Unreliable;

		localMouseLook = GetComponent<FPSMouseLook>();
		fpsController = GetComponent<FPSController>();
		fpsController.pn = this;

		playerWeapons.playerNetwork = this;
		playerWeapons.soldierAnimation = soldierAnimation;
		playerWeapons.fpsController = fpsController;
		playerWeapons.QuickSetup(photonView.isMine);

		gameObject.name = photonView.owner.name;
		thisT = transform;

		playerKilled = false;

		gameObject.layer = 2; //Set layer to Ignore Raycast

		if(!photonView.isMine)
        {
			//Deactivate all scripts and object that are not used by remote instance
			localMouseLook.enabled = false;
			fpsController.enabled = false;
			playerWeapons.enabled = false;
			firstPersonView.SetActive(false);
			if(!soldierAnimation.gameObject.activeSelf)
            {
				soldierAnimation.gameObject.SetActive(true);
			}
			soldierAnimation.playerWeapons = playerWeapons;
			soldierAnimation.playerNetwork = this;
			soldierAnimation.Setup();

			SetupBoxes(headHitBoxes, HitBox.BodyPart.Head);
			SetupBoxes(torsoHitBoxes, HitBox.BodyPart.Torso);
			SetupBoxes(limbsHitBoxes, HitBox.BodyPart.Limbs);

			//Add this player to reference
			GameSettings.rc.otherPlayers.Add(this);
		}
        else
        {
			firstPersonView.SetActive(true);
			soldierAnimation.gameObject.SetActive(false);
			cameraMouseLook = playerWeapons.playerCamera.GetComponent<FPSMouseLook>();
			nameLabelTransform.gameObject.SetActive(false);
			gameObject.tag = "Player";
		}

		if(PhotonNetwork.isMasterClient)
        {
			//Set player HP
			Hashtable setPlayerData = new Hashtable();
			setPlayerData.Add("PlayerHP", (int)100);
			photonView.owner.SetCustomProperties(setPlayerData);
		}

		playerTeam = (int)photonView.owner.customProperties["Team"];
		playerID = photonView.owner.ID;

		if(playerTeam == 1 || playerTeam == 2)
        {
			if(playerTeam == 1)
            {
				soldierBody.sharedMaterial = teamALook;
			}
            else
            {
				soldierBody.sharedMaterial = teamBLook;
			}

			if(photonView.isMine)
            {
				fpsHandMaterial.SetTexture("_MainTex", soldierBody.sharedMaterial.GetTexture("_MainTex"));
				fpsHandMaterial.color = soldierBody.sharedMaterial.color;
			}
            else
            {
				nameLabel.text = photonView.name;
				nameLabelShadow.text = photonView.name;
				nameLabel.color = playerTeam == 1 ? GameSettings.teamAColor : GameSettings.teamBColor;
			}
		}
	}

	void SetupBoxes (Collider[] tmpBoxes, HitBox.BodyPart bp)
    {
		for(int i = 0; i < tmpBoxes.Length; i++)
        {
			tmpBoxes[i].isTrigger = true;
			tmpBoxes[i].gameObject.AddComponent<HitBox>().AssignVariables(this, bp);
			tmpBoxes[i].tag = "Body";
		}
	}

	//Sync player over network
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
		if (stream.isWriting)
        {
			//Send data
			if(!thisT)
				return;

			stream.SendNext(thisT.position);
			stream.SendNext(playerWeapons.playerCamera.position + playerWeapons.playerCamera.forward * 100);
			stream.SendNext(playerWeapons.globalWeaponIndex);
			stream.SendNext(playerWeapons.isFiring);
			stream.SendNext(fpsController.movementState);
		}
        else
        {
			//Receive data
			playerPos = (Vector3)stream.ReceiveNext();
			aimPos = (Vector3)stream.ReceiveNext();
			currentWeaponIndex = (int)stream.ReceiveNext();
			isFiringRemote = (bool)stream.ReceiveNext();
			soldierAnimation.movementState = (int)stream.ReceiveNext();


			// Shift buffer contents, oldest data erased, 18 becomes 19, ... , 0 becomes 1
			for (int i=m_BufferedState.Length-1;i>=1;i--)
			{
				m_BufferedState[i] = m_BufferedState[i-1];
			}
			
			// Save currect received state as 0 in the buffer, safe to overwrite after shifting
			State state = new State();
			state.timestamp = info.timestamp;
			state.pos = playerPos;
			//state.rot = rot;
			m_BufferedState[0] = state;
			
			// Increment state count but never exceed buffer size
			m_TimestampCount = Mathf.Min(m_TimestampCount + 1, m_BufferedState.Length);
			
			// Check integrity, lowest numbered state in the buffer is newest and so on
			for (int i=0;i<m_TimestampCount-1;i++)
			{
				if (m_BufferedState[i].timestamp < m_BufferedState[i+1].timestamp)
					Debug.Log("State inconsistent");
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!photonView.isMine)
        {
			InterpolatePosition ();

			smoothAimPos = Vector3.Lerp(smoothAimPos, aimPos, Time.deltaTime * positionSmoother);

			if(aimPos != Vector3.zero)
            {
				thisT.LookAt(new Vector3(smoothAimPos.x, thisT.position.y, smoothAimPos.z));
				playerWeapons.playerCamera.LookAt(smoothAimPos);
			}

			if(isFiringRemote)
            {
				playerWeapons.FireRemote();
			}

			if(previousWeaponIndex != currentWeaponIndex)
            {
				previousWeaponIndex = currentWeaponIndex;
				playerWeapons.globalWeaponIndex = currentWeaponIndex;
				playerWeapons.SwitchWeaponRemote();
			}

			//Show name label for our teammates
			if(!playerKilled && ((playerTeam == GameSettings.ourTeam && GameSettings.currentGameMode != "FFA") || GameSettings.ourTeam == 0))
            {
				if(!nameLabelTransform.gameObject.activeSelf)
                {
					nameLabelTransform.gameObject.SetActive(true);
				}
				
				if(!mainCamera || !mainCamera.gameObject.activeInHierarchy)
                {
					if(Camera.main)
                    {
						mainCamera = Camera.main;
						mainCameraT = mainCamera.transform;
					}
				}
                else
                {
					offset = Vector3.Distance(mainCameraT.position, thisT.position)/50;
					screenPos = mainCamera.WorldToViewportPoint(new Vector3(thisT.position.x, thisT.position.y + 2.6f + offset, thisT.position.z));

					if(screenPos.z > 0)
                    {
						nameLabelTransform.position = new Vector3(screenPos.x, screenPos.y);
					}
                    else
                    {
						nameLabelTransform.position = new Vector3(-350, -350);
					}
				}

				nameLabelTransform.eulerAngles = Vector3.zero;
			}
            else
            {
				if(nameLabelTransform.gameObject.activeSelf)
                {
					nameLabelTransform.gameObject.SetActive(false);
				}
			}

			receivedMovementState = soldierAnimation.movementState;
		}
        else
        {
			//Send cameraFOV to reduce mouse sensitivity when aiming
			//localMouseLook.cameraFOV = playerWeapons.mainPlayerCamera.fieldOfView;
			//cameraMouseLook.cameraFOV = playerWeapons.mainPlayerCamera.fieldOfView;

			receivedMovementState = fpsController.movementState;
		}

		//Play footstep sound for both local and remote player instances
		if(!playerKilled)
        {
			if(movementStateLocal != receivedMovementState)
            {
				movementStateLocal = receivedMovementState;
				PlayWalkingSound();
			}
		}
        else
        {
			if(walkingAudio.isPlaying)
            {
				walkingAudio.Stop();
			}
		}
	}
	
	void InterpolatePosition ()
    {
		d = Vector3.Distance(thisT.position, m_BufferedState[0].pos);
		soldierAnimation.isMoving = d > 0.1f;

		double currentTime = PhotonNetwork.time;
		double interpolationTime = currentTime - interpolationBackTime;
		// We have a window of interpolationBackTime where we basically play 
		// By having interpolationBackTime the average ping, you will usually use interpolation.
		// And only if no more data arrives we will use extrapolation
		
		// Use interpolation
		// Check if latest state exceeds interpolation time, if this is the case then
		// it is too old and extrapolation should be used
		
		if (m_BufferedState[0].timestamp > interpolationTime)
        {
			for (int i=0;i<m_TimestampCount;i++)
            {
				// Find the state which matches the interpolation time (time+0.1) or use last state
				if (m_BufferedState[i].timestamp <= interpolationTime || i == m_TimestampCount-1)
                {
					// The state one slot newer (<100ms) than the best playback state
					State rhs = m_BufferedState[Mathf.Max(i-1, 0)];
					// The best playback state (closest to 100 ms old (default time))
					State lhs = m_BufferedState[i];
					
					// Use the time between the two slots to determine if interpolation is necessary
					double length = rhs.timestamp - lhs.timestamp;
					float t = 0.0F;
					// As the time difference gets closer to 100 ms t gets closer to 1 in 
					// which case rhs is only used
					if (length > 0.0001)
                    {
						t = (float)((interpolationTime - lhs.timestamp) / length);
					}
					
					// if t=0 => lhs is used directly
					thisT.position = Vector3.Lerp(lhs.pos, rhs.pos, t);
					//transform.localRotation = Quaternion.Slerp(lhs.rot, rhs.rot, t);
					return;
				}
			}
		}
        else
        {
			// Use extrapolation. Here we do something really simple and just repeat the last
			// received state. You can do clever stuff with predicting what should happen

			State latest = m_BufferedState[0];
			
			thisT.position = latest.pos;
			//transform.localRotation = latest.rot;
			
			//print ("Moving DIrectly to latest pos " + Random.Range(111, 333).ToString());
		}
	}

	//Called from Weapon.cs when player shooting single fire weapons
	public void FireSingleRemote()
    {
		photonView.RPC("FireRemoteRPC", PhotonTargets.Others);
	}

	[PunRPC]
	void FireRemoteRPC()
    {
		playerWeapons.FireRemote();
	}

	//Reload Sync
	public void DoReload ()
    {
		photonView.RPC("DoReloadRemote", PhotonTargets.Others);
	}

	[PunRPC]
	void DoReloadRemote()
    {
		if(playerWeapons.currentSelectedWeapon)
        {
			playerWeapons.currentSelectedWeapon.ReloadNetworkSync();
		}
	}

	//Do player damage, called from HitBox.cs
	public void ApplyDamage(int[] values)
    {
		if(!playerKilled && (playerTeam != GameSettings.ourTeam || photonView.isMine || GameSettings.currentGameMode == "FFA"))
        {
			photonView.RPC("DamageRemote", PhotonTargets.All, values, PhotonNetwork.player.ID);
		}
	}

	[PunRPC]
	void DamageRemote (int[] values, int killerID)
    {
		if(photonView.isMine)
        {
			GameSettings.rc.DoHitDetector((int)values[2]);
			fpsController.fallSlowDown = 0.5f;

			PlayHitSound();
		}
        else
        {
			soldierAnimation.DoHitMovement();
		}

		if(!playerKilled)
        {
			if(PhotonNetwork.player.ID == killerID)
            {
				//Save temp references
				lastWeaponName = GameSettings.rc.ourPlayer ? GameSettings.rc.ourPlayer.playerWeapons.currentSelectedWeapon.weaponName : "";
				lastBodyPart = values[1];
			}

			if(PhotonNetwork.isMasterClient)
            {
				int currentHP = photonView.owner.customProperties["PlayerHP"] != null ? (int)photonView.owner.customProperties["PlayerHP"] : 100;
				currentHP -= GetDMG((int)values[0], (int)values[1]);
				Hashtable setPlayerData = new Hashtable();
				setPlayerData.Add("PlayerHP", currentHP); //Setup player HP by master client
				photonView.owner.SetCustomProperties(setPlayerData);

				if(currentHP < 1)
                {
					photonView.RPC("KillPlayer", PhotonTargets.All, killerID);
					playerKilled = true;
				}
			}
		}
	}

	[PunRPC]
	void KillPlayer (int killerID)
    {
		playerKilled = true;

		if(photonView.isMine)
        {
			soldierAnimation.gameObject.SetActive(true);
			firstPersonView.SetActive(false);

			localMouseLook.enabled = false;
			//fpsController.enabled = false;
			playerWeapons.enabled = false;
			playerWeapons.isFiring = false;

			GameSettings.rc.PrepareRespawn(-killerID, false);
		}

		if(PhotonNetwork.isMasterClient)
        {
			//Set killed player deaths
			if(photonView.owner != null)
            {
				int newDeaths = photonView.owner.customProperties["Deaths"] == null ? 1 : (int)photonView.owner.customProperties["Deaths"] + 1;
				Hashtable setPlayerProperties = new Hashtable();
				setPlayerProperties.Add("Deaths", newDeaths);
				photonView.owner.SetCustomProperties(setPlayerProperties);

				//Do not add kills nor team score if we killed ourselves (fell down etc.)
				if(photonView.owner.ID != killerID)
                {
					//Find killer player instance
					PhotonPlayer killerPLayer = null;
					PhotonPlayer[] allPlayers = PhotonNetwork.playerList;
					for(int i = 0; i < allPlayers.Length; i++)
                    {
						if(allPlayers[i].ID == killerID)
                        {
							killerPLayer = allPlayers[i];
						}
					}

					if(killerPLayer != null)
                    {
						//Add kills for killer
						int newKills = killerPLayer.customProperties["Kills"] == null ? 1 : (int)killerPLayer.customProperties["Kills"] + 1;
						setPlayerProperties = new Hashtable();
						setPlayerProperties.Add("Kills", newKills);
						killerPLayer.SetCustomProperties(setPlayerProperties);

						//Update team scores
						if(GameSettings.currentGameMode == "TDM")
                        {
							Hashtable setRoomProperties = new Hashtable();
							int currentTeamScore = 0;

							if((int)killerPLayer.customProperties["Team"] == 1)
                            {
								currentTeamScore = PhotonNetwork.room.customProperties["TeamAScore"] != null ? (int)PhotonNetwork.room.customProperties["TeamAScore"] + 1: 1;
								setRoomProperties.Add("TeamAScore", currentTeamScore);
							}
							if((int)killerPLayer.customProperties["Team"] == 2)
                            {
								currentTeamScore = PhotonNetwork.room.customProperties["TeamBScore"] != null ? (int)PhotonNetwork.room.customProperties["TeamBScore"] + 1 : 1;
								setRoomProperties.Add("TeamBScore", currentTeamScore);
							}

							if(setRoomProperties.Count > 0)
                            {
								PhotonNetwork.room.SetCustomProperties(setRoomProperties);
							}
						}
					}
				}
			}
		}

		if(PhotonNetwork.player.ID == killerID)
        {
			//Check what weapon we used right before
			string selectedWeaponNameTmp = "[" + lastWeaponName + "]";
			string killedPlayerName = photonView.owner.name;
			int killedPlayerTeam = (int)photonView.owner.customProperties["Team"];

			if(lastBodyPart == -35)
            {
				selectedWeaponNameTmp = "fell";
				killedPlayerName = "down";
				killedPlayerTeam = 0;
			}
            else
            {
				if(lastBodyPart == 0)
                {
					selectedWeaponNameTmp += " -> Headshot";
				}
			}
			//Notify others on kill and add cash
			GameSettings.rc.ReportKill(killedPlayerName, selectedWeaponNameTmp, killedPlayerTeam);
			GameSettings.rc.AddKillCash(lastBodyPart );
		}

		soldierAnimation.PlayKillAnimation();
	}

	int GetDMG (int weaponIndex, int bodyPart)
    {
		if(weaponIndex > -1 && weaponIndex < playerWeapons.totalWeapons.Count)
        {
			//For shotgun we divide damage for number of fractions
			int divideBy = playerWeapons.totalWeapons[weaponIndex].fireType == PlayerWeapons.FireType.Shotgun ? 5 : 1;

			if(bodyPart == 0 || bodyPart == 1)
            {
				if(bodyPart == 0)
                {
					return  playerWeapons.totalWeapons[weaponIndex].headDamage/divideBy;
				}
                else
                {
					return playerWeapons.totalWeapons[weaponIndex].torsoDamage/divideBy;
				}
			}
            else
            {
				return playerWeapons.totalWeapons[weaponIndex].limbsDamage/divideBy;
			}
		}
        else
        {
			return Mathf.Abs(weaponIndex);
		}
	}

	void PlayHitSound ()
    {
		playerAudio.Stop();
		if(playerAudio.clip != hitSound)
        {
			playerAudio.clip = hitSound;
		}
		playerAudio.Play();
	}

	void PlayWalkingSound ()
    {
		//print ("Changed walking state");

		CancelInvoke("PlayWalkingSoundInvoke");

		stepLength = 0;

		if(movementStateLocal == 0 || movementStateLocal == 1 || movementStateLocal == 2 || movementStateLocal == 4)
        {
			if(movementStateLocal == 4)
            {
				//On ladder
				stepLength = 2.5f/fpsController.ladderSpeed;
			}
            else
            {
				if(movementStateLocal == 2)
                {
					//Crouching
					stepLength = 3.5f/fpsController.crouchSpeed;
				}
                else
                {
					if(movementStateLocal == 0)
                    {
						stepLength = 3.5f/fpsController.walkSpeed;
					}
                    else
                    {
						stepLength = 3.5f/fpsController.runSpeed;
					}
				}
			}
		}

		if(stepLength > 0)
        {
			InvokeRepeating("PlayWalkingSoundInvoke", 0.09f, stepLength);
		}
	}

	void PlayWalkingSoundInvoke ()
    {
		walkingAudio.Stop();

		if(fpsController.isMoving || soldierAnimation.isMoving)
        {
			if(movementStateLocal == 4)
            {
				//On ladder
				walkingAudio.clip = ladderSounds[Random.Range(0, ladderSounds.Length - 1)];
			}
            else
            {
				//Walkig
				walkingAudio.clip = walkingSounds[Random.Range(0, walkingSounds.Length - 1)];
			}

			walkingAudio.Play();
		}
	}
}
