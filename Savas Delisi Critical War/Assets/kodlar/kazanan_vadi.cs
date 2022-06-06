using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class kazanan_vadi : MonoBehaviour {
    public GameObject vadi_kazandımbutonu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerExit(Collider temas)
    {
        if (temas.gameObject.tag == "Player")
        {

            vadi_kazandımbutonu.SetActive(true);

        }
        else
        {
            vadi_kazandımbutonu.SetActive(false);
        }
    }
    
}
