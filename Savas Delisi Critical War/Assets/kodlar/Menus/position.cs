using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class position : MonoBehaviour {
    //public Image solateştuşu;
    public Image solateştuşu;
    public Image sagatestusu;
    public Image hedefalmatusu;
    public Image zıplamatusu;
    public Image egilmetusu;
    public Image sarjortusu;
    public int solatessayi;
    public int sagatessayi;
    public int hedefalmasayi;
    public int zıplmasayi;
    public int egilmesayi;
    public int sarjorsayi;
    public Image solateştuşu_real;
    public Image sagatestusu_real;
    public Image hedefalmatusu_real;
    public Image zıplamatusu_real;
    public Image egilmetusu_real;
    public Image sarjortusu_real;
    public GameObject butonkonumdegistirmekanvası;
    //private Touch touch;
    
    // Update is called once per frame
    public void solates()
    {
        solatessayi = 1;
    }
    public void sagates()
    {
        sagatessayi = 1;
    }
    public void hedefalma()
    {
        hedefalmasayi = 1;
    }
    public void zıplama()
    {
        zıplmasayi = 1;
    }
    public void egilme()
    {
        egilmesayi = 1;
    }
    public void sarjor()
    {
        sarjorsayi = 1;
    }
    public void olmasın()
    {
        solatessayi = 0;
        sagatessayi = 0;
        hedefalmasayi = 0;
        zıplmasayi = 0;
        egilmesayi = 0;
        sarjorsayi = 0;

    }public void ilkbuton_konumu_al_anamennu()
    {
        if (PlayerPrefs.GetInt("oldubitti11") == 0)
        {
            PlayerPrefs.SetFloat("solateskonum_x", solateştuşu.rectTransform.position.x);

            PlayerPrefs.SetFloat("solateskonum_y", solateştuşu.rectTransform.position.y);
            PlayerPrefs.SetFloat("sagateskonum_x", sagatestusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("sagateskonum_y", sagatestusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("hedefalma_x", hedefalmatusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("hedefalma_y", hedefalmatusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("zıplama_x", zıplamatusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("zıplama_y", zıplamatusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("egilme_x", egilmetusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("egilme_y", egilmetusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("sarjor_x", sarjortusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("sarjor_y", sarjortusu.rectTransform.position.y);
            ///////////////////////////////
            PlayerPrefs.SetFloat("solateskonum_x_ilk", solateştuşu.rectTransform.position.x);

            PlayerPrefs.SetFloat("solateskonum_y_ilk", solateştuşu.rectTransform.position.y);
            PlayerPrefs.SetFloat("sagateskonum_x_ilk", sagatestusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("sagateskonum_y_ilk", sagatestusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("hedefalma_x_ilk", hedefalmatusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("hedefalma_y_ilk", hedefalmatusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("zıplama_x_ilk", zıplamatusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("zıplama_y_ilk", zıplamatusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("egilme_x_ilk", egilmetusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("egilme_y_ilk", egilmetusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("sarjor_x_ilk", sarjortusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("sarjor_y_ilk", sarjortusu.rectTransform.position.y);
            ////////////////////////////////////
            PlayerPrefs.SetInt("oldubitti11", 1);
        }
    }
    public void ilkkonumkayıtharita()
    {
        if (PlayerPrefs.GetInt("oldubitti11") == 0)
        {

            PlayerPrefs.SetFloat("solateskonum_x", solateştuşu.rectTransform.position.x);

            PlayerPrefs.SetFloat("solateskonum_y", solateştuşu.rectTransform.position.y);
            PlayerPrefs.SetFloat("sagateskonum_x", sagatestusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("sagateskonum_y", sagatestusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("hedefalma_x", hedefalmatusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("hedefalma_y", hedefalmatusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("zıplama_x", zıplamatusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("zıplama_y", zıplamatusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("egilme_x", egilmetusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("egilme_y", egilmetusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("sarjor_x", sarjortusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("sarjor_y", sarjortusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("solateskonum_x_ilk", solateştuşu.rectTransform.position.x);

            PlayerPrefs.SetFloat("solateskonum_y_ilk", solateştuşu.rectTransform.position.y);
            PlayerPrefs.SetFloat("sagateskonum_x_ilk", sagatestusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("sagateskonum_y_ilk", sagatestusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("hedefalma_x_ilk", hedefalmatusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("hedefalma_y_ilk", hedefalmatusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("zıplama_x_ilk", zıplamatusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("zıplama_y_ilk", zıplamatusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("egilme_x_ilk", egilmetusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("egilme_y_ilk", egilmetusu.rectTransform.position.y);
            PlayerPrefs.SetFloat("sarjor_x_ilk", sarjortusu.rectTransform.position.x);

            PlayerPrefs.SetFloat("sarjor_y_ilk", sarjortusu.rectTransform.position.y);

            PlayerPrefs.SetInt("oldubitti11", 1);
        }
    }
    public void varsayılana_sıfırla()
    {
        PlayerPrefs.SetFloat("solateskonum_x", PlayerPrefs.GetFloat("solateskonum_x_ilk"));

        PlayerPrefs.SetFloat("solateskonum_y", PlayerPrefs.GetFloat("solateskonum_y_ilk"));
        PlayerPrefs.SetFloat("sagateskonum_x", PlayerPrefs.GetFloat("sagateskonum_x_ilk"));

        PlayerPrefs.SetFloat("sagateskonum_y", PlayerPrefs.GetFloat("sagateskonum_y_ilk"));
        PlayerPrefs.SetFloat("hedefalma_x", PlayerPrefs.GetFloat("hedefalma_x_ilk"));

        PlayerPrefs.SetFloat("hedefalma_y", PlayerPrefs.GetFloat("hedefalma_y_ilk"));
        PlayerPrefs.SetFloat("zıplama_x", PlayerPrefs.GetFloat("zıplama_x_ilk"));

        PlayerPrefs.SetFloat("zıplama_y", PlayerPrefs.GetFloat("zıplama_y_ilk"));
        PlayerPrefs.SetFloat("egilme_x", PlayerPrefs.GetFloat("egilme_x_ilk"));

        PlayerPrefs.SetFloat("egilme_y", PlayerPrefs.GetFloat("egilme_y_ilk"));
        PlayerPrefs.SetFloat("sarjor_x", PlayerPrefs.GetFloat("sarjor_x_ilk"));

        PlayerPrefs.SetFloat("sarjor_y", PlayerPrefs.GetFloat("sarjor_y_ilk"));
        if (Application.loadedLevelName == "_MainMenu")
        {
            solateştuşu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("solateskonum_x"), PlayerPrefs.GetFloat("solateskonum_y"), 0);
            sagatestusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sagateskonum_x"), PlayerPrefs.GetFloat("sagateskonum_y"), 0);
            hedefalmatusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("hedefalma_x"), PlayerPrefs.GetFloat("hedefalma_y"), 0);
            zıplamatusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("zıplama_x"), PlayerPrefs.GetFloat("zıplama_y"), 0);
            egilmetusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("egilme_x"), PlayerPrefs.GetFloat("egilme_y"), 0);
            sarjortusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sarjor_x"), PlayerPrefs.GetFloat("sarjor_y"), 0);
        }
        if (Application.loadedLevelName != "_MainMenu")
        {
            GameObject.Find("asdf").GetComponent<position>().solateştuşu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("solateskonum_x"), PlayerPrefs.GetFloat("solateskonum_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().sagatestusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sagateskonum_x"), PlayerPrefs.GetFloat("sagateskonum_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().hedefalmatusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("hedefalma_x"), PlayerPrefs.GetFloat("hedefalma_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().zıplamatusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("zıplama_x"), PlayerPrefs.GetFloat("zıplama_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().egilmetusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("egilme_x"), PlayerPrefs.GetFloat("egilme_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().sarjortusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sarjor_x"), PlayerPrefs.GetFloat("sarjor_y"), 0);
        }
        }
    void FixedUpdate()
    {
        //touch = Input.GetTouch(0);
        //float x = -7.5f + 16 * touch.position.x / Screen.width;
        //float y = -4.5f + 10 * touch.position.y / Screen.height;
        //transform.position = new Vector3(x, y, 0);
        // kasa.rectTransform.position = new Vector3(touch.position.x, touch.position.y, 0);
        

            if (solatessayi == 1) 
        {


            if (Input.touchCount > 0)
            {
               solateştuşu.rectTransform.position =new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);// Translate(Input.GetTouch(0).deltaPosition * Time.deltaTime * 10f);
                                                                                                                              // solateştuşu_real.rectTransform.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);// Translate(Input.GetTouch(0).deltaPosition * Time.deltaTime * 10f);
                PlayerPrefs.SetFloat("solateskonum_x", solateştuşu.rectTransform.position.x);

                PlayerPrefs.SetFloat("solateskonum_y", solateştuşu.rectTransform.position.y);
            }
        }
        else if (sagatessayi == 1)
        {


            if (Input.touchCount > 0)
            {
                sagatestusu.rectTransform.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                // sagatestusu_real.rectTransform.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                PlayerPrefs.SetFloat("sagateskonum_x", sagatestusu.rectTransform.position.x);

                PlayerPrefs.SetFloat("sagateskonum_y", sagatestusu.rectTransform.position.y);
            }
        }
        else if (hedefalmasayi == 1)
        {


            if (Input.touchCount > 0)
            {
                 hedefalmatusu.rectTransform.position =new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                // hedefalmatusu_real.rectTransform.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                PlayerPrefs.SetFloat("hedefalma_x", Input.GetTouch(0).position.x);

                PlayerPrefs.SetFloat("hedefalma_y", Input.GetTouch(0).position.y);
            }
        }
        else if (zıplmasayi == 1)
        {


            if (Input.touchCount > 0)
            {
                zıplamatusu.rectTransform.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                // zıplamatusu_real.rectTransform.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                PlayerPrefs.SetFloat("zıplama_x", Input.GetTouch(0).position.x);

                PlayerPrefs.SetFloat("zıplama_y", Input.GetTouch(0).position.y);
            }
        }
        else if (egilmesayi == 1)
        {


            if (Input.touchCount > 0)
            {
                  egilmetusu.rectTransform.position =new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                // egilmetusu_real.rectTransform.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                PlayerPrefs.SetFloat("egilme_x", Input.GetTouch(0).position.x);

                PlayerPrefs.SetFloat("egilme_y", Input.GetTouch(0).position.y);
            }
        }
        else if (sarjorsayi == 1)
        {


            if (Input.touchCount > 0)
            {
                  sarjortusu.rectTransform.position =new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                // sarjortusu_real.rectTransform.position = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);
                PlayerPrefs.SetFloat("sarjor_x", Input.GetTouch(0).position.x);

                PlayerPrefs.SetFloat("sarjor_y", Input.GetTouch(0).position.y);
            }
        }
        if (Application.loadedLevelName == "_MainMenu")
        {
            if (butonkonumdegistirmekanvası.active == true)
            {

                

                

                   
                        
                            solateştuşu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("solateskonum_x"), PlayerPrefs.GetFloat("solateskonum_y"), 0);
                            sagatestusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sagateskonum_x"), PlayerPrefs.GetFloat("sagateskonum_y"), 0);
                            hedefalmatusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("hedefalma_x"), PlayerPrefs.GetFloat("hedefalma_y"), 0);
                            zıplamatusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("zıplama_x"), PlayerPrefs.GetFloat("zıplama_y"), 0);
                            egilmetusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("egilme_x"), PlayerPrefs.GetFloat("egilme_y"), 0);
                            sarjortusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sarjor_x"), PlayerPrefs.GetFloat("sarjor_y"), 0);

                        //        GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
                         //        //GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
                         //    }

                    
                
            }
        }
            
            if (Application.loadedLevelName != "_MainMenu")
            {
            if (GameObject.Find("asdf").GetComponent<Scoreboard>().butonkonumdegistirmekanvası.active == true)
            {
                

                
                    
                        GameObject.Find("asdf").GetComponent<position>().solateştuşu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("solateskonum_x"), PlayerPrefs.GetFloat("solateskonum_y"), 0);
                        GameObject.Find("asdf").GetComponent<position>().sagatestusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sagateskonum_x"), PlayerPrefs.GetFloat("sagateskonum_y"), 0);
                        GameObject.Find("asdf").GetComponent<position>().hedefalmatusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("hedefalma_x"), PlayerPrefs.GetFloat("hedefalma_y"), 0);
                        GameObject.Find("asdf").GetComponent<position>().zıplamatusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("zıplama_x"), PlayerPrefs.GetFloat("zıplama_y"), 0);
                        GameObject.Find("asdf").GetComponent<position>().egilmetusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("egilme_x"), PlayerPrefs.GetFloat("egilme_y"), 0);
                        GameObject.Find("asdf").GetComponent<position>().sarjortusu.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sarjor_x"), PlayerPrefs.GetFloat("sarjor_y"), 0);
                        // GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
                        //GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
                    
                    
                
            }
        }
        
    }
}
