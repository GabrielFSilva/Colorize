using UnityEngine;
using System.Collections;
using System;

public class FrameworkCore : MonoBehaviour 
{
    private static GameObject _instance;
    public static GameObject instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("FrameworkCore", typeof(FrameworkCore));
            }
            return _instance;
        }
    }
	
	public event Action onAllCoresCleared;
	
	
	void Awake()
	{
		if(_instance != null)
		{
			if(_instance != this)
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			_instance = this.gameObject;
			DontDestroyOnLoad(this.gameObject);
		} 
	}
	
	public static void ClearAllCores(bool leaveASound)
	{
		//Get all cores
		if(_instance == null)
			return;
		
		CoreKTimer timer = _instance.GetComponent<CoreKTimer>();
		CoreKSound sound = _instance.GetComponent<CoreKSound>();
		CoreKTween tween = _instance.GetComponent<CoreKTween>();
		
		if(timer != null)
		{
			Destroy(timer);
		}
		
		if(sound != null && !leaveASound)
		{
			sound.PrepareCoreToBeDestroyed();
			Destroy(sound);
		}
		
		if(tween != null)
		{
			Destroy(tween);
		}
	}
	
	public static void ClearAllCoresASync(bool leaveASound)
	{
		FrameworkCore script = _instance.GetComponent<FrameworkCore>();
		script.StartCoroutine(script.ClearAllCoresAsyncFunction(leaveASound));
	}
	
	private IEnumerator ClearAllCoresAsyncFunction(bool leaveASound)
	{	
		CoreKTimer timer = GetComponent<CoreKTimer>();
		yield return null;
		
		CoreKSound sound = GetComponent<CoreKSound>();
		yield return null;
		
		CoreKTween tween = GetComponent<CoreKTween>();
		yield return null;
		
		if(timer != null)
		{
			Destroy(timer);
			yield return null;
		}
		
		if(sound != null && !leaveASound)
		{
			sound.PrepareCoreToBeDestroyed();
			yield return null;
			Destroy(sound);
		}
		
		if(tween != null)
		{
			Destroy(tween);
			yield return null;
		}
		
		if(onAllCoresCleared != null) onAllCoresCleared();
	}
	
	public static Coroutine HostCoroutine(IEnumerator routine)
	{
		return _instance.GetComponent<FrameworkCore>().StartCoroutine(routine);
	}
}
