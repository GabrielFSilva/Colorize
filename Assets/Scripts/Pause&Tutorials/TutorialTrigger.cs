using UnityEngine;
using System.Collections;

public class TutorialTrigger : MonoBehaviour 
{
	public int 					tutorialIndex;
	public float				tutorialDuration;
	public bool 				isActive = true;
	public InGameSceneManager	gameSceneManager;

	
	void OnTriggerEnter2D(Collider2D p_coll) 
	{
		//if it collides with a player
		if (p_coll.gameObject.name == "Player")
		{
			if (!isActive)
				return;
			Debug.Log("Trigger Tutorial " + tutorialIndex);

			isActive = false;
			gameSceneManager.currentTutorialIndex = tutorialIndex;
			gameSceneManager.currentTutorialDuration = tutorialDuration;
			gameSceneManager.ChangeToState(InGameSceneManager.InGameStates.TUTORIAL);
		}
	}
}
