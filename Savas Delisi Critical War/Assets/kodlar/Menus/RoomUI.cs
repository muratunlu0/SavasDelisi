

using UnityEngine;
using UnityEngine.UI;
using System;
public class RoomUI : MonoBehaviour
{

    public Sprite whiteTexture;
    public Sprite hitDetectorTexture;

#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
    //Mobile controller graphics
    public Sprite navigationCircle;
    public Sprite navigationButton;
    //public Sprite gunFireIcon;
    //public Sprite gunReloadIcon;
    //public Sprite gunAimIcon;
    //public Sprite jumpIcon;
    //public Sprite switchWeaponIcon;
#endif
    public int silahdegistir;
    int silahdegistirr;
    public int oyuniçişimşek;
    public Text öldüren_kisi;
    public Text ölen_kisi;

    //Crosshair
    GameObject crosshairRoot;
    Image[] crosshairSet = new Image[4];
    Image redScreen;
    Image[] hitDetectorSet = new Image[4];

    bool previousHitDetector = false;

    //General HUD - this will include general graphics like Round Time, Ammo, HP, Buy Menu buttons etc.
    public Canvas mainCanvas;

    Text roundTimeText;
    Text HPText;
    Text cashText;
    Text addMoreCashText;
    Text weaponAndAmmoText;

    [System.Serializable]
    public class ActionReports
    {
        public Text mainText;
        public Text textShadow;
    }

    ActionReports[] actionReports;
    ActionReports[] chatMessages;

    ActionReports respawnTimeText = new ActionReports();
    // ActionReports buyMenuText = new ActionReports();
    ActionReports refillAmmoText = new ActionReports();

    Image sniperScope;
    float scopeTextureRatio;
    Sprite scopeTextureTmp;

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
    int refillAmmoTapID = -1;

    //Mobile movement
    [System.Serializable]
    public class TouchMovements
    {
        public Image backgroundCircle;
        public Image mainButton;
        public Rect defaultArea;
        public Vector2 touchOffset;
        public Vector2 currentTouchPos;
        public int touchID;
        public bool isActive = false;
    }

    TouchMovements moveTouch = new TouchMovements();
    TouchMovements fpsLookTouch = new TouchMovements();

    [System.Serializable]
    public class ActionButton
    {
        public Image background;
        public Image icon;
        public Rect mainArea;
        public bool isActive = false;
        public int touchID;
    }

    ActionButton fireButton = new ActionButton();
    ActionButton reloadButton = new ActionButton();
    ActionButton aimButton = new ActionButton();
    ActionButton jumpButton = new ActionButton();

    ActionButton switchWeaponButton = new ActionButton();

    //Implement fast swiping to look around
    Vector2 initialTouchPos = Vector3.zero;
    float swipeTime = 0;
    //How long to keep rotating after we swiped
    float keepRotatingTime = 0;
    float previousTouchDirX = 0;

#endif

    RoomController rc;

