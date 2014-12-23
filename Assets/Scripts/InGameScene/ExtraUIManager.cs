using UnityEngine;
using System.Collections;

public class ExtraUIManager : MonoBehaviour 
{
	public InGameSceneManager	gameSceneManager;
	public ShootsUIManager		shootUIManager;
	public GFSCustomButton 		pauseButton;
	public GFSCustomButton 		replayButton;
	public GFSCustomButton 		overviewModeButton;
	public GFSCustomButton 		clickZone;

	public UIPanel				panel;
	public Vector2				panelSize;
	public UISprite				exitIndicator;
	public EnergySphere			energySphere;

	public LevelInfo			levelInfo;

	public Player player;
	
	void Start () 
	{
		player = LevelInfo.player;
		energySphere = LevelInfo.energySphere;

		panelSize = new Vector2 (panel.width - (panel.width/2), panel.height - (panel.height/2));

		if (InGameSceneManager.selectedChapter == 1 || (InGameSceneManager.selectedChapter == 2 && InGameSceneManager.selectedStage == 1))
			overviewModeButton.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Vector3.Distance(player.transform.position, energySphere.transform.position) > 7.0f && !(InGameSceneManager.selectedChapter == 1 && InGameSceneManager.selectedStage == 1))
		{
			exitIndicator.enabled = true;
			float __angle = Mathf.Atan2(player.transform.position.y - energySphere.transform.position.y, player.transform.position.x - energySphere.transform.position.x);

			exitIndicator.transform.localPosition = new Vector3 ((panelSize.x * Mathf.Cos(__angle)) * -.9f , panelSize.y * Mathf.Sin(__angle)* -.9f, 0.0f);
			exitIndicator.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * __angle,Vector3.forward);
		}
		else
			exitIndicator.enabled = false;

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

	
	void PauseButtonClicked(string p_name)
	{
		gameSceneManager.ChangeToState(InGameSceneManager.InGameStates.PAUSE);
	}
	void ReplayButtonClicked(string p_name)
	{

		levelInfo.RestartStage ();
		shootUIManager.RestartAmmo ();
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
