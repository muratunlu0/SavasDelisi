//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SoldierAnimation))]
public class SoldierAnimationEditor : Editor
{

	private SoldierAnimation myTarget
    {
		get
        {
			return target as SoldierAnimation;
		}
	}

	public override void OnInspectorGUI()
    {
		// Show default inspector property editor
		DrawDefaultInspector ();

		//EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("Rifle Pose"))
            {
				SetPose(myTarget.normalSet.idle);
			}
			if(GUILayout.Button("Pistol Pose"))
            {
				SetPose(myTarget.pistolSet.idle);
			}
			if(GUILayout.Button("Knife Pose"))
            {
				SetPose(myTarget.knifeSet.idle);
			}
			if(GUILayout.Button("T-Pose"))
            {
				SetPose(myTarget.TPose);
			}
		EditorGUILayout.EndHorizontal();

		if(GUI.changed)
        {
			EditorUtility.SetDirty( target );
			Undo.RegisterCompleteObjectUndo (target, "SoldierAnimationUndoRedo");
		}
	}

	public void SetPose(AnimationClip anm)
    {
		anm.SampleAnimation(myTarget.gameObject, 0);
	}
}
