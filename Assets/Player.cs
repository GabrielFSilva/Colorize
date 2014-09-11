using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public GameObject shootPrefab;

	public GlobalInfo.ShootTypes shootType = GlobalInfo.ShootTypes.RED;

	public float shootSpawnDistance;

	public bool leftArrow = false;
	public bool rightArrow = false;
	// Use this for initialization

	void Awake()
	{
		//find the bullet prefab
		shootPrefab = Resources.Load<GameObject> ("Prefabs/Shoot");
	}
	void Start () 
	{
	
	}

	void Update () 
	{
		if (leftArrow)
		{
			rigidbody2D.AddForce(new Vector2(-10,0));
			transform.localScale = new Vector3(-1f,1f,1f);
		}
		else if (rightArrow)
		{
			rigidbody2D.AddForce(new Vector2(10,0));
			transform.localScale = new Vector3(1f,1f,1f);
		}

		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
			rigidbody2D.AddForce(new Vector2(0,300));

		//if (Input.GetMouseButtonDown(0))
		//	CreateShoot();


	}

	public void CreateShoot()
	{
		//player position in relation to the screen
		Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint (transform.position);
		//angle in rad between player and the click position
		float __angle = Mathf.Atan2 (Input.mousePosition.y - playerScreenPosition.y, Input.mousePosition.x - playerScreenPosition.x);

		//position that the shoot will be spawned
		Vector3 __shootPosition = new Vector3 (transform.position.x + (shootSpawnDistance*Mathf.Cos (__angle)), transform.position.y + (shootSpawnDistance*Mathf.Sin (__angle)), transform.position.z);

		//Instantiate the object with the right direction;
		GameObject __tempShoot =  (GameObject)GameObject.Instantiate (shootPrefab, __shootPosition, Quaternion.AngleAxis(Mathf.Rad2Deg * __angle,Vector3.forward));
		__tempShoot.GetComponent<Shoot> ().shootType = shootType;


	}
}
