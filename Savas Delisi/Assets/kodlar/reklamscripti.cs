using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;
using UnityEngine.UI;
using System;
public class reklamscripti : MonoBehaviour {
    public int sysHour;
    
    //public int 
    public Text reklamsayacı;
    public GameObject ödülcanvası;
    public Text aldıgımiktaryazisi;
    
    void Start()
    {
        sysHour = System.DateTime.Now.Minute;
        reklamkontrol();
        
       

        

    }
    public void ShowAd()
    {

        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }

        
        
            
        
            
    }
    public void reklamkontrol()
    {
        if (Application.loadedLevelName == "_MainMenu")
        {
            sysHour = System.DateTime.Now.Minute;

            if (sysHour >= 0 & sysHour < 10)
            {
                if (PlayerPrefs.GetInt("A") == 0)
                {
                    reklamsayacı.text = "50 ALTIN İÇİN TIKLA";
                }
                else
                {
                    reklamsayacı.text = "BEKLEMELİSİN :)";
                }
            }
            else if (sysHour >= 10 & sysHour < 20)
            {
                if (PlayerPrefs.GetInt("B") == 0)
                {
                    reklamsayacı.text = "50 ALTIN İÇİN TIKLA";
                }
                else
                {
                    reklamsayacı.text = "BEKLEMELİSİN :)";
                }
            }
            else if (sysHour >= 20 & sysHour < 30)
            {
                if (PlayerPrefs.GetInt("C") == 0)
                {
                    reklamsayacı.text = "50 ALTIN İÇİN TIKLA";
                }
                else
                {
                    reklamsayacı.text = "BEKLEMELİSİN :)";
                }
            }
            else if (sysHour >= 30 & sysHour < 40)
            {
                if (PlayerPrefs.GetInt("D") == 0)
                {
                    reklamsayacı.text = "50 ALTIN İÇİN TIKLA";
                }
                else
                {
                    reklamsayacı.text = "BEKLEMELİSİN :)";
                }
            }
            else if (sysHour >= 40 & sysHour < 50)
            {
                if (PlayerPrefs.GetInt("E") == 0)
                {
                    reklamsayacı.text = "50 ALTIN İÇİN TIKLA";
                }
                else
                {
                    reklamsayacı.text = "BEKLEMELİSİN :)";
                }
            }
            else if (sysHour >= 50 & sysHour <= 59)
            {
                if (PlayerPrefs.GetInt("F") == 0)
                {
                    reklamsayacı.text = "50 ALTIN İÇİN TIKLA";
                }
                else
                {
                    reklamsayacı.text = "BEKLEMELİSİN :)";
                }

            }

        }
        Invoke("reklamkontrol", 2);
    }
    
    public void reklamgosterrrrr()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }
    
    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:

                GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek += 30;
                PlayerPrefs.SetInt("TOPLAMSİMSEK", GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
                
                
                ödülcanvası.SetActive(true);
                aldıgımiktaryazisi.text = "30" + " ALTIN KAZANDINIZ.";
                GameObject.Find("magazascripti").GetComponent<oyunmagaza>().magazatoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
                GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);

                GameObject.Find("firebase-message").GetComponent<databasee>().altin_ekle_ucretsiz();
                break;
            case ShowResult.Skipped:
                ödülcanvası.SetActive(true);
                aldıgımiktaryazisi.text = "ALTIN KAZANMAK İÇİN VİDEONUN TAMAMINI İZLEMELİSİN :)";
                Debug.Log("PLayer did not fully wath the ad");
                break;
            case ShowResult.Failed:
                Debug.Log("PLayer failed to launch the ad ? Internet?");
                break;
        }
    }
}
