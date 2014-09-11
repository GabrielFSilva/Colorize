using UnityEngine;
using System.Collections;

public class Shoot: MonoBehaviour 
{
	public GlobalInfo.ShootTypes shootType;
	public SpriteRenderer spriteRenderer;

	public float yDeath;

	void Start () 
	{
		//get the sprite rendered
		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();

		//updates de shoot type
		UpdateShootType ();

		//add the start force
		transform.rigidbody2D.AddRelativeForce (Vector2.right * 300f);
	}

	void Update () 
	{
		//Rotate the shoot towards it's velocity direction
		transform.rotation = Quaternion.AngleAxis (Mathf.Rad2Deg * Mathf.Atan2 (rigidbody2D.velocity.normalized.y,rigidbody2D.velocity.normalized.x), Vector3.forward);

		//y death limit
		if (transform.position.y < yDeath)
			Destroy(gameObject);
	}
	
	void UpdateShootType ()
	{
		spriteRenderer.sprite = ShootsManager.GetInstance ().GetShootSpritesSprite (shootType);
	}
	
	void OnTriggerEnter2D(Collider2D p_coll) 
	{
		//if it collides with a platform
		if (p_coll.gameObject.name == "Platform")
		{
			p_coll.gameObject.GetComponent<Platform>().ChangePlatformType((GlobalInfo.PlaformType)((int)shootType));
			Destroy (this.gameObject);
		}
	}
}