    odaui oi;
    // Use this for initialization
    void Start()
    {
        //GameObject.FindGameObjectWithTag("joystick").SetActive(false);
        //GameObject.FindGameObjectWithTag("joystickcemberi").SetActive(false);
        rc = GetComponent<RoomController>();
        //oi = GetComponent<odaui>();
        oyuniçişimşek = GameSettings.cnst - rc.totalCash;
        actionReports = new ActionReports[GameSettings.actionReportsLimit];
        chatMessages = new ActionReports[GameSettings.chatMessagesLimit];
        //Use new Unity UI
        InitializeGUI();

        
        mainCanvas.gameObject.SetActive(false);
        // GameObject.FindGameObjectWithTag("canvas").SetActive(false);
    }
    public void kontrolpaneliniac()
    {
        mainCanvas.gameObject.SetActive(true);
        GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(true);
    }
    public void kontrolpanelinikapa()
    {
        mainCanvas.gameObject.SetActive(false);
        GameObject.Find("asdf").GetComponent<Scoreboard>().controlkanvasi.SetActive(false);
    }
    void InitializeGUI()
    {
        //This function will initialize general GUI elements (HP, Cash, Round time and mobile controls)
        GameObject tmpObj = new GameObject("Canvas");
        tmpObj.transform.position = new Vector3(0, 0, 0);
        //tmpObj.transform.parent = this.transform;
        mainCanvas = tmpObj.AddComponent<Canvas>();
        mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        mainCanvas.pixelPerfect = true;



        CanvasScaler canvasScaled = tmpObj.AddComponent<CanvasScaler>();
        canvasScaled.scaleFactor = 1;
        canvasScaled.referencePixelsPerUnit = 100;

        tmpObj.AddComponent<GraphicRaycaster>();

        //Round Time
        GameObject hpTmpObj = new GameObject("RoundTimeText");
        hpTmpObj.transform.position = Vector3.zero;
        hpTmpObj.transform.parent = tmpObj.transform;
        //roundTimeText = hpTmpObj.AddComponent<Text>();
        //roundTimeText.font = GameSettings.guiSkin.font;
        //roundTimeText.fontSize = 27;
        //roundTimeText.color = Color.black;//GameSettings.HUDColor;
        //roundTimeText.text = "00:00";
        //roundTimeText.rectTransform.sizeDelta = new Vector2(100, 50);
        //roundTimeText.rectTransform.anchorMin = new Vector2(0.5f, 1);
        //roundTimeText.rectTransform.anchorMax = new Vector2(0.5f, 1);
        //roundTimeText.rectTransform.pivot = new Vector2(0.5f, 1);
        //roundTimeText.rectTransform.position = new Vector3(Screen.width / 2, Screen.height - 15, 0);
        //roundTimeText.alignment = TextAnchor.UpperCenter;



        //Player HP
        //hpTmpObj = new GameObject("HPText");
        //hpTmpObj.transform.position = Vector3.zero;
        //hpTmpObj.transform.parent = tmpObj.transform;
        //HPText = hpTmpObj.AddComponent<Text>();
        //HPText.font = GameSettings.guiSkin.font;
        //HPText.fontSize = 22;
        //HPText.color = Color.white;//GameSettings.HUDColor;
        //HPText.text = "";
        //HPText.rectTransform.sizeDelta = new Vector2(100, 50);
        //HPText.rectTransform.anchorMin = new Vector2(0, 0);
        //HPText.rectTransform.anchorMax = new Vector2(0, 0);
        //HPText.rectTransform.pivot = new Vector2(0, 0);
        //HPText.rectTransform.position = new Vector3(10, 10, 0);
        //HPText.alignment = TextAnchor.LowerLeft;

        //Player Cash
        hpTmpObj = new GameObject("CashText");
        hpTmpObj.transform.position = Vector3.zero;
        hpTmpObj.transform.parent = tmpObj.transform;
        cashText = hpTmpObj.AddComponent<Text>();
        cashText.font = GameSettings.guiSkin.font;
        cashText.fontSize = 22;
        cashText.color = GameSettings.HUDColor;
        cashText.text = "";
        cashText.rectTransform.sizeDelta = new Vector2(200, 50);
        cashText.rectTransform.anchorMin = new Vector2(0, 0);
        cashText.rectTransform.anchorMax = new Vector2(0, 0);
        cashText.rectTransform.pivot = new Vector2(0, 0);
        cashText.rectTransform.position = new Vector3(115, 10, 0);
        cashText.alignment = TextAnchor.LowerLeft;

        //Adding player cash
        GameObject tmpCashObject = Instantiate(cashText.gameObject, Vector3.zero, Quaternion.identity) as GameObject;
        tmpCashObject.name = "AddMoreCashText";
        addMoreCashText = tmpCashObject.GetComponent<Text>();
        addMoreCashText.rectTransform.SetParent(tmpObj.transform);
        addMoreCashText.rectTransform.position = new Vector3(115, 35, 0);

        //Weapon and Ammo
        hpTmpObj = new GameObject("WeaponAndAmmo");
        hpTmpObj.transform.position = Vector3.zero;
        hpTmpObj.transform.parent = tmpObj.transform;
        weaponAndAmmoText = hpTmpObj.AddComponent<Text>();
        weaponAndAmmoText.font = GameSettings.guiSkin.font;
        weaponAndAmmoText.fontSize = 22;
        weaponAndAmmoText.color = Color.white;//GameSettings.HUDColor;
        weaponAndAmmoText.text = "";
        weaponAndAmmoText.rectTransform.sizeDelta = new Vector2(350, 50);
        weaponAndAmmoText.rectTransform.anchorMin = new Vector2(1, 0);
        weaponAndAmmoText.rectTransform.anchorMax = new Vector2(1, 0);
        weaponAndAmmoText.rectTransform.pivot = new Vector2(1, 0);
        weaponAndAmmoText.rectTransform.position = new Vector3(mainCanvas.pixelRect.width - 10, 10, 0);
        weaponAndAmmoText.alignment = TextAnchor.LowerRight;
        weaponAndAmmoText.gameObject.SetActive(false);
        GameObject hpTmpObj1;

        //Setup action reports ###
        for (int i = 0; i < actionReports.Length; i++)
        {
            //Main Text
            hpTmpObj = new GameObject("ActionTextMain");
            actionReports[i] = new ActionReports();
            hpTmpObj.transform.position = Vector3.zero;
            //hpTmpObj.transform.parent = tmpObj.transform;
            actionReports[i].mainText = hpTmpObj.AddComponent<Text>();
            actionReports[i].mainText.font = GameSettings.guiSkin.font;

            actionReports[i].mainText.fontSize = Screen.height / 56;/// 55.384615384;//GameSettings.actionReportStyle.fontSize;
            //Debug.Log(GameSettings.actionReportStyle.fontSize+ "font sayısı");
            //actionReports[i].mainText.

            //actionReports[i].mainText.color = Color.white;
            actionReports[i].mainText.text = "Some text goes here";
            actionReports[i].mainText.rectTransform.sizeDelta = new Vector2(350, 25);

            actionReports[i].mainText.rectTransform.sizeDelta = new Vector2(Screen.height / 2.0571428571428f, Screen.height / 40.8f);

            actionReports[i].mainText.rectTransform.anchorMin = new Vector2(1, 1);
            actionReports[i].mainText.rectTransform.anchorMax = new Vector2(1, 1);
            actionReports[i].mainText.rectTransform.pivot = new Vector2(1, 1);
            actionReports[i].mainText.alignment = TextAnchor.UpperRight;
            actionReports[i].mainText.supportRichText = true;

            hpTmpObj1 = Instantiate(hpTmpObj, Vector3.zero, Quaternion.identity) as GameObject;
            hpTmpObj1.name = "ActionTextShadow";
            hpTmpObj1.transform.SetParent(tmpObj.transform);
            actionReports[i].textShadow = hpTmpObj1.GetComponent<Text>();
            actionReports[i].textShadow.color = Color.white; // GameSettings.textShadowColor;
            actionReports[i].textShadow.supportRichText = false;
            //actionReports[i].textShadow.rectTransform.position = new Vector3(0, 0, 0);
            ///////////////
            actionReports[i].mainText.gameObject.SetActive(false);
            actionReports[i].textShadow.gameObject.SetActive(false);
            ////////////////////
            hpTmpObj.transform.SetParent(hpTmpObj1.transform);
        }
        //###

        //Set chat messages ###
        for (int i = 0; i < chatMessages.Length; i++)
        {
            //Main text
            hpTmpObj = new GameObject("ChatMessageMain");
            chatMessages[i] = new ActionReports();
            hpTmpObj.transform.position = Vector3.zero;
            chatMessages[i].mainText = hpTmpObj.AddComponent<Text>();
            chatMessages[i].mainText.font = GameSettings.guiSkin.font;
            chatMessages[i].mainText.fontSize = GameSettings.actionReportStyle.fontSize;
            chatMessages[i].mainText.color = Color.white;
            chatMessages[i].mainText.text = "Some text goes here";
            chatMessages[i].mainText.rectTransform.sizeDelta = new Vector2(350, 25);
            chatMessages[i].mainText.rectTransform.anchorMin = new Vector2(0, 0);
            chatMessages[i].mainText.rectTransform.anchorMax = new Vector2(0, 0);
            chatMessages[i].mainText.rectTransform.pivot = new Vector2(0, 0);
            chatMessages[i].mainText.alignment = TextAnchor.LowerLeft;
            chatMessages[i].mainText.supportRichText = true;

            hpTmpObj1 = Instantiate(hpTmpObj, Vector3.zero, Quaternion.identity) as GameObject;
            hpTmpObj1.name = "ChatMessageShadow";
            hpTmpObj1.transform.SetParent(tmpObj.transform);
            chatMessages[i].textShadow = hpTmpObj1.GetComponent<Text>();
            chatMessages[i].textShadow.color = Color.white;//GameSettings.textShadowColor;
            chatMessages[i].textShadow.supportRichText = false;
            //chatMessages[i].textShadow.rectTransform.position = new Vector3(0, 0, 0);
            chatMessages[i].mainText.gameObject.SetActive(false);
            chatMessages[i].textShadow.gameObject.SetActive(false);
            hpTmpObj.transform.SetParent(hpTmpObj1.transform);
        }
        //###

        //Setup crosshair
        crosshairRoot = new GameObject("CrosshairRoot");
        crosshairRoot.transform.position = Vector3.zero;
        crosshairRoot.transform.parent = tmpObj.transform;

        for (int i = 0; i < 4; i++)
        {
            hpTmpObj = new GameObject("Crosshair " + i.ToString());
            hpTmpObj.transform.position = Vector3.zero;
            hpTmpObj.transform.parent = tmpObj.transform;
            crosshairSet[i] = hpTmpObj.AddComponent<Image>();
            crosshairSet[i].sprite = whiteTexture;
            crosshairSet[i].color = Color.red;//GameSettings.HUDColor;
            crosshairSet[i].rectTransform.sizeDelta = new Vector2(2, 7);
            crosshairSet[i].rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            crosshairSet[i].rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            crosshairSet[i].rectTransform.pivot = new Vector2(0.5f, 0);
            crosshairSet[i].rectTransform.SetParent(crosshairRoot.transform);
            crosshairSet[i].rectTransform.position = new Vector3(0, 0, 0);

            if (i == 0)
            {
                //Roated to top
                crosshairSet[i].rectTransform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (i == 1)
            {
                //Rotated to bottom
                crosshairSet[i].rectTransform.eulerAngles = new Vector3(0, 0, 180);
            }
            if (i == 2)
            {
                //Rotated to left
                crosshairSet[i].rectTransform.eulerAngles = new Vector3(0, 0, 90);
            }
            if (i == 3)
            {
                //Rotated to right
                crosshairSet[i].rectTransform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
        //###

        //Set hit detectors
        int distanceTmp = 125;
        int detectorWidth = 128;
        int detectorHeight = 31;

        for (int i = 0; i < hitDetectorSet.Length; i++)
        {
            hpTmpObj = new GameObject("HitDetector " + i.ToString());
            hpTmpObj.transform.position = Vector3.zero;
            hpTmpObj.transform.parent = tmpObj.transform;
            hitDetectorSet[i] = hpTmpObj.AddComponent<Image>();
            hitDetectorSet[i].sprite = hitDetectorTexture;
            hitDetectorSet[i].color = new Color(1, 1, 1, 0);
            hitDetectorSet[i].rectTransform.sizeDelta = new Vector2(detectorWidth, detectorHeight);
            hitDetectorSet[i].rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            hitDetectorSet[i].rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            hitDetectorSet[i].rectTransform.pivot = new Vector2(0.5f, 0);
            hitDetectorSet[i].rectTransform.position = new Vector3(0, 0, 0);

            if (i == 0)
            {
                //Roated to top
                hitDetectorSet[i].rectTransform.eulerAngles = new Vector3(0, 0, 0);
                hitDetectorSet[i].rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2, mainCanvas.pixelRect.height / 2 + detectorHeight + distanceTmp, 0);
            }
            if (i == 1)
            {
                //Rotated to bottom
                hitDetectorSet[i].rectTransform.eulerAngles = new Vector3(0, 0, 180);
                hitDetectorSet[i].rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2, mainCanvas.pixelRect.height / 2 - detectorHeight - distanceTmp, 0);
            }
            if (i == 2)
            {
                //Rotated to left
                hitDetectorSet[i].rectTransform.eulerAngles = new Vector3(0, 0, 90);
                hitDetectorSet[i].rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2 - detectorHeight - distanceTmp, mainCanvas.pixelRect.height / 2, 0);
            }
            if (i == 3)
            {
                //Rotated to right
                hitDetectorSet[i].rectTransform.eulerAngles = new Vector3(0, 0, -90);
                hitDetectorSet[i].rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2 + detectorHeight + distanceTmp, mainCanvas.pixelRect.height / 2, 0);
            }
        }

