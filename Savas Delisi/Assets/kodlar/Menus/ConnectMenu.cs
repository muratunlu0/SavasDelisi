using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable; //Replace default Hashtables with Photon hashtables
using System;
public class ConnectMenu : Photon.MonoBehaviour
{



    //This is main menu where users will browse and create new rooms
    //public Text isimuyarıyazısı;
    // public GameObject isimuyarıpaneli;
    //public GameObject isimdegistirpaneli;
    public GameObject yenileniyor_paneli;
    public GameObject odadolupaneli;
    public GameObject katılbutonu;
    public GameObject maksimumodauyarı;
    public GameObject harita1;
    public GameObject harita2;
    public GameObject harita3;
    public GameObject harita4;
    
    public GameObject harita1oyunkur;
    public GameObject harita2oyunkur;
    public GameObject harita3oyunkur;
    public GameObject harita4oyunkur;

    public GameObject savaşpaneli;
    public Text çevrimiçi_oyuncu_sayısıyazisi;
    public int çevrimiçi_oyuncu_sayısı;
    public Text aktifodasayısı;
    public Text kullanici_ismi;
    public Text kullanici_ismi_ANAMENU;
   
    public Text kullanici_ismibuyuk;
    public Text profilmenusu_kullaniciismi;
    public Text kacodabulundugu;
    public GameObject[] satırlar;
    public GameObject yükleniyorpaneli;
    public Text harita_ismibaslıgı;
    public Text oyun_modubaslıgı;
    public GameObject kacodaolduguobjesi;
    public Text uyarılabeli;
    public Text[] odaadıbilgisi;
    public Text[] oyuncusayısıbilgisi;
    public Text[] haritabilgisi;
    public Text[] oyunmodubilgisi;
    public Text[] pingbilgisi;
    public GameObject[] odasecildi;
    public Scrollbar odalarscroll;
    public Text süre;
    // public GameObject prefabgameobje;
    public enum CurrentWindow { Main, PlayNow, Options, Profile, None }
	[HideInInspector]
	public CurrentWindow currentWindow = CurrentWindow.Main;

	public GameObject roomControllerPrefab;
	public GameObject buySpotPrefab;
	public Texture2D background;
	public Texture2D defaultMapPreview;
    public int artısmiktarı;
	[System.Serializable]
	public class AvailableMaps
    {
		public string mapName;
		public Texture mapPreview;
	}

	public AvailableMaps[] availableMaps;

	Vector2 browseWindowSize = new Vector2(750, 500);
	Vector2 roomBrowserScroll = Vector2.zero;
	

	//Networking 
	CloudRegionCode selectedRegion = CloudRegionCode.eu; //Photon Cloud Region we will connect to
	int totalPlayers = 0;
	int totalRooms = 0;

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
    //Network version for Mobile
    string networkVersion = "v2.5";
#else
    //Network version for Desktop
    string networkVersion = "v1.7.5";
#endif


    bool createRoom = false;
	string roomName = "";

	//Available room creation settings
	int[] playerLimits = { 2, 4, 6, 8, 10, 12, 14, 16 };
	string[] gameModes = { "TDM", "FFA" };
	int[] roundDurations = { 300, 300 }; //Each field must correspond to gameMode index, time in seconds
	int[] killLimits = { 25, 50, 75, 100 };

    public Text oyunculimiti;
    public Text odaismi;
    public GameObject oyunkur_odaisim;
    int selectedPlayerLimit = 0;
	int selectedGameMode = 0;
	int selectedMap = 0;
	int selectedKillLimit = 1;

	bool loadingMap = false;
	bool refreshingRooms = false;
	int roomFieldCurrentlyHovering = -1;
	int selectedRoom = -1;
	int currentPing = -1;
    int kacodavarbulma =0;
    int odasıralasayısı;
	RoomInfo[] availableRooms = new RoomInfo[0];

	public string playerName = "";
	string tmpPlayerName;

	OptionsSettings os;

	public string playerNamePrefsName = "PlayerName";
	string playerLimitPrefsName = "SelectedPlayerLimit";
	string gameModePrefsName = "SelectedGameMode";
	string selectedMapPrefsName = "SelectedMap";

