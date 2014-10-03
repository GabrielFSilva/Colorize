using UnityEngine;
using System.Collections;

public class EnergySphere : MonoBehaviour 
{
	public SpriteRenderer 	spriteRenderer;
	public Transform 		floorTransform;
	public Transform 		ceilTransform;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		spriteRenderer.transform.position = Vector3.Lerp (floorTransform.position, ceilTransform.position, (Mathf.Cos(Time.time * 3.0f) *0.5f) + 0.5f);
	}

	void OnTriggerEnter2D(Collider2D p_coll) 
	{
		//if it collides with a platform
		if (p_coll.gameObject.name == "Player")
		{
			Debug.Log("Player got Energy Sphere");
			Destroy (this.gameObject);
			Application.LoadLevel("InGameScene");
		}
	}
}
