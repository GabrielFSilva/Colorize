using UnityEngine;
using System.Collections;

public class MovementUIManager : MonoBehaviour 
{
	public GFSCustomButton leftButton;
	public GFSCustomButton rightButton;
	public Player player;
	// Use this for initialization
	void Awake ()
	{
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}
	void Start () 
	{
		
		
	}
	
	void OnEnable()
	{
		leftButton.onPress += ArrowButtonClicked;
		rightButton.onPress += ArrowButtonClicked;
	}
	
	void OnDisable()
	{
		leftButton.onPress -= ArrowButtonClicked;
		rightButton.onPress -= ArrowButtonClicked;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
	}
	
	void ArrowButtonClicked(bool p_pressed,string p_name)
	{
		if (p_name == "LeftArrow")
			player.leftArrow = p_pressed;
		else if (p_name == "RightArrow")
			player.rightArrow = p_pressed;
	}

}