	// Use this for initialization
	void Start ()
    {
        
        Invoke("yenile", 0.5f);
        //if (PhotonNetwork.connectionState != ConnectionState.InitializingApplication && PhotonNetwork.connectionState != ConnectionState.Connecting &&
        //       PhotonNetwork.connectionState != ConnectionState.Disconnecting && PhotonNetwork.connectionState != ConnectionState.Connected)
        //{
        //    Debug.Log("buldum amk");
        //    BAGLANTIHATASIPANELİ.SetActive(true);
        //    PhotonNetwork.ConnectUsingSettings(networkVersion);
        //}

        selectedMap = -1;
        selectedGameMode = -1;
        selectedRoom = -1;
        PlayerPrefs.SetInt("oyunmod_", selectedGameMode);
        //Limit game Frames PEr Seconds
        DontDestroyOnLoad(gameObject);

		currentWindow = CurrentWindow.Main;

		os = GetComponent<OptionsSettings>();
		os.cm = this;


		if(GameSettings.errorText != "")
        {
			loadingMap = true;
		}

		PhotonNetwork.PhotonServerSettings.PreferredRegion = selectedRegion;

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
        //Enable connection timeout on mobile
        PhotonNetwork.BackgroundTimeout = 45;
#endif

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

        roomName = "OYUN ODASI " ;             //Random.Range(11111, 99999).ToString();
       
        oyunkur_odaisim.GetComponent<InputField>().text = "ODA " + UnityEngine.Random.Range(111, 999).ToString();

        //Load stored values if there any 
        //if (PlayerPrefs.GetInt("ilkisim") == 0)
        //{
        //    playerName = "Oyuncu " + UnityEngine.Random.Range(1111, 9999).ToString();//"OYUNCU ";//+ Random.Range(1111, 9999).ToString();
        //    kullanici_ismi.text = playerName;
        //    kullanici_ismikaydet();
        //    PlayerPrefs.SetInt("ilkisim", 1);
        //}

        if (PlayerPrefs.HasKey(playerNamePrefsName))
        {
            playerName = PlayerPrefs.GetString(playerNamePrefsName);
            kullanici_ismi.text = PlayerPrefs.GetString(playerNamePrefsName);
            kullanici_ismi_ANAMENU.text= PlayerPrefs.GetString(playerNamePrefsName);
            kullanici_ismibuyuk.text = PlayerPrefs.GetString(playerNamePrefsName);
            profilmenusu_kullaniciismi.text = kullanici_ismibuyuk.text;

            GameObject.Find("firebase-message").GetComponent<databasee>().email= kullanici_ismi.text;
           
        }
        GameObject.Find("magazascripti").GetComponent<oyunmagaza>().oyunculimitiayar = PlayerPrefs.GetInt("limitayar");
        if (GameObject.Find("magazascripti").GetComponent<oyunmagaza>().oyunculimitiayar == 0)
        {
            PlayerPrefs.SetInt(playerLimitPrefsName, 10);
            GameObject.Find("magazascripti").GetComponent<oyunmagaza>().oyunculimitiayar = 1;
            
        }
        PlayerPrefs.SetInt("limitayar", GameObject.Find("magazascripti").GetComponent<oyunmagaza>().oyunculimitiayar);
        //
        selectedPlayerLimit = PlayerPrefs.HasKey(playerLimitPrefsName) ? PlayerPrefs.GetInt(playerLimitPrefsName) : (int)playerLimits.Length/2;
		selectedPlayerLimit = selectedPlayerLimit > -1 && selectedPlayerLimit < playerLimits.Length ? selectedPlayerLimit : (int)playerLimits.Length/2;
        //selectedPlayerLimit = 7;

        oyunculimiti.text = Convert.ToString(selectedPlayerLimit* 2 +2);
        

        selectedGameMode = PlayerPrefs.HasKey(gameModePrefsName) ? PlayerPrefs.GetInt(gameModePrefsName) : 0;
		selectedGameMode = selectedGameMode > -1 && selectedGameMode < gameModes.Length ? selectedGameMode : 0;

		selectedMap = PlayerPrefs.HasKey(selectedMapPrefsName) ? PlayerPrefs.GetInt(selectedMapPrefsName) : 0;
		selectedMap = selectedMap > -1 && selectedMap < availableMaps.Length ? selectedMap : 0;
        //if (PlayerPrefs.GetInt("isimdegistirrr") == 0)
        //{
        //   // isimdegistirpaneli.SetActive(true);
            
        //}

#if (UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1) && !UNITY_EDITOR
        //Limit screen resolution on Android
        int highestVal = 1000; //Width or height cant be more than this value
		if(Screen.width == Screen.height)
        {
			Screen.SetResolution(highestVal, highestVal, true);
		}
        else
        {
			if(Screen.width > Screen.height)
            {
				float aspectRatio = (float)Screen.height/(float)Screen.width;
				Screen.SetResolution(highestVal, (int)(highestVal *  aspectRatio), true);
			}
            else
            {
				float aspectRatio = (float)Screen.width/(float)Screen.height;
				Screen.SetResolution((int)(highestVal *  aspectRatio), highestVal, true);
			}
		}

		//Also limit framerate
		Application.targetFrameRate = 60;
#else
        Application.targetFrameRate = 150;
        //AudioListener.volume = 0.5f;
#endif
        
    }
	
	// Update is called once per frame
	void Update ()
    {
       
        os.enabled = currentWindow == CurrentWindow.Options;
        //if (!PhotonNetwork.connected)
        //{
        //    if (PhotonNetwork.connectionState != ConnectionState.InitializingApplication && PhotonNetwork.connectionState != ConnectionState.Connecting &&
        //           PhotonNetwork.connectionState != ConnectionState.Disconnecting && PhotonNetwork.connectionState != ConnectionState.Connected)
        //    {

                
        //        PhotonNetwork.ConnectUsingSettings(networkVersion);
        //        //if(savaşpaneli.active==true)
        //        //{
        //        //    BAGLANTIHATASIPANELİ.SetActive(true);
        //        //}
        //        //else if (savaşpaneli.active == false)
        //        //{
        //        //    BAGLANTIHATASIPANELİ.SetActive(false);
        //        //}

        //    }

        //}
        //else
        //{
        //   // BAGLANTIHATASIPANELİ.SetActive(false);
        //}
        if(odalarscroll.size<0.1800f)
        {
            yenile();
            
        }
    }

