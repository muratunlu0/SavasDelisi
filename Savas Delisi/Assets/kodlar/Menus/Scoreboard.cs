
using UnityEngine.UI;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
public class Scoreboard : MonoBehaviour
{

    [HideInInspector]
    
	public RoomController rc; //Assigned from RoomController.cs
    public int leaderboard_kacıncısıra;
    
    public Text sayısı;
    public GameObject nokta;
    public GameObject kazandınobjesi;
    public GameObject kaybettinobjesi;
    public GameObject berabereobjesi;
    public int bildirikaçkişi;
    public Text bildiriyazısı;
    public GameObject bildiripaneli;
    public static bool isCrouching = false;
    public Text oyunsonuharita;
    public Text oyunsonumod;
    public int toplam_öldürme_kd;
    public int toplam_ölme_kd;
    public int oyuniçitextparası;
    public Text kdoraniy;
    public double kdgeçici;
    public Text oyunpuaniy;
    public Text oyunparasiy;
    public Text kupasayısiy;
    public int oyunpuanigeçici;
    public GameObject aimbutonu;
    public int silahdegistir_;
    public int ölümsayısıgecici;
    public int öldürmesayısıgecici;
    public int ölümsayısı_;
    public int öldürmesayısı_;
    public int oyuniçiöldürme;
    public int oyuniçiölme;
    public string oyuncuname;
    public Text kazanantakım_ismi;
    public float kd_oranı;
    public int kupa_aldımı;
    public GameObject bildirim_kutusu1;
    public GameObject bildirim_kutusu2;
    public GameObject bildirim_kutusu3;
    public Text öldüren_kisi;
    public Text ölen_kisi;
    public GameObject[] takımskorları;
	Vector2 scoreBoardScroll = Vector2.zero;
    public Color ctrengi;
    public GameObject[] skorsag5;
    public Text zaman;
    public Text maviskor;
    public Text kırmızıskor;
    public GameObject[] hangisilahta;
    public Text hptextt;
    public Text cashtextt;
    public Text weaponandammotextfelan;
    public Text oyunodası_ismi;
    public Text oyuncusayısı;
    public Text ffa_yenidenbaslamasuresi;
    public Text oyuniçiöldürmeyazisi;
    public Text oyuniçiölmeyazisi;
    public GameObject takımsecmecanvası;
    public GameObject controlkanvasi;
    public GameObject butonkonumdegistirmekanvası;
    public GameObject YÜKLEMECANVASI;
    public GameObject ffa_kazanancanvası;
    public GameObject seçeneklercanvası;
    public GameObject scoreboardcanvası;
    public Text ffa_kazananisim;
    public GameObject takımdegsitirmetusu;
    public GameObject ffanotu;
    public Text[] birincitakım_İSİM;
    public Text[] birincitakım_ÖLDÜRME;
    public Text[] birincitakım_ÖLÜM;
    public Text[] birincitakım_PİNG;
    public Text oyuncuismi;
    public Text[] tdm_İSİM;

    public Text[] birincitakım_İSİM_takımsecme;
    public Text[] ikincitakım_İSİM_takımsecme;

    public int a_takımıboş;
    public int b_takımıboş;

    public Text[] tdm_ÖLDÜRME;
    public Text[] tdm_ÖLÜM;
    public Text[] tdm_PİNG;
    public int birincitakımsecildi;
    public int ikincitakımsecildi;
    public int oyunmodu;
    public Text[] ikincitakım_İSİM;   
    public Text[] ikincitakım_ÖLDÜRME;    
    public Text[] ikincitakım_ÖLÜM;   
    public Text[] ikincitakım_PİNG;
    public int oyuncusayı_a;
    public int oyuncusayı_b;
    public int bos_A=0;
    public int bos_B=0;
    public GameObject takımsecmebutonu_A;
    public GameObject takımsecmebutonu_B;
    public Joystick joystick;
    public Text hangitakımda;
    //public int baslangıckapalıolmasıgereken;
    // Use this for initialization
    
