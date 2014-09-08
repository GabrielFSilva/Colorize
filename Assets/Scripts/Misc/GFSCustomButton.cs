using UnityEngine;
using System;

public class GFSCustomButton : MonoBehaviour 
{
	public event Action<string> onClick;
	public event Action<string> onDrop;
	public event Action<bool,string> onPress;

	void OnClick()
	{
		if(onClick != null)
		{
			onClick(gameObject.name);
		}
	}

	void OnDrop()
	{
		if(onDrop != null)
		{
			onDrop(gameObject.name);
		}
	}

	void OnPress(bool p_pressed)
	{
		if(onPress != null)
		{
			onPress(p_pressed,gameObject.name);
		}
	}
}
