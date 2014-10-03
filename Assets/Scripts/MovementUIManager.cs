using UnityEngine;
using System.Collections;

public class MovementUIManager : MonoBehaviour 
{
	public GFSCustomButton leftButton;
	public GFSCustomButton rightButton;
	public GFSCustomButton jumpButton;
	public Player player;
	// Use this for initialization
	void Awake ()
	{

	}
	void Start () 
	{
		player = GameObject.Find ("Player").GetComponent<Player> ();
		
	}
	
	void OnEnable()
	{
		leftButton.onPress += ArrowButtonPressed;
		rightButton.onPress += ArrowButtonPressed;
		jumpButton.onPress += ArrowButtonPressed;
	}
	
	void OnDisable()
	{
		leftButton.onPress -= ArrowButtonPressed;
		rightButton.onPress -= ArrowButtonPressed;
		jumpButton.onPress -= ArrowButtonPressed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
	}
	
	void ArrowButtonPressed(bool p_pressed,string p_name)
	{
		if (p_name == "LeftArrow")
			player.leftArrow = p_pressed;
		else if (p_name == "RightArrow")
			player.rightArrow = p_pressed;
		else if (p_name == "UpArrow" && p_pressed)
			player.Jump();
	}

}
