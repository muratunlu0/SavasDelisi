using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
public class menugecisi : MonoBehaviour {
    public string oyuncu_instagram;
    public string oyuncu_facebook;
    
    public string URLgame;
    public string oyungrubuu;
    public string URLinstagram;
    public string URLfacebook_kisisel;
    public string youtube_berkkalkan;
    public string youtube_kisisel;
    public string youtube_mertcanbz;
    public string youtube_kalitelipanda;
    public Material[] takımtexture;
    public GameObject playermesh;
    public GameObject olduinsta;
    public GameObject olduface;
    public GameObject olduyoutube;
    public GameObject olduplaystore;
    public Text Progress_text;
    public Slider slider_p;
    public GameObject Panel;
	

    void Start()
    {
        if (PlayerPrefs.GetInt("googleplay_linki") == 1)
        {
            olduplaystore.SetActive(true);

        }
        if (PlayerPrefs.GetInt("instagram_kisisel") == 1)
        {
            
            olduinsta.SetActive(true);
        }
        if (PlayerPrefs.GetInt("oyungrubu") == 1)
        {
            olduface.SetActive(true);


        }
        if (PlayerPrefs.GetInt("kisiselyoutube") == 1)
        {

            olduyoutube.SetActive(true);
        }
    }
    public void Atakımıbutonu()
    {
        playermesh.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material= takımtexture[0];
    }
    public void Btakımıbutonu()
    {
        playermesh.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = takımtexture[1];

    }
     
    public void exitmenuueveet()
    {
        Application.Quit();
    }
  
    public void urlLinkgame()
    {
        Application.OpenURL(URLgame);
    }
    
    public void urlLinkOrinstagram()
    {
        Application.OpenURL(URLinstagram);
    }

    public void LoadLevel()
    {
        Debug.Log("ayrıl tusuna basıldı");
        SceneManager.LoadScene("_MainMenu");
        // StartCoroutine(Load_Progress(1));
    }
    public void kisiselface()
    {
        Application.OpenURL(URLfacebook_kisisel);
    }
    public void berkkalkan_youtube()
    {
        Application.OpenURL(youtube_berkkalkan);
    }
    public void mertcanbz_youtube()
    {
        Application.OpenURL(youtube_mertcanbz);
    }
    public void kalitelipanda_youtube()
    {
        Application.OpenURL(youtube_kalitelipanda);
    }

    public void googleplay_linki()
    {
        if (PlayerPrefs.GetInt("googleplay_linki") == 0)
        {
            PlayerPrefs.SetInt("TOPLAMSİMSEK", GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek + 200);

            Application.OpenURL(URLgame);

            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            olduplaystore.SetActive(true);
            PlayerPrefs.SetInt("googleplay_linki", 1);
        }
    }
    public void kisisel_instagram()
    {
        if (PlayerPrefs.GetInt("instagram_kisisel") == 0)
        {
            PlayerPrefs.SetInt("TOPLAMSİMSEK", GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek + 200);
            
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            olduinsta.SetActive(true);
            PlayerPrefs.SetInt("instagram_kisisel", 1);
            Application.OpenURL(URLinstagram);
        }
    }
    public void oyungrubu_facebook()
    {
        if (PlayerPrefs.GetInt("oyungrubu") == 0)
        {
            PlayerPrefs.SetInt("TOPLAMSİMSEK", GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek+200);
            
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            olduface.SetActive(true);
            PlayerPrefs.SetInt("oyungrubu", 1);
            Application.OpenURL(oyungrubuu);
        }
    }
    public void kisiselyoutube()
    {
        if( PlayerPrefs.GetInt("kisiselyoutube")==0 )
        {
            PlayerPrefs.SetInt("TOPLAMSİMSEK", GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek+200);
            
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().anamenutoplamsimsek.text = Convert.ToString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().toplamşimşek);
            olduyoutube.SetActive(true);
            PlayerPrefs.SetInt("kisiselyoutube", 1);
            Application.OpenURL(youtube_kisisel);
        }
        
    }
    public void oyuncu_instagramm()
    {
        if(oyuncu_instagram!=""||oyuncu_instagram!=null||oyuncu_instagram!=" ")
        {
            Application.OpenURL("https://www.instagram.com/" + oyuncu_instagram);
        }
           
        
    }
    public void oyuncu_face()
    {
        if (oyuncu_instagram != "" || oyuncu_instagram != null || oyuncu_instagram != " ")
        {
            Application.OpenURL("https://www.facebook.com/" + oyuncu_facebook);
        }
    }
    //IEnumerator Load_Progress(int level_index)
    //{
    //    AsyncOperation Operation = SceneManager.LoadSceneAsync(level_index);
    //    SceneManager.LoadScene(level_index);

    //    Panel.SetActive(true);
    //    while (!Operation.isDone)
    //    {
    //        float progress = Mathf.Clamp01(Operation.progress / 0.9f);
    //        slider_p.value = progress;
    //        Progress_text.text = progress * 100 + "%";
    //        yield return null;

    //    }
    //}
}
