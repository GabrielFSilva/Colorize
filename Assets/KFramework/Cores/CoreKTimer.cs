using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CoreKTimer : MonoBehaviour
{
	    
	private static CoreKTimer 	_kTimerCore;
	public static CoreKTimer	kTimerCore
	{
		get
		{
			if(_kTimerCore == null)
			{
                _kTimerCore = FrameworkCore.instance.AddComponent<CoreKTimer>();
			}
			
			return _kTimerCore;
		}
	}
	 
	
	public float deltaTime = 0;
	private float _lastFrameTime;
	public float lastFrameTime
	{
		get { return _lastFrameTime; }
	}
	 
	
	    
	private List<KTimerNode> _nodules = new List<KTimerNode>();
	 
	
	/// <summary>
	/// 
	/// </summary>
	void Awake()
	{
		_lastFrameTime = Time.realtimeSinceStartup;
		_kTimerCore = this;
	}
	 
	
	/// <summary>
	/// 
	/// </summary>
	void Update()
	{
		if(_nodules.Count > 0)
		{
			//Delete needed nodules
			_nodules.RemoveAll(nodule => nodule == null);
			
			for(int i = 0; i < _nodules.Count; i ++)
			{
				if(_nodules[i].finished)
				{
					_nodules.Remove(_nodules[i]);
					GC.SuppressFinalize(_nodules[i]);
					i--; 
				}
			}
			
			//Update nodules
			for(int i = 0; i < _nodules.Count; i++)
			{
				_nodules[i].KTNUpdate();
			}
		}
	}
	 
	
	/// <summary>
	/// 
	/// </summary>
	void LateUpdate()
	{
		deltaTime = Time.realtimeSinceStartup - _lastFrameTime;
		if(deltaTime < 0)
			deltaTime = 0;
		_lastFrameTime = Time.realtimeSinceStartup;
	}
	 
	
	/// <summary>
	/// 
	/// </summary>
	public KTimerNode GetPointerTo(KTimerNode p_nodule)
	{
		foreach(var nodule in _nodules)
		{
			if(nodule == p_nodule)
				return p_nodule;
		}
		
		return null;
	}
	 
	
	/// <summary>
	/// 
	/// </summary>
	public void AddNodule(KTimerNode nodule)
	{
		if(_nodules == null)
			_nodules = new List<KTimerNode>();
		
		_nodules.Add(nodule);
	}
	
	public void RemoveNodule(KTimerNode nodule)
	{
		_nodules.Remove(nodule);
	}
	 
}
