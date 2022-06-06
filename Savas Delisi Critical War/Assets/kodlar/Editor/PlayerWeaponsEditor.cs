//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(PlayerWeapons))]
public class PlayerWeaponsEditor : Editor
{
	
	bool showPrimaryWeapons = true;
	bool showSecondaryWeapons = true;
	bool showSpecialWeapons = true;

	bool showPrimaryWeaponsPrevious = false;
	bool showSecondaryWeaponsPrevious = false;
	bool showSpecialWeaponsPrevious = false;

	int highlightIndex = 0;

	string statusString;

	GUIStyle foldoutStyleNew = new GUIStyle(EditorStyles.foldout);

	private void OnEnable()
    {
		PlayerWeapons myTarget = (PlayerWeapons)target;

		statusString = "Ready...";

		//Check if selected indexes within range
		if(myTarget.selectedPrimary < 0 || myTarget.selectedPrimary > myTarget.primaryWeapons.Count - 1)
        {
			myTarget.selectedPrimary = 0;
		}
		if(myTarget.selectedSecondary < 0 || myTarget.selectedSecondary > myTarget.secondaryWeapons.Count - 1)
        {
			myTarget.selectedSecondary = 0;
		}
		if(myTarget.selectedSpecial < 0 || myTarget.selectedSpecial > myTarget.specialWeapons.Count - 1)
        {
			myTarget.selectedSpecial = 0;
		}

		if(EditorPrefs.HasKey("FoldOutStates"))
        {
			string foldOutStates = EditorPrefs.GetString("FoldOutStates");
			showPrimaryWeapons = foldOutStates[0] == 'T';
			showSecondaryWeapons = foldOutStates[1] == 'T';
			showSpecialWeapons = foldOutStates[2] == 'T';
		}

		foldoutStyleNew.fontStyle = FontStyle.Bold;

		Undo.RegisterCompleteObjectUndo (target, "PlayerWeaponsUndoRedo");
	}

	public override void OnInspectorGUI()
    {
		PlayerWeapons myTarget = (PlayerWeapons)target;

		myTarget.playerCamera = EditorGUILayout.ObjectField("Player Camera", myTarget.playerCamera, typeof(Transform), true) as Transform;

		myTarget.concreteParticles= EditorGUILayout.ObjectField("Concrete Particles", myTarget.concreteParticles, typeof(GameObject), true) as GameObject;
		myTarget.metalParticles = EditorGUILayout.ObjectField("Metal Particles", myTarget.metalParticles, typeof(GameObject), true) as GameObject;
		myTarget.woodParticles = EditorGUILayout.ObjectField("Wood Particles", myTarget.woodParticles, typeof(GameObject), true) as GameObject;
		myTarget.bloodParticles = EditorGUILayout.ObjectField("Blood Particles", myTarget.bloodParticles, typeof(GameObject), true) as GameObject;

		myTarget.firstPersonAudioSource = EditorGUILayout.ObjectField("First Person Audio Source", myTarget.firstPersonAudioSource, typeof(AudioSource), true) as AudioSource;
		myTarget.thirdPersonAudioSource = EditorGUILayout.ObjectField("Third Person Audio Source", myTarget.thirdPersonAudioSource, typeof(AudioSource), true) as AudioSource;

		showPrimaryWeapons = EditorGUILayout.Foldout(showPrimaryWeapons, "Primary Weapons");
		if(showPrimaryWeapons)
        {
			DisplayWeaponUnit (myTarget.primaryWeapons, 1);
		}

		showSecondaryWeapons = EditorGUILayout.Foldout(showSecondaryWeapons, "Secondary Weapons"); 
		if(showSecondaryWeapons)
        {
			DisplayWeaponUnit (myTarget.secondaryWeapons, 2);
		}

		showSpecialWeapons = EditorGUILayout.Foldout(showSpecialWeapons, "Special Weapons"); 
		if(showSpecialWeapons)
        {
			DisplayWeaponUnit (myTarget.specialWeapons, 3);
		}

		EditorGUILayout.LabelField(statusString, EditorStyles.miniLabel);

		if(GUILayout.Button("Refresh Weapons"))
        {
			AutoAssignPlayerWeapons();
		}

		/*if(GUILayout.Button("Clear All"))
        {
			myTarget.primaryWeapons.Clear();
			myTarget.secondaryWeapons.Clear();
			myTarget.specialWeapons.Clear();
		}*/

		if(
			showPrimaryWeaponsPrevious != showPrimaryWeapons || 
			showSecondaryWeaponsPrevious != showSecondaryWeapons ||
			showSpecialWeaponsPrevious != showSpecialWeapons
		)
        {

			showPrimaryWeaponsPrevious = showPrimaryWeapons;
			showSecondaryWeaponsPrevious = showSecondaryWeapons;
			showSpecialWeaponsPrevious = showSpecialWeapons;

			//Set Editor prefs
			string foldOutStates = "";
			foldOutStates += showPrimaryWeapons ? "T" : "F";
			foldOutStates += showSecondaryWeapons ? "T" : "F";
			foldOutStates += showSpecialWeapons ? "T" : "F";
			EditorPrefs.SetString("FoldOutStates",  foldOutStates);

			//Debug.Log("Set EditorPrefs");
		}

		if(GUI.changed)
        {
			EditorUtility.SetDirty( target );
			Undo.RegisterCompleteObjectUndo (target, "PlayerWeaponsUndoRedo");
			//Debug.Log("Custom GUI changed for PlayerWeapons.cs");
		}
	}

