//FPS Kit 3.0
//NSdesignGames @2015

using UnityEngine;
using System.Collections;

public class SetupGUIStyles : MonoBehaviour
{

	//We use GUISkin to customize HUD appearance, however it can't cover all the required styles
	//so the purpose of this script is to create more custom styles derivated from main GUISkin

	public GUISkin guiSkin;
	public Texture2D closeIcon;
	public Texture2D closeIconHover;

	// Use this for initialization
	void Awake ()
    {
		//Here we setup all the custom styles and gui skin that will be shared between different scripts
		GameSettings.guiSkin = guiSkin;
		
		GameSettings.headerStyle.font = guiSkin.font;
		GameSettings.headerStyle.normal.textColor = Color.white;
		GameSettings.headerStyle.alignment = TextAnchor.MiddleCenter;
		GameSettings.headerStyle.fontSize = 35;
		
		GameSettings.closeButtonStyle.normal.background = closeIcon;
		GameSettings.closeButtonStyle.hover.background = closeIconHover;
		
		GameSettings.roomBrowserHeadersStyle.font = guiSkin.font;
		GameSettings.roomBrowserHeadersStyle.normal.textColor = Color.white;
		GameSettings.roomBrowserHeadersStyle.alignment = TextAnchor.MiddleLeft;
		GameSettings.roomBrowserHeadersStyle.fontSize = guiSkin.label.fontSize;
		GameSettings.roomBrowserHeadersStyle.normal.background = guiSkin.scrollView.normal.background;
		GameSettings.roomBrowserHeadersStyle.padding = new RectOffset(7, 7, 7, 7);
		
		GameSettings.createRoomOptionsStyle.font = guiSkin.font;
		GameSettings.createRoomOptionsStyle = new GUIStyle(guiSkin.label);
		GameSettings.createRoomOptionsStyle.alignment = TextAnchor.MiddleCenter;

		GameSettings.timeStyle.font = guiSkin.font;
		GameSettings.timeStyle.normal.textColor = Color.white;
		GameSettings.timeStyle.fontSize = 22;
		GameSettings.timeStyle.alignment = TextAnchor.UpperCenter;
		
		GameSettings.finalScreenStyle.normal.textColor = Color.white;
		GameSettings.finalScreenStyle.normal.background = guiSkin.box.normal.background;
		GameSettings.finalScreenStyle.fontSize = 15;
		GameSettings.finalScreenStyle.alignment = TextAnchor.UpperCenter;
		GameSettings.finalScreenStyle.padding = new RectOffset(15, 15, 15, 15);
		
		GameSettings.hudInfoStyle.font = guiSkin.font;
		GameSettings.hudInfoStyle.normal.textColor = Color.white;
		GameSettings.hudInfoStyle.fontSize = 22;
		GameSettings.hudInfoStyle.alignment = TextAnchor.LowerLeft;
		
		GameSettings.weaponInfoStyle = new GUIStyle(GameSettings.hudInfoStyle);
		GameSettings.weaponInfoStyle.alignment = TextAnchor.LowerRight;
		
		GameSettings.keyPressStyle = new GUIStyle(guiSkin.label);
		GameSettings.keyPressStyle.alignment = TextAnchor.MiddleCenter;
		
		GameSettings.buyMenuButtonStyle = new GUIStyle(guiSkin.button);
		GameSettings.buyMenuButtonStyle.alignment = TextAnchor.MiddleLeft;
		GameSettings.buyMenuButtonStyle.padding = new RectOffset(7, 7, 7, 7);
		
		GameSettings.actionReportStyle = new GUIStyle(guiSkin.label);
		GameSettings.actionReportStyle.alignment = TextAnchor.UpperRight;
	}
}
