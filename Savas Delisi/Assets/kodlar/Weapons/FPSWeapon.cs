//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;

public class FPSWeapon : MonoBehaviour
{

	public enum WeaponType { Primary, Secondary, Special }
	public WeaponType weaponType;
	public MeshRenderer muzzleFlash;

	[HideInInspector]
	public string weaponName;
	[HideInInspector]
	public PlayerWeapons.WeaponSet wSettings; //This value set from PlayerWeapons.cs
	[HideInInspector]
	public bool isThirdPerson = false; //Asigned externally from PlayerWeapons.cs
	[HideInInspector]
	public AudioSource audioSource;
	[HideInInspector]
	public bool isReloading = false;
	//These vairbales are assigned at PlayerWeapons.cs
	[HideInInspector]
	public PlayerWeapons playerWeapons; 
	[HideInInspector]
	public SoldierAnimation soldierAnimation;
	[HideInInspector]
	public PlayerNetwork playerNetwork;
	[HideInInspector]
	public int weaponIndex = 0; //This is used when displaying kill report

	[HideInInspector]
	public Vector3 aimOffset = Vector3.zero; //Used in combination with aim object to set aim position
	[HideInInspector]
	public Vector3 defaultPosition = Vector3.zero; //Default weapon position

	//First person animations
	[System.Serializable]
	public class FPSAnimations
    {
		public AnimationClip idle;
		public AnimationClip fire;
		public AnimationClip reload;
		public AnimationClip deploy;
	}
	public FPSAnimations fpsAnimations;

	int bulletRange = 500; //How far bullet can reach
	int knifeRange = 3; //How far knife can reach
	float nextFireTime;
	Animation animationController;
	bool setupDone = false;
	bool doneTaking = false;
	int defaultBulletsPerClip = 0;
	int defaultReserveBulelts = 0;
	int bulletsLeft = 0;
	int reserveBulletsLeft = 0;

	Transform muzzleFlashTransform;
	[HideInInspector]
	public Transform thisT;

	//Recoil values
	float highestBulletSpread = 3.5f; //Greater value mean greater bullet spread
	//This bulelt spread will be increased over time while shooting, only applied for non shotgun  and non sniper weapons
	float bulletSpread = 0; 
	//How many seconds after shooting should it take to reduce spread back to zero
	float timeToSpreadCooldown = 1; 

	//Called from FPS_PlayerWeapons.cs when selecting this weapon
	public void Deploy()
    {
		if(!setupDone)
        {
			aimOffset = Vector3.zero; 
			defaultPosition = Vector3.zero;

			//We create a static random number at StaticData.cs
			//Now we sum that number and bullet count to obscure ammo values
			//Which will make it harder to modify them with CheatEngine
			defaultBulletsPerClip = GameSettings.cnst + wSettings.bulletsPerClip;
			defaultReserveBulelts = GameSettings.cnst + wSettings.reserveBullets;
			bulletsLeft = defaultBulletsPerClip;
			reserveBulletsLeft = defaultReserveBulelts;

			//Setup animations
			if(GetComponent<Animation>() != null)
            {
				animationController = GetComponent<Animation>();
				animationController.playAutomatically = false;
				if(fpsAnimations.idle)
                {
					fpsAnimations.idle.wrapMode = WrapMode.Once;
				}
				if(fpsAnimations.fire)
                {
					fpsAnimations.fire.wrapMode = WrapMode.Once;
				}
				if(fpsAnimations.deploy)
                {
					fpsAnimations.deploy.wrapMode = WrapMode.Once;
				}
				if(fpsAnimations.reload)
                {
					fpsAnimations.reload.wrapMode = WrapMode.Once;
				}
			}
            else
            {
				if(!isThirdPerson)
                {
					Debug.LogError("First person weapon require Animation component to be assigned near FPSWeapon.cs");
				}
			}

			if(wSettings.fireRate <= 0)
            {
				wSettings.fireRate = 0.01f;
			}
			if(wSettings.timeToDeploy <= 0)
            {
				wSettings.timeToDeploy = 0.01f;
			}
			if(wSettings.reloadTime <= 0)
            {
				wSettings.reloadTime = 0.01f;
			}

			isReloading = false;

			thisT = transform;

			defaultPosition = thisT.localPosition;

			weaponName = gameObject.name;

			if(muzzleFlash != null)
            {
				muzzleFlashTransform = muzzleFlash.transform;
			}

			if(wSettings.aimObject)
            {
				/*aimOffset = wSettings.aimObject.position - thisT.position;
				aimOffset.z = defaultPosition.z;
				aimOffset.y *= -1;
				aimOffset.x = 0;*/

				aimOffset = new Vector3(wSettings.aimObject.localPosition.x * thisT.localScale.x, wSettings.aimObject.localPosition.y * thisT.localScale.y,  defaultPosition.z);
				aimOffset.y *= -1;
				aimOffset.x *= - 1;
			}

			setupDone = true;
		}

		if(muzzleFlash)
        {
			muzzleFlash.enabled = false;
		}

		this.StopAllCoroutines();
		isReloading = false;

		if(!isThirdPerson)
        {
			if(gameObject.activeSelf)
            {
				StartCoroutine(TakeInAnimation());
			}
		}
        else
        {
			doneTaking = true;
		}

		//Play take in sound
		audioSource.Stop();
		audioSource.clip = wSettings.takeInSound;
		audioSource.Play();

		thisT.localPosition = defaultPosition;
		
		bulletSpread = 0;
	}

