using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootsManager
{	
	#region Singleton
	private static ShootsManager _instance;
	public static ShootsManager GetInstance()
	{
		if (_instance == null)
		{
			_instance = new ShootsManager();
			_instance.StartUp();
		}
		return _instance;
	}
	#endregion
	
	public static ShootSprites bulletSprites;
	
	private void StartUp()
	{
		if (bulletSprites == null)
		{
			bulletSprites = ((GameObject)GameObject.Instantiate(Resources.Load<GameObject> ("Code Structures/ShootSprites"))).GetComponent<ShootSprites>();
			bulletSprites.name = "ShootSprites";
			GameObject.DontDestroyOnLoad(bulletSprites.gameObject);
		}
	}
	
	public Sprite GetShootSpritesSprite(GlobalInfo.ShootTypes p_bulletType)
	{
		return bulletSprites.sprites [(int)p_bulletType];
	}
}