        //Red screen
        //hpTmpObj = new GameObject("Red Screen");
        //hpTmpObj.transform.position = Vector3.zero;
        //hpTmpObj.transform.parent = tmpObj.transform;
        //redScreen = hpTmpObj.AddComponent<Image>();
        //redScreen.sprite = whiteTexture;
        //redScreen.color = new Color(1, 1, 1, 0);
        //redScreen.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        //redScreen.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        //redScreen.rectTransform.sizeDelta = new Vector2(64, 64);
        //redScreen.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        //redScreen.rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2, mainCanvas.pixelRect.height / 2, 0);
        //###

        //Setup respawn time text
        hpTmpObj = new GameObject("RrespawnTimeText");
        hpTmpObj.transform.position = Vector3.zero;
        hpTmpObj.transform.parent = tmpObj.transform;
        respawnTimeText.mainText = hpTmpObj.AddComponent<Text>();
        respawnTimeText.mainText.font = GameSettings.guiSkin.font;
        respawnTimeText.mainText.fontSize = GameSettings.actionReportStyle.fontSize;
        respawnTimeText.mainText.color = Color.white;//GameSettings.HUDColor;
        respawnTimeText.mainText.text = "Respawn time text";
        respawnTimeText.mainText.rectTransform.sizeDelta = new Vector2(350, 25);
        respawnTimeText.mainText.rectTransform.anchorMin = new Vector2(0, 1);
        respawnTimeText.mainText.rectTransform.anchorMax = new Vector2(0, 1);
        respawnTimeText.mainText.rectTransform.pivot = new Vector2(0, 1);
        respawnTimeText.mainText.alignment = TextAnchor.LowerLeft;
        respawnTimeText.mainText.rectTransform.position = new Vector3(10, mainCanvas.pixelRect.height - 55, 0);

        hpTmpObj1 = Instantiate(hpTmpObj, Vector3.zero, Quaternion.identity) as GameObject;
        hpTmpObj1.name = "RrespawnTimeTextShadow";
        hpTmpObj1.transform.SetParent(tmpObj.transform);
        respawnTimeText.textShadow = hpTmpObj1.GetComponent<Text>();
        respawnTimeText.textShadow.color = GameSettings.textShadowColor;
        respawnTimeText.textShadow.rectTransform.position = new Vector3(10 + 1, mainCanvas.pixelRect.height - 55 - 1, 0);

        hpTmpObj.transform.SetParent(hpTmpObj1.transform);
        //###

        //Setup buy menu text ###
        hpTmpObj = new GameObject("BuyMenuText");
        hpTmpObj.transform.position = Vector3.zero;
        hpTmpObj.transform.parent = tmpObj.transform;
        //buyMenuText.mainText = hpTmpObj.AddComponent<Text>();
        //buyMenuText.mainText.font = GameSettings.guiSkin.font;
        //buyMenuText.mainText.fontSize = GameSettings.actionReportStyle.fontSize;
        //buyMenuText.mainText.color = Color.black;// GameSettings.HUDColor;
        //buyMenuText.mainText.text = "Buy Menu Text";
        //buyMenuText.mainText.rectTransform.sizeDelta = new Vector2(400, 50);
        //buyMenuText.mainText.rectTransform.anchorMin = new Vector2(0.5f, 0);
        //buyMenuText.mainText.rectTransform.anchorMax = new Vector2(0.5f, 0);
        //buyMenuText.mainText.rectTransform.pivot = new Vector2(0.5f, 0);
        //buyMenuText.mainText.alignment = TextAnchor.MiddleCenter;
        //buyMenuText.mainText.rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2, 100, 0);

        //hpTmpObj1 = Instantiate(hpTmpObj, Vector3.zero, Quaternion.identity) as GameObject;
        //hpTmpObj1.name = "BuyMenuTextShadow";
        //hpTmpObj1.transform.SetParent(tmpObj.transform);
        //buyMenuText.textShadow = hpTmpObj1.GetComponent<Text>();
        //buyMenuText.textShadow.color = GameSettings.textShadowColor;
        //buyMenuText.textShadow.rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2 + 1, 100 - 1, 0);

        hpTmpObj.transform.SetParent(hpTmpObj1.transform);
        //###

