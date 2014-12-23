using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
	//====================================================================//
	//Player Movement Variables
	//====================================================================//
	public Animator		animator;
	public Collider2D[] collisions;

	public Transform groundCheckA;
	public Transform groundCheckB;

	public float commonJumpForce;
	public float buffedJumpForce;

	public float commonRunForce;
	public float buffedRunForce;

	public bool leftArrow = false;
	public bool rightArrow = false;
	
	public bool  grounded = true;
	private bool _groundedFlag = false;

	public float onAirForceMultiplier;
	public float maxHorizontalVelocity;
	public float maxHorizontalBuffedVelocity;

	public float 		yDeath;

	public bool 		isInverted = false;

	public bool 		isPaused = false;
	public Vector2 		playerVelocity;
	public float		playerAngularVelocity;

	//====================================================================//
	//Buff Management Variables
	//====================================================================//
	public float redBuffDuration;
	private bool _redBuffActive = false;
	private float _redBuffTimer = 0.0f;
	public GameObject redBuffParticle;
	
	public float blueBuffDuration;
	private bool _blueBuffActive = false;
	private float _blueBuffTimer = 0.0f;
	public GameObject blueBuffParticle;
	
	public float greenBuffDuration;
	private bool _greenBuffActive = false;
	private float _greenBuffTimer = 0.0f;
	public GameObject greenBuffParticle;



	//====================================================================//
	//Shoots Management Variables
	//====================================================================//
	public List<Shoot> activeShootsList;
	public GameObject shootPrefab;
	public ShootsUIManager shootsUIManager;
	public GlobalInfo.ShootTypes shootType = GlobalInfo.ShootTypes.RED;
	public float shootSpawnDistance;

	public float shootCooldownDuration;
	private float shootCooldownTimer;
	private bool shootOnCooldown = false;
	//====================================================================//
	void Awake()
	{
		shootPrefab = Resources.Load<GameObject> ("Prefabs/Shoot");
	}
	void Start () 
	{
	}
	/// <summary>
	/// the fixed update is used to apply forces and move the player's game object.
	/// </summary>
	void FixedUpdate()
	{
		if (leftArrow || Input.GetKey(KeyCode.A))
		{
			if (_blueBuffActive)
				rigidbody2D.AddForce(new Vector2(-1f * buffedRunForce,0));
			else
				rigidbody2D.AddForce(new Vector2(-1f * commonRunForce,0));
			transform.localScale = new Vector3(-1f,transform.localScale.y,1f);
		}
		else if (rightArrow || Input.GetKey(KeyCode.D))
		{
			if (_blueBuffActive)
				rigidbody2D.AddForce(new Vector2(buffedRunForce,0));
			else
				rigidbody2D.AddForce(new Vector2(commonRunForce,0));
			transform.localScale = new Vector3(1f,transform.localScale.y,1f);
		}

		if (_blueBuffActive)
			rigidbody2D.velocity = new Vector2 (Mathf.Clamp(rigidbody2D.velocity.x, maxHorizontalBuffedVelocity *-1 ,maxHorizontalBuffedVelocity), rigidbody2D.velocity.y);
		else
			rigidbody2D.velocity = new Vector2 (Mathf.Clamp(rigidbody2D.velocity.x, maxHorizontalVelocity *-1 ,maxHorizontalVelocity), rigidbody2D.velocity.y);
	

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
			Jump();

		if (Input.GetKeyDown(KeyCode.P))
			PausePlayer(!isPaused);

		animator.SetFloat ("SpeedX",Mathf.Abs(rigidbody2D.velocity.x));
		animator.SetFloat ("SpeedY",rigidbody2D.velocity.y);
		animator.SetFloat ("SpeedYAbs",Mathf.Abs(rigidbody2D.velocity.y));
		animator.SetBool ("AddingForce", leftArrow || rightArrow || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D) ? true : false);
	}

	/// <summary>
	/// The update function get all the objects that are in the between the groundCheckA and groundCheckB, process the grounded-check and update buffs duration.
	/// </summary>
	void Update () 
	{
		collisions = Physics2D.OverlapAreaAll (groundCheckA.transform.position, groundCheckB.transform.position);

		if (collisions.Length > 0)
		{
			_groundedFlag = false;
			for (int i = 0; i < collisions.Length; i ++)
			{
				if (collisions[i].name == "Platform")
					PlatformCollision(collisions[i].GetComponent<Platform>().platformType);
			}
			grounded = _groundedFlag;

			if (grounded &&  Mathf.Abs(rigidbody2D.velocity.y)> 0.1f)
				grounded = false;
		}
		else
			grounded = false;


		UpdateBuffs ();

		if (shootOnCooldown)
		{
			shootCooldownTimer += Time.deltaTime;
			if (shootCooldownTimer > shootCooldownDuration)
				shootOnCooldown = false;
		}
		animator.SetBool ("Grounded", grounded);
	}

	/// <summary>
	/// Performs a jump. This function is called by the UpArrow in the MovementUIManager
	/// </summary>
	public void Jump()
	{
		if (isPaused)
			return;

		if (grounded)
		{
			if (_redBuffActive)
				rigidbody2D.AddForce(new Vector2(0,buffedJumpForce * rigidbody2D.gravityScale));
			else
				rigidbody2D.AddForce(new Vector2(0,commonJumpForce * rigidbody2D.gravityScale));
		}

	}

	/// <summary>
	/// Inverts the gravity, changing player Y scale and rigidbody gravity scale
	/// </summary>
	public void InvertGravity()
	{
		isInverted = !isInverted;

		rigidbody2D.gravityScale *= -1f;
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y * -1f, 1f);
	}


	/// <summary>
	/// Update the buffs duration and removes then if the time has expired.
	/// </summary>
	void UpdateBuffs()
	{
		if (isPaused)
			return;

		if (_redBuffActive)
		{
			_redBuffTimer += Time.deltaTime;
			if (_redBuffTimer >= redBuffDuration && grounded)
				_redBuffActive = false;
		}
		if (_blueBuffActive)
		{
			_blueBuffTimer += Time.deltaTime;
			if (_blueBuffTimer >= blueBuffDuration && grounded)
				_blueBuffActive = false;
		}
		if (_greenBuffActive)
		{
			_greenBuffTimer += Time.deltaTime;
			if (_greenBuffTimer >= greenBuffDuration && grounded)
				_greenBuffActive = false;
		}

		redBuffParticle.SetActive (_redBuffActive);
		blueBuffParticle.SetActive (_blueBuffActive);
		greenBuffParticle.SetActive (_greenBuffActive);
	}

	/// <summary>
	/// Grants buffs and grounded-check based on the platform the player collides.
	/// </summary>
	/// <param name="p_platType">P_plat type.</param>
	void PlatformCollision(GlobalInfo.PlaformType p_platType)
	{
		if (isPaused)
			return;

		if (p_platType == GlobalInfo.PlaformType.WHITE || p_platType == GlobalInfo.PlaformType.LOCKED_WHITE)
		{
			_groundedFlag = true;
			return;
		}
		else if (p_platType == GlobalInfo.PlaformType.BLACK)
			return;

		else if (p_platType == GlobalInfo.PlaformType.RED || p_platType == GlobalInfo.PlaformType.LOCKED_RED)
		{
			_groundedFlag = true;
			_redBuffActive = true;
			_redBuffTimer = 0.0f;
		}
		else if (p_platType == GlobalInfo.PlaformType.BLUE || p_platType == GlobalInfo.PlaformType.LOCKED_BLUE)
		{
			_groundedFlag = true;
			_blueBuffActive = true;
			_blueBuffTimer = 0.0f;
		}
		else if (p_platType == GlobalInfo.PlaformType.GREEN || p_platType == GlobalInfo.PlaformType.LOCKED_GREEN)
		{
			_groundedFlag = true;
			if (!_greenBuffActive)
			{
				_greenBuffActive = true;
				_greenBuffTimer = 0.0f;
				InvertGravity();
			}
		}
	}

	/// <summary>
	/// Creates a shoot. This function if called by the ClickZone in ExtraUIManager.
	/// </summary>
	public void CreateShoot()
	{
		if (shootOnCooldown)
			return;

		if (isPaused)
			return;

		if (shootsUIManager.GetAmmo(shootType) == 0)
			return;

		shootOnCooldown = true;
		shootCooldownTimer = 0.0f;
		shootsUIManager.DescreaseAmmo (shootType);
		//player position in relation to the screen
		Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint (transform.position);

		if (Input.mousePosition.x > playerScreenPosition.x && transform.localScale.x < 0f)
			transform.localScale = new Vector3(1f,transform.localScale.y,1f);
		if (Input.mousePosition.x < playerScreenPosition.x && transform.localScale.x > 0f)
			transform.localScale = new Vector3(-1f,transform.localScale.y,1f);
		//angle in rad between player and the click position
		float __angle = Mathf.Atan2 (Input.mousePosition.y - playerScreenPosition.y, Input.mousePosition.x - playerScreenPosition.x);

		//position that the shoot will be spawned
		Vector3 __shootPosition = new Vector3 (transform.position.x + (shootSpawnDistance*Mathf.Cos (__angle)), transform.position.y + (shootSpawnDistance*Mathf.Sin (__angle)), transform.position.z);

		//Instantiate the object with the right direction;
		GameObject __tempShoot =  (GameObject)GameObject.Instantiate (shootPrefab, __shootPosition, Quaternion.AngleAxis(Mathf.Rad2Deg * __angle,Vector3.forward));
		__tempShoot.GetComponent<Shoot> ().playerReference = this;
		__tempShoot.GetComponent<Shoot> ().shootType = shootType;
		__tempShoot.GetComponent<Shoot> ().onDestroy += delegate(Shoot obj) {
			activeShootsList.Remove(obj);
		};

		activeShootsList.Add(__tempShoot.GetComponent<Shoot>());
		

	}

	public void PausePlayer(bool p_willPause)
	{
		if (p_willPause)
		{
			playerVelocity = rigidbody2D.velocity;
			playerAngularVelocity = rigidbody2D.angularVelocity;
			rigidbody2D.isKinematic = true;
		}
		else
		{
			rigidbody2D.isKinematic = false;
			rigidbody2D.velocity = playerVelocity;
			rigidbody2D.angularVelocity = playerAngularVelocity;
		}
		foreach (Shoot shoot in activeShootsList)
			shoot.PauseShoot(p_willPause);

		isPaused = p_willPause;
	}

	public void ResetVelocity()
	{
		playerVelocity = Vector2.zero;
		playerAngularVelocity = 0f;
		leftArrow = false;
		rightArrow = false;
	}
	
}
