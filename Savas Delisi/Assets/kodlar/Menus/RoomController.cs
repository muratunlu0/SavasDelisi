

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable; //Replace default Hashtables with Photon hashtables

public class RoomController : Photon.MonoBehaviour
{

    //This is main script that control in-room logic

    public GameObject playerPrefab;
    public Camera welcomeCamera;
    Color[] colors = new Color[7];
   // public Color sonrenk;
    public AudioClip buySound;
    public AudioClip cashRegisterSound;
    
    public int birincil11;
    public int birincil22;
    public int birincil33;
    public int ikincil11;
    public int bicak11;
    public int kapat_amk_shadow;
    [HideInInspector]
    public List<Transform> teamASpawnPoints = new List<Transform>();
    [HideInInspector]
    public List<Transform> teamBSpawnPoints = new List<Transform>();
    [HideInInspector]
    public List<PhotonPlayer> teamAPlayers = new List<PhotonPlayer>();
    [HideInInspector]
    public List<PhotonPlayer> teamBPlayers = new List<PhotonPlayer>();
    [HideInInspector]
    public string spectatorNames = "";

    [HideInInspector]
    public bool showScoreBoard = false;
    [HideInInspector]
    public bool eneteredBuySpot = false;
    [HideInInspector]
    public bool leavingRoom = false;
    [HideInInspector]
    public bool showFPS = true;
    [HideInInspector]
    public bool showBuyMenu = false;
    [HideInInspector]
    public bool showOptions;

    [HideInInspector]
    public PlayerNetwork ourPlayer;
    [HideInInspector]
    public List<PlayerNetwork> otherPlayers = new List<PlayerNetwork>(); //Keep references of other spawned players

    Transform welcomeCameraTransform;
    Vector3 defaultCamPos;
    Quaternion defaultCamRot;

    //bool playerKilled = false;
    [HideInInspector]
    public float distance; //Crosshair distance is also set at Weapon.cs when we fire

    [HideInInspector]
    public float redScreenFade = 0;
    [HideInInspector]
    public float hitTopFade = 0;
    [HideInInspector]
    public float hitBottomFade = 0;
    [HideInInspector]
    public float hitLeftFade = 0;
    [HideInInspector]
    public float hitRightFade = 0;
    [HideInInspector]
    public bool doingHitDetector = false;

    //Get current fps
    float updateInterval = 1f;
    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float roundDuration = 0;
    float referenceTime = 0;
    [HideInInspector]
    public float currentRoundTime;
    float seconds;
    float minutes;

    [HideInInspector]
    public string roundTimeString = "00:00";
    [HideInInspector]
    public int teamAScore = 0;
    [HideInInspector]
    public int teamBScore = 0;
    [HideInInspector]
    public int ourTeam = 0;
    [HideInInspector]
    public string currentGameMode = "";
    [HideInInspector]
    public int currentRespawnTime = -1;
    [HideInInspector]
    public int currentGameStatus = 0;

    //Used only for FFA (Free For All) mode
    [HideInInspector]
    public int currentKillLimit;
    [HideInInspector]
    public PhotonPlayer winningPlayer;

    [HideInInspector]
    public int totalCash = 0;
    [HideInInspector]
    public int currentHP = 0;
    [HideInInspector]
    public string scoreToAddTmp = "+ 100";
    [HideInInspector]
    public Color currentAddingCashColor;
    [HideInInspector]
    public Color currentTotalCashColor;

    int previousGameStatus = 0;
    bool doneSetup = false;
    int waitBeforeRespawn = 3; //How much seconds player need to wait before respawn again
    Color addingColor = new Color(0, 1, 0, 0.75f);
    Color substractingColor = new Color(1, 0.71f, 0.109f, 0.75f);

    Color addingColorFadeTo;
    bool addingKillCash = false;

    int lockState = -1;

    [System.Serializable]
    public class ActionReport
    {
        public string leftText;
        public string middleText;
        public string rightText;
        public Color leftTextColor;
        public Color rightTextColor;
        public float timer; //How long this itema appear on screen in seconds

        public ActionReport(string lt, string mt, string rt, Color ltc, Color rtc, float t)
        {
            leftText = lt;
            middleText = mt;
            rightText = rt;
            leftTextColor = ltc;
            rightTextColor = rtc;
            timer = t;
        }
    }

    [HideInInspector]
    public List<ActionReport> actionReports = new List<ActionReport>();

    [HideInInspector]
    public MultiplayerChat mc;
    [HideInInspector]
    public OptionsSettings os;
    
    FPSMouseLook cameraMouseLook;
    Scoreboard sb;
    BuyMenu bm;
    AudioSource audioSource;

    [HideInInspector]
	public int timeToPurchase = 0;

    //Compare players by kills (sort by list)
    private static int SortPlayers (PhotonPlayer A, PhotonPlayer B)
    {
		return (int)B.customProperties["Kills"] - (int)A.customProperties["Kills"];
	}



