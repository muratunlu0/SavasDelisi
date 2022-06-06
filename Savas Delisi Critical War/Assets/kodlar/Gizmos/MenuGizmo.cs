
#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class MenuGizmo : MonoBehaviour
{
		
	public Transform[] basePoints; //Connected points that for shape

	float distance = 3.5f;
	float size = 2;
		
	void OnDrawGizmos()
    {
		Gizmos.color = Color.white;
		Vector3 right = transform.right * size;
		Vector3 up = transform.up * size;

		Vector3 point1 = transform.position + (transform.forward * distance) - right + up;
		Vector3 point2 = transform.position + (transform.forward * distance) + right + up;
		Vector3 point3 = transform.position + (transform.forward * distance) + right - up;
		Vector3 point4 = transform.position + (transform.forward * distance) - right - up;

		Gizmos.DrawLine (transform.position, point1);
		Gizmos.DrawLine (transform.position, point2);
		Gizmos.DrawLine (transform.position, point3);
		Gizmos.DrawLine (transform.position, point4);

		Gizmos.DrawLine (point1, point2);
		Gizmos.DrawLine (point2, point3);
		Gizmos.DrawLine (point3, point4);
		Gizmos.DrawLine (point4, point1);
	}
}
#endif
