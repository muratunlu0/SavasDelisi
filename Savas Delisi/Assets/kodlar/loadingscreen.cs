using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class loadingscreen : MonoBehaviour {

    public Text Progress_text;
    public Slider slider_p;
    public GameObject Panel;

    void Start () {

        SceneManager.LoadScene("_MainMenu");
        // Invoke("LoadLevel", 0);
    }
    public void SAVAŞBUTONU()
    {
        Invoke("LoadLevelsavas", 0);
    }
    public void LoadLevel()
    {
       // StartCoroutine(Load_Progress(1));
    }
    public void LoadLevelsavas()
    {
       // StartCoroutine(Load_Progress(2));
    }
    //IEnumerator Load_Progress(int level_index)
    //{
    //    AsyncOperation Operation = SceneManager.LoadSceneAsync(level_index);

    //    //Panel.SetActive(true);



    //    while(!Operation.isDone)
    //    {
    //        int progress = (int)Mathf.Clamp01(Operation.progress / 0.9f);
    //        //slider_p.value = progress;
    //        //Progress_text.text = progress * 100 + "%";



    //        yield return null;
    //    }
    //}

	
}
