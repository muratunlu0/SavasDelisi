

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapons : MonoBehaviour
{
	
	public Transform playerCamera;
	//Impact particles used by every weapon
	public GameObject concreteParticles;
	public GameObject metalParticles;
	public GameObject woodParticles;
	public GameObject bloodParticles;
    RoomController rc;
    public AudioSource firstPersonAudioSource;
	public AudioSource thirdPersonAudioSource;

	public enum FireType { MachineGun, Pistol, Shotgun, SniperRifle, Knife }
	public enum AimType { None, CameraOnly, CameraAndIronsights }

	[System.Serializable]
	public class WeaponSet
    {
		public FPSWeapon firstPersonWeapon;
		public FPSWeapon thirdPersonWeapon;
		public int weaponCost;
		public FireType fireType;
		public float timeToDeploy; //How long it should take to take this weapon
		public float reloadTime; //How long it should take to reload this weapon
		public float fireRate;
		//Note: You can only change bullet count in Edit mode
		public int bulletsPerClip;
		public int reserveBullets;
		public int headDamage;
		public int torsoDamage;
		public int limbsDamage;
		public AudioClip fireSound;
		public AudioClip reloadSound;
		public AudioClip takeInSound;
		//Aiming
		public AimType aimType;
		public float aimFOV;
		public Transform aimObject;
		public Sprite scopeTexture;

		[HideInInspector]
		public int obfuscatedPrice;

		[HideInInspector]
		public bool showThis; //Used by custom editor to hide individual weapons in inspector

		public WeaponSet (FPSWeapon fpw, FPSWeapon tpw)
        {
			firstPersonWeapon = fpw;
			thirdPersonWeapon = tpw;
			weaponCost = 0;
			timeToDeploy = 1;
			reloadTime = 1;
			fireRate = 0.05f;
			bulletsPerClip = 30;
            reserveBullets = 150;
			headDamage = 30;
			torsoDamage = 15;
			limbsDamage = 10;

			aimType = AimType.None;
			aimFOV = GameSettings.defaultFOV;
			aimObject = null;
			scopeTexture = null;

			obfuscatedPrice = 0;
			showThis = true;
		}
	}

	public List<WeaponSet> primaryWeapons;
	public List<WeaponSet> secondaryWeapons;
	public List<WeaponSet> specialWeapons;
	//For reference purposes
	[HideInInspector]
	public List<WeaponSet> totalWeapons = new List<WeaponSet>();

	//Primary, Secondary and Special weapons should be index from weapons array
	//For example if primaryWeapon = 0, than player will select Element 1 from weapons (primaryWeapons[1])
	public int selectedPrimary = 1;
	public int selectedSecondary = 0;
	public int selectedSpecial = 0;

	Vector3 defaultSwayPosition;
	float bobbingSpeed = 0.0135f; 
	float bobbingAmount = 0.0175f; 
	float timer = 0.0f; 
	float currentBobbingSpeed = 0;

	Transform firstPersonWeapons;

	[HideInInspector]
	public FPSWeapon currentSelectedWeapon;
	[HideInInspector]
	public int globalWeaponIndex = -1; //This value will be sent over network to alert other players what wepaon we have selected
	[HideInInspector]
	public bool isFiring = false;

	//These vairbales are assigned at PlayerNetwork.cs
	[HideInInspector]
	public SoldierAnimation soldierAnimation;
	[HideInInspector]
	public PlayerNetwork playerNetwork;
	[HideInInspector]
	public FPSController fpsController;
	[HideInInspector]
	public Camera mainPlayerCamera;

	bool isAimed = false;

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
    int previousWeaponIndex = 0;
#endif

	//Called from PlayerNetwork.cs
	public void QuickSetup(bool isLocal)
    {
		firstPersonWeapons = firstPersonAudioSource.transform;

		//Prepare weapons
		totalWeapons.Clear();
		PrepareWepaons(primaryWeapons);
		PrepareWepaons(secondaryWeapons);
		PrepareWepaons(specialWeapons);

		if(isLocal)
        {
			//SwitchWeapon(primaryWeapons[selectedPrimary].firstPersonWeapon,  true);
			defaultSwayPosition = firstPersonWeapons.localPosition;
			mainPlayerCamera = playerCamera.GetComponent<Camera>();
			mainPlayerCamera.fieldOfView = GameSettings.defaultFOV;
		}
	}
    void Start()
    {
        rc = GetComponent<RoomController>();
        //if (Application.loadedLevelName == "ÖLÜM KOŞUSU")
        //{
        //    specialWeapons[0].headDamage = 0;
        //    specialWeapons[0].torsoDamage = 0;
        //    specialWeapons[0].limbsDamage = 0;
        //}
        
    }
    void PrepareWepaons(List<WeaponSet> tmpWeapons)
    {
		for(int i = 0; i < tmpWeapons.Count; i++)
        {
			tmpWeapons[i].firstPersonWeapon.playerWeapons = this;
			tmpWeapons[i].firstPersonWeapon.wSettings = tmpWeapons[i];
			tmpWeapons[i].firstPersonWeapon.audioSource = firstPersonAudioSource;
			tmpWeapons[i].firstPersonWeapon.gameObject.SetActive(false);
			tmpWeapons[i].firstPersonWeapon.playerNetwork = playerNetwork;
			tmpWeapons[i].firstPersonWeapon.isThirdPerson = false;

			if(tmpWeapons[i].thirdPersonWeapon != null)
            {
				tmpWeapons[i].thirdPersonWeapon.playerWeapons = this;
				tmpWeapons[i].thirdPersonWeapon.wSettings =  tmpWeapons[i];
				tmpWeapons[i].thirdPersonWeapon.audioSource = thirdPersonAudioSource;
				tmpWeapons[i].thirdPersonWeapon.gameObject.SetActive(false);
				tmpWeapons[i].thirdPersonWeapon.soldierAnimation = soldierAnimation;
				tmpWeapons[i].thirdPersonWeapon.playerNetwork = playerNetwork;
				tmpWeapons[i].thirdPersonWeapon.isThirdPerson = true;
			}

			totalWeapons.Add(tmpWeapons[i]);
		}
	}

    
	// Update is called once per frame
	void Update ()
    {
		if(GameSettings.menuOpened)
        {
			isFiring = false;
			return;
		}

#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WP8 && !UNITY_WP8_1
            //Switch weapons
            if (Input.GetKeyDown(GameSettings.playerKeys[2]))
            {
			    GetWeaponToSelect(1);
		    }

		    if(Input.GetKeyDown(GameSettings.playerKeys[3]))
            {
			    GetWeaponToSelect(2);
		    }

		    if(Input.GetKeyDown(GameSettings.playerKeys[4]))
            {
			    GetWeaponToSelect(3);
		    }
#else
        //Switch weapons mobile
        if(previousWeaponIndex != GameSettings.switchWeaponIndex)
        {
            if(GameSettings.switchWeaponIndex > 3 || GameSettings.switchWeaponIndex < 1)
            {
                GameSettings.switchWeaponIndex = 1;
            }

            previousWeaponIndex = GameSettings.switchWeaponIndex;

            GetWeaponToSelect(previousWeaponIndex);
            
        }
#endif

        if(currentSelectedWeapon)
        {

#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WP8 && !UNITY_WP8_1
			//Reload
			if(Input.GetKeyDown(GameSettings.playerKeys[5]) && !currentSelectedWeapon.isReloading)
            {
				currentSelectedWeapon.ReloadRemote();
			}

			//Fire
			if(currentSelectedWeapon.wSettings.fireType == PlayerWeapons.FireType.MachineGun)
            {
				//Automatic fire
				if(Input.GetKey(GameSettings.playerKeys[0]))
                {
					currentSelectedWeapon.Fire();
				}
                else
                {
					isFiring = false;
				}
			}
            else
            {
				//Single fire
				if(Input.GetKeyDown(GameSettings.playerKeys[0]))
                {
					currentSelectedWeapon.Fire();
				}

				isFiring = false;
			}

			//Aiming
			if(currentSelectedWeapon.wSettings.aimType != AimType.None)
            {
				if(Input.GetKeyDown(GameSettings.playerKeys[1]))
                {
					isAimed = !isAimed;

					if(isAimed && currentSelectedWeapon.wSettings.aimType == AimType.CameraOnly && currentSelectedWeapon.wSettings.scopeTexture != null)
                    {
						GameSettings.currentScopeTexture = currentSelectedWeapon.wSettings.scopeTexture;
					}
                    else
                    {
						GameSettings.currentScopeTexture = null;
					}
				}
			}
#else
            //Mobile reloading
            if (GameSettings.mobileReloading && !currentSelectedWeapon.isReloading)
            {
                currentSelectedWeapon.ReloadRemote();
            }

            if (GameSettings.mobileReloading)
            {
                GameSettings.mobileReloading = false;
            }

            //Mobiel Fire
            if (currentSelectedWeapon.wSettings.fireType == PlayerWeapons.FireType.MachineGun)
            {
                //Automatic fire
                if (GameSettings.mobileFiring)
                {
                    currentSelectedWeapon.Fire();
                }
                else
                {
                    isFiring = false;
                }
            }
            else
            {
                //Single fire
                if (GameSettings.mobileFiring)
                {
                    currentSelectedWeapon.Fire();
                    GameSettings.mobileFiring = false;
                }

                isFiring = false;
            }

            //Aiming
            if (currentSelectedWeapon.wSettings.aimType != AimType.None)
            {
                if (GameSettings.mobileAiming)
                {
                    isAimed = !isAimed;

                    if (isAimed && currentSelectedWeapon.wSettings.aimType == AimType.CameraOnly && currentSelectedWeapon.wSettings.scopeTexture != null)
                    {
                        GameSettings.currentScopeTexture = currentSelectedWeapon.wSettings.scopeTexture;
                    }
                    else
                    {
                        GameSettings.currentScopeTexture = null;
                    }
                }
            }

            if (GameSettings.mobileAiming)
            {
                GameSettings.mobileAiming = false;
            }

#endif

            //Stop aiming when we reload
            if (currentSelectedWeapon.isReloading)
            {
				isAimed = false;
			}

			if(isAimed)
            {
				mainPlayerCamera.fieldOfView = Mathf.Lerp(mainPlayerCamera.fieldOfView, currentSelectedWeapon.wSettings.aimFOV, Time.deltaTime * 19);

				if(currentSelectedWeapon.aimOffset != Vector3.zero)
                {
					currentSelectedWeapon.thisT.localPosition = Vector3.Lerp(currentSelectedWeapon.thisT.localPosition, currentSelectedWeapon.aimOffset,  Time.deltaTime * 19);
				}
			}
            else
            {
				mainPlayerCamera.fieldOfView = Mathf.Lerp(mainPlayerCamera.fieldOfView, GameSettings.defaultFOV, Time.deltaTime * 19);

				if(currentSelectedWeapon.aimOffset != Vector3.zero)
                {
					currentSelectedWeapon.thisT.localPosition = Vector3.Lerp(currentSelectedWeapon.thisT.localPosition, currentSelectedWeapon.defaultPosition,  Time.deltaTime * 19);
				}

				if(GameSettings.currentScopeTexture  != null)
                {
					GameSettings.currentScopeTexture  = null;
				}
			}

			//Notify other FPSMouseLook.cs what is current Field Of View
			GameSettings.currentFOV = isAimed ? currentSelectedWeapon.wSettings.aimFOV : GameSettings.defaultFOV;
		}

		//DoWeaponBobbing();
	}

	public void GetWeaponToSelect (int type)
    {
		if(type == 1)
        {
			SwitchWeapon(primaryWeapons[selectedPrimary].firstPersonWeapon,  true);
           // 
        }

		if(type == 2)
        {
			SwitchWeapon(secondaryWeapons[selectedSecondary].firstPersonWeapon, true);
            
        }

		if(type == 3)
        {
			SwitchWeapon(specialWeapons[selectedSpecial].firstPersonWeapon, true);
            
        }
        
    }

	void FixedUpdate ()
    {
		DoWeaponBobbing();
	}

	void SwitchWeapon(FPSWeapon tmpWeapon,  bool firstPerson)
    {
		if(tmpWeapon != null && tmpWeapon != currentSelectedWeapon )
        {
			if(currentSelectedWeapon)
            {
				currentSelectedWeapon.gameObject.SetActive(false);
				currentSelectedWeapon = null;
			}

			isAimed = false;

			currentSelectedWeapon = tmpWeapon;
			currentSelectedWeapon.gameObject.SetActive(true);
            
            currentSelectedWeapon.Deploy();

			if(firstPerson)
            {
				for(int i = 0; i < totalWeapons.Count; i++)
                {
					if(totalWeapons[i].firstPersonWeapon == tmpWeapon)
                    {
						globalWeaponIndex = i;
						currentSelectedWeapon.weaponIndex = i;
                    }
				}
		    }
            
        }
        
    }

	void DoWeaponBobbing()
    {
		if(fpsController.isMoving && fpsController.isGrounded)
        {
			//currentBobbingSpeed = (bobbingSpeed * 100)/GameSettings.currentFPS;
			currentBobbingSpeed = bobbingSpeed;
			currentBobbingSpeed *= fpsController.speed;
			
			float waveslice = Mathf.Sin(timer); 
			timer = timer + currentBobbingSpeed; 
			if (timer > Mathf.PI * 2)
            { 
				timer = timer - (Mathf.PI * 2); 
			} 
			
			if (waveslice != 0)
            { 
				float translateChange = waveslice * bobbingAmount; 
				float totalAxes = Mathf.Abs(1) + Mathf.Abs(1); 
				totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f); 
				translateChange = totalAxes * translateChange; 
				firstPersonWeapons.localPosition = new Vector3(defaultSwayPosition.x, defaultSwayPosition.y, defaultSwayPosition.z - translateChange);
			}
		}
        else
        {
			timer = 0.0f; 
			firstPersonWeapons.localPosition = Vector3.Lerp(firstPersonWeapons.localPosition,  defaultSwayPosition, Time.deltaTime * 5);
		}
	}

	//Called from PlayerNetworkController.cs
	public void SwitchWeaponRemote()
    {
		SwitchWeapon(totalWeapons[globalWeaponIndex].thirdPersonWeapon, false);
	}

	public void FireRemote()
    {
		if(currentSelectedWeapon)
        {
			currentSelectedWeapon.Fire();
		}
	}
}
