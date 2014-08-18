using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum KStateType
{
	NORMAL,
	PERSISTENT
}

public abstract class KState<T> : MonoBehaviour
{
       
    public T state;
	
	[HideInInspector]
	public int priorityLevel = 0;
	
	[HideInInspector]
    public KStateType type = KStateType.NORMAL; 
	
	private KStateMachine<T> _manager;
	public KStateMachine<T> manager
	{
		get { return _manager; }
		set { _manager = value; }
	}
	
	public override string ToString ()
	{
		return state.ToString();
	}

    public virtual void KInitialize()
    { 
    
    }
	 
    /// <summary>
    /// Called every frame update.
    /// </summary>
    public virtual void KUpdate()
    { 
    
    }
	 
    public virtual void KFixedUpdate()
    { 
    
    }
	 
    /// <summary>
    /// Called after every frame update.
    /// </summary>
    public virtual void KLateUpdate()
    { 
    
    }
	 
    /// <summary>
    /// Called when manager enable this state.
    /// </summary>
    public virtual void KOnEnable()
    { 
    
    }
	
	/// <summary>
    /// Called when manager enable this state with a object as a enable parameter.
    /// </summary>
    public virtual void KOnEnable(object p_enableParameter)
    { 
    	
    }
	 
    /// <summary>
    /// Called when scene disable this state.
    /// </summary>
    public virtual void KOnDisable()
    { 
    
    }
	 
    /// <summary>
    /// Called on every GUI event.
    /// </summary>
    /// <remarks>
    /// Note that this method can be called more than once per frame.
    /// </remarks>
    public virtual void KOnGUI()
    { 
    
    }
	
	public virtual void KFinishState(bool p_isMainMenu)
	{
		
	}
}