    // Use this for initialization
    IEnumerator Start()
    {
        PhotonNetwork.isMessageQueueRunning = true;
        
        photonView.viewID = 100;
        GameSettings.rc = this;
        GameSettings.menuOpened = true;
        GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_öldürme_kd = PlayerPrefs.GetInt("öldürmesayımpref");
        GameObject.Find("asdf").GetComponent<Scoreboard>().toplam_ölme_kd = PlayerPrefs.GetInt("ölmesayımpref");
        GameObject.Find("asdf").GetComponent<Scoreboard>().seçeneklercanvası.SetActive(false);
        GameObject.Find("asdf").GetComponent<Scoreboard>().scoreboardcanvası.SetActive(false);
        PlayerPrefs.SetInt("beraberesonuc", 0);
        leavingRoom = false;
        showScoreBoard = false;
        showOptions = false;
        eneteredBuySpot = false;
        showBuyMenu = false;
        totalCash = GameSettings.cnst;
        timeToPurchase = 0;
        showFPS = true;

        redScreenFade = 0;
        hitTopFade = 0;
        hitBottomFade = 0;
        hitLeftFade = 0;
        hitRightFade = 0;
        doingHitDetector = false;

        currentAddingCashColor = addingColorFadeTo;
        currentTotalCashColor = GameSettings.HUDColor;

        mc = GetComponent<MultiplayerChat>();
        os = GetComponent<OptionsSettings>();
        os.rc = this;
        sb = GetComponent<Scoreboard>();
        sb.rc = this;
        bm = GetComponent<BuyMenu>();

        audioSource = GetComponent<AudioSource>();

        cameraMouseLook = welcomeCamera.GetComponent<FPSMouseLook>();
        cameraMouseLook.AssignTarget(null);

        yield return new WaitForEndOfFrame();

        //Set out initial properties
        Hashtable setPlayerProperties = new Hashtable();
        setPlayerProperties.Add("Kills", (int)0);
        setPlayerProperties.Add("Deaths", (int)0);
        setPlayerProperties.Add("Ping", (int)PhotonNetwork.GetPing());
        setPlayerProperties.Add("Team", (int)0); //0 = Spectators, 1 = teamA, 2 = teamB, 3 = Draw
        setPlayerProperties.Add("PlayerHP", (int)-1);
        PhotonNetwork.player.SetCustomProperties(setPlayerProperties);

        RefreshPlayerList();

        welcomeCameraTransform = welcomeCamera.transform;
        defaultCamPos = welcomeCameraTransform.position;
        defaultCamRot = welcomeCameraTransform.rotation;

        timeleft = updateInterval;

        yield return new WaitForEndOfFrame();

        //Set Room properties
        if (PhotonNetwork.isMasterClient)
        {
            referenceTime = (float)PhotonNetwork.time;
            currentGameStatus = 0;

            Hashtable setRoomProperties = new Hashtable();
            setRoomProperties.Add("ReferenceTime", (float)PhotonNetwork.time);
            setRoomProperties.Add("GameStatus", (int)0); //0 = Play, 1 = team A won, 2 = team B won
            setRoomProperties.Add("TeamAScore", (int)0);
            setRoomProperties.Add("TeamBScore", (int)0);
            PhotonNetwork.room.SetCustomProperties(setRoomProperties);
        }
        else
        {
            referenceTime = (float)PhotonNetwork.room.customProperties["ReferenceTime"];
            currentGameStatus = (int)PhotonNetwork.room.customProperties["GameStatus"];
        }

        yield return new WaitForEndOfFrame();

        currentGameMode = (string)PhotonNetwork.room.customProperties["GameMode"];
        roundDuration = (float)PhotonNetwork.room.customProperties["RoundDuration"];
        GetTeamScores();

        previousGameStatus = currentGameStatus;

        //Used only for FFA mode
        if (PhotonNetwork.room.customProperties["KillLimit"] != null)
        {
            currentKillLimit = (int)PhotonNetwork.room.customProperties["KillLimit"];
        }
        else
        {
            currentKillLimit = -1;
        }

        if (PhotonNetwork.room.customProperties["WinningPlayer"] != null)
        {
            winningPlayer = (PhotonPlayer)PhotonNetwork.room.customProperties["WinningPlayer"];
        }
        else
        {
            winningPlayer = null;
        }

        //Display notification that we joined room (locally)
        PostActivityRemote("", PhotonNetwork.player.name + " odaya katıldı", "", 0, -50);
        InvokeRepeating("RefreshPing", 3.5f, 3.5f);

        GameSettings.currentGameMode = currentGameMode;

        yield return new WaitForEndOfFrame();

        doneSetup = true;
        GameObject.Find("asdf").GetComponent<Scoreboard>().kupa_aldımı = 0;
        GameSettings.switchWeaponIndex = 1;
    }

