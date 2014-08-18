using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class KScene<T> : KStateMachine<T>
{
	    
	private static KScene<T> _instance;
	public static KScene<T> instance { get { return _instance; } }
	     
	public bool disableSounds = false;

	public virtual void Awake()
	{
		//Defines singleton	
		if(instance != null)
		{
			Debug.LogError("Please, make sure you have only 1 AScene on the screen (Scene Manager).");
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}

		//Check for sounds	
		if(disableSounds && Application.isEditor)
		{
			AudioListener.volume = 0;
		}
	}
	
	/// <summary>
	/// Called after the load time but before the first Update of the scene.
	/// </summary>
	public virtual void Start()
	{
		base.KInitialize();
	}
	 
	/// <summary>
	/// Called every frame.
	/// </summary>
	public virtual void Update()
	{
		base.KUpdate();
	} 
	 
	/// <summary>
	/// Called every frame after the Update method.
	/// </summary>
	public virtual void LateUpdate()
	{
		base.KLateUpdate();
	}
	 
	/// <summary>
	/// Called every Physics frame step.
	/// </summary>
	public virtual void FixedUpdate()
	{
		base.KFixedUpdate();
	}
	 
	/// <summary>
	/// Method responsable for drawing elements in the HUD. Can be called more than once per frame.
	/// </summary>
	public virtual void OnGUI()
	{	
		base.KOnGUI();
	}
}