	void DisplayWeaponUnit (List<PlayerWeapons.WeaponSet> wsets, int type)
    {
		PlayerWeapons myTarget = (PlayerWeapons)target;

		for(int i = 0; i < wsets.Count; i++)
        {
			EditorGUILayout.BeginVertical("box");
				if(wsets[i].firstPersonWeapon != null)
                {
					EditorGUILayout.BeginHorizontal();
						//EditorGUILayout.LabelField("Weapon Name", wsets[i].firstPersonWeapon.name, EditorStyles.boldLabel);
						GUILayout.Space(15);
						wsets[i].showThis = EditorGUILayout.Foldout(wsets[i].showThis, wsets[i].firstPersonWeapon.name, foldoutStyleNew);
						if(wsets[i].thirdPersonWeapon == null)
                        {
							GUILayout.FlexibleSpace();
							GUI.color = Color.yellow;
							GUILayout.Label("[TPW Mising]", EditorStyles.miniLabel);
						}
					EditorGUILayout.EndHorizontal();

					GUI.color = Color.white;

					if(wsets[i].showThis)
                    {
						wsets[i].weaponCost = EditorGUILayout.IntField("Weapon Cost", wsets[i].weaponCost);
						wsets[i].fireType = (PlayerWeapons.FireType)EditorGUILayout.EnumPopup("Fire Type", wsets[i].fireType);
						wsets[i].timeToDeploy = EditorGUILayout.FloatField("Time To Deploy", wsets[i].timeToDeploy);
						wsets[i].reloadTime = EditorGUILayout.FloatField("Reload Time", wsets[i].reloadTime);
						wsets[i].fireRate = EditorGUILayout.FloatField("Fire Rate", wsets[i].fireRate);
						wsets[i].bulletsPerClip = EditorGUILayout.IntField("Bullets Per Clip", wsets[i].bulletsPerClip);
						wsets[i].reserveBullets = EditorGUILayout.IntField("Reserve Bullets", wsets[i].reserveBullets);

						wsets[i].fireSound = EditorGUILayout.ObjectField("Fire Sound", wsets[i].fireSound, typeof(AudioClip), false) as AudioClip;
						wsets[i].reloadSound = EditorGUILayout.ObjectField("Reload Sound", wsets[i].reloadSound, typeof(AudioClip), false) as AudioClip;
						wsets[i].takeInSound = EditorGUILayout.ObjectField("Take In Sound", wsets[i].takeInSound, typeof(AudioClip), false) as AudioClip;

						wsets[i].headDamage = EditorGUILayout.IntField("Head Damage", wsets[i].headDamage);
						wsets[i].torsoDamage = EditorGUILayout.IntField("Torso Damage", wsets[i].torsoDamage);
						wsets[i].limbsDamage = EditorGUILayout.IntField("Limbs Damage", wsets[i].limbsDamage);

						wsets[i].aimType = (PlayerWeapons.AimType)EditorGUILayout.EnumPopup("Aim Type", wsets[i].aimType);

						if(wsets[i].aimType == PlayerWeapons.AimType.CameraOnly || wsets[i].aimType == PlayerWeapons.AimType.CameraAndIronsights)
                        {
							wsets[i].aimFOV = EditorGUILayout.Slider("Aim FOV", wsets[i].aimFOV, 3, GameSettings.defaultFOV);
						}

						if(wsets[i].aimType == PlayerWeapons.AimType.CameraOnly)
                        {
							wsets[i].scopeTexture = EditorGUILayout.ObjectField("Scope Texture", wsets[i].scopeTexture, typeof(Sprite), false) as Sprite;
						}

						if(wsets[i].aimType == PlayerWeapons.AimType.CameraAndIronsights)
                        {
							wsets[i].aimObject = EditorGUILayout.ObjectField("Aim Object", wsets[i].aimObject, typeof(Transform), true) as Transform;
						}

						EditorGUILayout.BeginHorizontal();
							if(GUILayout.Button("Highlight",  EditorStyles.miniButton, GUILayout.Width(75)))
                            {
								if(highlightIndex == 0 || wsets[i].thirdPersonWeapon == null)
                                {
									EditorGUIUtility.PingObject(wsets[i].firstPersonWeapon.gameObject);
									highlightIndex = 1;
								}
                                else
                                {
									EditorGUIUtility.PingObject(wsets[i].thirdPersonWeapon.gameObject);
									highlightIndex = 0;
								}
							}

							GUILayout.FlexibleSpace();
							
							if(type == 1 && myTarget.selectedPrimary != i ||
					   			type == 2  && myTarget.selectedSecondary != i ||
						  		 type == 3 && myTarget.selectedSpecial != i
				  		 	)
                            {
								if(GUILayout.Button("Select",  EditorStyles.miniButton, GUILayout.Width(75)))
                                {
									if(type == 1)
                                    {
										myTarget.selectedPrimary = i;
									}
									if(type == 2)
                                    {
										myTarget.selectedSecondary = i;
									}
									if(type == 3)
                                    {
										myTarget.selectedSpecial = i;
									}
								}
							}
                            else
                            {
								GUILayout.Label("Selected", EditorStyles.whiteMiniLabel, GUILayout.Height(15));
							}

							//Uncomment this to able move elements up down on the list
							/*if(i > 0)
                            {
								if(GUILayout.Button("Move Up",  EditorStyles.miniButton, GUILayout.Width(75)))
                                {
									MoveWeapon(wsets, i, true);
								}
							}
							if(i != wsets.Count -1)
                            {
								if(GUILayout.Button("Move Down",  EditorStyles.miniButton, GUILayout.Width(75)))
                                {
									MoveWeapon(wsets, i, false);
								}
							}*/
						EditorGUILayout.EndHorizontal();
					}
				}
                else
                {
					EditorGUILayout.BeginHorizontal();
						GUILayout.Space(15);
						GUI.color = Color.yellow;
						EditorGUILayout.Foldout(true, "This field is empty, press Refresh Weapons to remove it", foldoutStyleNew);
						GUI.color = Color.white;
					EditorGUILayout.EndHorizontal();
				}
			EditorGUILayout.EndVertical();
		}
	}