    public void renkbirincil()
    {
        birincil11 = PlayerPrefs.GetInt("birincilrenk1");
        birincil22 = PlayerPrefs.GetInt("birincilrenk2");
        birincil33 = PlayerPrefs.GetInt("birincilrenk3");
        ikincil11 = PlayerPrefs.GetInt("ikincilrenk1");
        bicak11 = PlayerPrefs.GetInt("bicakrenk1");

        if (GameObject.FindGameObjectWithTag("birincil1") != null)
        {
            for (int i = 0; i < 11; i++)
            {
                if (PlayerPrefs.GetInt("ump40_" + i.ToString() + "_kusanıldı") == 1)
                {
                    GameObject.FindGameObjectWithTag("birincil1").GetComponent<SkinnedMeshRenderer>().material = GameObject.Find("asdf").GetComponent<oyunmagaza>().ump40_skins[i];
                }
            }
        }
        if (GameObject.FindGameObjectWithTag("birincil2") != null)
        {
            for (int i = 0; i < 11; i++)
            {
                if (PlayerPrefs.GetInt("awp_" + i.ToString() + "_kusanıldı") == 1)
                {
                    GameObject.FindGameObjectWithTag("birincil2").GetComponent<SkinnedMeshRenderer>().material = GameObject.Find("asdf").GetComponent<oyunmagaza>().awp_skins[i];
                }
            }
        }
        if (GameObject.FindGameObjectWithTag("birincil3") != null)
        {
            for (int i = 0; i < 11; i++)
            {
                if (PlayerPrefs.GetInt("pompalı_" + i.ToString() + "_kusanıldı") == 1)
                {
                    GameObject.FindGameObjectWithTag("birincil3").GetComponent<SkinnedMeshRenderer>().material = GameObject.Find("asdf").GetComponent<oyunmagaza>().pompalı_skins[i];
                }
            }
        }
        if (GameObject.FindGameObjectWithTag("ikincil1") != null)
        {
            for (int i = 0; i < 11; i++)
            {
                if (PlayerPrefs.GetInt("tabanca_" + i.ToString() + "_kusanıldı") == 1)
                {
                    GameObject.FindGameObjectWithTag("ikincil1").GetComponent<SkinnedMeshRenderer>().material = GameObject.Find("asdf").GetComponent<oyunmagaza>().tabanca_skins[i];
                }
            }
        }
        if (GameObject.FindGameObjectWithTag("bicak1") != null)
        {
            for (int i = 0; i < 11; i++)
            {
                if (PlayerPrefs.GetInt("bıçak_" + i.ToString() + "_kusanıldı") == 1)
                {
                    GameObject.FindGameObjectWithTag("bicak1").GetComponent<SkinnedMeshRenderer>().material = GameObject.Find("asdf").GetComponent<oyunmagaza>().bıçak_skins[i];
                }
            }
        }




    }
    //public void renkikincil()
    //{


    //    if (GameObject.FindGameObjectWithTag("ikincil1") != null)
    //    {
    //        for (int i = 0; i < 7; i++)
    //        {
    //            if (ikincil11 == i)
    //            {

    //                GameObject.FindGameObjectWithTag("ikincil1").GetComponent<SkinnedMeshRenderer>().material.color = colors[i];

    //            }
    //        }
    //    }
    //}
    //public void renkbicak()
    //{
    //    if (GameObject.FindGameObjectWithTag("bicak1") != null)
    //    {
    //        for (int i = 0; i < 7; i++)
    //        {
    //            if (bicak11 == i)
    //            {

    //                GameObject.FindGameObjectWithTag("bicak1").GetComponent<SkinnedMeshRenderer>().material.color = colors[i];

    //            }
    //        }
    //    }
    //}
    void GetTeamScores ()
    {
		teamAScore = (int)PhotonNetwork.room.customProperties["TeamAScore"];
		teamBScore = (int)PhotonNetwork.room.customProperties["TeamBScore"];
        GameObject.Find("asdf").GetComponent<Scoreboard>().maviskor.text = teamAScore.ToString();
        GameObject.Find("asdf").GetComponent<Scoreboard>().kırmızıskor.text = teamBScore.ToString();
    }

	public void RefreshPlayerList ()
    {
		PhotonPlayer[] playerList = PhotonNetwork.playerList;
		
		teamAPlayers.Clear();
		teamBPlayers.Clear();
		spectatorNames = "";
		
		for(int i = 0; i < playerList.Length; i++)
        {
			int playerTeamTmp = playerList[i].customProperties["Team"] != null ? (int)playerList[i].customProperties["Team"] : 0;
			
			if(playerTeamTmp == 0 || playerTeamTmp < 1 || playerTeamTmp > 2)
            {
				if(spectatorNames != "")
                {
					spectatorNames += ", ";
				}
				spectatorNames += playerList[i].name;
			}
            else
            {
				if(playerTeamTmp == 1)
                {
					teamAPlayers.Add(playerList[i]);
				}
                else
                {
					teamBPlayers.Add(playerList[i]);
				}
			}
		}
		
		teamAPlayers.Sort(SortPlayers);
		teamBPlayers.Sort(SortPlayers);

		if(PhotonNetwork.player.customProperties["Team"] != null)
        {
			ourTeam = (int)PhotonNetwork.player.customProperties["Team"];
			GameSettings.ourTeam = ourTeam;
		}
        GameObject.Find("asdf").GetComponent<Scoreboard>().skortahtasıA(teamAPlayers);
        GameObject.Find("asdf").GetComponent<Scoreboard>().skortahtasıB(teamBPlayers);
        GameObject.Find("asdf").GetComponent<Scoreboard>().takımbutonları();
        GameObject.Find("asdf").GetComponent<Scoreboard>().oyunodası_ismi.text = PhotonNetwork.room.name;
        GameObject.Find("asdf").GetComponent<Scoreboard>().oyuncusayısı.text = PhotonNetwork.room.playerCount.ToString() + "/" + PhotonNetwork.room.maxPlayers.ToString() ;
    }

	void RefreshPing ()
    {
		Hashtable setPlayerProperties = new Hashtable();
		setPlayerProperties.Add("Ping", (int)PhotonNetwork.GetPing());
		PhotonNetwork.player.SetCustomProperties(setPlayerProperties);
	}

