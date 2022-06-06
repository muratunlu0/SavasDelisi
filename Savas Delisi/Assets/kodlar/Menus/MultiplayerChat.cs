
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
public class MultiplayerChat : Photon.MonoBehaviour
{

	public enum ChatState { None, Public, Team }
	[HideInInspector]
	public ChatState chatState = ChatState.None;

	[System.Serializable]
	public class Message
    {
		public string senderName;
		public Color senderTeamColor;
		public string text;
		public string isTeamChat;
		public float timer;

		public Message (string sn, Color stc, string txt, string itc, float t)
        {
			senderName = sn;
			senderTeamColor = stc;
			text = txt;
			isTeamChat = itc;
			timer = t;
		}
	}

	[HideInInspector]
	public List<Message> messages = new List<Message>();

	string chatLabel = "Say:";
	string chatInput = "";

	float chatLabelWidth;
    
    RoomController rc;
     
	// Use this for initialization
	void Start ()
    {
		rc = GetComponent<RoomController>();
		chatState = ChatState.None;
        GameSettings.updateChatMessages = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
		for(int i = 0; i < messages.Count; i++)
        {
			if(messages[i].timer > 0)
            {
				messages[i].timer -= Time.deltaTime;
            }
            else
            {
                if(messages[i].timer != -1)
                {
                    messages[i].timer = -1;
                    GameSettings.updateChatMessages = true;
                }
            }
		}

        /*if (chatState == ChatState.None)
        {
            //Open chat
            if (!rc.showOptions)
            {
                if (Input.GetKeyDown(GameSettings.playerKeys[15]))
                {
                    //Public chat
                    chatState = ChatState.Public;
                }

                if (Input.GetKeyDown(GameSettings.playerKeys[16]))
                {
                    //Team chat
                    chatState = ChatState.Team;
                }

                GameSettings.updateChatMessages = true;
            }
        }*/
    }

	void OnGUI ()
    {
		GUI.skin = GameSettings.guiSkin;

#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WP8 && !UNITY_WP8_1
        //Show fps
        if (rc.showFPS)
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(6, 6, 150, 25), GameSettings.currentFPS.ToString("F2") + "FPS");
            GUI.color = GameSettings.HUDColor;
            GUI.Label(new Rect(5, 5, 150, 25), GameSettings.currentFPS.ToString("F2") + "FPS");
        }
