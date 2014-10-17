﻿using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour 
{
	public GlobalInfo.PlaformType platformType;
	public GlobalInfo.PlaformType platformStartType;
	public SpriteRenderer spriteRenderer;
	public SpriteRenderer tutorialFocusSprite;
	public bool isLocked;

	public int tutorialFocusIndex;
	public bool onTutorialMode = false;

	void Start () 
	{
		ProcessPlatformType ();

		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();

		UpdatePlatformType ();
	}

	void Update () 
	{
		if (onTutorialMode)
			tutorialFocusSprite.transform.Rotate (Vector3.forward, -40f * Time.deltaTime);
	}

	void OnMouseDown()
	{
		//platformType = (GlobalInfo.PlaformType)Random.Range (0, 9);
		//UpdatePlatformType ();
	}

	void ProcessPlatformType()
	{
		if ((int)platformType >= (int)GlobalInfo.PlaformType.LOCKED_WHITE && (int)platformType <= (int)GlobalInfo.PlaformType.LOCKED_GREEN)
			isLocked = true;
		else
			isLocked = false;
	}

	void UpdatePlatformType ()
	{
		ProcessPlatformType ();
		spriteRenderer.sprite = PlatformsManager.GetInstance ().GetPlatformSprite (platformType);
	}

	public void ChangePlatformType(GlobalInfo.PlaformType p_platType)
	{
		if (platformType == p_platType)
			return;

		platformType = p_platType;

		if (p_platType == GlobalInfo.PlaformType.BLACK)
			collider2D.isTrigger = true;
		else
			collider2D.isTrigger = false;

		UpdatePlatformType ();
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
				spriteRenderer.color = Color.white;

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
			spriteRenderer.color = Color.white;
			
		}


	}
}



