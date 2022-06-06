

#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class SpawnPointGizmo : MonoBehaviour
{

	void OnDrawGizmos ()
    { 
		// Draw spawn point gizmo  
		if(!gameObject.name.StartsWith("TeamASpawn") && !gameObject.name.StartsWith("TeamBSpawn"))
        {
			Gizmos.color = Color.black;    
		}
        else
        {
			if(gameObject.name.StartsWith("TeamASpawn"))
            {
				Gizmos.color = GameSettings.teamAColor;
			}
			if(gameObject.name.StartsWith("TeamBSpawn"))
            {
				Gizmos.color = GameSettings.teamBColor;
			}
		}

		Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

		Gizmos.DrawSphere (startPoint, 1);
		Gizmos.color = Color.green;
		Gizmos.DrawLine (startPoint, startPoint + transform.forward * 2);
	}
}
#endif
