using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TGS_Constants
{
    public static ScenesList CurrentScene = ScenesList.MainMenu;
	public static int currentlevel = 1;
	public static int[] EnemyCounter;

	public static int scenetoload;
	public static int currentObject = 0;
	public static Vector3 respawnpos;
	public static Vector3 respawnrot;
	public static bool respawntriggered;
	public static int Levellife;
	public static bool oncefall;
	public static int reward = 300;
	public static bool accel;
	public static bool brake;
	public static bool steerright;
	public static bool steerleft;
	public static float[] leveltime;
	public static float bikespeed;
	public static bool hurdle;
	public static bool wheeling;
	public static int checkpointcounter = 0;
	public static int currentskin;
	public static bool treefall;
	public static bool play;
}
[System.Serializable]
public enum ScenesList
{
    MainMenu, LevelSelection, ObjectSelection, GamePlay
}
public enum Panels
{
    MainMenuPanel,
    LevelSelectionPanel,
    ObjectSelectionPanel,
    SettingsPanel,
    RateUsPanel,
    QuitPanel,
    ModeSelectionPanel,
    HUDMenuPanel
}
[System.Serializable]
public struct PanelObj
{
    public Panels Name;
    public GameObject PanelRef;
}
