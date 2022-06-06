


using UnityEngine;
using System.Collections;

public class AmmoSpot : MonoBehaviour
{

	//Ammo spots are placed around map where player can refill ammo

	//These are assigned at ConnectMenu.cs
	[HideInInspector]
	public RoomController rc;
	[HideInInspector]
	public Transform thisT;

	//How close player need to be to able use this ammo spot
	float distanceToBuyLimit = 1.5f; 
	float distance = -1;
	bool isInside = false;

	bool refillingAmmo = false;
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(rc.ourPlayer && thisT && rc.ourPlayer.thisT)
        {
			distance = Vector3.Distance(thisT.position, rc.ourPlayer.thisT.position);
		}
        else
        {
			distance = -1;
		}

		if(distance > 0 && distance < distanceToBuyLimit)
        {
			if(!isInside)
            {
				isInside = true;
				rc.eneteredBuySpot = true;
			}
		}
        else
        {
			if(isInside)
            {
				isInside = false;
				rc.eneteredBuySpot = false;
			}
		}

		//Refill ammo
		if(isInside && rc.eneteredBuySpot && rc.ourPlayer)
        {

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
            //Refill ammo on mobile
            if(GameSettings.refillingAmmo)
            { 
#else
            //Refill ammo on desktop
            if (Input.GetKey(GameSettings.playerKeys[14]))
            {
#endif

                if (!refillingAmmo)
                {
					StartCoroutine(RefillAmmo());
				}
			}
		}
	}

	IEnumerator RefillAmmo ()
    {
		if(rc.ourPlayer.playerWeapons.currentSelectedWeapon)
        {
			if(rc.GetCash() >= 100)
            {
				if(rc.ourPlayer.playerWeapons.currentSelectedWeapon.RefillAmmo())
                {
					//Substract cash
					refillingAmmo = true;
					rc.SubstractCash(-1);
				}
			}
		}

		yield return new WaitForSeconds(1f);

		refillingAmmo = false;
	}
}
