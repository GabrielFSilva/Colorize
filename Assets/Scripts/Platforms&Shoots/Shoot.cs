using UnityEngine;
using System.Collections;
using System;

public class Shoot: MonoBehaviour 
{
	public event Action<Shoot> 		onDestroy;
	public GlobalInfo.ShootTypes 	shootType;
	public SpriteRenderer 			spriteRenderer;
	public Player 					playerReference;


	public float yDeath;

	// Pause Variables
	public bool 		isPaused = false;
	public Vector2 		shootVelocity;
	public float		shootAngularVelocity;


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
		if (isPaused)
			return;
		//Rotate the shoot towards it's velocity direction
		transform.rotation = Quaternion.AngleAxis (Mathf.Rad2Deg * Mathf.Atan2 (rigidbody2D.velocity.normalized.y,rigidbody2D.velocity.normalized.x), Vector3.forward);

		//y death limit
		if (transform.position.y < yDeath)
		{
			onDestroy(this);
			Destroy(gameObject);
		}
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
			Platform __tempPlat = p_coll.gameObject.GetComponent<Platform>();

			if (__tempPlat.platformType == GlobalInfo.PlaformType.BLACK)
			{
				if (shootType == GlobalInfo.ShootTypes.BLACK)
					return;
			}
			__tempPlat.ChangePlatformType((GlobalInfo.PlaformType)((int)shootType));

			onDestroy(this);
			Destroy (this.gameObject);
		}
	}

	public void PauseShoot(bool p_pause)
	{
		if (p_pause)
		{
			shootVelocity = rigidbody2D.velocity;
			shootAngularVelocity = rigidbody2D.angularVelocity;
			rigidbody2D.isKinematic = true;
		}
		else
		{
			rigidbody2D.isKinematic = false;
			rigidbody2D.velocity = shootVelocity;
			rigidbody2D.angularVelocity = shootAngularVelocity;
		}
		
		isPaused = p_pause;
	}
}