        //Setup refill ammo text ###
        hpTmpObj = new GameObject("RefillAmmoText");
        hpTmpObj.transform.position = Vector3.zero;
        hpTmpObj.transform.parent = tmpObj.transform;
        refillAmmoText.mainText = hpTmpObj.AddComponent<Text>();
        refillAmmoText.mainText.font = GameSettings.guiSkin.font;
        refillAmmoText.mainText.fontSize = GameSettings.actionReportStyle.fontSize;
        refillAmmoText.mainText.color = Color.black;//GameSettings.HUDColor;
        refillAmmoText.mainText.text = "Refill Ammo Text";
        refillAmmoText.mainText.rectTransform.sizeDelta = new Vector2(400, 25);
        refillAmmoText.mainText.rectTransform.anchorMin = new Vector2(0.5f, 0);
        refillAmmoText.mainText.rectTransform.anchorMax = new Vector2(0.5f, 0);
        refillAmmoText.mainText.rectTransform.pivot = new Vector2(0.5f, 0);
        refillAmmoText.mainText.alignment = TextAnchor.MiddleCenter;
        refillAmmoText.mainText.rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2, 150, 0);

        hpTmpObj1 = Instantiate(hpTmpObj, Vector3.zero, Quaternion.identity) as GameObject;
        hpTmpObj1.name = "RefillAmmoTextShadow";
        hpTmpObj1.transform.SetParent(tmpObj.transform);
        refillAmmoText.textShadow = hpTmpObj1.GetComponent<Text>();
        refillAmmoText.textShadow.color = GameSettings.textShadowColor;
        refillAmmoText.textShadow.rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2 + 1, 150 - 1, 0);

        hpTmpObj.transform.SetParent(hpTmpObj1.transform);
        //###

        //Setup sniper scope ###
        hpTmpObj = new GameObject("Sniper Scope");
        hpTmpObj.transform.position = Vector3.zero;
        hpTmpObj.transform.parent = tmpObj.transform;
        sniperScope = hpTmpObj.AddComponent<Image>();
        sniperScope.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        sniperScope.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        sniperScope.rectTransform.sizeDelta = new Vector2(64, 64);
        sniperScope.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        sniperScope.rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2, mainCanvas.pixelRect.height / 2, 0);
        sniperScope.gameObject.SetActive(false);
        //###

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
        //Mobile controls
        int circleSize = 200;
        int buttonSize = 180;
        int marginLeft = 100;
        int marginBottom = 100;

        //Navigation background
        GameObject cntrlTmpObj = new GameObject("Moviment Circle");
        cntrlTmpObj.transform.position = Vector3.zero;
        cntrlTmpObj.transform.parent = tmpObj.transform;
        //moveTouch.backgroundCircle = cntrlTmpObj.AddComponent<Image>();
        //moveTouch.backgroundCircle.rectTransform.anchorMin = new Vector2(0, 0);
        //moveTouch.backgroundCircle.rectTransform.anchorMax = new Vector2(0, 0);
        //moveTouch.backgroundCircle.rectTransform.sizeDelta = new Vector2(circleSize, circleSize);
        //moveTouch.backgroundCircle.sprite = navigationCircle;
        //moveTouch.backgroundCircle.rectTransform.pivot = new Vector2(0, 0);
        ////moveTouch.backgroundCircle.rectTransform.position = new Vector3(marginLeft, marginBottom, 0);
        //moveTouch.backgroundCircle.rectTransform.position = new Vector3(Screen.width / 20, Screen.height / 20, 0);
        //moveTouch.backgroundCircle.tag = "coystickcemberi";
        //Navigation button
        //cntrlTmpObj = new GameObject("Moviment Button");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //moveTouch.mainButton = cntrlTmpObj.AddComponent<Image>();
        //moveTouch.mainButton.rectTransform.anchorMin = new Vector2(0, 0);
        //moveTouch.mainButton.rectTransform.anchorMax = new Vector2(0, 0);
        //moveTouch.mainButton.rectTransform.sizeDelta = new Vector2(Screen.height / 6, Screen.height /6);
        //moveTouch.mainButton.sprite = navigationButton;
        //moveTouch.mainButton.rectTransform.pivot = new Vector2(0, 0);
        ////moveTouch.mainButton.rectTransform.position = new Vector3(marginLeft + (circleSize - buttonSize) / 2, marginBottom + (circleSize - buttonSize) / 2, 0);
        //moveTouch.mainButton.rectTransform.position = new Vector3(Screen.width / 6f, Screen.height / 5f, 0);//20 20
        //moveTouch.defaultArea = new Rect(moveTouch.mainButton.rectTransform.position.x,
        //    moveTouch.mainButton.rectTransform.position.y,
        //    moveTouch.mainButton.rectTransform.sizeDelta.x,
        //    moveTouch.mainButton.rectTransform.sizeDelta.y);
        //moveTouch.mainButton.tag = "joystick";

        //Mobile firing button
        //cntrlTmpObj = new GameObject("Fire button background");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //fireButton.background = cntrlTmpObj.AddComponent<Image>();
        //fireButton.background.rectTransform.anchorMin = new Vector2(1, 0);
        //fireButton.background.rectTransform.anchorMax = new Vector2(1, 0);
        //fireButton.background.rectTransform.sizeDelta = new Vector2(buttonSize, buttonSize);
        //fireButton.background.sprite = navigationButton;
        //fireButton.background.rectTransform.pivot = new Vector2(1, 0);
        //fireButton.background.rectTransform.position = new Vector3(mainCanvas.pixelRect.width - marginLeft - ((circleSize - buttonSize) / 2), marginBottom + (circleSize - buttonSize) / 2, 0);

        //int iconSize = 64;

        //cntrlTmpObj = new GameObject("Fire button icon");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //fireButton.icon = cntrlTmpObj.AddComponent<Image>();
        //fireButton.icon.rectTransform.anchorMin = new Vector2(1, 0);
        //fireButton.icon.rectTransform.anchorMax = new Vector2(1, 0);
        //fireButton.icon.rectTransform.sizeDelta = new Vector2(iconSize, iconSize);
        //fireButton.icon.sprite = gunFireIcon;
        //fireButton.icon.rectTransform.pivot = new Vector2(1, 0);
        //fireButton.icon.rectTransform.position = new Vector3(fireButton.background.rectTransform.position.x - (buttonSize - iconSize) / 2,
        //fireButton.background.rectTransform.position.y + (buttonSize - iconSize) / 2, 0);

        //fireButton.mainArea = new Rect(Screen.width - fireButton.background.rectTransform.position.x, 
        //    fireButton.background.rectTransform.position.y, buttonSize, buttonSize);

        //print(fireButton.mainArea.x);

        int smallButtonSize = 64;
        int smallIconSize = 45;
        int marginRight = 25;

        //Reload button ###
        //cntrlTmpObj = new GameObject("Reload button background");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //reloadButton.background = cntrlTmpObj.AddComponent<Image>();
        //reloadButton.background.rectTransform.anchorMin = new Vector2(1, 0);
        //reloadButton.background.rectTransform.anchorMax = new Vector2(1, 0);
        //reloadButton.background.rectTransform.sizeDelta = new Vector2(smallButtonSize, smallButtonSize);
        //reloadButton.background.sprite = navigationButton;
        //reloadButton.background.rectTransform.pivot = new Vector2(1, 0);
        //reloadButton.background.rectTransform.position = new Vector3(fireButton.background.rectTransform.position.x - buttonSize - marginRight,
        //    fireButton.background.rectTransform.position.y, 0);

        //cntrlTmpObj = new GameObject("Reload button icon");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //reloadButton.icon = cntrlTmpObj.AddComponent<Image>();
        //reloadButton.icon.rectTransform.anchorMin = new Vector2(1, 0);
        //reloadButton.icon.rectTransform.anchorMax = new Vector2(1, 0);
        //reloadButton.icon.rectTransform.sizeDelta = new Vector2(smallIconSize, smallIconSize);
        //reloadButton.icon.sprite = gunReloadIcon;
        //reloadButton.icon.rectTransform.pivot = new Vector2(1, 0);
        //reloadButton.icon.rectTransform.position = new Vector3(reloadButton.background.rectTransform.position.x - (smallButtonSize - smallIconSize) / 2,
        //    reloadButton.background.rectTransform.position.y + (smallButtonSize - smallIconSize) / 2, 0);

        //reloadButton.mainArea = new Rect(Screen.width - reloadButton.background.rectTransform.position.x,
        //    reloadButton.background.rectTransform.position.y, smallButtonSize, smallButtonSize);
        //###

        //Aim button ###
        //cntrlTmpObj = new GameObject("Aim button background");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //aimButton.background = cntrlTmpObj.AddComponent<Image>();
        //aimButton.background.rectTransform.anchorMin = new Vector2(1, 0);
        //aimButton.background.rectTransform.anchorMax = new Vector2(1, 0);
        //aimButton.background.rectTransform.sizeDelta = new Vector2(smallButtonSize, smallButtonSize);
        //aimButton.background.sprite = navigationButton;
        //aimButton.background.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        //aimButton.background.rectTransform.position = new Vector3(fireButton.background.rectTransform.position.x - buttonSize - marginRight,
        //   fireButton.background.rectTransform.position.y + buttonSize + marginRight, 0);

        //cntrlTmpObj = new GameObject("Aim button icon");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //aimButton.icon = cntrlTmpObj.AddComponent<Image>();
        //aimButton.icon.rectTransform.anchorMin = new Vector2(1, 0);
        //aimButton.icon.rectTransform.anchorMax = new Vector2(1, 0);
        //aimButton.icon.rectTransform.sizeDelta = new Vector2(smallIconSize, smallIconSize);
        //aimButton.icon.sprite = gunAimIcon;
        //aimButton.icon.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        //aimButton.icon.rectTransform.position = aimButton.background.rectTransform.position;

        //aimButton.mainArea = new Rect(Screen.width - aimButton.background.rectTransform.position.x - smallButtonSize / 2,
        //    aimButton.background.rectTransform.position.y - smallButtonSize / 2, smallButtonSize, smallButtonSize);
        //###

        //Jump button ###
        //cntrlTmpObj = new GameObject("Jump button background");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //jumpButton.background = cntrlTmpObj.AddComponent<Image>();
        //jumpButton.background.rectTransform.anchorMin = new Vector2(1, 0);
        //jumpButton.background.rectTransform.anchorMax = new Vector2(1, 0);
        //jumpButton.background.rectTransform.sizeDelta = new Vector2(smallButtonSize, smallButtonSize);
        //jumpButton.background.sprite = navigationButton;
        //jumpButton.background.rectTransform.pivot = new Vector2(1, 0);
        //jumpButton.background.rectTransform.position = new Vector3(fireButton.background.rectTransform.position.x,
        //   fireButton.background.rectTransform.position.y + buttonSize + marginRight, 0);

        //cntrlTmpObj = new GameObject("Jump button icon");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //jumpButton.icon = cntrlTmpObj.AddComponent<Image>();
        //jumpButton.icon.rectTransform.anchorMin = new Vector2(1, 0);
        //jumpButton.icon.rectTransform.anchorMax = new Vector2(1, 0);
        //jumpButton.icon.rectTransform.sizeDelta = new Vector2(smallIconSize, smallIconSize);
        //jumpButton.icon.sprite = jumpIcon;
        //jumpButton.icon.rectTransform.pivot = new Vector2(1, 0);
        //jumpButton.icon.rectTransform.position = new Vector3(jumpButton.background.rectTransform.position.x - (smallButtonSize - smallIconSize) / 2,
        //    jumpButton.background.rectTransform.position.y + (smallButtonSize - smallIconSize) / 2, 0);

        //jumpButton.mainArea = new Rect(Screen.width - jumpButton.background.rectTransform.position.x,
        //    jumpButton.background.rectTransform.position.y, smallButtonSize, smallButtonSize);
        //###

        //Switch weapon button ###
        //cntrlTmpObj = new GameObject("Switch Weapon Background");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //switchWeaponButton.background = cntrlTmpObj.AddComponent<Image>();
        //switchWeaponButton.background.rectTransform.anchorMin = new Vector2(1, 0);
        //switchWeaponButton.background.rectTransform.anchorMax = new Vector2(1, 0);
        //switchWeaponButton.background.rectTransform.sizeDelta = new Vector2(smallButtonSize, smallButtonSize);
        //switchWeaponButton.background.sprite = navigationButton;
        //switchWeaponButton.background.rectTransform.pivot = new Vector2(1, 0);
        //switchWeaponButton.background.rectTransform.position = new Vector3(fireButton.background.rectTransform.position.x + smallButtonSize + marginRight,
        //   fireButton.background.rectTransform.position.y + buttonSize + marginRight - smallButtonSize/2, 0);

        //cntrlTmpObj = new GameObject("Switch Weapon icon");
        //cntrlTmpObj.transform.position = Vector3.zero;
        //cntrlTmpObj.transform.parent = tmpObj.transform;
        //switchWeaponButton.icon = cntrlTmpObj.AddComponent<Image>();
        //switchWeaponButton.icon.rectTransform.anchorMin = new Vector2(1, 0);
        //switchWeaponButton.icon.rectTransform.anchorMax = new Vector2(1, 0);
        //switchWeaponButton.icon.rectTransform.sizeDelta = new Vector2(smallIconSize, smallIconSize);
        //switchWeaponButton.icon.sprite = switchWeaponIcon;
        //switchWeaponButton.icon.rectTransform.pivot = new Vector2(1, 0);
        //switchWeaponButton.icon.rectTransform.position = new Vector3(switchWeaponButton.background.rectTransform.position.x - (smallButtonSize - smallIconSize) / 2,
        //    switchWeaponButton.background.rectTransform.position.y + (smallButtonSize - smallIconSize) / 2, 0);

        //switchWeaponButton.mainArea = new Rect(Screen.width - switchWeaponButton.background.rectTransform.position.x,
        //    switchWeaponButton.background.rectTransform.position.y, smallButtonSize, smallButtonSize);
        //###