    void Start()
    {
        
        // oyuncuismi.text = PlayerPrefs.GetString(GameObject.Find("_ConnectMenu").GetComponent<ConnectMenu>().playerNamePrefsName);
        if (PlayerPrefs.GetInt("oylesinee")==0)
        {
            butonkonumdegistirmekanvası.SetActive(true);
            GameObject.Find("asdf").GetComponent<position>().ilkkonumkayıtharita();
            GameObject.Find("asdf").GetComponent<position>().varsayılana_sıfırla();
            butonkonumdegistirmekanvası.SetActive(false);
            PlayerPrefs.SetInt("oylesinee", 1);
        }
        
        this.enabled = false;
        oyunmodu = PlayerPrefs.GetInt("oyunmod_");
        // ffa_kazanancanvası.SetActive(false);
        
        Debug.Log(oyunmodu);
        if(oyunmodu== 0)
        {
            
            // takımsecmecanvası.SetActive(true);
            ffanotu.SetActive(false);
           // ffa_kazanancanvası.SetActive(false);
        }
        
        if (oyunmodu == 1)
        {
            takımskorları[0].SetActive(false);
            takımskorları[1].SetActive(false);
            takımdegsitirmetusu.SetActive(false);
            takımsecmecanvası.SetActive(false);
            YÜKLEMECANVASI.SetActive(true);
            Invoke("birincitakım", 1);
            //birincitakım();
           // ffa_kazanancanvası.SetActive(false);
        }


       
    }
    // Update is called once per frame
    public void kazanan_fonk()
    {
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomController>().vadi_kazandı_kim();
    }
    public void dürbünac()
    {
        if (PlayerPrefs.GetInt("birincil_3_kusanıldı") == 1 && silahdegistir_ == 0)
        {
            
        }
        else
        {
            GameSettings.mobileAiming = true;
        }
        
    }
    
    public void sarjordoldur()
    {
        GameSettings.mobileReloading = true;
    }
    public void atesetamk()
    {
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomUI>().ateset();
    }
    public void atesdursunamk()
    {
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomUI>().atesdursun();
    }
    public void silagdegistiramk()
    {
       
       
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomUI>().silahdegistirla();

    }
     public void otur()
    {
    isCrouching = true;
        Invoke("dur", 0.001f);
    }
    public void dur()
    {
        isCrouching = false;
    }
    public void zıplaamk()
    {
        GameSettings.mobileJumping = true;
    }
    public void kontrolpanelinikapaamk()
    {
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomUI>().kontrolpanelinikapa();
    }
    public void kontrolpaneliniacmk()
    {
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomUI>().kontrolpaneliniac();
        
    }
    public void canvası_ac()
    {
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomUI>().mainCanvas.gameObject.SetActive(true);
    }
    
    
    void OnGUI ()
    {
		GUI.skin = GameSettings.guiSkin;

		if(PhotonNetwork.room != null)
        {
			GUI.Window (0, new Rect(Screen.width/2 - 425, Screen.height/2 - 250,  850, 500), ScoreboardWindow, "");//850,500
		}
	}
    //void FixedUpdate()
    //{
    //    if(birincitakımsecildi==0 & ikincitakımsecildi==0)
    //    {
    //        ffa_kazanancanvası.SetActive(false);
    //        takımsecmecanvası.SetActive(true);
    //    }
    //}
    public void resetgame()
    {
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomController>().ResetGameStatus(0, null);
    }
    public void birincitakım()
    {
        //SKORBOARD();
        birincitakımsecildi = 0;

        for (int i = 0; i < 8; i++)
        {
            if (birincitakımsecildi == 0)
            {


                if (birincitakım_İSİM[i].text == "*")
                {
                    GameObject.Find("_RoomController(Clone)").GetComponent<RoomController>().PrepareRespawn(1, true);
                    takımsecmecanvası.SetActive(false);
                    Debug.Log("birinci takım secildi.");
                    hangitakımda.text = GameSettings.teamAName;
                    birincitakımsecildi = 1;
                    YÜKLEMECANVASI.SetActive(true);
                }
            }
        }

        // rc.PrepareRespawn(1, true);


    }
    public void ikincitakım()
    {
        //skorboard();
        ikincitakımsecildi = 0;

        for (int i = 0; i < 8; i++)
        {
            if (ikincitakımsecildi == 0)
            {

                
                if (ikincitakım_İSİM[i].text == "*")
                {
                    GameObject.Find("_RoomController(Clone)").GetComponent<RoomController>().PrepareRespawn(2, true);
                    takımsecmecanvası.SetActive(false);
                    Debug.Log("ikinci takım secildi.");
                    hangitakımda.text = GameSettings.teamBName;
                    ikincitakımsecildi = 1;
                    YÜKLEMECANVASI.SetActive(true);
                }
            }
        }

    }
//    public void takımsecmebutonu()
//    {
        
//        bos_A = 0;
//        bos_B = 0;
        

//        for (int i = 0; i < 5; i++)
//        {
            


//                if (birincitakım_İSİM[i].text == "*")
//                {
//                    bos_A = 1;
//                    takımsecmebutonu_A.SetActive(true);
//                }
            
//        }
        

//        for (int i = 0; i < 5; i++)
//        {
            


//                if (ikincitakım_İSİM[i].text == "*")
//                {
//                    bos_B = 1;
//                    takımsecmebutonu_B.SetActive(true);
//                }
            
//        }
//        if(bos_A==0)
//        {
//            takımsecmebutonu_A.SetActive(false);
//}
//        if (bos_B == 0)
//        {
//            takımsecmebutonu_B.SetActive(false);
//        }
        
//    }
    public void odadancık()
    {
        PhotonNetwork.LeaveRoom();
    }
    void ScoreboardWindow (int windowID)
    {
		GUI.Label(new Rect(15, 0, 750, 35), PhotonNetwork.room.name + " - " + PhotonNetwork.room.playerCount.ToString() + "/" + PhotonNetwork.room.maxPlayers.ToString() + " - " + rc.currentGameMode);

		if(GUI.Button(new Rect(850 - 30, 5, 25, 25), "", GameSettings.closeButtonStyle))
        {
			rc.showScoreBoard = false;
		}


        
    






    GUI.enabled = true;

        

        

        
		

		

		GUILayout.Space(100);

		//Scoreboard
		scoreBoardScroll = GUILayout.BeginScrollView(scoreBoardScroll, true, true, GUILayout.Height(500 - 175));
			GUILayout.BeginHorizontal();
				if(rc.currentGameMode == "TDM")
                {
            oyunmodu = 0;
					//Team A
					//DisplayTeamPlayers(rc.teamAPlayers, 400);
					//Team B
					//DisplayTeamPlayerss(rc.teamBPlayers, 400);
            skortahtasıA(rc.teamAPlayers);
            skortahtasıB(rc.teamBPlayers);
            takımbutonları();
        }

				if(rc.currentGameMode == "FFA")
                {
            oyunmodu = 1;
            skortahtasıA(rc.teamAPlayers);
            //Team A and the only in Free For ALl Mode
            //DisplayTeamPlayers(rc.teamAPlayers, 808);
        }
			GUILayout.EndHorizontal();
		GUILayout.EndScrollView();

		GUILayout.Space(15);

		GUI.color = Color.white;
		GUILayout.Label(rc.spectatorNames);
	}
    public void sonuclar()
    {
        maviskor.text = rc.teamAScore.ToString();
       kırmızıskor.text = rc.teamBScore.ToString();
    }
    public void killdeath()
    {
        
    }

