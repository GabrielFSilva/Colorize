using UnityEngine;
using System.Collections;

public class ExtraUIManager : MonoBehaviour 
{
	public InGameSceneManager	gameSceneManager;
	public GFSCustomButton 		pauseButton;
	public GFSCustomButton 		replayButton;
	public GFSCustomButton 		overviewModeButton;
	public GFSCustomButton 		clickZone;

	public LevelInfo			levelInfo;

	public Player player;

	// Use this for initialization
	void Awake ()
	{

	}
	void Start () 
	{
		player = LevelInfo.player;

		if (InGameSceneManager.selectedChapter == 1 || (InGameSceneManager.selectedChapter == 2 && InGameSceneManager.selectedStage == 1))
			overviewModeButton.gameObject.SetActive(false);
	}
	
	void OnEnable()
	{
		pauseButton.onClick += PauseButtonClicked;
		replayButton.onClick += ReplayButtonClicked;
		overviewModeButton.onClick += OverviewModeButtonClicked;
		clickZone.onPress += ClickZonePressed;
	}

	void OnDisable()
	{
		pauseButton.onClick -= PauseButtonClicked;
		replayButton.onClick -= ReplayButtonClicked;
		overviewModeButton.onClick -= OverviewModeButtonClicked;
		clickZone.onPress -= ClickZonePressed;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	void PauseButtonClicked(string p_name)
	{
		gameSceneManager.ChangeToState(InGameSceneManager.InGameStates.PAUSE);
	}
	void ReplayButtonClicked(string p_name)
	{
		levelInfo.RestartStage ();
	}
	void OverviewModeButtonClicked(string p_name)
	{
		gameSceneManager.ChangeToState(InGameSceneManager.InGameStates.OVERVIEW);
	}
	void ClickZonePressed(bool p_pressed,string p_name)
	{
		if (p_pressed)
			player.CreateShoot ();
	}

}