#endif

        if (chatState == ChatState.None)
        {

#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WP8 && !UNITY_WP8_1
            GUI.color = GameSettings.textShadowColor;
            GUI.Label(new Rect(10 + 1, Screen.height - 275 + 1, 250, 25), "Press '" + GameSettings.playerKeys[15].ToString() + "' or '" + GameSettings.playerKeys[16].ToString() + "' to chat");
            GUI.color = GameSettings.HUDColor;
            GUI.Label(new Rect(10, Screen.height - 275, 250, 25), "Press '" + GameSettings.playerKeys[15].ToString() + "' or '" + GameSettings.playerKeys[16].ToString() + "' to chat");
#endif

            //Open chat
            if (!GameSettings.menuOpened)
            {

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1

                //Open chat on mobile
                //GUI.color = new Color(1, 1, 1, 0.91f);
                //if (GUI.Button(new Rect(5, 5, 100, 30), "Chat"))
                //  {

#else

                //Open chat
                if (Event.current.type == EventType.keyDown && Event.current.keyCode == GameSettings.playerKeys[15])
                { 

#endif

                //               chatState = ChatState.Public;
                //chatLabel = "Say:";
                //chatLabelWidth = GameSettings.guiSkin.label.CalcSize(new GUIContent(chatLabel)).x + 5;
                //StartCoroutine(ClearChat());
                //}

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1

                //Open team chat on mobile
                //if (GUI.Button(new Rect(110, 5, 100, 30), "Team Chat"))
                //{

#else

                //Open team chat
                if (Event.current.type == EventType.keyDown && Event.current.keyCode == GameSettings.playerKeys[16])
                { 

#endif

                //               chatState = ChatState.Team;
                //chatLabel = "Say [TEAM]:";
                //chatLabelWidth = GameSettings.guiSkin.label.CalcSize(new GUIContent(chatLabel)).x + 5;
                //StartCoroutine(ClearChat());
                //           }



#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1

                //Open team chat on mobile
                GUI.color = new Color(4, 0, 0, 0.30f);
                //if (GUI.Button(new Rect(580, 5, 120, 40), "            "))//215,5
                //{
                //    rc.ShowScoreboard();
                //}
            }
#else
            }
            else
            {
                //Show fullscreen button
                GUI.color = Color.white;
                if (GUI.Button(new Rect(Screen.width - 105, 5, 100, 20), "Fullscreen"))
                {
                    rc.os.SetFullscreen();
                }
            }
#endif

        }
        else
        {

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1

            //Send chat on mobile
            GUI.color = new Color(1, 1, 1, 0.91f);
            if (GUI.Button(new Rect(10 + chatLabelWidth + 405, Screen.height - 275, 50, 25), "Send"))
            {

#else

            //Send chat
            if (Event.current.type == EventType.keyDown && Event.current.keyCode == KeyCode.Return)
            { 

#endif

                GUI.FocusControl("");
				SendChat(PhotonNetwork.playerName, chatInput, rc.ourTeam, chatState == ChatState.Team);
				StartCoroutine(ClearChat());
				chatState = ChatState.None;
			}

            GUI.color = GameSettings.HUDColor;

            GUI.SetNextControlName("ChatField");
			GUI.Label(new Rect(10, Screen.height - 275, 150, 25), chatLabel);
			chatInput = GUI.TextField(new Rect(10 + chatLabelWidth, Screen.height - 275, 400, 25), chatInput, 60);
			GUI.FocusControl("ChatField");
        }

        //Messages are displayed at RoomUI.cs

        //Show winning screens
        GUI.color = Color.white;

        //Show final screen
        
        if (rc.currentGameMode == "TDM")
        {
            //GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(true);
            
            //////////////////////////////////////////
            

            if (rc.currentGameStatus == 1)
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().seçeneklercanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().scoreboardcanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().takımsecmecanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().cashtextt.text = " ";
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyuniçitextparası = 0;
                GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazananisim.text = GameSettings.teamAName + " kazandı";
                Debug.Log(GameSettings.teamAName);
                GameObject.Find("asdf").GetComponent<Scoreboard>().kazanantakım_ismi.text = GameSettings.teamAName;
                Debug.Log(GameObject.Find("asdf").GetComponent<Scoreboard>().kazanantakım_ismi.text + "    kazanantakım");
                
                if (GameObject.Find("asdf").GetComponent<Scoreboard>().kupa_aldımı == 0)
                {
                    PlayerPrefs.SetInt("oynanan_macsayısı", PlayerPrefs.GetInt("oynanan_macsayısı") + 1);
                    if (GameObject.Find("asdf").GetComponent<Scoreboard>().kazanantakım_ismi.text == GameObject.Find("asdf").GetComponent<Scoreboard>().hangitakımda.text)
                    {
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kazandınobjesi.SetActive(true);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kaybettinobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().berabereobjesi.SetActive(false);                       
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı = PlayerPrefs.GetInt("kupasayısı");
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı += 10;
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kupasayısiy.text = "+10";
                      
                        Debug.Log(GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı + "    kupa sayısı");
                        PlayerPrefs.SetInt("TOPLAMSİMSEK", GameObject.Find("asdf").GetComponent<oyunmagaza>().toplamşimşek);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().cashtextt.text = (GameSettings.cnst - rc.totalCash +500).ToString();
                        PlayerPrefs.SetInt("kupasayısı", GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı);
                        
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = ((double)PlayerPrefs.GetInt("öldürmesayımpref") / (double)PlayerPrefs.GetInt("ölmesayımpref")) - ((double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_öldürme_kd / (double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = Math.Round(GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici, 3);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdoraniy.text = GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici.ToString();
                        
                        PlayerPrefs.SetInt("kazandıgımaçsayısıı", PlayerPrefs.GetInt("kazandıgımaçsayısıı") + 1);
                        
                        
                       
                    }
                    else if (GameObject.Find("asdf").GetComponent<Scoreboard>().kazanantakım_ismi.text != GameObject.Find("asdf").GetComponent<Scoreboard>().hangitakımda.text)
                    {
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kazandınobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kaybettinobjesi.SetActive(true);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().berabereobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı = PlayerPrefs.GetInt("kupasayısı");
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı -= 10;
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kupasayısiy.text = "-10";
                       
                        Debug.Log(GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı + "    kupa sayısı");
                        PlayerPrefs.SetInt("kupasayısı", GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı);
                        
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = ((double)PlayerPrefs.GetInt("öldürmesayımpref") / (double)PlayerPrefs.GetInt("ölmesayımpref")) - ((double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_öldürme_kd / (double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = Math.Round(GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici, 3);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdoraniy.text = GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici.ToString();
                       
                        PlayerPrefs.SetInt("kaybettigimaçsayısıı", PlayerPrefs.GetInt("kaybettigimaçsayısıı") + 1);
                        
                        
                    }
                    if (PlayerPrefs.GetInt("no_ads1") == 0)
                    {
                        GameObject.Find("reklam").GetComponent<reklamscripti>().ShowAd();
                    }
                    GameObject.Find("asdf").GetComponent<Scoreboard>().kupa_aldımı = 1;
                    GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(true);
                   
                }
                
                

            }

            if (rc.currentGameStatus == 2)
            {

                GameObject.Find("asdf").GetComponent<Scoreboard>().seçeneklercanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().scoreboardcanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().takımsecmecanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().cashtextt.text = " ";
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyuniçitextparası = 0;
                GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(false);



                GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazananisim.text = GameSettings.teamBName + " kazandı";
                GameObject.Find("asdf").GetComponent<Scoreboard>().kazanantakım_ismi.text = GameSettings.teamBName;
                Debug.Log(GameObject.Find("asdf").GetComponent<Scoreboard>().kazanantakım_ismi.text + "    kazanantakım ismi");
                
                if (GameObject.Find("asdf").GetComponent<Scoreboard>().kupa_aldımı == 0)
                {

                    PlayerPrefs.SetInt("oynanan_macsayısı", PlayerPrefs.GetInt("oynanan_macsayısı") + 1);

                    if (GameObject.Find("asdf").GetComponent<Scoreboard>().kazanantakım_ismi.text == GameObject.Find("asdf").GetComponent<Scoreboard>().hangitakımda.text)
                    {
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kazandınobjesi.SetActive(true);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kaybettinobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().berabereobjesi.SetActive(false);
                        // GameObject.Find("asdf").GetComponent<oyunmagaza>().toplamşimşek += 500;
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı = PlayerPrefs.GetInt("kupasayısı");
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı += 10;
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kupasayısiy.text = "+10";
                        
                        Debug.Log(GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı + "    kupa sayısı");
                        PlayerPrefs.SetInt("TOPLAMSİMSEK", GameObject.Find("asdf").GetComponent<oyunmagaza>().toplamşimşek);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().cashtextt.text = (GameSettings.cnst - rc.totalCash + 500).ToString();
                        PlayerPrefs.SetInt("kupasayısı", GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı);
                        
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = ((double)PlayerPrefs.GetInt("öldürmesayımpref") / (double)PlayerPrefs.GetInt("ölmesayımpref")) - ((double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_öldürme_kd / (double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = Math.Round(GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici, 3);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdoraniy.text = GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici.ToString();
                        PlayerPrefs.SetInt("kazandıgımaçsayısıı", PlayerPrefs.GetInt("kazandıgımaçsayısıı") + 1);
                        
                        
                    }
                    else if (GameObject.Find("asdf").GetComponent<Scoreboard>().kazanantakım_ismi.text != GameObject.Find("asdf").GetComponent<Scoreboard>().hangitakımda.text)
                    {
                        if(0==GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd)
                        {
                            GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd += 1;
                        }
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kazandınobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kaybettinobjesi.SetActive(true);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().berabereobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı = PlayerPrefs.GetInt("kupasayısı");
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı -= 10;
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kupasayısiy.text = "-10";
                        
                        Debug.Log(GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı + "    kupa sayısı");
                        PlayerPrefs.SetInt("kupasayısı", GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = ((double)PlayerPrefs.GetInt("öldürmesayımpref") / (double)PlayerPrefs.GetInt("ölmesayımpref")) - ((double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_öldürme_kd / (double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = Math.Round(GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici, 3);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdoraniy.text = GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici.ToString();
                        PlayerPrefs.SetInt("kaybettigimaçsayısıı", PlayerPrefs.GetInt("kaybettigimaçsayısıı") + 1);
                        
                        
                    }
                    if (PlayerPrefs.GetInt("no_ads1") == 0)
                    {
                        GameObject.Find("reklam").GetComponent<reklamscripti>().ShowAd();
                    }
                    GameObject.Find("asdf").GetComponent<Scoreboard>().kupa_aldımı = 1;
                    
                }
                GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(true);
            }

            if (rc.currentGameStatus == 3)
            {
               
                

                if (PlayerPrefs.GetInt("beraberesonuc")==0)
                {
                    if (rc.teamAScore != 0)
                    {
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = ((double)PlayerPrefs.GetInt("öldürmesayımpref") / (double)PlayerPrefs.GetInt("ölmesayımpref")) - ((double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_öldürme_kd / (double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = Math.Round(GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici, 3);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdoraniy.text = GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici.ToString();
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kupasayısiy.text = "0";                        
                    }

                    else if (rc.teamAScore == 0)
                    {
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kupasayısiy.text = "0";
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdoraniy.text = "0";
                    }
                    GameObject.Find("asdf").GetComponent<Scoreboard>().kazandınobjesi.SetActive(false);
                    GameObject.Find("asdf").GetComponent<Scoreboard>().kaybettinobjesi.SetActive(false);
                    GameObject.Find("asdf").GetComponent<Scoreboard>().berabereobjesi.SetActive(true);
                    PlayerPrefs.SetInt("oynanan_macsayısı", PlayerPrefs.GetInt("oynanan_macsayısı") + 1);
                    PlayerPrefs.SetInt("berabere_macsayısı", PlayerPrefs.GetInt("berabere_macsayısı") + 1);
                    
                    if (PlayerPrefs.GetInt("no_ads1") == 0)
                    {
                        GameObject.Find("reklam").GetComponent<reklamscripti>().ShowAd();
                    }
                    PlayerPrefs.SetInt("beraberesonuc", 1);
                    
                }
                GameObject.Find("asdf").GetComponent<Scoreboard>().seçeneklercanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().scoreboardcanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().takımsecmecanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().cashtextt.text = " ";
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyuniçitextparası = 0;
                GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(true);
            }
            
        }

        if (rc.currentGameMode == "FFA")
        {
            
            if (rc.currentGameStatus == 3)
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().cashtextt.text = " ";
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyuniçitextparası = 0;
                GameObject.Find("asdf").GetComponent<Scoreboard>().takımsecmecanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().seçeneklercanvası.SetActive(false);
                GameObject.Find("asdf").GetComponent<Scoreboard>().scoreboardcanvası.SetActive(false);
                
                //GUI.color = rc.winningPlayer == null ? GameSettings.drawColor : GameSettings.teamAColor;
                //GUI.Label(new Rect(Screen.width / 2 - 175, Screen.height / 2 - 35, 350, 70), (rc.winningPlayer == null ? "No winning player" : rc.winningPlayer.name + " Won") + "\n\n" + ((int)rc.currentRoundTime).ToString(), GameSettings.finalScreenStyle);



               
                GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazananisim.text = rc.winningPlayer.name + " kazandı";
                if (GameObject.Find("asdf").GetComponent<Scoreboard>().kupa_aldımı == 0)
                {
                    PlayerPrefs.SetInt("oynanan_macsayısı", PlayerPrefs.GetInt("oynanan_macsayısı") + 1);
                    if (rc.winningPlayer.name == PhotonNetwork.player.name)
                    {
                       
                        PlayerPrefs.SetInt("kazandıgımaçsayısıı", PlayerPrefs.GetInt("kazandıgımaçsayısıı") + 1);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kazandınobjesi.SetActive(true);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kaybettinobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().berabereobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı=PlayerPrefs.GetInt("kupasayısı");
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı += 15;
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kupasayısiy.text = "+15";
                        
                        PlayerPrefs.SetInt("kupasayısı", GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı);
                       
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = ((double)PlayerPrefs.GetInt("öldürmesayımpref") / (double)PlayerPrefs.GetInt("ölmesayımpref")) - ((double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_öldürme_kd / (double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = Math.Round(GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici, 3);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdoraniy.text = GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici.ToString();
                       
                        
                    }
                    else if (rc.winningPlayer.name != PhotonNetwork.player.name)
                    {
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kazandınobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kaybettinobjesi.SetActive(true);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().berabereobjesi.SetActive(false);
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı = PlayerPrefs.GetInt("kupasayısı");
                        
                        PlayerPrefs.SetInt("kaybettigimaçsayısıı", PlayerPrefs.GetInt("kaybettigimaçsayısıı") + 1);
                        GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı -= 10;
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kupasayısiy.text = "-10";
                        
                        PlayerPrefs.SetInt("kupasayısı", GameObject.Find("asdf").GetComponent<oyunmagaza>().kupasayısı);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = ((double)PlayerPrefs.GetInt("öldürmesayımpref") / (double)PlayerPrefs.GetInt("ölmesayımpref")) - ((double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_öldürme_kd / (double)GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici = Math.Round(GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici, 3);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().kdoraniy.text = GameObject.Find("asdf").GetComponent<Scoreboard>().kdgeçici.ToString();
                        
                        
                    }
                    if (PlayerPrefs.GetInt("no_ads1") == 0)
                    {
                        GameObject.Find("reklam").GetComponent<reklamscripti>().ShowAd();
                    }
                    GameObject.Find("asdf").GetComponent<Scoreboard>().kupa_aldımı = 1;
                    
                }
                GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(true);
                
            }
        }
	}
    public void ffakanvaskapama()
    {
        GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
    }
	void SendChat ( string ourName, string text, int team, bool isTeamChat)
    {
		if(text.Replace(" ", "") != "")
        {
			photonView.RPC ("SendChatRemote", PhotonTargets.All, ourName, text, team, isTeamChat);
		}
	}

	[PunRPC]
    void SendChatRemote (string senderName, string text,  int senderTeam, bool isTeamChat)
    {
		if(!isTeamChat || rc.ourTeam == senderTeam)
        {
			Color senderTeamColor = GameSettings.HUDColor;
			if(senderTeam == 1 || senderTeam == 2)
            {
				senderTeamColor = senderTeam == 1 ? GameSettings.teamAColor : GameSettings.teamBColor;
			}

            //Remote rich text tags
            string nonRichTextString = Regex.Replace(text, "<.*?>", string.Empty);

            messages.Insert(0, new Message(senderName, senderTeamColor, nonRichTextString, isTeamChat ? "[T]" : "", 25));

			if(messages.Count > GameSettings.chatMessagesLimit)
            {
				messages.RemoveAt(messages.Count - 1);
			}

            GameSettings.updateChatMessages = true;
		}
	}

	IEnumerator ClearChat ()
    {
        GameSettings.updateChatMessages = true;
        yield return new WaitForEndOfFrame();
		chatInput = "";
	}
}