	IEnumerator TakeInAnimation()
    {
		doneTaking = false;

		//Play "Take-in" animation
		if(fpsAnimations.deploy)
        {
			animationController.Rewind(fpsAnimations.deploy.name);
			animationController[fpsAnimations.deploy.name].speed = animationController[fpsAnimations.deploy.name].length/wSettings.timeToDeploy;
			animationController[fpsAnimations.deploy.name].time = 0;
			animationController.Play(fpsAnimations.deploy.name);
		}

		yield return new WaitForSeconds(wSettings.timeToDeploy);

		doneTaking = true;
	}

	public void Fire ()
    {
		//NOTE: Input is handles at FPSPlayerWeapons.cs

		if (!doneTaking || bulletsLeft - GameSettings.cnst <= 0 && !isThirdPerson || isReloading)
        {
			playerWeapons.isFiring = false;
			return;
		}

		if(wSettings.fireType == PlayerWeapons.FireType.MachineGun)
        {
			//Automatic fire sync over network
			playerWeapons.isFiring = true;
		}
		
		// If there is more than one bullet between the last and this frame
		// Reset the nextFireTime
		if (Time.time - wSettings.fireRate > nextFireTime)
        {
			nextFireTime = Time.time - Time.deltaTime;
		}
		
		// Keep firing until we used up the fire time
		while( nextFireTime < Time.time)
        {
			FireOneShot();
			nextFireTime += wSettings.fireRate;
		}
	}

