using UnityEngine;
using System.Collections;

public class LoadingInGameState : MonoBehaviour 
{
	public Transform loadingIcon;
	public UILabel loadingLabel;

	private float _loadingLabelTimeCounter = 0f;
	private int _loadingLabelState = 0;

	void Start () 
	{
		
	}
	void OnEnable()
	{
		StartCoroutine (CoroutineTest ());
	}
	IEnumerator CoroutineTest()
	{
		yield return new WaitForSeconds (5f);
		gameObject.transform.parent.GetComponent<InGameSceneManager> ().ChangeToState(InGameSceneManager.InGameStates.GAME);
	}
	void OnDisable()
	{

	}
	void Update () 
	{
		loadingIcon.Rotate (Vector3.forward, -40f * Time.deltaTime);
		_loadingLabelTimeCounter += Time.deltaTime * 1.3f;
		if (_loadingLabelTimeCounter >= 1.0f)
			UpdateLabelState();
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
}