	void MoveWeapon (List<PlayerWeapons.WeaponSet> wsets, int index, bool up)
    {
		PlayerWeapons.WeaponSet set = wsets[index];
		wsets.RemoveAt(index);
		if(up)
        {
			wsets.Insert(index - 1, set);
		}
        else
        {
			wsets.Insert(index + 1, set);
		}
	}

	void AutoAssignPlayerWeapons ()
    {
		PlayerWeapons myTarget = (PlayerWeapons)target;

		int newWeapons = 0;

		//Clear empty weapons
		for(int i = myTarget.primaryWeapons.Count - 1; i > -1; i--)
        {
			if( myTarget.primaryWeapons[i].firstPersonWeapon == null ||  myTarget.primaryWeapons[i].firstPersonWeapon.weaponType != FPSWeapon.WeaponType.Primary)
            {
				myTarget.primaryWeapons.RemoveAt(i);
			}
		}
		for(int i = myTarget.secondaryWeapons.Count - 1; i > -1; i--)
        {
			if(myTarget.secondaryWeapons[i].firstPersonWeapon == null || myTarget.secondaryWeapons[i].firstPersonWeapon.weaponType != FPSWeapon.WeaponType.Secondary)
            {
				myTarget.secondaryWeapons.RemoveAt(i);
			}
		}
		for(int i = myTarget.specialWeapons.Count - 1; i > -1; i--)
        {
			if(myTarget.specialWeapons[i].firstPersonWeapon == null || myTarget.specialWeapons[i].firstPersonWeapon.weaponType != FPSWeapon.WeaponType.Special)
            {
				myTarget.specialWeapons.RemoveAt(i);
			}
		}

		//Find first person weapons
		FPSWeapon[] tmpWeapons = myTarget.firstPersonAudioSource.GetComponentsInChildren<FPSWeapon>(true);

		for(int i = 0; i < tmpWeapons.Length; i++)
        {
			bool canBeAdded = true;

			if(tmpWeapons[i].weaponType == FPSWeapon.WeaponType.Primary)
            {
				for(int a = 0; a < myTarget.primaryWeapons.Count; a++)
                {
					if(myTarget.primaryWeapons[a].firstPersonWeapon == tmpWeapons[i])
                    {
						canBeAdded = false;
					}
				}

				if(canBeAdded)
                {
					myTarget.primaryWeapons.Add(new PlayerWeapons.WeaponSet(tmpWeapons[i], null));
					newWeapons ++;
				}
			}

			if(tmpWeapons[i].weaponType == FPSWeapon.WeaponType.Secondary)
            {
				for(int a = 0; a < myTarget.secondaryWeapons.Count; a++)
                {
					if(myTarget.secondaryWeapons[a].firstPersonWeapon == tmpWeapons[i])
                    {
						canBeAdded = false;
					}
				}
				
				if(canBeAdded)
                {
					myTarget.secondaryWeapons.Add(new PlayerWeapons.WeaponSet(tmpWeapons[i], null));
					newWeapons ++;
				}
			}
			if(tmpWeapons[i].weaponType == FPSWeapon.WeaponType.Special)
            {
				for(int a = 0; a < myTarget.specialWeapons.Count; a++)
                {
					if(myTarget.specialWeapons[a].firstPersonWeapon == tmpWeapons[i])
                    {
						canBeAdded = false;
					}
				}
				
				if(canBeAdded)
                {
					myTarget.specialWeapons.Add(new PlayerWeapons.WeaponSet(tmpWeapons[i], null));
					newWeapons ++;
				}
			}
		}

		//Now add third person weapons
		FPSWeapon[] tmpWeaponsThirdPerson = myTarget.thirdPersonAudioSource.GetComponentsInChildren<FPSWeapon>(true);

		for(int i = 0; i < tmpWeaponsThirdPerson.Length; i++)
        {
			for(int a = 0; a < myTarget.primaryWeapons.Count; a++)
            {
				if(myTarget.primaryWeapons[a].firstPersonWeapon.name.Replace(" ", "") == tmpWeaponsThirdPerson[i].name.Replace(" ", ""))
                {
					myTarget.primaryWeapons[a].thirdPersonWeapon = tmpWeaponsThirdPerson[i];
				}
			}
			
			for(int a = 0; a < myTarget.secondaryWeapons.Count; a++)
            {
				if(myTarget.secondaryWeapons[a].firstPersonWeapon.name.Replace(" ", "") == tmpWeaponsThirdPerson[i].name.Replace(" ", ""))
                {
					myTarget.secondaryWeapons[a].thirdPersonWeapon = tmpWeaponsThirdPerson[i];
				}
			}
			
			for(int a = 0; a < myTarget.specialWeapons.Count; a++)
            {
				if(myTarget.specialWeapons[a].firstPersonWeapon.name.Replace(" ", "") == tmpWeaponsThirdPerson[i].name.Replace(" ", ""))
                {
					myTarget.specialWeapons[a].thirdPersonWeapon = tmpWeaponsThirdPerson[i];
				}
			}
		}

		if(newWeapons == 0)
        {
			statusString = "No new weapons found";
		}
        else
        {
			statusString = "Added " + newWeapons.ToString() + " new weapons";
		}
	}
}
