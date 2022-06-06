//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;

public class SoldierAnimation : MonoBehaviour
{
	
	public Transform spine1;
	public Transform spine2;

	//Animations
	[System.Serializable]
	public class WeaponAnimationSet
    {
		public AnimationClip idle;
		public AnimationClip fire;
	}
	
	public WeaponAnimationSet normalSet;
	public WeaponAnimationSet pistolSet;
	public WeaponAnimationSet knifeSet;

	[System.Serializable]
	public class AnimationSet
    {
		public AnimationClip idle;
		public AnimationClip walk_front;
		public AnimationClip walk_front_left;
		public AnimationClip walk_front_right;
	}

	public AnimationSet standingSet;
	public AnimationSet crouchSet;
	
	public AnimationClip reload;
	public AnimationClip jumpPose;
	public AnimationClip[] killedFalls;
	public AnimationClip TPose;

	WeaponAnimationSet currentWeaponAnimationSet;
	AnimationSet currentAnimationSet;
	AnimationClip currentWalkAnimation;
	FPSWeapon previousSelectedWeapon;

	float forwardSpeed;
	float strafeSpeed;
	Vector3 velocity;
	float speed;
	Vector3 lastPosition;

	[HideInInspector]
	Animation soldierAnimationComponent;
	[HideInInspector]
	public PlayerWeapons playerWeapons;
	[HideInInspector]
	public PlayerNetwork playerNetwork;
	[HideInInspector]
	public int movementState = 0; //1 - grounded, 2 - crouch, 3 - in air
	[HideInInspector]
	public bool isMoving = false;

	bool isKilled = false;

	Vector2 hitPosition;
	Vector2 currentHitPosition;

	AnimationClip currentAnimationToPlay;
	AnimationClip previousAnimationPlayed;

	//Aiming
	Quaternion referenceRotation;
	Quaternion startParentRotationQ = Quaternion.identity;
	Quaternion startChildRotationQ;
	//Spine2
	Quaternion startParentRotationQ2;
	Quaternion startChildRotationQ2;

	bool doneSetup = false;

	//Called from PlayerNetwork.cs upon initialization
	public void Setup ()
    {
		soldierAnimationComponent = GetComponent<Animation>();
		//playerWeapons = GetComponent<PlayerWeapons>();
		soldierAnimationComponent.playAutomatically = false;

		//Prepare animations
		SetMixedTransforms(normalSet);
		SetMixedTransforms(pistolSet);
		SetMixedTransforms(knifeSet);

		SetWalkAnimations(standingSet);
		SetWalkAnimations(crouchSet);

		soldierAnimationComponent[reload.name].wrapMode = WrapMode.Once;
		soldierAnimationComponent[reload.name].AddMixingTransform(spine2);
		soldierAnimationComponent[reload.name].layer = 4;

		soldierAnimationComponent[jumpPose.name].wrapMode = WrapMode.Loop;
		
		for(int i = 0; i < killedFalls.Length; i++)
        {
			soldierAnimationComponent[killedFalls[i].name].wrapMode = WrapMode.Once;
		}

		movementState = 1;

		standingSet.idle.SampleAnimation(gameObject, 0);
		Invoke ("RecalculateBoneRotations", 0.15f);

		//Keep track of player speed
		InvokeRepeating("CalculatePlayerSpeed", 0, 0.15f);

		doneSetup = true;
	}

	void SetMixedTransforms (WeaponAnimationSet anmset)
    {
		if(anmset.idle != null && anmset.fire != null)
        {
			soldierAnimationComponent[anmset.idle.name].wrapMode = WrapMode.Loop;
			soldierAnimationComponent[anmset.idle.name].AddMixingTransform(spine2);	
			soldierAnimationComponent[anmset.idle.name].layer = 3;

			soldierAnimationComponent[anmset.fire.name].wrapMode = WrapMode.Once;
			soldierAnimationComponent[anmset.fire.name].AddMixingTransform(spine2);	
			soldierAnimationComponent[anmset.fire.name].layer = 5;
			//soldierAnimationComponent[anmset.fire.name].speed = 1.25f;
		}
	}

