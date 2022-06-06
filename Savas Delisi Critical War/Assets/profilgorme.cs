using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class profilgorme : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void profile_bakcanmi()
    {
        var nickname = gameObject.transform.GetChild(0);
        GameObject.Find("firebase-message").GetComponent<databasee>().oyuncu_nick_profile_bak.text = nickname.GetComponent<Text>().text;
    }
}
