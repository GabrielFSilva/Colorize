using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour 
{
	public GlobalInfo.PlaformType platformType;
	public SpriteRenderer spriteRenderer;
	public bool isLocked;

	void Start () 
	{
		ProcessPlatformType ();

		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();

		UpdatePlatformType ();
	}

	void Update () 
	{
	
	}

	void OnMouseDown()
	{
		platformType = (GlobalInfo.PlaformType)Random.Range (0, 9);
		UpdatePlatformType ();
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
		UpdatePlatformType ();
	}
}