#endif

        //And finally apply UI layer to every child object of canvas
        foreach (Transform t in mainCanvas.GetComponentsInChildren<Transform>(true))
        {
            t.gameObject.layer = 5;
        }
    }

    // Update is called once per frame
    public void croshair()
    {

    }
    void Update()
    {
        GameSettings.moveDirection.x = Mathf.Clamp(GameObject.Find("asdf").GetComponent<Scoreboard>().joystick.Horizontal * 1.25f, -1.000f, 1.000f);
        GameSettings.moveDirection.y = Mathf.Clamp(GameObject.Find("asdf").GetComponent<Scoreboard>().joystick.Vertical * 1.25f, -1.000f, 1.000f);

        if (PlayerPrefs.GetInt("birincil_2_kusanıldı") == 1 && GameSettings.switchWeaponIndex  == 1)
        {
            crosshairRoot.SetActive(false);
            if (Application.loadedLevelName == "CEZA EVİ")
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().nokta.SetActive(false);

            }
            if (Application.loadedLevelName == "DÖRT KÖŞE")
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().nokta.SetActive(false);

            }

        }
        else if (Application.loadedLevelName == "AWP KULE")
        {
            crosshairRoot.SetActive(false);
            GameObject.Find("asdf").GetComponent<Scoreboard>().nokta.SetActive(false);
        }

        else
        {
           

            crosshairRoot.SetActive(true);
            //Top
            crosshairSet[0].rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2, mainCanvas.pixelRect.height / 2 + 7 + rc.distance, 0);
            //Bottom
            crosshairSet[1].rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2, mainCanvas.pixelRect.height / 2 - 7 - rc.distance, 0);
            //Left
            crosshairSet[2].rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2 - 7 - rc.distance, mainCanvas.pixelRect.height / 2, 0);
            //Right
            crosshairSet[3].rectTransform.position = new Vector3(mainCanvas.pixelRect.width / 2 + 7 + rc.distance, mainCanvas.pixelRect.height / 2, 0);
            if (Application.loadedLevelName == "CEZA EVİ")
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().nokta.SetActive(true);

            }
            if (Application.loadedLevelName == "DÖRT KÖŞE")
            {
                GameObject.Find("asdf").GetComponent<Scoreboard>().nokta.SetActive(true);

            }
            if (Application.loadedLevelName == "VADİ")
            {
                crosshairRoot.SetActive(true);

            }
            // }
        }
        //Populate UI values
        if (rc.leavingRoom)
        {
            //Leaving room, show black screen
        }
        else
        {
            if (rc.ourPlayer && !rc.ourPlayer.playerKilled)
            {
                //Show player HP
                //HPText.text = rc.currentHP.ToString() + " HP";
                GameObject.Find("asdf").GetComponent<Scoreboard>().hptextt.text = rc.currentHP.ToString() + "";
                //Weapon name and ammo
                if (rc.ourPlayer.playerWeapons.currentSelectedWeapon)
                {
                    // weaponAndAmmoText.text = rc.ourPlayer.playerWeapons.currentSelectedWeapon.wSettings.bulletsPerClip.ToString() + " / " + rc.ourPlayer.playerWeapons.currentSelectedWeapon.wSettings.reserveBullets.ToString() + " " + rc.ourPlayer.playerWeapons.currentSelectedWeapon.weaponName;
                    GameObject.Find("asdf").GetComponent<Scoreboard>().weaponandammotextfelan.text = rc.ourPlayer.playerWeapons.currentSelectedWeapon.wSettings.bulletsPerClip.ToString() + " / " + rc.ourPlayer.playerWeapons.currentSelectedWeapon.wSettings.reserveBullets.ToString();
                }
                else
                {
                    weaponAndAmmoText.text = "";
                }

                //Crosshair, only show when not aiming
                if (GameSettings.currentFOV == GameSettings.defaultFOV)
                {
                    //if (!crosshairRoot.activeSelf)
                    //{
                    //    crosshairRoot.SetActive(true);
                    //}







                }
                else
                {
                    if (crosshairRoot.activeSelf)
                    {
                        crosshairRoot.SetActive(false);
                    }
                }

                //Show sniper scope
                if (scopeTextureTmp != GameSettings.currentScopeTexture)
                {
                    scopeTextureTmp = GameSettings.currentScopeTexture;
                    if (scopeTextureTmp != null)
                    {
                        scopeTextureRatio = ((float)scopeTextureTmp.rect.width * 0.01f) / ((float)scopeTextureTmp.rect.height * 0.01f);
                        sniperScope.sprite = scopeTextureTmp;
                        sniperScope.rectTransform.sizeDelta = new Vector2(Screen.height * scopeTextureRatio, Screen.height);
                        sniperScope.gameObject.SetActive(true);
                    }
                    else
                    {
                        sniperScope.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                //HPText.text = "";
                GameObject.Find("asdf").GetComponent<Scoreboard>().hptextt.text = "";
                weaponAndAmmoText.text = "";



            }
        }

        //Show hit detectors
        if (rc.doingHitDetector)
        {
            //Fade hit detectors
            //redScreen.rectTransform.sizeDelta = new Vector3(mainCanvas.pixelRect.width, mainCanvas.pixelRect.height, 0);
            //redScreen.color = new Color(1, 0, 0, rc.redScreenFade / 7);
            hitDetectorSet[0].color = new Color(1, 1, 1, rc.hitTopFade);
            hitDetectorSet[1].color = new Color(1, 1, 1, rc.hitBottomFade);
            hitDetectorSet[2].color = new Color(1, 1, 1, rc.hitLeftFade);
            hitDetectorSet[3].color = new Color(1, 1, 1, rc.hitRightFade);
            previousHitDetector = true;
        }
        else
        {
            if (previousHitDetector)
            {
                previousHitDetector = false;

                //redScreen.color = new Color(1, 1, 1, 0);
                hitDetectorSet[0].color = new Color(1, 1, 1, 0);
                hitDetectorSet[1].color = new Color(1, 1, 1, 0);
                hitDetectorSet[2].color = new Color(1, 1, 1, 0);
                hitDetectorSet[3].color = new Color(1, 1, 1, 0);
            }
        }

        //Show current cash
        //cashText.color = Color.white;//rc.currentAddingCashColor;
        //cashText.text = (GameSettings.cnst - rc.totalCash).ToString() + " $";

        GameObject.Find("asdf").GetComponent<oyunmagaza>().toplamşimşek = PlayerPrefs.GetInt("TOPLAMSİMSEK");
        //GameObject.Find("asdf").GetComponent<Scoreboard>().cashtextt.text = (GameSettings.cnst - rc.totalCash).ToString();

        if (oyuniçişimşek != GameSettings.cnst - rc.totalCash)
        {
            if (GameSettings.cnst - rc.totalCash - oyuniçişimşek == 150)
            {
                GameObject.Find("asdf").GetComponent<oyunmagaza>().toplamşimşek += (GameSettings.cnst - rc.totalCash - oyuniçişimşek) / 10;
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyuniçitextparası += (GameSettings.cnst - rc.totalCash - oyuniçişimşek) / 10;
                
            }
            else if (GameSettings.cnst - rc.totalCash - oyuniçişimşek == 100)
            {
                GameObject.Find("asdf").GetComponent<oyunmagaza>().toplamşimşek += (GameSettings.cnst - rc.totalCash - oyuniçişimşek) / 10;
                GameObject.Find("asdf").GetComponent<Scoreboard>().oyuniçitextparası += (GameSettings.cnst - rc.totalCash - oyuniçişimşek) / 10;
               
            }
            GameObject.Find("asdf").GetComponent<Scoreboard>().cashtextt.text = GameObject.Find("asdf").GetComponent<Scoreboard>().oyuniçitextparası.ToString();
            GameObject.Find("asdf").GetComponent<Scoreboard>().oyunparasiy.text = GameObject.Find("asdf").GetComponent<Scoreboard>().cashtextt.text;
            //if (GameSettings.cnst - rc.totalCash - oyuniçişimşek == 500)
            //{
            //    GameObject.Find("asdf").GetComponent<oyunmagaza>().toplamşimşek += (GameSettings.cnst - rc.totalCash - oyuniçişimşek) / 10;
            //}
            //GameObject.Find("asdf").GetComponent<oyunmagaza>().toplamşimşek += (GameSettings.cnst - rc.totalCash- oyuniçişimşek)/;
            PlayerPrefs.SetInt("TOPLAMSİMSEK", GameObject.Find("asdf").GetComponent<oyunmagaza>().toplamşimşek);
            GameObject.Find("asdf").GetComponent<oyunmagaza>().seviyepuani_once = GameObject.Find("asdf").GetComponent<oyunmagaza>().oyunpuanı;
            GameObject.Find("asdf").GetComponent<oyunmagaza>().oyunpuanı += ((GameSettings.cnst - rc.totalCash - oyuniçişimşek) / 10) + (GameObject.Find("asdf").GetComponent<oyunmagaza>().seviye - 1) * 5;
            GameObject.Find("asdf").GetComponent<oyunmagaza>().seviyepuani_sonra = GameObject.Find("asdf").GetComponent<oyunmagaza>().oyunpuanı;

            GameObject.Find("asdf").GetComponent<Scoreboard>().oyunpuanigeçici += ((GameSettings.cnst - rc.totalCash - oyuniçişimşek) / 10) + (GameObject.Find("asdf").GetComponent<oyunmagaza>().seviye - 1) * 5;
            PlayerPrefs.SetInt("oyunpuani", GameObject.Find("asdf").GetComponent<oyunmagaza>().oyunpuanı);
            
            GameObject.Find("asdf").GetComponent<Scoreboard>().oyunpuaniy.text = GameObject.Find("asdf").GetComponent<Scoreboard>().oyunpuanigeçici.ToString();
            GameObject.Find("asdf").GetComponent<oyunmagaza>().şimşek_kazan();
            oyuniçişimşek = GameSettings.cnst - rc.totalCash;
        }

        //Adding cash
        addMoreCashText.color = rc.currentAddingCashColor;
        addMoreCashText.text = rc.scoreToAddTmp;

        //Show Round Timer
        //roundTimeText.text = rc.roundTimeString;
        GameObject.Find("asdf").GetComponent<Scoreboard>().zaman.text = rc.roundTimeString;
        //zaman.text = rc.roundTimeString;
        //oi.zaman.text = roundTimeText.text;
        //Show respawn time
        if (rc.currentRespawnTime > -1)
        {
            respawnTimeText.mainText.text = "Canlanmana Son : " + rc.currentRespawnTime.ToString() + " saniye";
            respawnTimeText.textShadow.text = "Canlanmana Son : " + rc.currentRespawnTime.ToString() + " saniye";
        }
        else
        {
            respawnTimeText.mainText.text = "";
            respawnTimeText.textShadow.text = "";
        }

        //Show buy menu text
        //        if (!rc.ourPlayer || rc.timeToPurchase > 0)
        //        {

        ////#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
        ////            string textTmp = "Market için dokun" + "\n";
        ////            textTmp += rc.timeToPurchase > 0 && rc.ourPlayer ? rc.timeToPurchase.ToString() + " saniye sonra kapanacak" : "";
        ////#else
        ////            string textTmp = "Press '" + GameSettings.playerKeys[13].ToString() + "' to open buy menu";
        ////            textTmp += rc.timeToPurchase > 0 && rc.ourPlayer ? " - " + rc.timeToPurchase.ToString() + " saniye sonra kapanacak" : "";
        ////#endif

        ////            buyMenuText.mainText.text = textTmp;
        ////            buyMenuText.textShadow.text = textTmp;
        //        }
        //        else
        //        {
        //            buyMenuText.mainText.text = "";
        //            buyMenuText.textShadow.text = "";
        //        }

        //Entered buy spot area
        if (rc.eneteredBuySpot)
        {

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
            refillAmmoText.mainText.text = "       ";
            refillAmmoText.textShadow.text = "         ";
#else
            refillAmmoText.mainText.text = "Hold '" + GameSettings.playerKeys[14].ToString() + "' to refill ammo";
            refillAmmoText.textShadow.text = "Hold '" + GameSettings.playerKeys[14].ToString() + "' to refill ammo";
#endif

        }
        else
        {
            refillAmmoText.mainText.text = "";
            refillAmmoText.textShadow.text = "";
        }

        //Action reports
        if (GameSettings.updateActionReports)
        {
            //Update actions on screen
            //Show action reports
            for (int i = 0; i < actionReports.Length; i++)
            {
                if (i < rc.actionReports.Count)
                {
                    string composeText = "";
                    string composeShadowText = "";

                    if (rc.actionReports[i].leftText != "")
                    {
                        composeText += "<color=#" + GameSettings.ColorToHex(rc.actionReports[i].leftTextColor) + ">" + rc.actionReports[i].leftText + "</color>" + " ";
                        composeShadowText += rc.actionReports[i].leftText + " ";

                        //  GameObject.Find("asdf").GetComponent<Scoreboard>().öldüren_kisi.text += composeShadowText;


                    }

                    composeText += "<color=#" + GameSettings.ColorToHex(Color.white) + ">" + rc.actionReports[i].middleText + "</color>" + " "; //GameSettings.HUDColor
                    composeShadowText += rc.actionReports[i].middleText + " ";
                    //GameObject.Find("asdf").GetComponent<Scoreboard>().öldüren_kisi.text += composeShadowText;
                    if (rc.actionReports[i].rightText != "")
                    {
                        composeText += "<color=#" + GameSettings.ColorToHex(rc.actionReports[i].rightTextColor) + ">" + rc.actionReports[i].rightText + "</color>";
                        composeShadowText += rc.actionReports[i].rightText;
                        GameObject.Find("asdf").GetComponent<Scoreboard>().bildirim_kutusu1.SetActive(true);
                        GameObject.Find("asdf").GetComponent<Scoreboard>().öldüren_kisi.text = composeShadowText;
                        // Invoke("kapat", 3);
                    }
                    Debug.Log(rc.actionReports[i].leftText + " " + " " + rc.actionReports[i].rightText);
                    //Text Shadow
                    actionReports[i].textShadow.rectTransform.position = new Vector3(mainCanvas.pixelRect.width - 10 + 1, mainCanvas.pixelRect.height - 10 - (i * 25) - 1, 0);
                    actionReports[i].textShadow.text = composeShadowText;
                    //Main Text
                    actionReports[i].mainText.rectTransform.position = new Vector3(mainCanvas.pixelRect.width - 10, mainCanvas.pixelRect.height - 10 - (i * 25), 0);
                    actionReports[i].mainText.text = composeText;
                }
                else
                {
                    actionReports[i].mainText.text = "";
                    actionReports[i].textShadow.text = "";
                }
            }

            GameSettings.updateActionReports = false;
        }

        //Chat messages
        //if (GameSettings.updateChatMessages)
        //{
        //    for (int i = 0; i < chatMessages.Length; i++)
        //    {
        //        if (i < rc.mc.messages.Count && (rc.mc.messages[i].timer > 0 || rc.mc.chatState != MultiplayerChat.ChatState.None))
        //        {
        //            string composeText = "";
        //            string composeShadowText = "";

        //            if (rc.mc.messages[i].isTeamChat != "")
        //            {
        //                composeText += "<color=#" + GameSettings.ColorToHex(GameSettings.HUDColor) + ">" + rc.mc.messages[i].isTeamChat + "</color>" + " ";
        //                composeShadowText += rc.mc.messages[i].isTeamChat + " ";
        //            }

        //            composeText += "<color=#" + GameSettings.ColorToHex(rc.mc.messages[i].senderTeamColor) + ">" + rc.mc.messages[i].senderName + ": " + "</color>";
        //            composeShadowText += rc.mc.messages[i].senderName + ": ";

        //            composeText += "<color=#" + GameSettings.ColorToHex(GameSettings.HUDColor) + ">" + rc.mc.messages[i].text + "</color>";
        //            composeShadowText += rc.mc.messages[i].text;

        //            //Text Shadow
        //            chatMessages[i].textShadow.rectTransform.position = new Vector3(10 + 1, 290 + 10 + (i * 25) - 1, 0);
        //            chatMessages[i].textShadow.text = composeShadowText;
        //            //Main Text
        //            chatMessages[i].mainText.rectTransform.position = new Vector3(10, 290 + 10 + (i * 25), 0);
        //            chatMessages[i].mainText.text = composeText;
        //        }
        //        else
        //        {
        //            chatMessages[i].mainText.text = "";
        //            chatMessages[i].textShadow.text = "";
        //        }
        //    }

        //    GameSettings.updateChatMessages = false;
        // }

        //Check UI interaction

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1

#if !UNITY_EDITOR
        //Handle touch input
        for (var i = 0; i < Input.touchCount; ++i)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            {
                MobileButtonsCheck(new Vector2(touch.position.x, Screen.height - touch.position.y), touch.fingerId);
            }

            if (touch.phase == TouchPhase.Moved )
            {
                if(moveTouch.isActive && moveTouch.touchID == touch.fingerId)
                {
                    moveTouch.currentTouchPos = touch.position;
                }

                if (fpsLookTouch.isActive && fpsLookTouch.touchID == touch.fingerId)
                {
                    fpsLookTouch.currentTouchPos = touch.position;
                }
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                MobileButtonStop(touch.fingerId);
            }
        }
#else
        //Test mobile controls in editor, use mouse instead of touch controls
        if (Input.GetMouseButtonDown(0))
        {
            MobileButtonsCheck(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y), -1);
        }

        if (Input.GetMouseButtonUp(0))
        {
            MobileButtonStop(-1);
        }

        moveTouch.currentTouchPos = Input.mousePosition;
        fpsLookTouch.currentTouchPos = Input.mousePosition;
