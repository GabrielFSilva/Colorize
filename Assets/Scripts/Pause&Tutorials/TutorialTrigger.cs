using UnityEngine;
using System.Collections;

public class TutorialTrigger : MonoBehaviour 
{
	public int 					tutorialIndex;
	public float				tutorialDuration;
	public bool 				isActive = true;
	public bool					useImage = false;
	public string				imageName = "";

	public Vector3				cameraPosition;

	public InGameSceneManager	gameSceneManager;

	void Start()
	{
		if (gameSceneManager == null)
			gameSceneManager = ((GameObject)GameObject.Find("InGameSceneManager")).GetComponent<InGameSceneManager>();
	}
	void OnTriggerEnter2D(Collider2D p_coll) 
	{
		//if it collides with a player
		if (p_coll.gameObject.name == "Player")
		{
			if (!isActive)
				return;
			//Debug.Log("Trigger Tutorial " + tutorialIndex);

			//EnableTrigger(false);

			TutorialManager.currentTutorialIndex = tutorialIndex;
			//TutorialManager.currentTutorialDuration = tutorialDuration;
			TutorialManager.currentTutorialCameraPosition = cameraPosition;
			TutorialManager.currentTutorialImageName = imageName;
			gameSceneManager.ChangeToState(InGameSceneManager.InGameStates.TUTORIAL);
		}
	}

	public void EnableTrigger(bool p_enable)
	{
		isActive = p_enable;
		particleSystem.renderer.enabled = p_enable;
	}
}
