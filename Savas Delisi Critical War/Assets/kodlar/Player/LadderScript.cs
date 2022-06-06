//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour
{

	FPSController fc;
	Transform thisT;

	// Use this for initialization
	void Start ()
    {
		GetComponent<BoxCollider>().isTrigger = true;
		thisT = transform;
	}
	
	void OnTriggerEnter(Collider other)
    {
		if(other.CompareTag("Player"))
        {
			if(!fc)
            {
				fc = other.GetComponent<FPSController>();
			}

			if(fc)
            {
				fc.AssignLadder(thisT);
			}
		}
	}

	void OnTriggerExit(Collider other)
    {
		if(other.CompareTag("Player"))
        {
			if(!fc)
            {
				fc = other.GetComponent<FPSController>();
			}
			
			if(fc)
            {
				fc.RemoveLadder(thisT);
			}
		}
	}
}