#endif

        //Moving
        //if (moveTouch.isActive)
        //{
        //    moveTouch.mainButton.rectTransform.position = new Vector3(moveTouch.currentTouchPos.x - moveTouch.touchOffset.x, moveTouch.currentTouchPos.y - moveTouch.touchOffset.y);
        //    GameSettings.moveDirection.x = moveTouch.mainButton.rectTransform.position.x - moveTouch.defaultArea.x;
        //    GameSettings.moveDirection.y = moveTouch.mainButton.rectTransform.position.y - moveTouch.defaultArea.y;

        //    //Vector3 moveVector = (transform.right * joystick.Horizontal + transform.forward * joystick.Vertical).normalized;
        //    //transform.Translate(moveVector * moveSpeed * Time.deltaTime);

        //    if (Mathf.Abs(GameSettings.moveDirection.x) < 19)
        //    {
        //        GameSettings.moveDirection.x = 0;
        //    }
        //    else
        //    {
        //        GameSettings.moveDirection.x = Mathf.Clamp(GameObject.Find("asdf").GetComponent<Scoreboard>().joystick.Horizontal , -1.000f, 1.000f);
        //    }

        //    if (Mathf.Abs(GameSettings.moveDirection.y) < 19)
        //    {
        //        GameSettings.moveDirection.y = 0;
        //    }
        //    else
        //    {

        //        GameSettings.moveDirection.y = Mathf.Clamp(GameObject.Find("asdf").GetComponent<Scoreboard>().joystick.Vertical, -1.000f, 1.000f);
        //    }
        //}
        //else
        //{
        //    moveTouch.mainButton.rectTransform.position = new Vector3(moveTouch.defaultArea.x, moveTouch.defaultArea.y);
        //    GameSettings.moveDirection = Vector2.zero;
        //}

        //Looking around
        if (fpsLookTouch.isActive)
        {
            if (fpsLookTouch.touchOffset.x != fpsLookTouch.currentTouchPos.x || fpsLookTouch.touchOffset.y != fpsLookTouch.currentTouchPos.y)
            {
                GameSettings.lookDirection = new Vector2(
                    Mathf.Clamp((fpsLookTouch.currentTouchPos.x - fpsLookTouch.touchOffset.x) / 5.500f, -5.500f, 5.500f),
                    Mathf.Clamp((fpsLookTouch.currentTouchPos.y - fpsLookTouch.touchOffset.y) / 5.500f, -5.500f, 5.500f));

                //print(GameSettings.lookDirection);

                fpsLookTouch.touchOffset = fpsLookTouch.currentTouchPos;
            }
            else
            {
                GameSettings.lookDirection = Vector2.zero;
            }


            //Fast swiping timer
            swipeTime += Time.deltaTime;
        }
        else
        {
            if (initialTouchPos != Vector2.zero)
            {
                float distanceXTmp = Mathf.Abs(fpsLookTouch.currentTouchPos.x - initialTouchPos.x);
                float distanceYTmp = Mathf.Abs(fpsLookTouch.currentTouchPos.y - initialTouchPos.y);

                //if (swipeTime < 0.195f && distanceXTmp > 4 && distanceXTmp > distanceYTmp)
                //{
                //    GameSettings.lookDirection = new Vector2(Mathf.Clamp(GameSettings.lookDirection.x, - 1, 1), Mathf.Clamp(GameSettings.lookDirection.y, -1, 1));

                //    keepRotatingTime = 1.05f;
                //    previousTouchDirX = Mathf.Clamp(GameSettings.lookDirection.x * 2, -10, 10);
                //    GameSettings.lookDirection.y = 0;
                //}
                //else
                //{
                //    keepRotatingTime = 0;
                //}

                //print(swipeTime);

                initialTouchPos = Vector2.zero;
            }

            if (keepRotatingTime > 0)
            {
                //Slowly decrease rotation time, make it framerate independent
                keepRotatingTime = Mathf.Lerp(keepRotatingTime, -0.1f, Time.deltaTime / (GameSettings.currentFPS / 100.00f));
                GameSettings.lookDirection.x = (previousTouchDirX * keepRotatingTime) / (GameSettings.currentFPS / 100.00f);

                //print(GameSettings.currentFPS);
            }
            else
            {
                //fpsLookTouch.mainButton.rectTransform.position = new Vector3(fpsLookTouch.defaultArea.x, fpsLookTouch.defaultArea.y);
                GameSettings.lookDirection = Vector2.zero;
            }
        }

        //print(GameSettings.moveDirection.x.ToString() + "   " + GameSettings.moveDirection.y.ToString());
