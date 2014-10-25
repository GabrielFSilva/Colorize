using UnityEngine;
using System.Collections;

public class TutorialTrigger : MonoBehaviour 
{
	public int 					tutorialIndex;
	public float				tutorialDuration;
	public bool 				isActive = true;
	public Vector3				cameraPosition;
	
	public InGameSceneManager	gameSceneManager;
	
	void OnTriggerEnter2D(Collider2D p_coll) 
	{
		//if it collides with a player
		if (p_coll.gameObject.name == "Player")
		{
			if (!isActive)
				return;
			Debug.Log("Trigger Tutorial " + tutorialIndex);

			EnableTrigger(false);

			TutorialManager.currentTutorialIndex = tutorialIndex;
			TutorialManager.currentTutorialDuration = tutorialDuration;
			TutorialManager.currentTutorialCameraPosition = cameraPosition;
			gameSceneManager.ChangeToState(InGameSceneManager.InGameStates.TUTORIAL);
		}
	}

	public void EnableTrigger(bool p_enable)
	{
		isActive = p_enable;
		particleSystem.renderer.enabled = p_enable;
	}
}