	void SetWalkAnimations (AnimationSet anmset)
    {
		soldierAnimationComponent[anmset.idle.name].wrapMode = WrapMode.Loop;
		soldierAnimationComponent[anmset.walk_front.name].wrapMode = WrapMode.Loop;

		if(anmset.walk_front_left != null)
        {
			soldierAnimationComponent[anmset.walk_front_left.name].wrapMode = WrapMode.Loop;
		}
		if(anmset.walk_front_right != null)
        {
			soldierAnimationComponent[anmset.walk_front_right.name].wrapMode = WrapMode.Loop;
		}
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
		if(isKilled || !playerWeapons || !doneSetup)
			return;

		if(playerWeapons.currentSelectedWeapon != null && previousSelectedWeapon != playerWeapons.currentSelectedWeapon)
        {
			if(playerWeapons.currentSelectedWeapon.wSettings.fireType == PlayerWeapons.FireType.Knife || playerWeapons.currentSelectedWeapon.wSettings.fireType == PlayerWeapons.FireType.Pistol)
            {
				if(playerWeapons.currentSelectedWeapon.wSettings.fireType == PlayerWeapons.FireType.Knife)
                {
					currentWeaponAnimationSet = knifeSet;
				}
                else
                {
					currentWeaponAnimationSet = pistolSet;
				}
			}
            else
            {
				currentWeaponAnimationSet = normalSet;
			}

			soldierAnimationComponent.Play(currentWeaponAnimationSet.idle.name);

			//RecalculateBoneRotations();

			if(currentWeaponAnimationSet.idle == null)
            {
				currentWeaponAnimationSet = normalSet;
			}

			previousSelectedWeapon = playerWeapons.currentSelectedWeapon;
		}

		if(movementState == 3)
        {
			//In Air
			//soldierAnimationComponent.CrossFade(jumpPose.name);
			currentAnimationToPlay = jumpPose;
		}
        else
        {
			if(movementState == 0 || movementState == 1)
            {
				if(currentAnimationSet != standingSet)
                {
					currentAnimationSet = standingSet;
				}
			}

			if(movementState == 2)
            {
				if(currentAnimationSet != crouchSet)
                {
					currentAnimationSet = crouchSet;
				}
			}

			if(isMoving && (forwardSpeed > 0.2f || forwardSpeed < -0.2f))
            {
				if((currentAnimationSet.walk_front_left != null && currentAnimationSet.walk_front_right != null) && (strafeSpeed > 0.3f || strafeSpeed < -0.3f))
                {
					currentWalkAnimation = strafeSpeed > 0.3f ? currentAnimationSet.walk_front_left : currentAnimationSet.walk_front_right;
				}
                else
                {
					currentWalkAnimation = currentAnimationSet.walk_front;
				}

				if(soldierAnimationComponent[currentWalkAnimation.name].speed != forwardSpeed)
                {
					soldierAnimationComponent[currentWalkAnimation.name].speed = forwardSpeed*1.1f;
				}

				//soldierAnimationComponent.CrossFade(currentWalkAnimation.name);
				currentAnimationToPlay = currentWalkAnimation;
			}
            else
            {
				//soldierAnimationComponent.CrossFade(currentAnimationSet.idle.name);
				currentAnimationToPlay = currentAnimationSet.idle;
			}
		}

		if(previousAnimationPlayed != currentAnimationToPlay)
        {
			previousAnimationPlayed = currentAnimationToPlay;
			soldierAnimationComponent.CrossFade(currentAnimationToPlay.name);

			//print (previousAnimationPlayed.name + " " + soldierAnimationComponent[previousAnimationPlayed.name].wrapMode.ToString());
		}

		//Aiming
		if(startParentRotationQ != Quaternion.identity)
        {
			Vector3 directionTmp = playerWeapons.playerCamera.forward;
			//Rotate first spine only 35% of direction and rotate spine 2 fully
			referenceRotation = Quaternion.LookRotation((playerWeapons.playerCamera.position + new Vector3(directionTmp.x, directionTmp.y * 0.35f, directionTmp.z) * 2.5f) - spine1.position, spine1.position - spine2.position);
			//print ("Tmp direction: " + playerWeapons.playerCamera.forward.ToString());
			spine1.rotation = (referenceRotation * Quaternion.Inverse(startParentRotationQ)) * startChildRotationQ;

			//Spine2
			referenceRotation = Quaternion.LookRotation((playerWeapons.playerCamera.position + directionTmp * 2.5f) - spine2.position, spine1.position - spine2.position);
			spine2.rotation = (referenceRotation * Quaternion.Inverse(startParentRotationQ2)) * startChildRotationQ2;
		}

		//Hit effect rotation
		spine2.eulerAngles = new Vector3(spine2.eulerAngles.x + currentHitPosition.x, spine2.eulerAngles.y + currentHitPosition.y, spine2.eulerAngles.z);
	}

