using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public GlobalInfo.ShootTypes bulletType;
	public SpriteRenderer spriteRenderer;

	public float yDeath;

	void Start () 
	{
		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();
		
		UpdateBulletType ();
		transform.rigidbody2D.AddRelativeForce (Vector2.right * 300f);
	}

	void Update () 
	{
		if (transform.position.y < yDeath)
			Destroy(gameObject);
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
	
	void OnTriggerEnter2D(Collider2D p_coll) 
	{
		if (p_coll.gameObject.name == "Platform")
		{
			p_coll.gameObject.GetComponent<Platform>().ChangePlatformType((GlobalInfo.PlaformType)((int)bulletType));
			Destroy (this.gameObject);
		}
	}
}