	public void ResetGameStatus (int statusIndex, PhotonPlayer wp)
    {

		referenceTime = (float)PhotonNetwork.time;
		Hashtable setRoomProperties = new Hashtable();
		setRoomProperties.Add("ReferenceTime", referenceTime);
		setRoomProperties.Add("GameStatus", statusIndex); //0 = Play, 1 = team A won, 2 = team B won, 3 = Draw
		if(statusIndex == 0)
        {
			setRoomProperties.Add("TeamAScore", (int)0);
			setRoomProperties.Add("TeamBScore", (int)0);
		}
		setRoomProperties.Add("WinningPlayer", wp); //Used only for FFA (Fre For All) mode to notify other the winning player
		PhotonNetwork.room.SetCustomProperties(setRoomProperties);
        
        
        //GameSettings.switchWeaponIndex = 1;
        //GameObject.Find("asdf").GetComponent<RoomUI>().silahdegistir = 0;
        //GameObject.Find("asdf").GetComponent<Scoreboard>().silahdegistir_ = 0;
        // GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(true);
        GameObject.Find("asdf").GetComponent<Scoreboard>().atesetamk();
        GameObject.Find("asdf").GetComponent<Scoreboard>().atesdursunamk();
        if (GameObject.Find("asdf").GetComponent<Scoreboard>().oyunmodu == 0)
        {
            GameObject.Find("asdf").GetComponent<Scoreboard>().takımsecmecanvası.SetActive(false);
        }
        else if(GameObject.Find("asdf").GetComponent<Scoreboard>().oyunmodu == 1)
        {
            GameObject.Find("asdf").GetComponent<Scoreboard>().takımsecmecanvası.SetActive(false);
        }
        PlayerPrefs.SetInt("beraberesonuc", 0);
        GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(false);
        GameObject.Find("asdf").GetComponent<Scoreboard>().seçeneklercanvası.SetActive(false);
        GameObject.Find("asdf").GetComponent<Scoreboard>().scoreboardcanvası.SetActive(false);
        GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
        GameObject.Find("asdf").GetComponent<Scoreboard>().kupa_aldımı = 0;
        if (GameSettings.mobileAiming == true)
        {
            GameSettings.mobileAiming = true;
        }
        //Invoke("kapan", 0.1f);
        GameSettings.switchWeaponIndex = 1;
        GameObject.Find("_RoomController(Clone)").GetComponent<RoomUI>().silahdegistir = 1;
        GameObject.Find("asdf").GetComponent<Scoreboard>().silahdegistir_ = 1;

       

        //if (PlayerPrefs.GetInt("oyunmod_") == 0)
        //{
        //    GameObject.Find("asdf").GetComponent<Scoreboard>().rastgeletakımsecme();
        //}
        //Invoke("renkbirincil", 0.01f);
    }