	void OnGUI ()
    {
		GUI.skin = GameSettings.guiSkin;

		//GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);

		//GUI.Label(new Rect(15, Screen.height - 35, 300, 30), buildVersion);

		if(loadingMap)
        {
			if(GameSettings.errorText == "")
            {
				GUI.Label(new Rect(Screen.width/2 - 150, Screen.height/2 - 15, 300, 30), "Baglanıyor,lütfen bekleyin...", GameSettings.createRoomOptionsStyle);
			}
            else
            {
				GUI.color = Color.red;
				GUI.Label(new Rect(Screen.width/2 - 150, Screen.height/2 - 15, 700, 30), GameSettings.errorText, GameSettings.createRoomOptionsStyle);
			}

			GUI.color = Color.white;
			//if(SceneManager.GetActiveScene().buildIndex == 0)
   //         {
			//	if(GUI.Button(new Rect(Screen.width - 265, Screen.height - 45, 250, 30), "ANA MENU"))
   //             {
			//		this.StopAllCoroutines();
			//		if(PhotonNetwork.room != null)
   //                 {
			//			PhotonNetwork.LeaveRoom();
			//		}
			//		GameSettings.errorText = "";
			//		loadingMap = false;
			//	}
			//}
		}
        else
        {
			//Initial Screens
			if(currentWindow == CurrentWindow.Main)
            {
                //GUI.Label(new Rect(Screen.width/2 - 150, Screen.height/2 - 160, 300, 100), "FPS Kit 3.0", GameSettings.headerStyle);

                //if(GUI.Button(new Rect(Screen.width/2 - 125, Screen.height/2 - 45, 250, 30), "oyna"))
                //            {
                //	currentWindow = CurrentWindow.PlayNow;
                //	createRoom = false;

                //	if(availableRooms.Length == 0)
                //                {
                //		this.StopAllCoroutines();
                //		this.StartCoroutine(RefreshRooms());
                //	}
                //}

                //if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 30), "ayarlar"))
                //{
                //    currentWindow = CurrentWindow.Options;
                //}

                //if (GUI.Button(new Rect(Screen.width / 2 - 125, Screen.height / 2 + 45, 250, 30), "Profil"))
                //{
                //    currentWindow = CurrentWindow.Profile;
                    tmpPlayerName = playerName;
                //}
            }

			if(currentWindow == CurrentWindow.PlayNow)
            {
				GUI.Window (0, new Rect(Screen.width/2 - browseWindowSize.x/2, Screen.height/2 - browseWindowSize.y/2,  browseWindowSize.x, browseWindowSize.y), BrowseRoomsWindow, "");
			}

			if(currentWindow == CurrentWindow.Options)
            {

			}

			if(currentWindow == CurrentWindow.Profile)
            {
				GUI.Window (0, new Rect(Screen.width/2 - 150, Screen.height/2 - 100,  300, 200), ProfileWindow, "");
			}

#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WP8 && !UNITY_WP8_1
			if(GUI.Button(new Rect(Screen.width - 105, 5, 100, 20), "Fullscreen"))
            {
				os.SetFullscreen();
			}
#endif

            //Replace button name nad url with your own site and uncomment it so players can visit your site from game
            /*if(GUI.Button(new Rect(5, 5, 150, 20), "nsdevstore.com"))
            {
				Application.OpenURL("http://nsdevstore.com/");
			}*/
        }
    }
    public void oyna()
    {
        currentWindow = CurrentWindow.PlayNow;
        createRoom = false;

        if (availableRooms.Length == 0)
        {
            this.StopAllCoroutines();
            this.StartCoroutine(RefreshRooms());
        }
    }
	void ProfileWindow(int windowID)
    {
		GUI.Label(new Rect(15, 0, 300, 35), "oyuncu profili");

		if(GUI.Button(new Rect(300 - 30, 5, 25, 25), "", GameSettings.closeButtonStyle))
        {
			currentWindow = CurrentWindow.Main;

			if(!PlayerPrefs.HasKey(playerNamePrefsName))
            {
				//PlayerPrefs.SetString(playerNamePrefsName, kullanici_ismi.text);
			}
		}

		//GUI.Label(new Rect(15, 40, 300, 30), "oyuncu ismi");
		//tmpPlayerName = GUI.TextField(new Rect(15, 70, 300 - 30, 25), tmpPlayerName, 19);

		//ToDO remove this before final build
		string tmpNameCheck = (tmpPlayerName.ToLower()).Replace(" ", "");

		GUI.enabled = tmpNameCheck.Length > 0;

		if(GUI.Button(new Rect(160, 160, 125, 25), "Kaydet"))
        {
			playerName = tmpPlayerName;
          //  playerName = kullanici_ismi.text;

           // PlayerPrefs.SetString(playerNamePrefsName, kullanici_ismi.text);
            
            currentWindow = CurrentWindow.Main;
		}

		GUI.enabled = true;
	}
    