    public  void skortahtasıA(List<PhotonPlayer> teamTmp)
    {
        //List<PhotonPlayer> teamTmp =rc.teamAPlayers;
        //if (oyunmodu.text == "FFA")
        //{
        if (oyunmodu == 1)
        {
            
        if (teamTmp.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {

                    tdm_İSİM[i].text = "*";
                    tdm_ÖLDÜRME[i].text = "*";
                    tdm_ÖLÜM[i].text = "*";
                    tdm_PİNG[i].text = "*";
                    
                }
            }
            else
            {
                //for (int i = 0; i < teamTmp.Count; i++)
                //{
                //    //if (birincitakım_İSİM.Length > teamTmp.Count)
                //    //{
                //        if (teamTmp[i].customProperties["PlayerHP"] == null || (int)teamTmp[i].customProperties["PlayerHP"] < 1)
                //        {

                //            // GUILayout.Label("ÖLÜ");

                //        }
                //        birincitakım_İSİM[i].text = teamTmp[i].name;
                //        birincitakım_ÖLDÜRME[i].text = ((int)teamTmp[i].customProperties["Kills"]).ToString();
                //        birincitakım_ÖLÜM[i].text = ((int)teamTmp[i].customProperties["Deaths"]).ToString();
                //        birincitakım_PİNG[i].text = ((int)teamTmp[i].customProperties["Ping"]).ToString();
                //   // }
                //}
                
                if (öldürmesayısıgecici < (int)PhotonNetwork.player.customProperties["Kills"])
                {
                    öldürmesayısı_ = PlayerPrefs.GetInt("öldürmesayımpref") + (int)PhotonNetwork.player.customProperties["Kills"] - öldürmesayısıgecici;
                    oyuniçiöldürme = oyuniçiöldürme + (int)PhotonNetwork.player.customProperties["Kills"] - öldürmesayısıgecici;
                    oyuniçiöldürmeyazisi.text = oyuniçiöldürme.ToString();
                    bildirikaçkişi += 1;
                    bildirifonk();
                    öldürmesayısıgecici = (int)PhotonNetwork.player.customProperties["Kills"];
                    PlayerPrefs.SetInt("öldürmesayımpref", öldürmesayısı_);
                    


                    Debug.Log(öldürmesayısı_ + " öldürme sayısı");



                }
                if (ölümsayısıgecici < (int)PhotonNetwork.player.customProperties["Deaths"])
                {
                    ölümsayısı_ = PlayerPrefs.GetInt("ölmesayımpref") + (int)PhotonNetwork.player.customProperties["Deaths"] - ölümsayısıgecici;
                    //seçeneklercanvası.SetActive(false);
                    //takımsecmecanvası.SetActive(false);
                    //scoreboardcanvası.SetActive(false);
                    bildirikaçkişi = 0;
                    oyuniçiölme = oyuniçiölme + (int)PhotonNetwork.player.customProperties["Deaths"] - ölümsayısıgecici;
                    oyuniçiölmeyazisi.text = oyuniçiölme.ToString();
                    ölümsayısıgecici = (int)PhotonNetwork.player.customProperties["Deaths"];
                    PlayerPrefs.SetInt("ölmesayımpref", ölümsayısı_);
                    
                    Debug.Log(ölümsayısı_ + " ölüm sayısı");

                }
                for (int i = 0; i < 5; i++)
                {
                    skorsag5[i].GetComponent<Image>().color = ctrengi;
                }
                for (int i = 0; i < 10; i++)
                {
                    if (i < teamTmp.Count)
                    {
                        if (teamTmp[i].customProperties["PlayerHP"] == null || (int)teamTmp[i].customProperties["PlayerHP"] < 1)
                        {

                            //GUILayout.Label("ÖLÜ");

                        }
                        tdm_İSİM[i].text = teamTmp[i].name;
                        tdm_ÖLDÜRME[i].text = ((int)teamTmp[i].customProperties["Kills"]).ToString();
                        tdm_ÖLÜM[i].text = ((int)teamTmp[i].customProperties["Deaths"]).ToString();
                        tdm_PİNG[i].text = ((int)teamTmp[i].customProperties["Ping"]).ToString();


                        Debug.Log(oyuncuname);

                        sayısı.text = ((int)PhotonNetwork.player.customProperties["Team"]).ToString();

                        
                            Debug.Log(tdm_İSİM[i].text);
                            


                        
                        //if (PlayerPrefs.HasKey(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().playerName_))
                        //{
                        //    if (tdm_İSİM[i].text == PlayerPrefs.GetString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().playerName_))
                        //    {
                        //        Debug.Log(PlayerPrefs.GetString(GameObject.Find("magazascripti").GetComponent<oyunmagaza>().playerName_));
                        //        // GameObject.Find("asdf").GetComponent<Scoreboard>().öldürmesayısı += 1;
                        //        // GameObject.Find("asdf").GetComponent<Scoreboard>().ölümsayısı += 1;
                        //        // Debug.Log(GameObject.Find("asdf").GetComponent<Scoreboard>().öldürmesayısı+"öldürme");
                        //        //  Debug.Log(GameObject.Find("asdf").GetComponent<Scoreboard>().ölümsayısı+ "ölüm");
                        //    }


                        //}

                    }
                    else if (i >= teamTmp.Count)
                    {
                        tdm_İSİM[i].text = "*";
                        tdm_ÖLDÜRME[i].text = "*";
                        tdm_ÖLÜM[i].text = "*";
                        tdm_PİNG[i].text = "*";
                    }
                }
                
                }

        }
        if (oyunmodu == 0)
        {
            if (teamTmp.Count == 0)
            {
                for (int i = 0; i < 5; i++)
                {

                    birincitakım_İSİM[i].text = "*";
                    birincitakım_ÖLDÜRME[i].text = "*";
                    birincitakım_ÖLÜM[i].text = "*";
                    birincitakım_PİNG[i].text = "*";
                    birincitakım_İSİM_takımsecme[i].text = "*";
                }
            }
            else
            {
                a_takımıboş = teamTmp.Count;
                Debug.Log(a_takımıboş+" atakımı sayısı");
                //for (int i = 0; i < teamTmp.Count; i++)
                //{
                //    //if (birincitakım_İSİM.Length > teamTmp.Count)
                //    //{
                //        if (teamTmp[i].customProperties["PlayerHP"] == null || (int)teamTmp[i].customProperties["PlayerHP"] < 1)
                //        {

                //            // GUILayout.Label("ÖLÜ");

                //        }
                //        birincitakım_İSİM[i].text = teamTmp[i].name;
                //        birincitakım_ÖLDÜRME[i].text = ((int)teamTmp[i].customProperties["Kills"]).ToString();
                //        birincitakım_ÖLÜM[i].text = ((int)teamTmp[i].customProperties["Deaths"]).ToString();
                //        birincitakım_PİNG[i].text = ((int)teamTmp[i].customProperties["Ping"]).ToString();
                //   // }
                //}
                //if (1 == (int)PhotonNetwork.player.customProperties["Team"])
                //{
                //    hangitakımda.text = GameSettings.teamAName;
                //}
                //if (2 == (int)PhotonNetwork.player.customProperties["Team"])
                //{
                //    hangitakımda.text = GameSettings.teamBName;
                //}
                sayısı.text = ((int)PhotonNetwork.player.customProperties["Team"]).ToString();
                if (öldürmesayısıgecici < (int)PhotonNetwork.player.customProperties["Kills"])
                {
                    öldürmesayısı_ = PlayerPrefs.GetInt("öldürmesayımpref") + (int)PhotonNetwork.player.customProperties["Kills"] - öldürmesayısıgecici;
                    bildirikaçkişi += 1;
                    bildirifonk();
                    oyuniçiöldürme = oyuniçiöldürme + (int)PhotonNetwork.player.customProperties["Kills"] - öldürmesayısıgecici;
                    oyuniçiöldürmeyazisi.text = oyuniçiöldürme.ToString();
                    öldürmesayısıgecici = (int)PhotonNetwork.player.customProperties["Kills"];
                    PlayerPrefs.SetInt("öldürmesayımpref", öldürmesayısı_);
                   
                    Debug.Log(öldürmesayısı_ + " öldürme sayısı");

                }
                if (ölümsayısıgecici < (int)PhotonNetwork.player.customProperties["Deaths"])
                {
                    ölümsayısı_ = PlayerPrefs.GetInt("ölmesayımpref") + (int)PhotonNetwork.player.customProperties["Deaths"] - ölümsayısıgecici;
                    bildirikaçkişi = 0;
                    oyuniçiölme = oyuniçiölme + (int)PhotonNetwork.player.customProperties["Deaths"] - ölümsayısıgecici;
                    oyuniçiölmeyazisi.text = oyuniçiölme.ToString();
                    ölümsayısıgecici = (int)PhotonNetwork.player.customProperties["Deaths"];
                    PlayerPrefs.SetInt("ölmesayımpref", ölümsayısı_);
                    
                    Debug.Log(ölümsayısı_ + " ölüm sayısı");
                }
                for (int i = 0; i < 5; i++)
                {
                    if (i < teamTmp.Count)
                    {
                        if (teamTmp[i].customProperties["PlayerHP"] == null || (int)teamTmp[i].customProperties["PlayerHP"] < 1)
                        {

                            //GUILayout.Label("ÖLÜ");

                        }
                        birincitakım_İSİM[i].text = teamTmp[i].name;
                        birincitakım_ÖLDÜRME[i].text = ((int)teamTmp[i].customProperties["Kills"]).ToString();
                        birincitakım_ÖLÜM[i].text = ((int)teamTmp[i].customProperties["Deaths"]).ToString();
                        birincitakım_PİNG[i].text = ((int)teamTmp[i].customProperties["Ping"]).ToString();

                        

                        birincitakım_İSİM_takımsecme[i].text = birincitakım_İSİM[i].text;
                       
                            if(Convert.ToInt32(birincitakım_PİNG[i].text) > 1000)
                            {
                                PhotonNetwork.LeaveRoom();
                            }
                            Debug.Log(birincitakım_İSİM[i].text);
                            //Debug.Log((int)teamTmp[i].customProperties["Kills"]);
                            


                        }
                    
                    else if (i >= teamTmp.Count)
                    {
                        birincitakım_İSİM[i].text = "*";
                        birincitakım_ÖLDÜRME[i].text = "*";
                        birincitakım_ÖLÜM[i].text = "*";
                        birincitakım_PİNG[i].text = "*";
                        birincitakım_İSİM_takımsecme[i].text = "*";

                        
                    }
                }
            }
            takımbutonları();
        }

    }
    public void takımbutonları()
    {
        if(a_takımıboş< b_takımıboş)
        {
            takımsecmebutonu_A.SetActive(true);
            takımsecmebutonu_B.SetActive(false);

        }
        else if (a_takımıboş > b_takımıboş)
        {
            takımsecmebutonu_A.SetActive(false);
            takımsecmebutonu_B.SetActive(true);

        }
        else if (a_takımıboş==b_takımıboş)
        {
            takımsecmebutonu_A.SetActive(true);
            takımsecmebutonu_B.SetActive(true);
        }

    }
    //public void rastgeletakımsecme()
    //{
    //    if (a_takımıboş < b_takımıboş)
    //    {
    //        birincitakım();
            

