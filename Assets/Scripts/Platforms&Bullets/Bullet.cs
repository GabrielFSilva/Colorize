using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public GlobalInfo.ShootTypes bulletType;
	public SpriteRenderer spriteRenderer;

	void Start () 
	{
		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();
		Debug.Log (name);
		
		UpdateBulletType ();
	}

	void Update () 
	{
	}
	
	void OnMouseDown()
	{
		bulletType = (GlobalInfo.ShootTypes)Random.Range (0, 4);
		UpdateBulletType ();
	}
	
	void UpdateBulletType ()
	{
		spriteRenderer.sprite = BulletsManager.GetInstance ().GetBulletSpritesSprite (bulletType);
	}

	void OnCollisionEnter2D(Collision2D p_coll) 
	{
		if (p_coll.gameObject.name == "Platform")
		{
			p_coll.gameObject.GetComponent<Platform>().ChangePlatformType((GlobalInfo.PlaformType)((int)bulletType));
			Destroy (this.gameObject);
		}
	}
}
