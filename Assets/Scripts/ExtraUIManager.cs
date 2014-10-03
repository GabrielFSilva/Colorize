using UnityEngine;
using System.Collections;

public class ExtraUIManager : MonoBehaviour 
{
	public GFSCustomButton pauseButton;
	public GFSCustomButton clickZone;

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
		pauseButton.onClick += PauseButtonClicked;
		clickZone.onPress += ClickZonePressed;
	}

	void OnDisable()
	{
		pauseButton.onClick -= PauseButtonClicked;
		clickZone.onPress -= ClickZonePressed;
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	
	void PauseButtonClicked(string p_name)
	{
		Debug.Log ("Pause");
	}

	void ClickZonePressed(bool p_pressed,string p_name)
	{
		if (p_pressed)
			player.CreateShoot ();
	}

}
