using UnityEngine;
using System.Collections;

public class EnergySphere : MonoBehaviour 
{
	public SpriteRenderer 	spriteRenderer;
	public Transform 		floorTransform;
	public Transform 		ceilTransform;
	public float 			movementSpeed;
	private float 			timeCount;

	public InGameSceneManager		gameSceneManager;

	public bool 					isPaused = false;

	public GlobalInfo.ShootTypes	sphereType;

	void Start () 
	{
		timeCount = Time.time;
	}

	void Update ()
	{
		if (!isPaused)
		{
			timeCount += Time.deltaTime * movementSpeed;
			spriteRenderer.transform.position = Vector3.Lerp (floorTransform.position, ceilTransform.position, (Mathf.Cos(timeCount) *0.5f) + 0.5f);
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
}