	void FireOneShot()
    {
		if(!isThirdPerson)
        {
			//Sync single shot over network
			if(wSettings.fireType != PlayerWeapons.FireType.MachineGun)
            {
				playerNetwork.FireSingleRemote();
			}

			//Play fire animation
			if(animationController && fpsAnimations.fire)
            {
				animationController.Rewind(fpsAnimations.fire.name);
				animationController[fpsAnimations.fire.name].speed = 1;
				animationController.Play(fpsAnimations.fire.name);
			}

			GameSettings.rc.distance = 15.5f;
			//Do Recoil
			playerNetwork.cameraMouseLook.Recoil();
			float recoilPushAmount = wSettings.fireType != PlayerWeapons.FireType.Shotgun && wSettings.fireType != PlayerWeapons.FireType.SniperRifle ? 0.41f : 1.95f;
			//Reduce recoil shake if we croaching or slowly walking
			if(playerNetwork.fpsController.isCrouching || playerNetwork.fpsController.isSprintingSlowly)
            {
				recoilPushAmount *= 0.5f;
			}
			playerNetwork.fpsController.firstPersonViewRotation = new Vector2(-recoilPushAmount, recoilPushAmount);
		}
        else
        {
			//Play fire animation on third person character
			if(soldierAnimation)
            {
				soldierAnimation.PlayFireAnimation();
			}
		}

		//Play Weapon audio
		audioSource.Stop();
		audioSource.clip = wSettings.fireSound;
		audioSource.Play();

		if(!isThirdPerson && wSettings.fireType != PlayerWeapons.FireType.Knife)
        {
			bulletsLeft--;
			wSettings.bulletsPerClip = bulletsLeft - GameSettings.cnst;
		}

		if(muzzleFlash)
        {
			StopCoroutine(DoMuzzleFlash());
			StartCoroutine(DoMuzzleFlash());
		}

		//Setup fire direction and fire point, for third person we push fire point slightly to front to not hit our Hitboxes
		Vector3 fireDirection = isThirdPerson ? (playerWeapons.playerCamera.position + playerWeapons.playerCamera.forward * 100) - thisT.position : playerWeapons.playerCamera.forward * 100;
		Vector3 firePoint = isThirdPerson ? thisT.position + fireDirection * 0.001f : playerWeapons.playerCamera.position;

		//Shoot bullet
		if(wSettings.fireType == PlayerWeapons.FireType.Shotgun)
        {
			//Shotgun shooting 7 fractions
			for(int i = 0; i < 7; i++)
            {
				//Do bullet spread, always do spread for shotgun even if aiming
				Vector3 spread = new Vector3(Random.Range(-highestBulletSpread, highestBulletSpread), Random.Range(-highestBulletSpread, highestBulletSpread), Random.Range(-highestBulletSpread, highestBulletSpread));
				ShootBullet(firePoint, fireDirection + spread);
			}
		}
        else
        {
			//Other weapons
			Vector3 spread = new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));
			if(GameSettings.currentFOV != GameSettings.defaultFOV)
            {
				//We aiming, do not apply bullet spread
				spread = Vector3.zero;
			}

			ShootBullet(firePoint, fireDirection + spread);

