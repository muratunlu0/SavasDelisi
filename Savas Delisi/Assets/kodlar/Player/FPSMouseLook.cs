//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;

public class FPSMouseLook : MonoBehaviour
{

	public enum RotationAxes { MouseX, MouseY, MouseOrbit}
	public RotationAxes axes = RotationAxes.MouseX;

	[HideInInspector]
	public Transform target; //This needed to be assigned in case of MouseOrbit option selected

	float currentSensitivityX = 1.5F;
	float currentSensitivityY = 1.5F;

	float sensitivityX = 1.5F;
	float sensitivityY = 1.5F;

	float minimumY = -60F;
	float maximumY = 60F;
	
	float rotationX = 0F;
	float rotationY = 0F;

	float minimumX = -360F;
	float maximumX = 360F;
	
	Quaternion originalRotation;

	bool doingRecoil = false;
	float recoilAmount = 0.15f;
	float tempRecoil = 0;

	float distance = 5.0f;
	bool reachedTarget = false;

	void Start ()
    {
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
        {
			GetComponent<Rigidbody>().freezeRotation = true;
		}

		originalRotation = transform.localRotation;
	}
	
	void LateUpdate ()
    {
		if(GameSettings.menuOpened)
			return;

		if(currentSensitivityX != GameSettings.mouseSensitivity || currentSensitivityY != GameSettings.mouseSensitivity)
        {
			currentSensitivityX = currentSensitivityY = GameSettings.mouseSensitivity;
		}

		sensitivityX = currentSensitivityX * (GameSettings.currentFOV/GameSettings.defaultFOV);
		sensitivityY = currentSensitivityY * (GameSettings.currentFOV/GameSettings.defaultFOV);

		if (axes == RotationAxes.MouseX)
        {

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
            rotationX += GameSettings.lookDirection.x * sensitivityX;
#else
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
			
#endif

            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;
		}

		if (axes == RotationAxes.MouseY)
        {

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
            rotationY += GameSettings.lookDirection.y * sensitivityY;
#else
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			
#endif

            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis (-rotationY, Vector3.right);
			transform.localRotation = originalRotation * yQuaternion;
		}

		if (axes == RotationAxes.MouseOrbit && reachedTarget && target)
        {

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
            rotationX += GameSettings.lookDirection.x * sensitivityX;
            rotationY += GameSettings.lookDirection.y * sensitivityY;
#else
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY -= Input.GetAxis("Mouse Y") * sensitivityY;
#endif

            rotationX = ClampAngle (rotationX, minimumX, maximumX);
			rotationY = ClampAngle (rotationY, minimumY, maximumY);

			Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
			Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
	
			transform.rotation = rotation;
			transform.position =  position;
		}
	}
	
	public static float ClampAngle (float angle, float min, float max)
    {
		if (angle < -360F)
        {
			angle += 360F;
		}

		if (angle > 360F)
        {
			angle -= 360F;
		}

		return Mathf.Clamp (angle, min, max);
	}

	public void Recoil()
    {
		//currentRecoil += recoilAmount;
		tempRecoil = 0;

		if(!doingRecoil)
        {
			StartCoroutine(SmoothRecoil());
		}
	}

	IEnumerator SmoothRecoil()
    {
		doingRecoil = true;

		while(tempRecoil < recoilAmount - 0.05f)
        {
			if(tempRecoil < recoilAmount/1.5f)
            {
				tempRecoil = Mathf.Lerp(tempRecoil, recoilAmount, Time.deltaTime * 15);
			}
            else
            {
				tempRecoil = Mathf.Lerp(tempRecoil, recoilAmount, Time.deltaTime * 9);
			}

			rotationY += tempRecoil;

			yield return null;
		}

		//tempRecoil = 0;
		//currentRecoil = 0;

		doingRecoil = false;
	}

	public void AssignTarget(Transform t1)
    {
		if(t1)
        {
			rotationX = t1.eulerAngles.y;
			rotationY = 25;
			target = t1;
			reachedTarget = false;
		}
        else
        {
			rotationX = transform.eulerAngles.y;
			rotationY = 0;
			target = null;
			reachedTarget = true;
		}

		if(target)
        {
			StopCoroutine("MoveToTarget");
			StartCoroutine("MoveToTarget");
		}
	}

	IEnumerator MoveToTarget ()
    {
		Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
		transform.rotation = rotation;
		while(target && Vector3.Distance(rotation * new Vector3(0.0f, 0.0f, -distance) + target.position, transform.position) > 0.1f)
        {
			transform.position = Vector3.MoveTowards(transform.position,  rotation * new Vector3(0.0f, 0.0f, -distance) + target.position, Time.deltaTime * 79);
			yield return null;
		}

		reachedTarget = true;
	}
}