	// Update is called once per frame
    public void canvas()
    {
        
    }
    public void secenekler()
    {

    }
    public void takımsecmecanvası()
    {

    }
    void Update ()
    {
		if(!doneSetup)
			return;

		if(Input.GetKeyDown(KeyCode.Tab))
        {
            ShowScoreboard();
		}

		os.enabled = showOptions;
		sb.enabled = showScoreBoard;
		bm.enabled = showBuyMenu;
        GameObject.Find("asdf").GetComponent<Scoreboard>().takımbutonları();

        if (Input.GetKeyDown(GameSettings.playerKeys[13]))
        {
            //print ("Buy menu key pressed");
            OpenBuyMenu();
		}
        if(GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.active==true)
        {
            GameObject.Find("asdf").GetComponent<position>().solateştuşu_real.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("solateskonum_x"), PlayerPrefs.GetFloat("solateskonum_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().sagatestusu_real.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sagateskonum_x"), PlayerPrefs.GetFloat("sagateskonum_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().hedefalmatusu_real.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("hedefalma_x"), PlayerPrefs.GetFloat("hedefalma_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().zıplamatusu_real.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("zıplama_x"), PlayerPrefs.GetFloat("zıplama_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().egilmetusu_real.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("egilme_x"), PlayerPrefs.GetFloat("egilme_y"), 0);
            GameObject.Find("asdf").GetComponent<position>().sarjortusu_real.rectTransform.position = new Vector3(PlayerPrefs.GetFloat("sarjor_x"), PlayerPrefs.GetFloat("sarjor_y"), 0);
            GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
            //if(kapat_amk_shadow==0)
            //{
            //    GameObject.Find("ActionTextShadow").SetActive(false);
            //    kapat_amk_shadow = 1;
            //}
            


            //GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
        }
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
        if (GameObject.Find("asdf").GetComponent<Scoreboard>().seçeneklercanvası.active == true)
        {
            GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(false);
            //GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
        }
        if (GameObject.Find("asdf").GetComponent<Scoreboard>().takımsecmecanvası.active == true)
        {
            GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(false);
            //GameObject.Find("asdf").GetComponent<Scoreboard>().ffa_kazanancanvası.SetActive(false);
        }

        //When some menu opened, block mouse views, walking, shooting etc.
        GameSettings.menuOpened = showScoreBoard || showBuyMenu || mc.chatState != MultiplayerChat.ChatState.None || showOptions;

		if(!ourPlayer)
        {
			eneteredBuySpot = false;
		}

		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if( timeleft <= 0.0 )
        {
			// display two fractional digits (f2 format)
			//fps = (accum/frames).ToString("f2") + "FPS";
			GameSettings.currentFPS = accum/frames;
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0;
		}

		//Gradually reduce crosshair distance
		distance = Mathf.Lerp(distance, 5, Time.deltaTime * 10.5f);

		//Lock cursor when needed
		if(showScoreBoard || showBuyMenu || showOptions)
        {
			if(lockState != 0)
            {
				lockState = 0;
				LockCursor(false);
			}
		}
        else
        {
			if(lockState != 1)
            {
				lockState = 1;
				LockCursor(true);
			}
		}
        Invoke("renkbirincil", 0);
    }

    public void ShowScoreboard()
    {
        showScoreBoard = !showScoreBoard;
        showBuyMenu = false;
        showOptions = false;

        if (showScoreBoard)
        {
            RefreshPlayerList();
        }
    }

    public void OpenBuyMenu()
    {
        if (!ourPlayer || timeToPurchase > 0)
        {
            //print ("Buy menu key pressed");
            showBuyMenu = !showBuyMenu;
            showScoreBoard = false;
            showOptions = false;
            bm.buySection = BuyMenu.BuySection.Primary;
        }
    }

	void LockCursor (bool lockCursor)
    {

#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WP8 && !UNITY_WP8_1
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
		Cursor.visible = !lockCursor;
#endif

    }
    public void vadi_kazandı_kim()
    {
        RefreshPlayerList();
        if (currentGameMode == "FFA" && PhotonNetwork.isMasterClient && currentGameStatus == 0)
        {
            if (teamAPlayers.Count > 0 && (float)PhotonNetwork.time - referenceTime > 10)
            {
                
                    int tmpGameState = 3;
                for(int i =0;i< teamAPlayers.Count;i++)
                {
                    if(teamAPlayers[i].name== GameObject.Find("asdf").GetComponent<Scoreboard>().oyuncuname)
                    {
                        GameObject.Find("asdf").GetComponent<Scoreboard>().leaderboard_kacıncısıra = i;
                    }
                }
               
                    ResetGameStatus(tmpGameState, teamAPlayers[GameObject.Find("asdf").GetComponent<Scoreboard>().leaderboard_kacıncısıra]);
                
                   
                
            }
        }
      
    }
	void FixedUpdate ()
    {
		if(!doneSetup)
			return;

		if(actionReports.Count > 0)
        {
			for(int i = 0; i < actionReports.Count; i++)
            {
				if(actionReports[i].timer > 0)
                {
					actionReports[i].timer -= Time.deltaTime;
				}
                else
                {
					actionReports.RemoveAt(i);
                    GameSettings.updateActionReports = true;
                }
			}
		}

		//Room logic, track round time
		if(currentGameStatus == 0)
        {
			currentRoundTime = roundDuration - Mathf.Round(((float)PhotonNetwork.time - referenceTime)*10)/10;   //roundDuration

          

            seconds = Mathf.FloorToInt(Mathf.CeilToInt(currentRoundTime) % 60);
			minutes = Mathf.FloorToInt((Mathf.CeilToInt(currentRoundTime) / 60) % 60);
			roundTimeString = string.Format("{0:00}:{1:00}", minutes, seconds);
            
			if(PhotonNetwork.isMasterClient)
            {
				if(currentRoundTime < 1 && PhotonNetwork.time > 0 && referenceTime > 0)
                {
					//Round time ended, check who won
					int tmpGameState = 3;
					PhotonPlayer tmpWinningPlayer = null;

					if(currentGameMode == "TDM")
                    {

						GetTeamScores ();

						if(teamAScore > teamBScore)
                        {
							tmpGameState = 1;
						}
						if(teamAScore < teamBScore)
                        {
							tmpGameState = 2;
						}
					}
                    


                        if (currentGameMode == "FFA")
                        {
                            RefreshPlayerList();
                            tmpWinningPlayer = teamAPlayers.Count > 0 ? teamAPlayers[0] : null;
                        }
                    
					ResetGameStatus(tmpGameState, tmpWinningPlayer);
				}
                else
                {
					//Ensure that reference time always set
					if(referenceTime < 1)
                    {
						referenceTime = (float)PhotonNetwork.time;
						Hashtable setRoomProperties = new Hashtable();
						setRoomProperties.Add("ReferenceTime", referenceTime);
						PhotonNetwork.room.SetCustomProperties(setRoomProperties);
						print ("Reference time missing, setting it again");
					}
				}
			}
		}
        else
        {
			////Interval before next round
			//currentRoundTime = 10 - Mathf.Round(((float)PhotonNetwork.time - referenceTime) * 10) / 10;
			//roundTimeString = "00:00";

			//if(PhotonNetwork.isMasterClient)
   //         {
			//	if(currentRoundTime < 1)
   //             {
			//		//Restart round
			//		ResetGameStatus(0, null);
                    
                    
   //             }
			//}
		}
	}

	//SPAWN PLAYER CONTROLLER ###############################################################################################################################################
	public void PrepareRespawn (int team, bool wasSwitched)
    {
		//playerKilled = true;
		
		if(wasSwitched)
        {
			showScoreBoard = false;
			showBuyMenu = false;
			showOptions = false;
		}
        else
        {
			//Reset weapons if we got killed
			bm.ResetSelectedWeapons();
		}
		
		if(team < 0)
        {
			team = Mathf.Abs(team);
			Transform targetTmp = null;

			//print ("Killer ID: " + team.ToString());

			//Clear empty slots
			for(int i = otherPlayers.Count - 1; i >= 0 ; i--)
            {
				if(otherPlayers[i] == null)
                {
					otherPlayers.RemoveAt(i);
				}
                else
                {
					if(otherPlayers[i].playerID == team)
                    {
						targetTmp = otherPlayers[i].playerAudio.transform;
						//print ("Target player ID: " + otherPlayers[i].playerID.ToString());
					}
				}
			}

			if(ourPlayer)
            {
				welcomeCameraTransform.position = ourPlayer.thisT.position;
				if(targetTmp == null)
                {
					targetTmp = ourPlayer.playerAudio.transform;
				}
			}

			welcomeCamera.gameObject.SetActive(true);
			cameraMouseLook.AssignTarget(targetTmp);
		}
        else
        {
			if(ourPlayer)
            {
				PhotonNetwork.Destroy(ourPlayer.gameObject);
			}
			
			welcomeCamera.transform.position = defaultCamPos;
			welcomeCamera.transform.rotation = defaultCamRot;
			welcomeCamera.gameObject.SetActive(true);
			
			Hashtable setPlayerProperties = new Hashtable();
			setPlayerProperties.Add("Team", team); //0 = Spectators, 1 = teamA, 2 = teamB
			setPlayerProperties.Add("PlayerHP", (int)-1);
			PhotonNetwork.player.SetCustomProperties(setPlayerProperties);
		}

		if(currentGameStatus == 0)
        {
			this.StopCoroutine("PrepareRespawnCoroutine");
			this.StartCoroutine("PrepareRespawnCoroutine");
		}

		if(wasSwitched)
        {
			JoinedTeam(team);
		}

	}

	IEnumerator PrepareRespawnCoroutine ()
    {
		currentRespawnTime = waitBeforeRespawn;

		while(currentRespawnTime > 1)
        {
			currentRespawnTime --;
            

            yield return new WaitForSeconds(1);
           // Invoke("renkdegistir", 0);
        
    }

		SpawnPlayer((int)PhotonNetwork.player.customProperties["Team"]);
        
            
    }
    
	public void SpawnPlayer (int team)
    {
		if(ourPlayer)
        {
			PhotonNetwork.Destroy(ourPlayer.gameObject);
		}
		
		if(team == 1 || team == 2)
        {
			Transform spawnPontTmp = null;

			if(currentGameMode == "TDM")
            {
				spawnPontTmp = team == 1 ? teamASpawnPoints[Random.Range(0,  teamASpawnPoints.Count - 1)] : teamBSpawnPoints[Random.Range(0,  teamBSpawnPoints.Count - 1)];
			}

			if(currentGameMode == "FFA")
            {
				//For FFA mode with use every available spawn point
				int rndTmp = Mathf.Abs(Random.Range(- (teamASpawnPoints.Count - 1 + teamBSpawnPoints.Count - 1), teamASpawnPoints.Count - 1 + teamBSpawnPoints.Count - 1));
				if(rndTmp < teamASpawnPoints.Count)
                {
					spawnPontTmp = teamASpawnPoints[rndTmp];
				}
                else
                {
					spawnPontTmp = teamBSpawnPoints[rndTmp - teamASpawnPoints.Count];
				}
			}

			GameObject ourPlayerTmp = PhotonNetwork.Instantiate(playerPrefab.name, spawnPontTmp.position, spawnPontTmp.rotation, 0);
			ourPlayer = ourPlayerTmp.GetComponent<PlayerNetwork>();

			bm.lastSelectedWeapon = 1;
			bm.Invoke("ApplySelectedWeapons", 0.035f);
            
            welcomeCamera.gameObject.SetActive(false);
            //Invoke("renkdegistir", 0.1f);

            //this.StopCoroutine("PurchaseTimer");
            //this.StartCoroutine("PurchaseTimer");
        }
        else
        {
			welcomeCameraTransform.position = defaultCamPos;
			welcomeCameraTransform.rotation = defaultCamRot;
			welcomeCamera.gameObject.SetActive(true);
			cameraMouseLook.AssignTarget(null);
			team = 0;
		}
		
		if(PhotonNetwork.player.customProperties["Team"] == null || (int)PhotonNetwork.player.customProperties["Team"] != team)
        {
			Hashtable setPlayerProperties = new Hashtable();
			setPlayerProperties.Add("Team", team); //0 = Spectators, 1 = teamA, 2 = teamB
			PhotonNetwork.player.SetCustomProperties(setPlayerProperties);
		}

		currentRespawnTime = -1;
        
            
        
    }

	IEnumerator PurchaseTimer ()
    {
		//Allow some time (in seconds) to allow newly spawned player purchase weapon)
		timeToPurchase  = 30;

		while(timeToPurchase > 0)
        {
			timeToPurchase --;
			yield return new WaitForSeconds(1);
		}
	}
	//SPAWN PLAYER CONTROLLE REND ############################################################################################################################################
	
	//PHOTON NETWORK CALLBACKS ############################################################################################################################################
	void OnLeftRoom()
    {
		//Back to MainMenu scene
        SceneManager.LoadScene(0);
    }

	void OnPhotonPlayerPropertiesChanged(/*object[] playerAndUpdatedProps*/)
    {
		//PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
		//Hashtable props = playerAndUpdatedProps[1] as Hashtable;

		RefreshPlayerList();

		if(PhotonNetwork.player.customProperties["PlayerHP"] != null)
        {
			currentHP = (int)PhotonNetwork.player.customProperties["PlayerHP"];
		}

        //print ("Round is already at: " + ((float)PhotonNetwork.time - referenceTime).ToString() + " seconds");
        
            if (currentGameMode == "FFA" && PhotonNetwork.isMasterClient && currentGameStatus == 0)
            {
                if (teamAPlayers.Count > 0 && (float)PhotonNetwork.time - referenceTime > 10)
                {
                    if ((int)teamAPlayers[0].customProperties["Kills"] >= currentKillLimit)
                    {
                        int tmpGameState = 3;
                        ResetGameStatus(tmpGameState, teamAPlayers[0]);
                    }
                }
            
        }
	}

	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    { 
		//Display notification that player connected
		PostActivityRemote( "", newPlayer.name + " odaya katıldı", "", 0, 0);
	}

	void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    { 
		RefreshPlayerList();

		//Display notification that player disconnected
		PostActivityRemote( "", otherPlayer.name + " odadan ayrıldı", "", 0, 0);
	}

	void OnPhotonCustomRoomPropertiesChanged(/*Hashtable propertiesThatChanged*/)
    {
		GetTeamScores ();
		referenceTime = (float)PhotonNetwork.room.customProperties["ReferenceTime"];
		currentGameStatus = (int)PhotonNetwork.room.customProperties["GameStatus"];
		currentGameMode = (string)PhotonNetwork.room.customProperties["GameMode"];


        
        //Used only for FFA mode
        
            if (PhotonNetwork.room.customProperties["KillLimit"] != null)
            {
                currentKillLimit = (int)PhotonNetwork.room.customProperties["KillLimit"];
            }

            if (PhotonNetwork.room.customProperties["WinningPlayer"] != null)
            {
                winningPlayer = (PhotonPlayer)PhotonNetwork.room.customProperties["WinningPlayer"];
            }
            else
            {
                winningPlayer = null;
            }
        
		if(currentGameStatus == 1 || currentGameStatus == 2 || currentGameStatus == 3)
        {
			//Round ended
			this.StopCoroutine("PrepareRespawnCoroutine");
            
            if (previousGameStatus != currentGameStatus)
            {
				if(ourPlayer)
                {
					PhotonNetwork.Destroy(ourPlayer.gameObject);
				}

				welcomeCamera.transform.position = defaultCamPos;
				welcomeCamera.transform.rotation = defaultCamRot;
				welcomeCamera.gameObject.SetActive(true);

				showScoreBoard = false;

				if(winningPlayer != null && winningPlayer == PhotonNetwork.player)
                {
					//Wee won, add award
					StartCoroutine(AddCashDelayed());
				}

				previousGameStatus = currentGameStatus;
                
            }
            
        }

		//Round was restarted, reset Kills/Deaths and Spawn our player if needed
		if(currentGameStatus == 0 && previousGameStatus != 0)
        {
			if(PhotonNetwork.isMasterClient)
            {
				//Reset kills and deaths for every player
				StopCoroutine("ResetPlayersKillsDeaths");
				StartCoroutine("ResetPlayersKillsDeaths");
			}

			//Recheck our team
			if(PhotonNetwork.player.customProperties["Team"] != null)
            {
				ourTeam = (int)PhotonNetwork.player.customProperties["Team"];
				GameSettings.ourTeam = ourTeam;
			}

			if(ourTeam == 1 || ourTeam == 2)
            {
				SpawnPlayer(ourTeam);
			}

			previousGameStatus = 0;
		}
	}

	IEnumerator ResetPlayersKillsDeaths ()
    {
		while(PhotonNetwork.playerList.Length == 0)
        {
			yield return null;
		}

		PhotonPlayer[] playersTmp = PhotonNetwork.playerList;

		Hashtable setPlayerProperties = new Hashtable();
		setPlayerProperties.Add("Kills", (int)0);
		setPlayerProperties.Add("Deaths", (int)0);

		for(int i = 0; i < playersTmp.Length; i++)
        {
			playersTmp[i].SetCustomProperties(setPlayerProperties);
		}
	}

	IEnumerator AddCashDelayed ()
    {
		yield return new WaitForSeconds(0.35f);
		AddKillCash(-1);
	}
	//PHOTON NETWORK CALLBACKS END ########################################################################################################################################

	//ROOM ACTIVITY REPORTS ################################################################################################################################################
	void JoinedTeam (int team)
    {
		//team = 0 - spectators, 1 - team A, 2 - team B
		string joinedTeam = "Spectators";
		int colorRef = 0;

		if(team == 1 || team == 2)
        {
			joinedTeam = team == 1 ? GameSettings.teamAName : GameSettings.teamBName;
			colorRef = team;
		}

		photonView.RPC("PostActivityRemote", PhotonTargets.All, "", PhotonNetwork.playerName + " katıldı", joinedTeam, 0, colorRef);
	}

	public void ReportKill (string killedName, string weaponName, int killedTeam)
    {
		photonView.RPC("PostActivityRemote", PhotonTargets.All,  PhotonNetwork.playerName, weaponName, killedName, ourTeam, killedTeam);
        Debug.Log(PhotonNetwork.playerName + " " + weaponName + " " + killedName);



    }

	[PunRPC]
	void PostActivityRemote (string leftText, string middleText, string rightText, int leftColorRef, int rightColorRef)
    {
		Color leftColorTmp = GameSettings.HUDColor;
		Color rightColorTmp = GameSettings.HUDColor;

		if(leftColorRef == 1 || leftColorRef == 2)
        {
			leftColorTmp = leftColorRef == 1 ? GameSettings.teamAColor : GameSettings.teamBColor;
		}

		if(rightColorRef == 1 || rightColorRef == 2)
        {
			rightColorTmp = rightColorRef == 1 ? GameSettings.teamAColor : GameSettings.teamBColor;
		}

		actionReports.Add(new ActionReport(leftText, middleText, rightText, leftColorTmp, rightColorTmp, 15));

		if(actionReports.Count > GameSettings.actionReportsLimit)
        {
			actionReports.RemoveAt(0);
        }

        GameSettings.updateActionReports = true;
    }
	//ROOM ACTIVITY REPORTS END ############################################################################################################################################

	//Player HUD controllers
	public void DoHitDetector (int direction)
    {

		if(direction > 0 && direction < 5)
        {
			if(direction == 1)
            {
				hitTopFade = 1;
			}
			if(direction == 2)
            {
				hitBottomFade = 1;
			}
			if(direction == 3)
            {
				hitLeftFade = 1;
			}
			if(direction == 4)
            {
				hitRightFade = 1;
			}
		}else{
			hitTopFade = 1;
			hitBottomFade = 1;
			hitLeftFade = 1;
			hitRightFade = 1;
		}

		redScreenFade = 1;

		if(!doingHitDetector)
        {
			StartCoroutine(DoHitDetectorCoroutine());
		}
	}

	IEnumerator DoHitDetectorCoroutine()
    {
		doingHitDetector = true;

		while(redScreenFade > 0.01f)
        {
			redScreenFade = Mathf.Lerp(redScreenFade, 0, Time.deltaTime * 5);
			hitTopFade = Mathf.Lerp(hitTopFade , 0, Time.deltaTime * 5);
			hitBottomFade = Mathf.Lerp(hitBottomFade, 0, Time.deltaTime * 5);
			hitLeftFade = Mathf.Lerp(hitLeftFade , 0, Time.deltaTime * 5);
			hitRightFade = Mathf.Lerp(hitRightFade, 0, Time.deltaTime * 5);

			yield return null;
		}

		doingHitDetector = false;
		redScreenFade = 0;
		hitTopFade = 0;
		hitBottomFade = 0;
		hitLeftFade = 0;
		hitRightFade = 0;
	}

	public void  AddKillCash (int bodyPart)
    {
		if(bodyPart < -1)
			return;

		//bodyPart 0 = head. 1 = torso, 2 = limbs
		scoreToAddTmp = "";

		if(bodyPart == -1)
        {
			//Add winning award cash, used for FFA (Free For All) mode
			scoreToAddTmp = "500";
			totalCash -= 500;
		}
        else
        {
            
			if(bodyPart == 0)
            {
				//Add cash for headshot
				scoreToAddTmp = "150";
				totalCash -= 150;
			}
            else
            {
				//Add cash for normal kill
				scoreToAddTmp = "100";
				totalCash -= 100;
			}
		}

		currentAddingCashColor = addingColor;
		currentTotalCashColor = addingColor;

		/*audioSource.Stop();
		audioSource.clip = cashRegisterSound;
		audioSource.Play();*/

		if(!addingKillCash && scoreToAddTmp != "")
        {
			StartCoroutine(AddKIllCashCoroutine ());
		}
	}

	public void SubstractCash (int type)
    {
		//Substract cash when refilling ammo or purchasing new Weapon. "type" variable used to detect whether it's secondary primay etc.
		scoreToAddTmp = "";

		if(type == -1)
        {
			//Substracting cash for refilling ammo
			if(GetCash() >= 100)
            {
				scoreToAddTmp = "100";
				totalCash += 100;
				//Play buy sound
				audioSource.Stop();
				audioSource.clip = buySound;
				audioSource.Play();
			}
		}
        else
        {
			if(bm.GetWeaponCost(type) > -1)
            {
				scoreToAddTmp = bm.GetWeaponCost(type).ToString();
				totalCash += bm.GetWeaponCost(type);

				audioSource.Stop();
				audioSource.clip = cashRegisterSound;
				audioSource.Play();
			}
		}
		
		currentAddingCashColor = substractingColor;
		currentTotalCashColor = substractingColor;

		if(!addingKillCash /*&& scoreToAddTmp != ""*/)
        {
			StartCoroutine(AddKIllCashCoroutine ());
		}
	}

	public int GetCash ()
    {
		return GameSettings.cnst - totalCash;
	}

	IEnumerator AddKIllCashCoroutine ()
    {
		addingKillCash = true;

		addingColorFadeTo = new Color(currentAddingCashColor.r, currentAddingCashColor.g, currentAddingCashColor.b, 0);

		while(Mathf.Abs(((Vector4)GameSettings.HUDColor).magnitude - ((Vector4)currentTotalCashColor).magnitude) > 0.01f)
        {
			currentTotalCashColor = Color.Lerp(currentTotalCashColor, GameSettings.HUDColor, Time.deltaTime * 0.5f);
			currentAddingCashColor = Color.Lerp(currentAddingCashColor, addingColorFadeTo, Time.deltaTime * 0.5f);
			yield return null;
		}

		currentAddingCashColor = addingColorFadeTo;
		currentTotalCashColor = GameSettings.HUDColor;
		addingKillCash = false;
	}
}