#endif
    }

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
    public void kapat()
    {
        GameObject.Find("asdf").GetComponent<Scoreboard>().bildirim_kutusu1.SetActive(false);
    }
    void MobileButtonsCheck(Vector2 touchPos, int touchID)
    {
        //Move controller
        //if (moveTouch.defaultArea.Contains(new Vector2(touchPos.x, Screen.height - touchPos.y)) && !moveTouch.isActive)
        //{
        //    moveTouch.isActive = true;
        //    moveTouch.touchOffset = new Vector2(touchPos.x - moveTouch.defaultArea.x, Screen.height - touchPos.y - moveTouch.defaultArea.y);
        //    moveTouch.currentTouchPos = new Vector2(touchPos.x, Screen.height - touchPos.y);
        //    moveTouch.touchID = touchID;
        //}

        //Look around
        if (touchPos.x > Screen.width / 2 && !fpsLookTouch.isActive)
        {
            //To use later for fast swiping
            initialTouchPos = new Vector2(touchPos.x, Screen.height - touchPos.y);
            swipeTime = 0;

            fpsLookTouch.isActive = true;
            fpsLookTouch.touchID = touchID;
            fpsLookTouch.touchOffset = initialTouchPos;
            fpsLookTouch.currentTouchPos = initialTouchPos;
        }

        //Fire button press
        //if ((new Rect(Screen.width - fireButton.mainArea.x - fireButton.mainArea.width, Screen.height - fireButton.mainArea.y - fireButton.mainArea.height,
        //    fireButton.mainArea.width, fireButton.mainArea.height)).Contains(touchPos))
        //{
        //    fireButton.isActive = true;
        //    GameSettings.mobileFiring = true;
        //    fireButton.touchID = touchID;

        //}

        ////Reload button press
        //if ((new Rect(Screen.width - reloadButton.mainArea.x - reloadButton.mainArea.width, Screen.height - reloadButton.mainArea.y - reloadButton.mainArea.height,
        //    reloadButton.mainArea.width, reloadButton.mainArea.height)).Contains(touchPos))
        //{
        //    //reloadButton.isActive = true;
        //    GameSettings.mobileReloading = true;
        //}

        ////Aim button press
        //if ((new Rect(Screen.width - aimButton.mainArea.x - aimButton.mainArea.width, Screen.height - aimButton.mainArea.y - aimButton.mainArea.height,
        //    aimButton.mainArea.width, aimButton.mainArea.height)).Contains(touchPos))
        //{
        //    //reloadButton.isActive = true;
        //    GameSettings.mobileAiming = true;

        //}

        //Jump button press
        //if ((new Rect(Screen.width - jumpButton.mainArea.x - jumpButton.mainArea.width, Screen.height - jumpButton.mainArea.y - jumpButton.mainArea.height,
        //    jumpButton.mainArea.width, jumpButton.mainArea.height)).Contains(touchPos))
        //{
        //    //reloadButton.isActive = true;
        //    GameSettings.mobileJumping = true;

        //}

        //Switch weapon button
        //if ((new Rect(Screen.width - switchWeaponButton.mainArea.x - switchWeaponButton.mainArea.width, Screen.height - switchWeaponButton.mainArea.y - switchWeaponButton.mainArea.height,
        //    switchWeaponButton.mainArea.width, switchWeaponButton.mainArea.height)).Contains(touchPos))
        //{
        //    //reloadButton.isActive = true;

        //    GameSettings.switchWeaponIndex++;
            //rc.Invoke("renkbirincil", 0.01f);
            //rc.Invoke("renkikincil", 0.01f);
            //rc.Invoke("renkbicak", 0.01f);

        //}

        //Refill ammo
        if (touchPos.x > Screen.width / 2 - 100 && touchPos.x < Screen.width / 2 + 100 && Screen.height - touchPos.y > 140 && !GameSettings.menuOpened)
        {
            refillAmmoTapID = touchID;
            GameSettings.refillingAmmo = true;
        }

        //Open buy menu
        //if (touchPos.x > Screen.width / 2 - 100 && touchPos.x < Screen.width / 2 + 100 && 
        //    //Screen.height - touchPos.y > buyMenuText.textShadow.rectTransform.position.y && Screen.height - touchPos.y < buyMenuText.textShadow.rectTransform.position.y + 45 &&
        //    !GameSettings.menuOpened)
        //{
        //    if (!rc.ourPlayer || rc.timeToPurchase > 0)
        //    {
        //        rc.OpenBuyMenu();
        //    }
        //}
    }
    //public void zıpla()
    //{
    //    GameSettings.mobileJumping = true;
    //}

    public void ateset()
    {

        fireButton.isActive = true;
        GameSettings.mobileFiring = true;
        // fireButton.touchID = touchID;
    }
    public void atesdursun()
    {

        fireButton.isActive = false;
        GameSettings.mobileFiring = false;
    }
    

