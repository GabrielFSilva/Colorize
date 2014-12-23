using UnityEngine;
using System.Collections;

public class EnergySphere : MonoBehaviour 
{
	public SpriteRenderer 	spriteRenderer;
	public SpriteRenderer 	tutorialFocusSprite;
	public Transform 		floorTransform;
	public Transform 		ceilTransform;
	public float 			movementSpeed;
	private float 			timeCount;

	public InGameSceneManager		gameSceneManager;

	public bool 					isPaused = false;

	public GlobalInfo.ShootTypes	sphereType;

	public int tutorialFocusIndex;
	public bool onTutorialMode = false;
	
	public float tutorialSpriteScale = 1.25f;

	void Start () 
	{
		timeCount = Time.time;

		if (sphereType == GlobalInfo.ShootTypes.WHITE)
			spriteRenderer.color = Color.white;
		else if (sphereType == GlobalInfo.ShootTypes.RED)
			spriteRenderer.color = Color.red;
		else if (sphereType == GlobalInfo.ShootTypes.BLUE)
			spriteRenderer.color = Color.blue;
		else if (sphereType == GlobalInfo.ShootTypes.GREEN)
			spriteRenderer.color = Color.green;
		else if (sphereType == GlobalInfo.ShootTypes.BLACK)
			spriteRenderer.color = Color.black;
	}

	void Update ()
	{
		if (!isPaused)
		{
			timeCount += Time.deltaTime * movementSpeed;
			spriteRenderer.transform.position = Vector3.Lerp (floorTransform.position, ceilTransform.position, (Mathf.Cos(timeCount) *0.5f) + 0.5f);
		}
		if (onTutorialMode)
		{
			tutorialFocusSprite.transform.Rotate (Vector3.forward, -90f * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D p_coll) 
	{
		//if it collides with a player
		if (p_coll.gameObject.name == "Player")
		{
			Debug.Log("Player got Energy Sphere");
			ChaptersManager.GetInstance().CompleteStage(InGameSceneManager.selectedChapter -1,InGameSceneManager.selectedStage -1);
			Destroy (this.gameObject);
			gameSceneManager.ChangeToState(InGameSceneManager.InGameStates.FINISHED);
		}
	}

	public void PauseEnergySphere(bool p_willPause)
	{
		isPaused = p_willPause;
	}

	public void TutorialMode(bool p_willEnterOnTutorial, int p_tutorialIndex)
	{
		onTutorialMode = p_willEnterOnTutorial;
		
		//Entering Tutorial Mode
		if (p_willEnterOnTutorial)
		{
			//Is on focus during tutorial
			if (p_tutorialIndex == tutorialFocusIndex)
			{
				tutorialFocusSprite.gameObject.SetActive (true);
				if (sphereType == GlobalInfo.ShootTypes.WHITE)
					spriteRenderer.color = Color.white;
				else if (sphereType == GlobalInfo.ShootTypes.RED)
					spriteRenderer.color = Color.red;
				else if (sphereType == GlobalInfo.ShootTypes.BLUE)
					spriteRenderer.color = Color.blue;
				else if (sphereType == GlobalInfo.ShootTypes.GREEN)
					spriteRenderer.color = Color.green;
				else if (sphereType == GlobalInfo.ShootTypes.BLACK)
					spriteRenderer.color = Color.black;
				
			}
			//Isn't in focus
			else
			{
				tutorialFocusSprite.gameObject.SetActive (false);
				spriteRenderer.color = Color.gray;
			}
		}
		//Leaving Tutorial Mode
		else
		{
			tutorialFocusSprite.gameObject.SetActive (false);
			if (sphereType == GlobalInfo.ShootTypes.WHITE)
				spriteRenderer.color = Color.white;
			else if (sphereType == GlobalInfo.ShootTypes.RED)
				spriteRenderer.color = Color.red;
			else if (sphereType == GlobalInfo.ShootTypes.BLUE)
				spriteRenderer.color = Color.blue;
			else if (sphereType == GlobalInfo.ShootTypes.GREEN)
				spriteRenderer.color = Color.green;
			else if (sphereType == GlobalInfo.ShootTypes.BLACK)
				spriteRenderer.color = Color.black;
			
		}
		
		
	}
}