    //    }
    //    else if (a_takımıboş > b_takımıboş)
    //    {
    //        ikincitakım();

    //    }
    //    else if (a_takımıboş == b_takımıboş)
    //    {
    //        birincitakım();
    //    }

    //}
    public  void skortahtasıB(List<PhotonPlayer> teamTmp)
    {
        //List<PhotonPlayer> teamTmp = rc.teamBPlayers;

        if (teamTmp.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                ikincitakım_İSİM[i].text = "*";
                ikincitakım_ÖLDÜRME[i].text = "*";
                ikincitakım_ÖLÜM[i].text = "*";
                ikincitakım_PİNG[i].text = "*";
                ikincitakım_İSİM_takımsecme[i].text = "*";


            }
            }
        else
        {

            b_takımıboş = teamTmp.Count;
            Debug.Log(b_takımıboş + " btakımı sayısı");
            //for (int i = 0; i < teamTmp.Count; i++)
            //{
            //    if (teamTmp[i].customProperties["PlayerHP"] == null || (int)teamTmp[i].customProperties["PlayerHP"] < 1)
            //    {

            //        //GUILayout.Label("ÖLÜ");

            //    }
            //    ikincitakım_İSİM[i].text = teamTmp[i].name;
            //    ikincitakım_ÖLDÜRME[i].text = ((int)teamTmp[i].customProperties["Kills"]).ToString();
            //    ikincitakım_ÖLÜM[i].text = ((int)teamTmp[i].customProperties["Deaths"]).ToString();
            //    ikincitakım_PİNG[i].text = ((int)teamTmp[i].customProperties["Ping"]).ToString();

            //if (1 == (int)PhotonNetwork.player.customProperties["Team"])
            //{
            //    hangitakımda.text = GameSettings.teamAName;
            //}
            //if (2 == (int)PhotonNetwork.player.customProperties["Team"])
            //{
            //    hangitakımda.text = GameSettings.teamBName;
            //}
            sayısı.text = ((int)PhotonNetwork.player.customProperties["Team"]).ToString();
            //}
            if (öldürmesayısıgecici < (int)PhotonNetwork.player.customProperties["Kills"])
            {
                öldürmesayısı_ = PlayerPrefs.GetInt("öldürmesayımpref") + (int)PhotonNetwork.player.customProperties["Kills"] - öldürmesayısıgecici;
                bildirikaçkişi += 1;
                oyuniçiöldürme = oyuniçiöldürme + (int)PhotonNetwork.player.customProperties["Kills"] - öldürmesayısıgecici;
                oyuniçiöldürmeyazisi.text = oyuniçiöldürme.ToString();

                bildirifonk();
                öldürmesayısıgecici = (int)PhotonNetwork.player.customProperties["Kills"];
                PlayerPrefs.SetInt("öldürmesayımpref", öldürmesayısı_);
               
                Debug.Log(öldürmesayısı_ + " öldürme sayısı");

            }
            if (ölümsayısıgecici < (int)PhotonNetwork.player.customProperties["Deaths"])
            {
                ölümsayısı_ = PlayerPrefs.GetInt("ölmesayımpref") + (int)PhotonNetwork.player.customProperties["Deaths"] - ölümsayısıgecici;
                oyuniçiölme = oyuniçiölme + (int)PhotonNetwork.player.customProperties["Deaths"] - ölümsayısıgecici;
                oyuniçiölmeyazisi.text = oyuniçiölme.ToString();
                bildirikaçkişi = 0;
                ölümsayısıgecici = (int)PhotonNetwork.player.customProperties["Deaths"];
                PlayerPrefs.SetInt("ölmesayımpref", ölümsayısı_);
               
                Debug.Log(ölümsayısı_ + " ölüm sayısı");
            }
            for (int i=0;i<5;i++)
            {
                if(i< teamTmp.Count)
                {
                    if (teamTmp[i].customProperties["PlayerHP"] == null || (int)teamTmp[i].customProperties["PlayerHP"] < 1)
                    {

                        //GUILayout.Label("ÖLÜ");

                    }
                    ikincitakım_İSİM[i].text = teamTmp[i].name;
                    ikincitakım_ÖLDÜRME[i].text = ((int)teamTmp[i].customProperties["Kills"]).ToString();
                    ikincitakım_ÖLÜM[i].text = ((int)teamTmp[i].customProperties["Deaths"]).ToString();
                    ikincitakım_PİNG[i].text = ((int)teamTmp[i].customProperties["Ping"]).ToString();

                    ikincitakım_İSİM_takımsecme[i].text = ikincitakım_İSİM[i].text;
                    
                        if (Convert.ToInt32(ikincitakım_PİNG[i].text) > 1000)
                        {
                            PhotonNetwork.LeaveRoom();
                        }
                        
                        //Debug.Log((int)teamTmp[i].customProperties["Kills"]);
                        


                    
                }
                else if (i >= teamTmp.Count)
                {
                    ikincitakım_İSİM[i].text = "*";
                    ikincitakım_ÖLDÜRME[i].text = "*";
                    ikincitakım_ÖLÜM[i].text = "*";
                    ikincitakım_PİNG[i].text = "*";
                    ikincitakım_İSİM_takımsecme[i].text = "*";
                }
            }
            
        }
        takımbutonları();

    }
    public void bildirikapat()
    {
        bildiripaneli.SetActive(false);
    }
    public void bildirifonk()
    {
        if (bildirikaçkişi == 1)
        {

        }
        else if (bildirikaçkişi == 2)
        {
            bildiripaneli.SetActive(true);
            bildiriyazısı.text = "İKİ KİŞİ";
            Invoke("bildirikapat", 1f);
        }
        else if (bildirikaçkişi == 3)
        {
            bildiripaneli.SetActive(true);
            bildiriyazısı.text = "ÜÇ KİŞİ";
            Invoke("bildirikapat", 1f);

        }
        else if (bildirikaçkişi == 4)
        {
            bildiripaneli.SetActive(true);
            bildiriyazısı.text = "KATİL";
            Invoke("bildirikapat", 1f);

        }
        else if (bildirikaçkişi >4 )
        {
            bildiripaneli.SetActive(true);
            bildiriyazısı.text = "SERİ KATİL";
            Invoke("bildirikapat", 1f);

        }
    }

    public void SKORBOARD()
    {
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomController>().RefreshPlayerList();
        
        //skortahtasıA(rc.teamAPlayers);
        //Team B
        
    }
    //public void OYUNCUSAYISI_B()
    //{
        
    //}
    void DisplayTeamPlayers (List<PhotonPlayer> teamTmp, int fieldWidth)
    {
        Debug.Log(teamTmp.Count);
        oyuncusayı_a = teamTmp.Count;

        GUILayout.BeginVertical();
			if(teamTmp.Count == 0)
            {
				GUI.color = new Color(1, 1, 1, 0.5f);
				GUILayout.BeginVertical("box", GUILayout.Width(fieldWidth));
				GUILayout.Label("Boş");
				GUILayout.Space(5);
				GUILayout.Label("...");
				GUILayout.EndVertical();
			}
            else
            {
            //for (int a = 0; a < oyuncusayı_a; a++)
            //{
            //    if (teamTmp[a].customProperties["PlayerHP"] == null || (int)teamTmp[a].customProperties["PlayerHP"] < 1)
            //    {

            //        // GUILayout.Label("ÖLÜ");

            //    }
            //    birincitakım_İSİM[a].text = teamTmp[a].name;
            //    birincitakım_ÖLDÜRME[a].text = ((int)teamTmp[a].customProperties["Kills"]).ToString();
            //    birincitakım_ÖLÜM[a].text = ((int)teamTmp[a].customProperties["Deaths"]).ToString();
            //    birincitakım_PİNG[a].text = ((int)teamTmp[a].customProperties["Ping"]).ToString();
            //}

            GUI.color = Color.white;
				for(int i = 0; i < teamTmp.Count; i ++)
                {
					GUI.color = teamTmp[i] == PhotonNetwork.player ? Color.white : GameSettings.otherPlayerGUIBoxColor;
					GUILayout.BeginVertical("box", GUILayout.Width(fieldWidth));
					
					//Display player name
					GUILayout.BeginHorizontal();
						if(teamTmp[i].customProperties["PlayerHP"] == null || (int)teamTmp[i].customProperties["PlayerHP"] < 1)
                        {
							GUI.color = GameSettings.customRedColor;
							GUILayout.Label("*");
							GUILayout.Space(5);
						}
						
						GUI.color = (int)teamTmp[i].customProperties["Team"] == 1 ? GameSettings.teamAColor : GameSettings.teamBColor;
						GUILayout.Label(teamTmp[i].name);
                //birincitakımbilgi[i].text = teamTmp[i].name;
                

                GUILayout.EndHorizontal();
					
					GUILayout.Space(5);
					
					GUI.color = Color.white;
					
					GUILayout.BeginHorizontal();
					GUILayout.Label("Ping: " + ((int)teamTmp[i].customProperties["Ping"]).ToString(), GUILayout.Width(125));
					GUILayout.FlexibleSpace();
					GUILayout.Label(((int)teamTmp[i].customProperties["Kills"]).ToString(), GUILayout.Width(75));
					GUILayout.Label(((int)teamTmp[i].customProperties["Deaths"]).ToString(), GUILayout.Width(75));
					GUILayout.EndHorizontal();
					GUILayout.EndVertical();
                //Debug.Log(teamTmp[i].name);
                //Debug.Log(((int)teamTmp[i].customProperties["Kills"]));
                //Debug.Log(((int)teamTmp[i].customProperties["Deaths"]));
                //Debug.Log(((int)teamTmp[i].customProperties["Ping"]));

                ////birincitakım_İSİM1.text = teamTmp[i].name;
                //birincitakım_ÖLDÜRME1.text = ((int)teamTmp[i].customProperties["Kills"]).ToString();
                //birincitakım_ÖLÜM1.text = ((int)teamTmp[i].customProperties["Deaths"]).ToString();
                //birincitakım_PİNG1.text = ((int)teamTmp[i].customProperties["Ping"]).ToString();
            }
        }
		GUILayout.EndVertical();
	}
    void DisplayTeamPlayerss(List<PhotonPlayer> teamTmp, int fieldWidth)
    {
        oyuncusayı_b = teamTmp.Count;
        GUILayout.BeginVertical();
        if (teamTmp.Count == 0)
        {
            GUI.color = new Color(1, 1, 1, 0.5f);
            GUILayout.BeginVertical("box", GUILayout.Width(fieldWidth));
            GUILayout.Label("Boş");
            GUILayout.Space(5);
            GUILayout.Label("...");
            GUILayout.EndVertical();
        }
        else
        {
            //for (int i = 0; i < oyuncusayı_b; i++)
            //{
            //    if (teamTmp[i].customProperties["PlayerHP"] == null || (int)teamTmp[i].customProperties["PlayerHP"] < 1)
            //    {

            //        //GUILayout.Label("ÖLÜ");

            //    }
            //    ikincitakım_İSİM[i].text = teamTmp[i].name;
            //    ikincitakım_ÖLDÜRME[i].text = ((int)teamTmp[i].customProperties["Kills"]).ToString();
            //    ikincitakım_ÖLÜM[i].text = ((int)teamTmp[i].customProperties["Deaths"]).ToString();
            //    ikincitakım_PİNG[i].text = ((int)teamTmp[i].customProperties["Ping"]).ToString();
            //}

            GUI.color = Color.white;
            for (int i = 0; i < teamTmp.Count; i++)
            {
                GUI.color = teamTmp[i] == PhotonNetwork.player ? Color.white : GameSettings.otherPlayerGUIBoxColor;
                GUILayout.BeginVertical("box", GUILayout.Width(fieldWidth));

                //Display player name
                GUILayout.BeginHorizontal();
                if (teamTmp[i].customProperties["PlayerHP"] == null || (int)teamTmp[i].customProperties["PlayerHP"] < 1)
                {
                    GUI.color = GameSettings.customRedColor;
                    GUILayout.Label("*");
                    GUILayout.Space(5);
                }

                GUI.color = (int)teamTmp[i].customProperties["Team"] == 1 ? GameSettings.teamAColor : GameSettings.teamBColor;
                GUILayout.Label(teamTmp[i].name);
                
                //ikincitakımbilgi[i].text = teamTmp[i].name;
                GUILayout.EndHorizontal();

                GUILayout.Space(5);

                GUI.color = Color.white;

                GUILayout.BeginHorizontal();
                GUILayout.Label("Ping: " + ((int)teamTmp[i].customProperties["Ping"]).ToString(), GUILayout.Width(125));
                GUILayout.FlexibleSpace();
                GUILayout.Label(((int)teamTmp[i].customProperties["Kills"]).ToString(), GUILayout.Width(75));
                GUILayout.Label(((int)teamTmp[i].customProperties["Deaths"]).ToString(), GUILayout.Width(75));
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
                //ikincitakım_İSİM[i].text = teamTmp[i].name;
                //ikincitakım_ÖLDÜRME[i].text = ((int)teamTmp[i].customProperties["Kills"]).ToString();
                //ikincitakım_ÖLÜM[i].text = ((int)teamTmp[i].customProperties["Deaths"]).ToString();
                //ikincitakım_PİNG[i].text = ((int)teamTmp[i].customProperties["Ping"]).ToString();
            }
        }
        GUILayout.EndVertical();
    }
}