	void CalculatePlayerSpeed ()
    {	
		if(!doneSetup)
			return;

		//Calculate speed
		if(isMoving)
        {
			velocity = (playerNetwork.thisT.position - lastPosition) / Time.deltaTime; //Units per second.
			speed = (playerNetwork.thisT.position - lastPosition).magnitude / Time.deltaTime;
			
			lastPosition = playerNetwork.thisT.position;
			
			if(speed > 0)
            {
				forwardSpeed = playerNetwork.thisT.InverseTransformDirection(velocity).z * Time.deltaTime * 0.61f;
				strafeSpeed = -(playerNetwork.thisT.InverseTransformDirection(velocity).x * Time.deltaTime) * 0.61f;
				
				if((strafeSpeed > 0.3f || strafeSpeed < -0.3f) && forwardSpeed > -0.2f && forwardSpeed < 0.2f)
                {
					forwardSpeed = Mathf.Abs (strafeSpeed);
				}
			}
		}
        else
        {
			speed = 0;
			velocity = Vector3.zero;
			forwardSpeed = 0;
			strafeSpeed = 0;
		}
	}

	void RecalculateBoneRotations()
    {
		if(gameObject.activeSelf && playerWeapons)
        {
			//Setup references for aiming
			Vector3 directionTmp = transform.forward;
			referenceRotation = Quaternion.LookRotation((playerWeapons.playerCamera.position + directionTmp * 2.5f) - spine1.position, spine1.position - spine2.position);
			startParentRotationQ = referenceRotation;
			startChildRotationQ = spine1.rotation;
			//Spine2
			referenceRotation = Quaternion.LookRotation((playerWeapons.playerCamera.position + directionTmp * 2.5f) - spine2.position, spine1.position - spine2.position);
			startParentRotationQ2 = referenceRotation;
			startChildRotationQ2 = spine2.rotation;
		}
	}
	
	public void PlayFireAnimation ()
    {
		soldierAnimationComponent.Rewind(currentWeaponAnimationSet.fire.name);
		soldierAnimationComponent.Play(currentWeaponAnimationSet.fire.name);
		//print ("Play fire animation remote!");
	}
	
	public void PlayReloadAnimation (float duration)
    {
		soldierAnimationComponent.Rewind(reload.name);
		soldierAnimationComponent[reload.name].speed = soldierAnimationComponent[reload.name].length/duration;
		soldierAnimationComponent.Play(reload.name);
	}

	public void PlayKillAnimation ()
    {
		if(!doneSetup)
        {
			soldierAnimationComponent = GetComponent<Animation>();
			for(int i = 0; i < killedFalls.Length; i++)
            {
				soldierAnimationComponent[killedFalls[i].name].wrapMode = WrapMode.Once;
			}
		}

		isKilled = true;
		soldierAnimationComponent.Stop();
		if(killedFalls.Length > 0)
        {
			int rnd = Random.Range(0, killedFalls.Length - 1);
			soldierAnimationComponent.Play(killedFalls[rnd].name);
		}
	}

	public void DoHitMovement ()
    {
		hitPosition = new Vector2(Random.Range(-15, 15), Random.Range(-15, 15));
		StopCoroutine(DoHitMovementCoroutine());
		StartCoroutine(DoHitMovementCoroutine());
	}

	IEnumerator DoHitMovementCoroutine ()
    {
		while(Mathf.Abs(hitPosition.magnitude - currentHitPosition.magnitude) > 0.25f)
        {
			currentHitPosition = Vector2.Lerp(currentHitPosition, hitPosition, Time.deltaTime * 19);
			yield return null;
		}

		hitPosition = Vector2.zero;

		while(Mathf.Abs(hitPosition.magnitude - currentHitPosition.magnitude) > 0.01f)
        {
			currentHitPosition = Vector2.Lerp(currentHitPosition, hitPosition, Time.deltaTime * 7);
			yield return null;
		}
	}
}
