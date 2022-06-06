//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;

public class BulletAutoDestroy : MonoBehaviour
{

	public float timeOut = 5;

	// Use this for initialization
	void Start ()
    {
		Destroy(gameObject, timeOut);
	}
}
