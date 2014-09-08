using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformsManager
{
	#region Singleton
	private static PlatformsManager _instance;
	public static PlatformsManager GetInstance()
	{
		if (_instance == null)
		{
			_instance = new PlatformsManager();
			_instance.StartUp();
		}
		return _instance;
	}
	#endregion

	public static PlatformSprites platformSprites;

	private void StartUp()
	{
		if (platformSprites == null)
		{
			platformSprites = ((GameObject)GameObject.Instantiate(Resources.Load<GameObject> ("Code Structures/PlatformSprites"))).GetComponent<PlatformSprites>();
			platformSprites.name = "PlatformSprites";
			GameObject.DontDestroyOnLoad(platformSprites.gameObject);
		}
	}

	public Sprite GetPlatformSprite(GlobalInfo.PlaformType p_platType)
	{
		return platformSprites.sprites [(int)p_platType];
	}
}