			//Start do bullet spread only after first shot
			if(highestBulletSpread > bulletSpread)
            {
				if(bulletSpread == 0)
                {
					StartCoroutine(ReduceSpreadOverTime());
				}
				bulletSpread += wSettings.fireRate * highestBulletSpread;
			}
		}

		if(!isThirdPerson && bulletsLeft - GameSettings.cnst <= 0 && reserveBulletsLeft - GameSettings.cnst > 0)
        {
			StopCoroutine("ReloadCoroutine");
			StartCoroutine(ReloadCoroutine (true));
		}
	}

	void ShootBullet (Vector3 firePoint, Vector3 fireDirection)
    {
		RaycastHit hit;
		int tmpFireRange = wSettings.fireType == PlayerWeapons.FireType.Knife ? knifeRange : bulletRange;
		
		// Did we hit anything?
		if (Physics.Raycast (firePoint, fireDirection, out hit, tmpFireRange))
        {
			if(hit.transform.CompareTag("Body"))
            {
				//Blood particle
				Instantiate(playerWeapons.bloodParticles, hit.point, Quaternion.LookRotation(hit.normal));
				
				if(!isThirdPerson)
                {
					HitBox tmp;
					if((tmp = hit.transform.GetComponent<HitBox>()) != null)
                    {
						//Compose values we are going to pass
						int[] values = new int[3]; 
						values[0] = weaponIndex; //What weapon we used to make damage
						values[1] = (int)tmp.bodyPart; //What body part we hit
						values[2] = 0; //What side of player was hit (For hit marks), this is assigned later at HitBox.cs
						tmp.Damage(values, hit.point);
					}
				}
			}
            else
            {
				if(hit.transform.CompareTag("Metal") || hit.transform.CompareTag("Wood"))
                {
					if(hit.transform.CompareTag("Metal"))
                    {
						Instantiate(playerWeapons.metalParticles, hit.point, Quaternion.LookRotation(hit.normal));
					}
                    else
                    {
						Instantiate(playerWeapons.woodParticles, hit.point, Quaternion.LookRotation(hit.normal));
					}
				}
                else
                {
					Instantiate(playerWeapons.concreteParticles, hit.point, Quaternion.LookRotation(hit.normal));
				}
			}
		}
	}

	//Called from FPSPlayerWeapons.cs
	public void ReloadRemote ()
    {
		if(!isThirdPerson)
        {
			if(bulletsLeft < defaultBulletsPerClip && reserveBulletsLeft - GameSettings.cnst > 0)
            {
				StopCoroutine("ReloadCoroutine");
				StartCoroutine(ReloadCoroutine (false));
			}
		}
	}

	public void ReloadNetworkSync ()
    {
		//Called from PlayerNetwork.cs to sync reload with remote instances
		if(soldierAnimation)
        {
			soldierAnimation.PlayReloadAnimation(wSettings.reloadTime);
		}

		//Play reload sound
		audioSource.Stop();
		audioSource.clip = wSettings.reloadSound;
		audioSource.Play();
	}

	IEnumerator ReloadCoroutine (bool extraDelay)
    {
		isReloading = true;

		if(extraDelay)
        {
			yield return new WaitForSeconds(0.5f);
		}

		if(fpsAnimations.reload)
        {
			animationController.Rewind(fpsAnimations.reload.name);
			animationController[fpsAnimations.reload.name].speed = animationController[fpsAnimations.reload.name].length/wSettings.reloadTime;
			animationController[fpsAnimations.reload.name].time = 0;
			animationController.Play(fpsAnimations.reload.name);
		}

		//Sync reload over network
		if(!isThirdPerson)
        {
			playerNetwork.DoReload();
		}

		//Play reload sound
		audioSource.Stop();
		audioSource.clip = wSettings.reloadSound;
		audioSource.Play();

		yield return new WaitForSeconds(wSettings.reloadTime);

		if(bulletsLeft - GameSettings.cnst > 0)
        {
			int bulletsWeNeed = defaultBulletsPerClip - bulletsLeft;
			if(bulletsWeNeed > reserveBulletsLeft - GameSettings.cnst)
            {
				bulletsLeft += bulletsWeNeed;
				reserveBulletsLeft = GameSettings.cnst;
			}
            else
            {
				bulletsLeft = defaultBulletsPerClip;
				reserveBulletsLeft -= bulletsWeNeed;
			}
		}
        else
        {
			if(reserveBulletsLeft > defaultBulletsPerClip)
            {
				bulletsLeft = defaultBulletsPerClip;
				reserveBulletsLeft -= (defaultBulletsPerClip - GameSettings.cnst);
			}
            else
            {
				bulletsLeft = Mathf.Clamp(reserveBulletsLeft, 0, defaultBulletsPerClip);
				reserveBulletsLeft = GameSettings.cnst;
			}
		}


		wSettings.reserveBullets = reserveBulletsLeft - GameSettings.cnst;
		wSettings.bulletsPerClip = bulletsLeft - GameSettings.cnst;

		isReloading = false;
	}

	IEnumerator DoMuzzleFlash ()
    {
		muzzleFlash.enabled = true;
		//muzzleFlashTransform.localEulerAngles = new Vector3(Random.Range(1, 179), muzzleFlashTransform.localEulerAngles.y, muzzleFlashTransform.localEulerAngles.z);
		muzzleFlashTransform.Rotate(0, Random.Range(-360, 360), 0, Space.Self);
		yield return new WaitForSeconds(0.05f);
		muzzleFlash.enabled = false;
	}

	//Called from BuySpot.cs
	public bool RefillAmmo ()
    {
		if(reserveBulletsLeft < defaultReserveBulelts)
        {
			reserveBulletsLeft += defaultBulletsPerClip - GameSettings.cnst;
			if(reserveBulletsLeft > defaultReserveBulelts)
            {
				reserveBulletsLeft = defaultReserveBulelts;
			}

			wSettings.reserveBullets = reserveBulletsLeft - GameSettings.cnst;
			wSettings.bulletsPerClip = bulletsLeft - GameSettings.cnst;

			return true;
		}

		return false;
	}

	IEnumerator ReduceSpreadOverTime ()
    {
		yield return new WaitForEndOfFrame();

		while(bulletSpread > 0.001f)
        {
			bulletSpread = Mathf.Lerp(bulletSpread, 0, Time.deltaTime/timeToSpreadCooldown );
			yield return null;
		}

		bulletSpread = 0;
	}
}
