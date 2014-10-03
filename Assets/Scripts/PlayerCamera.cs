using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour 
{
	public Player player;

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Vector3 point = camera.WorldToViewportPoint(player.transform.position);
		Vector3 delta = player.transform.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
		Vector3 destination = transform.position + delta;
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
	}
}