    public void kullanici_ismikaydet()
    {
        
            kullanici_ismibuyuk.text = kullanici_ismi.text;
        kullanici_ismi_ANAMENU.text = kullanici_ismi.text;
        profilmenusu_kullaniciismi.text = kullanici_ismi.text;
            playerName = tmpPlayerName;
            playerName = kullanici_ismi.text;
            PlayerPrefs.SetString(playerNamePrefsName, kullanici_ismi.text);



        
       // isimdegistirpaneli.SetActive(false);
        PlayerPrefs.SetInt("isimdegistirrr", 1);


    }
	void BrowseRoomsWindow(int windowID)
    {
		if(!createRoom)
        {
			GUI.Label(new Rect(15, 0, 450, 35), "Taralı Odalar - " + totalPlayers.ToString() + " oyuncular - " + totalRooms.ToString() + " odalar - " + PhotonNetwork.PhotonServerSettings.PreferredRegion.ToString());
		}
        else
        {
			GUI.Label(new Rect(15, 0, 300, 35), "Oda Oluştur");
		}

        if (GUI.Button(new Rect(browseWindowSize.x - 30, 5, 25, 25), "", GameSettings.closeButtonStyle))
        {
            currentWindow = CurrentWindow.Main;
        }

        if (!createRoom)
        {
			if(refreshingRooms )
            {
				GUI.enabled = false;
			}
            else
            {
				GUI.enabled = true;
			}

			//if(GUI.Button(new Rect(browseWindowSize.x - 360, browseWindowSize.y - 40, 165, 25), "yeni oda oluştur"))
   //         {
			//	createRoom = true;
			//}

			//Refresh rooms
			//if(GUI.Button(new Rect(browseWindowSize.x - 180, browseWindowSize.y - 40, 75, 25), "YEnile"))
   //         {
			//	this.StopAllCoroutines();
			//	this.StartCoroutine(RefreshRooms());
			//}

			if(refreshingRooms || selectedRoom == -1)
            {
				GUI.enabled = false;
			}
            else
            {
				GUI.enabled = true;
			}

			if(GUI.Button(new Rect(browseWindowSize.x - 90, browseWindowSize.y - 40, 75, 25), "baglan"))
            {
				StartCoroutine(JoinCreateRoom(availableRooms[selectedRoom].name, 
				                              (string)availableRooms[selectedRoom].customProperties["MapName"], 
				                              availableRooms[selectedRoom].maxPlayers, 
				                              (string)availableRooms[selectedRoom].customProperties["GameMode"],
				                              (float)availableRooms[selectedRoom].customProperties["RoundDuration"],
				                              availableRooms[selectedRoom].customProperties["KillLimit"] != null ? (int)availableRooms[selectedRoom].customProperties["KillLimit"] : -1
				                              ));
			}

			GUI.enabled = true;

            GUI.Label(new Rect(10, 39, 270, 21), "Oda adı", GameSettings.roomBrowserHeadersStyle);
            GUI.Label(new Rect(288, 39, 100, 21), "Oyun modu", GameSettings.roomBrowserHeadersStyle);
            GUI.Label(new Rect(396, 39, 100, 21), "Oyuncular", GameSettings.roomBrowserHeadersStyle);
            GUI.Label(new Rect(504, 39, 100, 21), "Harita", GameSettings.roomBrowserHeadersStyle);
            GUI.Label(new Rect(612, 39, 100, 21), "Ping", GameSettings.roomBrowserHeadersStyle);

            GUILayout.Space(50);

			roomBrowserScroll = GUILayout.BeginScrollView(roomBrowserScroll, true, true, GUILayout.Height(browseWindowSize.y - 115));
				if(!refreshingRooms)
                {
					if(availableRooms.Length == 0)
                    {

                    //	GUILayout.Space(15);
                    //	GUILayout.BeginHorizontal();
                    //		GUILayout.Space(15);
                    //GUILayout.Label("Aktif oda malesef yok, ilk odayı sen oluştur", GUILayout.Width(browseWindowSize.x - 50));
                    //	GUILayout.EndHorizontal();
                }
                else
                    {
                      if(kacodavarbulma==0)
                    {
                        Debug.Log(availableRooms.Length+"");
                        odasıralasayısı = availableRooms.Length;
                       
                        
                        kacodavarbulma = 1;
                    }


                    
                    for (int i = 0; i < availableRooms.Length; i++)
                        {

                        

                        if (roomFieldCurrentlyHovering != i)
                            {
								//GUI.color = new Color(1, 1, 1, 0.35f);
							}
                            else
                            {
								GUI.color = Color.white;
							}
							
							GUILayout.BeginHorizontal("box");
                                
								GUI.color = selectedRoom == i ? Color.yellow : Color.white;//tıklandıgında sarı olsun kodu..

                       // gameobjectt[i] = Instantiate(prefabgameobje, new Vector3(0.06498718f, 135.7f, 0), Quaternion.identity);






                        GUILayout.Label(availableRooms[i].name, GUILayout.Width(260), GUILayout.Height(20));
								GUILayout.Space(18);
								GUILayout.Label((string)availableRooms[i].customProperties["GameMode"], GUILayout.Width(90), GUILayout.Height(20));
								GUILayout.Space(18);
								GUILayout.Label(availableRooms[i].playerCount.ToString() + " / " + availableRooms[i].maxPlayers.ToString(), GUILayout.Width(90), GUILayout.Height(20));
								GUILayout.Space(18);
								GUILayout.Label((string)availableRooms[i].customProperties["MapName"], GUILayout.Width(90), GUILayout.Height(20));
								GUILayout.Space(18);
								GUILayout.Label(currentPing.ToString(), GUILayout.Width(90), GUILayout.Height(20));
							GUILayout.EndHorizontal();
							
							if(GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                            {
								roomFieldCurrentlyHovering = i;
								if(Input.GetMouseButtonDown(0))
                                {
									selectedRoom = i;
								}
							}
						}

						GUILayout.BeginVertical();
							GUILayout.FlexibleSpace();
						GUILayout.EndVertical();

						if(GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
                        {
							roomFieldCurrentlyHovering = -1;
						}
					}
				}
			GUILayout.EndScrollView();

			if(Event.current.mousePosition.y < 60 || Event.current.mousePosition.y > (60 + browseWindowSize.y - 115))
            {
				roomFieldCurrentlyHovering = -1;
			}
		}else{
			GUI.Label(new Rect(15, 40, 300, 30), "Oda Adı");
			roomName = GUI.TextField(new Rect(15, 70, (int)(browseWindowSize.x * 0.75f), 25), roomName, 55);

			GUI.Label(new Rect(15,100, 300, 30), "Oyuncu Limiti");
			GUI.enabled = selectedPlayerLimit > 0;
			if(GUI.Button(new Rect(15, 130, 25, 25), "<"))
            {

				selectedPlayerLimit --;

			}
			GUI.enabled = true;
			GUI.Label(new Rect(45,130, 75, 25), playerLimits[selectedPlayerLimit].ToString(), GameSettings.createRoomOptionsStyle);
			GUI.enabled = selectedPlayerLimit < playerLimits.Length - 1;
			if(GUI.Button(new Rect(125, 130, 25, 25), ">"))
            {
				selectedPlayerLimit ++;
			}

			GUI.enabled = true;

			GUI.Label(new Rect(15,160, 300, 30), "Oyun modu");
			GUI.enabled = selectedGameMode > 0;
			if(GUI.Button(new Rect(15, 190, 25, 25), "<"))
            {
				selectedGameMode --;
			}
			GUI.enabled = true;
			GUI.Label(new Rect(45,190, 75, 25), gameModes[selectedGameMode], GameSettings.createRoomOptionsStyle);
			GUI.enabled = selectedGameMode < gameModes.Length - 1;
			if(GUI.Button(new Rect(125, 190, 25, 25), ">"))
            {
				selectedGameMode ++;
			}

			if(gameModes[selectedGameMode] == "FFA")
            {
				GUI.enabled = true;

				GUI.Label(new Rect(185,160, 300, 30), "ölme limiti");
				GUI.enabled = selectedKillLimit > 0;
				if(GUI.Button(new Rect(185, 190, 25, 25), "<"))
                {
					selectedKillLimit --;
				}
				GUI.enabled = true;
				GUI.Label(new Rect(210,190, 75, 25), killLimits[selectedKillLimit].ToString(), GameSettings.createRoomOptionsStyle);
				GUI.enabled = selectedKillLimit < killLimits.Length - 1;
				if(GUI.Button(new Rect(290, 190, 25, 25), ">"))
                {
					selectedKillLimit ++;
				}
			}
			
			GUI.enabled = true;

			GUI.Label(new Rect(15,220, 300, 30), "Oyun Haritası");
			GUI.enabled = selectedMap > 0;
			if(GUI.Button(new Rect(15, 250, 25, 25), "<"))
            {
				selectedMap --;
			}
			GUI.enabled = true;
			GUI.Label(new Rect(45 , 250, 240, 30), availableMaps[selectedMap].mapName, GameSettings.createRoomOptionsStyle);
			GUI.enabled = selectedMap < availableMaps.Length - 1;
			if(GUI.Button(new Rect(290, 250, 25, 25), ">"))
            {
				selectedMap ++;
			}
			GUI.enabled = true;
			GUI.DrawTexture(new Rect(15, 285, 300, 150), availableMaps[selectedMap].mapPreview ? availableMaps[selectedMap].mapPreview : defaultMapPreview);

			if(GUI.Button(new Rect(browseWindowSize.x - 90, browseWindowSize.y - 40, 75, 25), "Oluştur"))
            {
                StartCoroutine(JoinCreateRoom(
                    roomName, availableMaps[selectedMap].mapName, playerLimits[selectedPlayerLimit], gameModes[selectedGameMode], (float)roundDurations[selectedGameMode], gameModes[selectedGameMode] == "FFA" ? killLimits[selectedKillLimit] : -1
                ));

                //Remember player settings when creating new room
                PlayerPrefs.SetInt(playerLimitPrefsName, selectedPlayerLimit);
                PlayerPrefs.SetInt(gameModePrefsName, selectedGameMode);
                PlayerPrefs.SetInt(selectedMapPrefsName, selectedMap);
            }

			if(GUI.Button(new Rect(15, browseWindowSize.y - 40, 75, 25), "Geri"))
            {
				createRoom = false;
			}
		}

		if(refreshingRooms)
        {
			//GUI.Box(new Rect(browseWindowSize.x/2 - 75, browseWindowSize.y/2 - 30, 150, 30), "Yenileniyor...");
		}
		//GUI.DragWindow (new Rect (0, 0, 10000, 35));
	}
    public void baglan()
    {
        
        yükleniyorpaneli.SetActive(false);
        if (selectedRoom<0)
        {
            kacodabulundugu.text = "Lütfen oda seçiniz !!";
        }
        // selectedRoom = 0;
        if (availableRooms.Length > 0)
        {
            if (selectedRoom > -1)
            {
                if((string)availableRooms[selectedRoom].customProperties["GameMode"] =="FFA")
                {
                    PlayerPrefs.SetInt("oyunmod_", 1);
                    
                    oyun_modubaslıgı.text = "HERKES TEK";
                }
                if ((string)availableRooms[selectedRoom].customProperties["GameMode"] == "TDM")
                {
                    PlayerPrefs.SetInt("oyunmod_", 0);
                    oyun_modubaslıgı.text = "KLASİK";
                }
                if ((string)availableRooms[selectedRoom].customProperties["MapName"] == "DÖRT KÖŞE")
                {
                    PlayerPrefs.SetInt("harita", 0);
                    harita_ismibaslıgı.text = "DÖRT KÖŞE";
                    harita1.SetActive(true);
                    harita2.SetActive(false);
                    harita3.SetActive(false);
                    harita4.SetActive(false);

                }
                if ((string)availableRooms[selectedRoom].customProperties["MapName"] == "CEZA EVİ")
                {
                    PlayerPrefs.SetInt("harita", 1);
                    harita_ismibaslıgı.text = "CEZA EVİ";
                    harita1.SetActive(false);
                    harita2.SetActive(true);
                    harita3.SetActive(false);
                    harita4.SetActive(false);
                }
                if ((string)availableRooms[selectedRoom].customProperties["MapName"] == "AWP KULE")
                {
                    PlayerPrefs.SetInt("harita", 2);
                    harita_ismibaslıgı.text = "AWP KULE";
                    harita1.SetActive(false);
                    harita2.SetActive(false);
                    harita3.SetActive(true);
                    harita4.SetActive(false);

                }
               




                yükleniyorpaneli.SetActive(true);
                
                
                
                
                StartCoroutine(JoinCreateRoom(availableRooms[selectedRoom].name,
                                                      (string)availableRooms[selectedRoom].customProperties["MapName"],
                                                      availableRooms[selectedRoom].maxPlayers,
                                                      (string)availableRooms[selectedRoom].customProperties["GameMode"],
                                                      (float)availableRooms[selectedRoom].customProperties["RoundDuration"],
                                                      availableRooms[selectedRoom].customProperties["KillLimit"] != null ? (int)availableRooms[selectedRoom].customProperties["KillLimit"] : -1
                                                      ));
                
            }
        }

    }
    
    public void satır1()
    {
        if(availableRooms[0].playerCount !=availableRooms[0].maxPlayers)
        {
            selectedRoom = 0;
            katılbutonu.SetActive(true);
        }
        else if(availableRooms[0].playerCount == availableRooms[0].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        
        Debug.Log("satır 1 secildi.");
        
    }
    public void satır2()
    {
        if (availableRooms[1].playerCount != availableRooms[1].maxPlayers)
        {
            selectedRoom = 1;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[1].playerCount == availableRooms[1].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 2 secildi.");

    }
    public void satır3()
    {
        if (availableRooms[2].playerCount != availableRooms[2].maxPlayers)
        {
            selectedRoom = 2;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[2].playerCount == availableRooms[2].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 3 secildi.");

    }
    public void satır4()
    {
        if (availableRooms[3].playerCount != availableRooms[3].maxPlayers)
        {
            selectedRoom = 3;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[3].playerCount == availableRooms[3].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 4 secildi.");

    }
    public void satır5()
    {
        if (availableRooms[4].playerCount != availableRooms[4].maxPlayers)
        {
            selectedRoom = 4;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[4].playerCount == availableRooms[4].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 5 secildi.");

    }
    public void satır6()
    {
        if (availableRooms[5].playerCount != availableRooms[5].maxPlayers)
        {
            selectedRoom = 5;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[5].playerCount == availableRooms[5].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 6 secildi.");

    }
    public void satır7()
    {
        if (availableRooms[6].playerCount != availableRooms[6].maxPlayers)
        {
            selectedRoom = 6;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[6].playerCount == availableRooms[6].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 7 secildi.");

    }
    public void satır8()
    {
        if (availableRooms[7].playerCount != availableRooms[7].maxPlayers)
        {
            selectedRoom = 7;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[7].playerCount == availableRooms[7].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 8 secildi.");

    }
    public void satır9()
    {
        if (availableRooms[8].playerCount != availableRooms[8].maxPlayers)
        {
            selectedRoom = 8;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[8].playerCount == availableRooms[8].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 9 secildi.");
    }
    public void satır10()
    {
        if (availableRooms[9].playerCount != availableRooms[9].maxPlayers)
        {
            selectedRoom = 9;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[9].playerCount == availableRooms[9].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 10 secildi.");

    }
    public void satır11()
    {
        if (availableRooms[10].playerCount != availableRooms[10].maxPlayers)
        {
            selectedRoom = 10;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[10].playerCount == availableRooms[10].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 11 secildi.");

    }
    public void satır12()
    {
        if (availableRooms[11].playerCount != availableRooms[11].maxPlayers)
        {
            selectedRoom = 11;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[11].playerCount == availableRooms[11].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 12 secildi.");

    }
    public void satır13()
    {
        if (availableRooms[12].playerCount != availableRooms[12].maxPlayers)
        {
            selectedRoom = 12;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[12].playerCount == availableRooms[12].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 13 secildi.");

    }
    public void satır14()
    {
        if (availableRooms[13].playerCount != availableRooms[13].maxPlayers)
        {
            selectedRoom = 13;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[13].playerCount == availableRooms[13].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 14 secildi.");

    }
    public void satır15()
    {
        if (availableRooms[14].playerCount != availableRooms[14].maxPlayers)
        {
            selectedRoom = 14;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[14].playerCount == availableRooms[14].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 15 secildi.");

    }
    public void satır16()
    {
        if (availableRooms[15].playerCount != availableRooms[15].maxPlayers)
        {
            selectedRoom = 15;
            katılbutonu.SetActive(true);
        }
        else if (availableRooms[15].playerCount == availableRooms[15].maxPlayers)
        {
            odadolupaneli.SetActive(true);
            katılbutonu.SetActive(false);
        }
        Debug.Log("satır 16 secildi.");

    }
    public void satır17()
    {
        selectedRoom = 16;
        Debug.Log("satır 17 secildi.");
    }
    public void satır18()
    {
        selectedRoom = 17;
        Debug.Log("satır 18 secildi.");

    }
    public void satır19()
    {
        selectedRoom = 18;
        Debug.Log("satır 19 secildi.");

    }
    public void satır20()
    {
        selectedRoom = 19;
        Debug.Log("satır 20 secildi.");

    }
    public void satır21()
    {
        selectedRoom = 20;
        Debug.Log("satır 21 secildi.");

    }
    public void satır22()
    {
        selectedRoom = 21;
        Debug.Log("satır 22 secildi.");

    }
    public void satır23()
    {
        selectedRoom = 22;
        Debug.Log("satır 23 secildi.");

    }
    public void satır24()
    {
        selectedRoom = 23;
        Debug.Log("satır 24 secildi.");

    }
    public void satır25()
    {
        selectedRoom = 24;
        Debug.Log("satır 25 secildi.");
    }
    public void satır26()
    {
        selectedRoom = 25;
        Debug.Log("satır 26 secildi.");

    }
    public void satır27()
    {
        selectedRoom = 26;
        Debug.Log("satır 27 secildi.");

    }
    public void satır28()
    {
        selectedRoom = 27;
        Debug.Log("satır 28 secildi.");

    }
    public void satır29()
    {
        selectedRoom = 28;
        Debug.Log("satır 29 secildi.");

    }
    public void satır30()
    {
        selectedRoom = 29;
        Debug.Log("satır 30 secildi.");

    }
    public void satır31()
    {
        selectedRoom = 30;
        Debug.Log("satır 31 secildi.");

    }
    public void satır32()
    {
        selectedRoom = 31;
        Debug.Log("satır 32 secildi.");

    }

    public void odalarısırala()
    {
        
        for (int i = 0; i < 32; i++)
        {
            //satırlar[odasıralasayısı  - i].SetActive(true);
            
            if (i < availableRooms.Length)
            {
                satırlar[i].SetActive(true);
            }
            if (i>= availableRooms.Length)
            {
                satırlar[i].SetActive(false);
            }

        }
        


        for (int i = 0; i < availableRooms.Length; i++)
        {

            
              
            odaadıbilgisi[i].text = availableRooms[i].name;

            if((string)availableRooms[i].customProperties["GameMode"]=="TDM")
            {
                oyunmodubilgisi[i].text = "KLASİK";
            }
            if ((string)availableRooms[i].customProperties["GameMode"] == "FFA")
            {
                oyunmodubilgisi[i].text = "HERKES TEK";
            }
            //oyunmodubilgisi[i].text = (string)availableRooms[i].customProperties["GameMode"];
            oyuncusayısıbilgisi[i].text = availableRooms[i].playerCount.ToString() + " / " + availableRooms[i].maxPlayers.ToString();
            haritabilgisi[i].text = (string)availableRooms[i].customProperties["MapName"];
            pingbilgisi[i].text = currentPing.ToString();
            çevrimiçi_oyuncu_sayısı += availableRooms[i].playerCount;
            


            //        public Text[] odaadıbilgisi;
            //public Text[] oyuncusayısıbilgisi;
            //public Text[] haritabilgisi;
            //public Text[] oyunmodubilgisi;
            //public Text[] pingbilgisi;


            //GUILayout.Label(availableRooms[i].name, GUILayout.Width(260), GUILayout.Height(20));
            //    GUILayout.Space(18);
            //    GUILayout.Label((string)availableRooms[i].customProperties["GameMode"], GUILayout.Width(90), GUILayout.Height(20));
            //    GUILayout.Space(18);
            //    GUILayout.Label(availableRooms[i].playerCount.ToString() + " / " + availableRooms[i].maxPlayers.ToString(), GUILayout.Width(90), GUILayout.Height(20));
            //    GUILayout.Space(18);
            //    GUILayout.Label((string)availableRooms[i].customProperties["MapName"], GUILayout.Width(90), GUILayout.Height(20));
            //    GUILayout.Space(18);
            //    GUILayout.Label(currentPing.ToString(), GUILayout.Width(90), GUILayout.Height(20));


            // gameobjectt[i] = Instantiate(Resources.Load("Object", typeof(GameObject))) as GameObject;
            //bilgiler[i] = (GameObject)Instantiate(gameobjectt[i], new Vector3(0.06498718f, 135.7f, 0), Quaternion.identity);
            // bilgiler[i].transform.localScale = new Vector3(1.011952f, 0.7726837f, 0.8584105f);
            //  gameobjectt[i].transform.localScale = new Vector3(1.011952f, 0.7726837f, 0.8584105f);



        }
        çevrimiçi_oyuncu_sayısıyazisi.text = "Çevrimiçi oyuncu : " + çevrimiçi_oyuncu_sayısı.ToString();
        Debug.Log(çevrimiçi_oyuncu_sayısı);
}
    
    public void yenile()
    {
        katılbutonu.SetActive(false);
        çevrimiçi_oyuncu_sayısı = 0;
        //aktifodasayısı.text = "Açık Oda Sayısı : " + "0";
        for (int i=0;i<8;i++)
        {
            odasecildi[i].SetActive(false);
        }
        kacodabulundugu.text = "Yenileniyor..";
        kacodavarbulma = 0;
        this.StopAllCoroutines();
        this.StartCoroutine(RefreshRooms());
        
        //if (savaşpaneli.active == true)
        //{
        //    Invoke("yenile", 10);
        //}
        odalarscroll.value = 1;
    }
    public void sıfırlama()
    {
        selectedMap = -1;
        selectedGameMode = -1;
        selectedRoom = -1;
        //odaismi.text = "";
        

    }
    public void takimsavasi()
    {
        selectedGameMode = 0;
        Debug.Log("takim savasi secildi");
        
        oyun_modubaslıgı.text = "KLASİK";
            Debug.Log("ölümmaçı");
        
        
        this.StopAllCoroutines();
        this.StartCoroutine(RefreshRooms());
    }
    public void olummaci()
    {
        selectedGameMode = 1;
        Debug.Log("olum savasi secildi");
       
            oyun_modubaslıgı.text = "HERKES TEK";
        
    }
    public void ilkmap()
    {
        selectedMap = 0;
        Debug.Log("ilk map  secildi");
        
            harita_ismibaslıgı.text = "DÖRT KÖŞE";
        harita1.SetActive(true);
        harita2.SetActive(false);
        harita3.SetActive(false);
        harita4.SetActive(false);
        harita1oyunkur.SetActive(true);
        harita2oyunkur.SetActive(false);
        harita3oyunkur.SetActive(false);
        harita4oyunkur.SetActive(false);
        Debug.Log("dört köşes");
        
    }
    public void map2()
    {
        selectedMap = 1;
        Debug.Log("2. map  secildi");
        
            harita_ismibaslıgı.text = "CEZA EVİ";
        harita1.SetActive(false);
        harita2.SetActive(true);
        harita3.SetActive(false);
        harita4.SetActive(false);
        harita1oyunkur.SetActive(false);
        harita2oyunkur.SetActive(true);
        harita3oyunkur.SetActive(false);
        harita4oyunkur.SetActive(false);
        Debug.Log("CEZA EVİ");
    }
    public void map3()
    {
        selectedMap = 2;
        Debug.Log("3. map  secildi");

        harita_ismibaslıgı.text = "AWP KULE";
        harita1.SetActive(false);
        harita2.SetActive(false);
        harita3.SetActive(true);
        harita4.SetActive(false);
        harita1oyunkur.SetActive(false);
        harita2oyunkur.SetActive(false);
        harita3oyunkur.SetActive(true);
        harita4oyunkur.SetActive(false);
        Debug.Log("AWP KULE");
    }
    
    public void oyuncuarttırtusu()
    {
        if(Convert.ToInt32(oyunculimiti.text) == 10)
        {

        }
        else
        {
            oyunculimiti.text = Convert.ToString(Convert.ToInt32(oyunculimiti.text) + 2);
            selectedPlayerLimit = (Convert.ToInt32(oyunculimiti.text) - 2) / 2;
            
        }
    }
    public void oyuncuazalttusu()
    {
        if (Convert.ToInt32(oyunculimiti.text) == 2)
        {

        }
        else
        {
            oyunculimiti.text = Convert.ToString(Convert.ToInt32(oyunculimiti.text) -2);
            selectedPlayerLimit = (Convert.ToInt32(oyunculimiti.text) - 2) / 2;

        }
    }
    public void oyunkursoltus()
    {
        
        kacodabulundugu.text = "";
        
        
    }
    public void oyunakatılsoltus()
    {
        kacodaolduguobjesi.SetActive(true);
            this.StopAllCoroutines();
            this.StartCoroutine(RefreshRooms());
        
    }
    public void oyunolustur()
    {
        if (availableRooms.Length >= 11)
        {
            uyarılabeli.text = "Mevcut odalardan birine girin !";
            maksimumodauyarı.SetActive(true);
        }
        else 
        {


            if (odaismi.text != "")
            {

                if (selectedGameMode > -1)
                {
                    if (selectedMap > -1)
                    {
                        yükleniyorpaneli.SetActive(true);

                        selectedPlayerLimit = (Convert.ToInt32(oyunculimiti.text) - 2) / 2;

                        roomName = odaismi.text;

                        PlayerPrefs.SetInt(playerLimitPrefsName, selectedPlayerLimit);
                        PlayerPrefs.SetInt(gameModePrefsName, selectedGameMode);
                        PlayerPrefs.SetInt(selectedMapPrefsName, selectedMap);
                        PlayerPrefs.SetInt("oyunmod_", selectedGameMode);
                        PlayerPrefs.SetInt("harita", selectedMap);
                        selectedKillLimit = 0;
                        
                        StartCoroutine(JoinCreateRoom(
                                    roomName, availableMaps[selectedMap].mapName, playerLimits[selectedPlayerLimit], gameModes[selectedGameMode], (float)roundDurations[selectedGameMode], gameModes[selectedGameMode] == "FFA" ? killLimits[selectedKillLimit] : -1
                                ));

                        //Remember player settings when creating new room
                        PlayerPrefs.SetInt(playerLimitPrefsName, selectedPlayerLimit);
                        PlayerPrefs.SetInt(gameModePrefsName, selectedGameMode);
                        PlayerPrefs.SetInt(selectedMapPrefsName, selectedMap);
                        
                    }
                    else
                    {
                        uyarılabeli.text = "Oyun haritasını seçiniz !";
                    }
                }
                else
                {
                    uyarılabeli.text = "Oyun modunu seçiniz !";
                }
            }
            else
            {
                uyarılabeli.text = "Oda adını yazmalısın !";
            }
        }
        Invoke("oyunolustur", 7);
    }
    public void tekrarbaglan()
    {
        if (!PhotonNetwork.connected)
        {
            if (PhotonNetwork.connectionState != ConnectionState.InitializingApplication && PhotonNetwork.connectionState != ConnectionState.Connecting &&
                   PhotonNetwork.connectionState != ConnectionState.Disconnecting && PhotonNetwork.connectionState != ConnectionState.Connected)
            {

               // BAGLANTIHATASIPANELİ.SetActive(true);
               // PhotonNetwork.ConnectUsingSettings(networkVersion);

            }

        }
        else
        {
           // BAGLANTIHATASIPANELİ.SetActive(false);
        }
    }
    
    
    
    
    
    //Networking Part ********************************************************************************************************************************************
    IEnumerator RefreshRooms ()
    {
		refreshingRooms = true;
		selectedRoom = -1;

  //      while (PhotonNetwork.connected)
  //      {
  //          if (PhotonNetwork.connectionState != ConnectionState.Disconnecting)
  //          {
  //              PhotonNetwork.Disconnect();
  //          }
		//	yield return null;
		//}

        while (!PhotonNetwork.connected)
        {
            if(PhotonNetwork.connectionState != ConnectionState.InitializingApplication && PhotonNetwork.connectionState != ConnectionState.Connecting && 
                PhotonNetwork.connectionState != ConnectionState.Disconnecting && PhotonNetwork.connectionState != ConnectionState.Connected)
            {
                Debug.Log("buldum amk");
               // BAGLANTIHATASIPANELİ.SetActive(true);
                PhotonNetwork.ConnectUsingSettings(networkVersion);
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.75f);

		currentPing = PhotonNetwork.GetPing();

		availableRooms = PhotonNetwork.GetRoomList();
        

        //print (availableRooms.Length.ToString() + " available rooms");

        totalPlayers = PhotonNetwork.countOfPlayers;
		totalRooms = availableRooms.Length;
        if (availableRooms.Length == 0)
        {
            Invoke("odalarısırala", 0);
            aktifodasayısı.text = "Açık Oda Sayısı : " + availableRooms.Length;
            katılbutonu.SetActive(false);
            kacodabulundugu.text = "Oda bulunamadı.";
            
            for (int i = 0; i < 32; i++)
            {
                //satırlar[odasıralasayısı  - i].SetActive(true);
                satırlar[i].SetActive(false);

            }

        }
        else if (availableRooms.Length > 0)
        {
            kacodabulundugu.text = availableRooms.Length +" oda bulundu.";
            aktifodasayısı.text = "Açık Oda Sayısı : " + availableRooms.Length;
            
            Invoke("odalarısırala", 0);
        }
        yenileniyor_paneli.SetActive(false);
        refreshingRooms = false;
    }

	IEnumerator JoinCreateRoom (string newRoomName, string newMapName, int newMaxPlayers, string newGameMode, float roundDuration, int newKillLimit)
    {
		//loadingMap = true;

		print ("Odaya gir & Oda oluştur");

		while(!PhotonNetwork.connected)
        {
			if(PhotonNetwork.connectionState != ConnectionState.InitializingApplication && PhotonNetwork.connectionState != ConnectionState.Connecting && 
		   		PhotonNetwork.connectionState != ConnectionState.Disconnecting && PhotonNetwork.connectionState != ConnectionState.Connected)
            {

				PhotonNetwork.ConnectUsingSettings(networkVersion);
			}
			yield return null;
		}
		
		yield return new WaitForSeconds(0.5f);

		PhotonNetwork.playerName = playerName;
		
		//Create Room
		if(PhotonNetwork.connected)
        {
			Hashtable roomProperties = new Hashtable(); 
			roomProperties["MapName"] = newMapName;
			roomProperties["GameMode"] = newGameMode;
			roomProperties["RoundDuration"] = roundDuration; 
			if(newKillLimit > 0)
            {
				roomProperties["KillLimit"] = newKillLimit; //Used only for FFA mode
			}
			string[] exposedProps = new string[newKillLimit > 0 ? 4 : 3];
			exposedProps[0] = "MapName";
			exposedProps[1] = "GameMode";
			exposedProps[2] = "RoundDuration";
			if(newKillLimit > 0)
            {
				exposedProps[3] = "KillLimit";
			}

			RoomOptions roomOptions = new RoomOptions();
			roomOptions.cleanupCacheOnLeave = true;
			roomOptions.isOpen = true;
			roomOptions.isVisible = true;
			roomOptions.maxPlayers = (byte)newMaxPlayers;
			roomOptions.customRoomProperties = roomProperties;
			roomOptions.customRoomPropertiesForLobby = exposedProps;
			
			PhotonNetwork.JoinOrCreateRoom(newRoomName, roomOptions, null);
		}else{
			GameSettings.errorText = "Server'e bağlanılamadı, lütfen tekrar dene";
			yield return null;		
		}
	}

	void OnJoinedRoom()
    {
		//Load room map
		//print ("Done joining/creating new room");
		PhotonNetwork.isMessageQueueRunning = false;
        SceneManager.LoadScene((string)PhotonNetwork.room.customProperties["MapName"]);
	}

	void OnPhotonCreateGameFailed()
    {
		GameSettings.errorText = "oda oluşturulamadı,lütfen tekrar deneyin";
		this.StopAllCoroutines();
	}
	
	void OnPhotonJoinRoomFailed()
    {
		GameSettings.errorText = "oda oluşturulamadı,lütfen tekrar deneyin";
		this.StopAllCoroutines();
	}

	void OnFailedToConnectToPhoton()
    {
		GameSettings.errorText = "";
		this.StopAllCoroutines();
	}

	void OnLevelWasLoaded(int index)
    {
		if(index > 0)
        {
			//Spawn all required objects
			Transform allPoints = GameObject.Find("_ReferencePoints").transform;

			Transform welcomeCameraRef = allPoints.Find("WelcomeCamera");
			GameObject roomControllerObject = Instantiate(roomControllerPrefab,  welcomeCameraRef.position,  welcomeCameraRef.rotation) as GameObject;
			RoomController roomController = roomControllerObject.GetComponent<RoomController>();

			Transform buySpots = allPoints.Find("BuySpots");

			foreach(Transform child in buySpots)
            {
				GameObject buySpotObj = Instantiate(buySpotPrefab, child.position, child.rotation) as GameObject;
				AmmoSpot bs = buySpotObj.GetComponent<AmmoSpot>();
				bs.rc = roomController;
				bs.thisT = buySpotObj.transform;
			}

			Transform ladders = allPoints.Find("Ladders");

			foreach(Transform child in  ladders)
            {
				child.gameObject.AddComponent<LadderScript>();
			}

			Transform spawnPoints = allPoints.Find("SpawnPoints");

			foreach(Transform child in spawnPoints)
            {
				if(child.name.StartsWith("TeamASpawn"))
                {
					roomController.teamASpawnPoints.Add (child);
				}
				if(child.name.StartsWith("TeamBSpawn"))
                {
					roomController.teamBSpawnPoints.Add (child);	
				}
			}

			Destroy(gameObject);
		}
	}
}
