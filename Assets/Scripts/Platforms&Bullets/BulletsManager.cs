using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletsManager
{	
	#region Singleton
	private static BulletsManager _instance;
	public static BulletsManager GetInstance()
	{
		if (_instance == null)
		{
			_instance = new BulletsManager();
			_instance.StartUp();
		}
		return _instance;
	}
	#endregion
	
	public static BulletSprites bulletSprites;
	
	private void StartUp()
	{
		if (bulletSprites == null)
		{
			bulletSprites = ((GameObject)GameObject.Instantiate(Resources.Load<GameObject> ("Code Structures/BulletSprites"))).GetComponent<BulletSprites>();
			bulletSprites.name = "BulletSprites";
			GameObject.DontDestroyOnLoad(bulletSprites.gameObject);
		}
	}
	
	public Sprite GetBulletSpritesSprite(GlobalInfo.ShootTypes p_bulletType)
	{
		return bulletSprites.sprites [(int)p_bulletType];
	}
}
