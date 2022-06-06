


using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour
{

    //Purpose of this script is to collect all the static values that will be shared between scripts
    //Example is - team colors, player controls, static styles, shared components, default FOV etc.

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
    //Mobiel variables
    public static Vector2 moveDirection = Vector2.zero;
    public static Vector2 lookDirection = Vector2.zero;
    public static bool mobileFiring = false;
    public static bool mobileReloading = false;
    public static bool mobileAiming = false;
    public static bool mobileJumping = false;
    public static int switchWeaponIndex = 1; //1 = primary weapon, 2 = secondary weapon,  3 = special weapon 
    public static bool refillingAmmo = false;
#endif

    //Static references
    public static RoomController rc;

	//Random number used for value obfuscation
	public static int cnst = Random.Range(1111111, 9999999);

	//Camera settings
	public static float defaultFOV = 60;
	public static float currentFOV = GameSettings.defaultFOV;
     
	public static Sprite currentScopeTexture = null; //This is assigned at PlayerWeapons.cs when we aiming and using scope texture

	//Color settings
	public static string teamAName = "MAVİ TAKIM";
    public static Color teamBColor = new Color(25f, 0f, 0f, 0.5f);
    public static string teamBName = "KIRMIZI TAKIM";
    public static Color teamAColor = new Color(0f, 0f, 25f, 0.5f);
    public static Color drawColor = Color.white;//new Color(0.254f, 1, 0.395f, 1);
    public static Color HUDColor = Color.black;//new Color(29 / 255.0f, 217 / 255.0f, 242 / 255.0f, 1);
	public static Color textShadowColor = new Color(0, 0, 0, 0.85f);
	public static Color customRedColor = new Color(0.858f, 0.352f, 0.388f, 1);
    public static Color otherPlayerGUIBoxColor = Color.black;// new Color(1, 1, 1, 0.5f);

	//Controls
	public static float mouseSensitivity = 1.5f;

	public static KeyCode[] playerKeys = 
	{ 
		KeyCode.Mouse0, //0
		KeyCode.Mouse1, //1
		KeyCode.Alpha3, //2
		KeyCode.Alpha2, //3
		KeyCode.Alpha1, //4
		KeyCode.R, //5
		KeyCode.W, //6
		KeyCode.S, //7
		KeyCode.A, //8
		KeyCode.D, //9
		KeyCode.C, //10
		KeyCode.LeftShift, //11
		KeyCode.Space, //12
		KeyCode.B, //13
		KeyCode.F, //14
		KeyCode.T, //15
		KeyCode.Y //16
	};

	public static string[] playerKeysNames = 
	{ 
		"Fire", //0
		"Aim", //1
		"Primary Weapon", //2
		"Secondary Weapon", //3
		"Special Weapon", //4
		"Reload", //5
		"Move Front", //6
		"Move Backward", //7
		"Move Left", //8
		"Move Right", //9
		"Crouch", //10
		"Sprint/Slow Walk", //11
		"Jump", //12
		"Buy Menu", //13
		"Use", //14
		"Chat", //15
		"Team Chat" //16
	};

	public static KeyCode[] defaultPlayerKeys;

	//Other
	public static string errorText = "";
	public static float currentFPS = 100.0f; //Framerate is calculated at RoomController.cs
	public static int ourTeam; //Set from RoomController.cs
	public static string currentGameMode;

	//Block player movement, firing etc. Set in RoomController.cs
	public static bool menuOpened = false;

    //Limit number of message and action reports appeared on screen
    public static int actionReportsLimit = 5;
    public static int chatMessagesLimit = 9;
    public static bool updateActionReports = false;
    public static bool updateChatMessages = false;

	//Here we store GUISkin and custom styles to be used from different scripts, this is assigned at SetupGUIStyles.cs
	public static GUISkin guiSkin;

	public static GUIStyle headerStyle = new GUIStyle();
	public static GUIStyle closeButtonStyle = new GUIStyle();
	public static GUIStyle roomBrowserHeadersStyle = new GUIStyle();
	public static GUIStyle createRoomOptionsStyle = new GUIStyle();

	public static GUIStyle timeStyle = new GUIStyle();
	public static GUIStyle finalScreenStyle = new GUIStyle();
	public static GUIStyle hudInfoStyle = new GUIStyle();
	public static GUIStyle weaponInfoStyle = new GUIStyle();
	public static GUIStyle keyPressStyle = new GUIStyle();
	public static GUIStyle buyMenuButtonStyle = new GUIStyle();
	public static GUIStyle actionReportStyle = new GUIStyle();

    //Convert color to hex representation
    public static string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    //Convert hex color to int representation
    public static Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