public void silahdegistirla()
    {
        //if(silahdegistirr==0)
        //{
        //    GameSettings.switchWeaponIndex++;
        //    GameSettings.switchWeaponIndex++;
        //    silahdegistirr = 1;
        //}
        //else
        //{
        //    GameSettings.switchWeaponIndex++;
        //}
        GameSettings.switchWeaponIndex++;
        Debug.Log(GameSettings.switchWeaponIndex + " silahsırası");
        silahdegistir = silahdegistir + 1;
        GameObject.Find("asdf").GetComponent<Scoreboard>().silahdegistir_ = silahdegistir;
        Debug.Log(silahdegistir);
        
       // rc.Invoke("renkbirincil", 0.01f);
        //rc.Invoke("renkikincil", 0);
        //rc.Invoke("renkbicak", 0);

        if (silahdegistir==3)
        {
            silahdegistir = 0;
            GameObject.Find("asdf").GetComponent<Scoreboard>().silahdegistir_ = 0;
        }
        
        // hangisilahscripti();
    }
    //public void hangisilahscripti()
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        if (silahdegistir == i)
    //        {
    //            GameObject.Find("asdf").GetComponent<Scoreboard>().hangisilahta[i].SetActive(true);
    //        }
    //        else
    //        {
    //            GameObject.Find("asdf").GetComponent<Scoreboard>().hangisilahta[i].SetActive(false);
    //        }
    //    }
        
    //}
    void MobileButtonStop(int touchID)
    {
        //if (moveTouch.isActive && moveTouch.touchID == touchID)
        //{
        //    moveTouch.isActive = false;
        //    moveTouch.touchOffset = Vector2.zero;
        //    moveTouch.touchID = -1;
        //}

        if (fpsLookTouch.isActive && fpsLookTouch.touchID == touchID)
        {
            fpsLookTouch.isActive = false;
            fpsLookTouch.touchOffset = Vector2.zero;
            fpsLookTouch.touchID = -1;
        }

        if (fireButton.isActive && fireButton.touchID == touchID)
        {
            fireButton.isActive = false;
            GameSettings.mobileFiring = false;
        }

        if(refillAmmoTapID == touchID)
        {
            refillAmmoTapID = -1;
            GameSettings.refillingAmmo = false;
        }
    }
#endif

}
