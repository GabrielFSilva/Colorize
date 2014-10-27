using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingInGameState : MonoBehaviour 
{
	public LevelLoader	levelLoader;
	public Transform loadingIcon;
	public UILabel loadingLabel;

	private float _loadingLabelTimeCounter = 0f;
	private int _loadingLabelState = 0;

	public UITexture			fadeTexture;
	
	public float 	fadeDuration;
	private bool	fadeActive = false;
	private float	fadeTimeCount;

	public List<UIPanel>		hintPanels;

	void Start () 
	{
		
	}
	void OnEnable()
	{
		StartFade ();
		StartCoroutine (CoroutineTest ());

		DisplayHint ();
	}

	void OnDisable()
	{
		fadeActive = false;
		fadeTimeCount = 0f;
		fadeTexture.alpha = 0f;
	}
	void Update () 
	{
		loadingIcon.Rotate (Vector3.forward, -90f * Time.deltaTime);
		_loadingLabelTimeCounter += Time.deltaTime * 1.3f;
		if (_loadingLabelTimeCounter >= 1.0f)
			UpdateLabelState();

		if (fadeActive)
		{
			fadeTimeCount += Time.deltaTime/fadeDuration;
			fadeTexture.alpha = 1f - fadeTimeCount;
		}
	}
	private void UpdateLabelState()
	{
		if (_loadingLabelState == 0)
		{
			_loadingLabelState = 1;
			loadingLabel.text = "Loading.";
		}
		else if (_loadingLabelState == 1)
		{
			_loadingLabelState = 2;
			loadingLabel.text = "Loading..";
		}
		else if (_loadingLabelState == 2)
		{
			_loadingLabelState = 3;
			loadingLabel.text = "Loading...";
		}
		else
		{
			_loadingLabelState = 0;
			loadingLabel.text = "Loading";
		}
		_loadingLabelTimeCounter = 0f;
	}

	private void DisplayHint()
	{
		int _index = Random.Range (0,hintPanels.Count);

		foreach (UIPanel panel in hintPanels)
		{
			panel.gameObject.SetActive( _index == hintPanels.IndexOf(panel) ? true : false);
		}
	}
	void StartFade ()
	{
		fadeActive = true;
		fadeTimeCount = 0.0f;
		StartCoroutine (WaitFade ());
	}
	IEnumerator CoroutineTest()
	{
		if (Application.loadedLevelName == "InGameScene")
			levelLoader.LoadLevel ();
		yield return new WaitForSeconds (0f);
		gameObject.transform.parent.GetComponent<InGameSceneManager> ().ChangeToState(InGameSceneManager.InGameStates.GAME);


	}
	IEnumerator WaitFade()
	{
		yield return new WaitForSeconds (fadeDuration);
		fadeActive = false;

	}
}
