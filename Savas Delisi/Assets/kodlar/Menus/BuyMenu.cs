

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyMenu : MonoBehaviour
{

    //This script is enabled/disabled from RoomController.cs
    //Here player select weapons to buy
    
    
    [HideInInspector]
	public int lastSelectedWeapon;

	public enum BuySection { Primary, Secondary, Special}
	[HideInInspector]
	public BuySection buySection = BuySection.Primary;

	RoomController rc;
	Vector2 buyMenuScroll = Vector2.zero;

	//Temporarily store player wepaon list
	List<PlayerWeapons.WeaponSet> primaryWeaponsTmp = new List<PlayerWeapons.WeaponSet>();
	List<PlayerWeapons.WeaponSet> secondaryWeaponsTmp = new List<PlayerWeapons.WeaponSet>();
	List<PlayerWeapons.WeaponSet> specialWeaponsTmp = new List<PlayerWeapons.WeaponSet>();
	
	int selectedPrimary;
	int selectedSecondary;
	int selectedSpecial;
    int oldubitti;
	//Sort weapons by their cost
	private static int SortWeapons (PlayerWeapons.WeaponSet weaponA, PlayerWeapons.WeaponSet weaponB)
    {
		return weaponA.weaponCost - weaponB.weaponCost;
	}
    

    // Use this for initialization
    void Start ()
    {
        
        // birincilsilah1.GetComponent<SkinnedMeshRenderer>().material.color = Color.yellow;//colors[PlayerPrefs.GetInt("birincilrenk1")];

        rc = GetComponent<RoomController>();

		primaryWeaponsTmp.Clear();
		secondaryWeaponsTmp.Clear();
		specialWeaponsTmp.Clear();

		//Here we get all available weapons, sort them, obfusctae price and making them ready to use in game
		if(rc.playerPrefab)
        {
			PlayerWeapons pwTmp = rc.playerPrefab.GetComponent<PlayerNetwork>().playerWeapons;
			
			selectedPrimary = pwTmp.selectedPrimary;
			selectedSecondary = pwTmp.selectedSecondary;
			selectedSpecial = pwTmp.selectedSpecial;
			
			primaryWeaponsTmp.InsertRange(0, pwTmp.primaryWeapons);
			secondaryWeaponsTmp.InsertRange(0, pwTmp.secondaryWeapons);
			specialWeaponsTmp.InsertRange(0, pwTmp.specialWeapons);
			
			//print (primaryWeaponsTmp.Count);
			
			if(selectedPrimary < 0 || selectedPrimary > primaryWeaponsTmp.Count - 1)
            {
				selectedPrimary = 0;
			}
			if(selectedSecondary < 0 || selectedSecondary > secondaryWeaponsTmp.Count - 1)
            {
				selectedSecondary = 0;
			}
			if(selectedSpecial < 0 || selectedSpecial > specialWeaponsTmp.Count - 1)
            {
				selectedSpecial = 0;
			}
			
			//Set initially selected weapon to be first in the list
			PlayerWeapons.WeaponSet tmpPrimary = primaryWeaponsTmp[selectedPrimary];
			primaryWeaponsTmp.RemoveAt(selectedPrimary);
			
			PlayerWeapons.WeaponSet  tmpSecondary = secondaryWeaponsTmp[selectedSecondary];
			secondaryWeaponsTmp.RemoveAt(selectedSecondary);
			
			PlayerWeapons.WeaponSet tmpSpecial = specialWeaponsTmp[selectedSpecial];
			specialWeaponsTmp.RemoveAt(selectedSpecial);
			
			//Sort remaining weapons by price
			primaryWeaponsTmp.Sort(SortWeapons);
			secondaryWeaponsTmp.Sort(SortWeapons);
			specialWeaponsTmp.Sort(SortWeapons);
			
			//Add selected weapons back
			primaryWeaponsTmp.Insert(0, tmpPrimary);
			secondaryWeaponsTmp.Insert(0, tmpSecondary);
			specialWeaponsTmp.Insert(0, tmpSpecial);
			
			selectedPrimary = 0;
			selectedSecondary = 0;
			selectedSpecial = 0;
			
			//Obfuscate each weapon cost
			ObfuscateWeaponCost(primaryWeaponsTmp, selectedPrimary);
			ObfuscateWeaponCost(secondaryWeaponsTmp, selectedSecondary);
			ObfuscateWeaponCost(specialWeaponsTmp, selectedSpecial);
		}

		this.enabled = false;
        //oldubitti= PlayerPrefs.GetInt("oldubittifelan");
        //if (oldubitti==0)
        //{
        //    selectedPrimary = 0;
        //    selectedSecondary = 0;
        //    selectedSpecial = 0;

        //    oldubitti = 1;

        //}
        //PlayerPrefs.SetInt("oldubittifelan", oldubitti);

        //PlayerPrefs.SetInt("birincilsilah", selectedPrimary);
        //PlayerPrefs.SetInt("ikincilsilah", selectedSecondary);
        //PlayerPrefs.SetInt("bicaksilah", selectedSpecial);


        //birincilsilah2.GetComponent<MeshRenderer>().material.color = colors[PlayerPrefs.GetInt("birincilrenk2")];
        //birincilsilah3.GetComponent<MeshRenderer>().material.color = colors[PlayerPrefs.GetInt("birincilrenk3")];
        //ikincilsilah.GetComponent<MeshRenderer>().material.color = colors[PlayerPrefs.GetInt("ikincilrenk1")];
        //bicaksilah.GetComponent<MeshRenderer>().material.color = colors[PlayerPrefs.GetInt("bicakrenk1")];
        if (Application.loadedLevelName == "AWP KULE")
        {
            selectedPrimary = 1;

        }
        else
        {
            selectedPrimary = PlayerPrefs.GetInt("birincilsilah");
        }
       // selectedPrimary = PlayerPrefs.GetInt("birincilsilah");
        selectedSecondary = PlayerPrefs.GetInt("ikincilsilah");
        selectedSpecial = PlayerPrefs.GetInt("bicaksilah");
        ObfuscateWeaponCost(primaryWeaponsTmp, selectedPrimary);
        ObfuscateWeaponCost(secondaryWeaponsTmp, selectedSecondary);
        ObfuscateWeaponCost(specialWeaponsTmp, selectedSpecial);
    }

	void ObfuscateWeaponCost (List<PlayerWeapons.WeaponSet> wpmList, int selectedIndex)
    {
		for(int i = 0; i < wpmList.Count; i++)
        {
			if(i == selectedIndex || wpmList[i].weaponCost < 1)
            {
				wpmList[i].obfuscatedPrice = GameSettings.cnst;
			}
            else
            {
				wpmList[i].obfuscatedPrice = GameSettings.cnst - wpmList[i].weaponCost;
			}
		}
       // rc.Invoke("renkbirincil", 0);
    }

	public void  ApplySelectedWeapons ()
    {
		//Check what weapons we have selected and apply them to newly spawned player
		if(rc.ourPlayer)
        {
			for(int i = 0; i < rc.ourPlayer.playerWeapons.primaryWeapons.Count; i++)
            {
				if(rc.ourPlayer.playerWeapons.primaryWeapons[i].firstPersonWeapon.name == primaryWeaponsTmp[selectedPrimary].firstPersonWeapon.name)
                {
					rc.ourPlayer.playerWeapons.selectedPrimary = i;
				}
			}
			
			for(int i = 0; i < rc.ourPlayer.playerWeapons.secondaryWeapons.Count; i++)
            {
				if(rc.ourPlayer.playerWeapons.secondaryWeapons[i].firstPersonWeapon.name == secondaryWeaponsTmp[selectedSecondary].firstPersonWeapon.name)
                {
					rc.ourPlayer.playerWeapons.selectedSecondary = i;
				}
			}
			
			for(int i = 0; i < rc.ourPlayer.playerWeapons.specialWeapons.Count; i++)
            {
				if(rc.ourPlayer.playerWeapons.specialWeapons[i].firstPersonWeapon.name == specialWeaponsTmp[selectedSpecial].firstPersonWeapon.name)
                {
					rc.ourPlayer.playerWeapons.selectedSpecial =  i;
				}
			}
			
			rc.ourPlayer.playerWeapons.GetWeaponToSelect(lastSelectedWeapon);
            
           // rc.Invoke("renkbirincil", 0);
            GameObject.Find("_RoomController(Clone)").GetComponent<RoomUI>().kontrolpaneliniac();
            GameObject.Find("asdf").GetComponent<Scoreboard>().YÜKLEMECANVASI.SetActive(false);
        }
        
	}

	//This is called from RoomController.cs when our player was killed
	public void ResetSelectedWeapons ()
    {
        //selectedPrimary = 0;
        //selectedSecondary = 0;
        //selectedSpecial = 0;
        if (Application.loadedLevelName == "AWP KULE")
        {
            selectedPrimary = 1;

        }
        else
        {
            selectedPrimary = PlayerPrefs.GetInt("birincilsilah");
        }
        
        selectedSecondary = PlayerPrefs.GetInt("ikincilsilah");
        selectedSpecial = PlayerPrefs.GetInt("bicaksilah");

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
        GameSettings.switchWeaponIndex = 1;
#endif

    }

	//This is called from RoomController.cs when buying new weapon
	public int GetWeaponCost (int type)
    {
		if(type == 1)
        {
			return GameSettings.cnst - primaryWeaponsTmp[selectedPrimary].obfuscatedPrice;
		}

		if(type == 2)
        {
			return GameSettings.cnst - secondaryWeaponsTmp[selectedSecondary].obfuscatedPrice;
		}

		if(type == 3)
        {
			return GameSettings.cnst - specialWeaponsTmp[selectedSpecial].obfuscatedPrice;
		}

		return -1;
	}
	
	// Update is called once per frame
	//void OnGUI ()
 //   {
	//	GUI.skin = GameSettings.guiSkin;

	//	//GUI.Window (0, new Rect(Screen.width/2 - 425, Screen.height/2 - 175,  850, 350), BuyMenuWindow, "");
	//}

	//void BuyMenuWindow (int windowID)
 //   {
	//	GUI.Label(new Rect(15, 0, 300, 35), "Market");

	//	if(GUI.Button(new Rect(850 - 30, 5, 25, 25), "", GameSettings.closeButtonStyle))
 //       {
	//		rc.showBuyMenu = false;
	//	}

	//	GUI.enabled = buySection != BuySection.Primary;

	//	if(GUI.Button(new Rect(15, 50, 115, 25), "Birincil", GameSettings.buyMenuButtonStyle))
 //       {
	//		buySection = BuySection.Primary;
	//	}

	//	GUI.enabled = buySection != BuySection.Secondary;

	//	if(GUI.Button(new Rect(140, 50, 115, 25), "İkincil", GameSettings.buyMenuButtonStyle))
 //       {
	//		buySection = BuySection.Secondary;
	//	}

	//	GUI.enabled = buySection != BuySection.Special;

	//	if(GUI.Button(new Rect(265, 50, 115, 25), "Özel", GameSettings.buyMenuButtonStyle))
 //       {
	//		buySection = BuySection.Special;
	//	}

	//	GUI.enabled = true;
		
	//	if(buySection == BuySection.Primary)
 //       {
	//		ShowWeaponItems(primaryWeaponsTmp, selectedPrimary, 1);
	//	}

	//	if(buySection == BuySection.Secondary)
 //       {
	//		ShowWeaponItems(secondaryWeaponsTmp, selectedSecondary, 2);
	//	}
		
	//	if(buySection == BuySection.Special)
 //       {
	//		ShowWeaponItems(specialWeaponsTmp, selectedSpecial, 3);
	//	}
	//}

	void ShowWeaponItems (List<PlayerWeapons.WeaponSet> weaponListTmp, int selectedIndex, int type)
    {
		//if(weaponListTmp != null)
  //      {
		//	GUILayout.Space(75);

		//	buyMenuScroll = GUILayout.BeginScrollView(buyMenuScroll, true, true, GUILayout.Height(255));
		//		GUILayout.BeginHorizontal();
		//			for(int i = 0; i < weaponListTmp.Count; i++)
  //                  {
		//				GUILayout.BeginVertical("box", GUILayout.Width(200), GUILayout.Height(230));
		//		            GUILayout.Label(weaponListTmp[i].firstPersonWeapon.name, GameSettings.hudInfoStyle);
		//					GUI.color = weaponListTmp[i].obfuscatedPrice >= rc.totalCash ? GameSettings.drawColor : GameSettings.customRedColor;
		//					GUILayout.Label((GameSettings.cnst - weaponListTmp[i].obfuscatedPrice).ToString() + " $");
		//					GUI.color = Color.white;

		//					GUILayout.Space(10);
		//					GUILayout.Label("Fire Type: " + weaponListTmp[i].fireType.ToString());
				
		//					GUILayout.Space(10);
		//					GUILayout.Label("Ammo per clip: " + weaponListTmp[i].bulletsPerClip.ToString());
		//					GUILayout.Label("Reserve ammo: " + weaponListTmp[i].reserveBullets.ToString());

		//					GUILayout.Space(10);
		//					GUILayout.Label("kafa vuruş: " + weaponListTmp[i].headDamage.ToString());
		//					GUILayout.Label("Gövde vuruş: " + weaponListTmp[i].torsoDamage.ToString());
		//					GUILayout.Label("Uzuv vuruş: " + weaponListTmp[i].limbsDamage.ToString());
				
		//					GUILayout.FlexibleSpace();
		//					GUI.enabled = i != selectedIndex &&  weaponListTmp[i].obfuscatedPrice >= rc.totalCash;
		//					if(GUILayout.Button(i != selectedIndex ? "Satın al" : "Alındı", GUILayout.Height(25)))
  //                          {
		//						if(rc.GetCash() >= GameSettings.cnst - weaponListTmp[i].obfuscatedPrice)
  //                              {
		//							if(type == 1)
  //                                  {
		//								selectedPrimary = i;
		//								rc.SubstractCash(1);
		//							}
		//							if(type == 2)
  //                                  {
		//								selectedSecondary = i;
		//								rc.SubstractCash(2);
		//							}
		//							if(type == 3)
  //                                  {
		//								selectedSpecial = i;
		//								rc.SubstractCash(3);
		//							}

		//							if(rc.ourPlayer)
  //                                  {
		//								lastSelectedWeapon = type;
		//								Invoke ("ApplySelectedWeapons", 0.035f);
		//							}

		//							rc.showBuyMenu = false;
		//						}
		//			   		}
		//					GUI.enabled = true;
		//				GUILayout.EndVertical();
		//			}
		//		GUILayout.EndHorizontal();
		//	GUILayout.EndScrollView();
		//}
	}
}
