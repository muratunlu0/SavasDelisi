//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using UnityEditor;
using System.IO;

//This script will check if all required tags are defined and if not, it will add them automatically

[InitializeOnLoad]
public class CheckTags : Editor
{
	static CheckTags()
    {
		//Here we define project unique ID
		string projectID = Random.Range(1111111, 9999999).ToString();
		//File where ID will be stored
		string pathToIDFile = Application.dataPath.Replace("Assets", "") + "ProjectSettings/FPSKitProjectID.txt";
		if(File.Exists(pathToIDFile))
        {
			var sr = File.OpenText(pathToIDFile);
			projectID = (sr.ReadToEnd().Split("\n"[0]))[0];
			sr.Close();
		}
        else
        {
			File.WriteAllText(pathToIDFile, projectID);
		}

		//Debug.Log(pathToIDFile);
		//Debug.Log (projectID);

		string editorPrefKey = "TagsAdded" + projectID + "v5";

		if(!EditorPrefs.HasKey(editorPrefKey))
        {
			string[] requiredTags = new string[]{ "Metal", "Wood", "Body"  };

			Object[] asset = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");

			if (asset != null && asset.Length > 0)
            {
				SerializedObject so = new SerializedObject(asset[0]);
				SerializedProperty tags = so.FindProperty("tags");

				for(int a = 0; a < requiredTags.Length; a++)
                {
					bool canAdd = true;
					for (int i = 0; i < tags.arraySize; i++)
                    {
						if (tags.GetArrayElementAtIndex(i).stringValue == requiredTags[a])
                        {
							// Tag is already defined, nothing to do.
							canAdd = false;    
						}
					}

					if(canAdd)
                    {
						tags.InsertArrayElementAtIndex(0);
						tags.GetArrayElementAtIndex(0).stringValue = requiredTags[a];
						Debug.Log("Added tag '" + requiredTags[a] + "' to Tag Manager");
					}
				}

				so.ApplyModifiedProperties();
				so.Update();

				EditorPrefs.SetBool(editorPrefKey, true);
			}

			Debug.Log("asdasfsafsafsagsaf");
		}
        else
        {
			//Debug.Log("Required tags already added");
		}
	}
}