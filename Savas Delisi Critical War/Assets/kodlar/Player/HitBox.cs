//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;

public class HitBox : MonoBehaviour
{

	//This script will be automatically assigned to hit box colliders by PlayerNetwork.cs

	public enum BodyPart { Head, Torso, Limbs }
	public BodyPart bodyPart;
	public PlayerNetwork playerNetwork;

	Vector3 cameraRelative;

	//Here we receive damage from FPSWeapon.cs and pass it further to PlayerNetwork.cs
	public void Damage (int[] values, Vector3 hitPos)
    {
		cameraRelative = playerNetwork.thisT.InverseTransformPoint(hitPos);

		//1 - Up, 2 - Down, 3 - Left, 4 - Right
		if(cameraRelative.x > -0.14f && cameraRelative.x < 0.14f)
        {
			if (cameraRelative.z > 0)
            {
				values[2] = 1; // Up
			}
            else
            {
				values[2] = 2; // Down
			}
		}
        else
        {
			if(cameraRelative.x < -0.14f)
            {
				values[2] = 3; // Left
			}
			if(cameraRelative.x > 0.14f)
            {
				values[2] = 4; // Right
			}
		}
		
		playerNetwork.ApplyDamage(values);
	}

	public void AssignVariables (PlayerNetwork pn, HitBox.BodyPart bp)
    {
		playerNetwork = pn;
		bodyPart = bp;
	}
}
