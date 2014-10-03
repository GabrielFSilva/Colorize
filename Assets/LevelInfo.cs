using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelInfo : MonoBehaviour 
{
	public List<Platform> 		platformsList;
	public Player				player;
	public Vector3				playerSpawnPosition;

	void Start()
	{
		//platformsList = new List<Platform>(GameObject.FindObjectsOfType<Platform>());
		playerSpawnPosition = GameObject.Find ("Player").transform.position;
	}

	public void ClearInfo()
	{
		platformsList.Clear ();
		playerSpawnPosition = Vector3.zero;
	}
}
