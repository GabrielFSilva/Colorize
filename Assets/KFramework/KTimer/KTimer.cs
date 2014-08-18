using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class KTimer
{
	public static float realDeltaTime
	{
		get
		{
			return CoreKTimer.kTimerCore.deltaTime;
		}
	}
	
	#region Wait Frames
	
	public static KTimerNode WaitFrames(int p_frames, Action p_method)
	{
		return TimedFunction(0, false, 0, p_frames, true, p_method);
	}
	
	#endregion
	
	
	#region Wait seconds
	
	public static KTimerNode WaitSeconds(float p_time, Action p_method)
	{
		return WaitSeconds(p_time, true, p_method);
	}
	public static KTimerNode WaitSeconds(float p_time, bool p_useUnityTime, Action p_method)
	{
		return TimedFunction(p_time, false, 0, 0, p_useUnityTime, p_method);
	}
	
	#endregion
	
	#region Timed Function
	
	/// <summary>
	/// Base method for all timer functions
	/// </summary>
    private static KTimerNode TimedFunction(float p_time,bool p_isLoopTimer, int p_repeatTime, int p_framesToWait, bool p_useUnityTime, Action p_method)
	{
		//Creates a new nodule to be add to the nodules' list
		KTimerNode __nodule = new KTimerNode();
		
		//Set its minimum parameters
		__nodule.isLoopTimer = p_isLoopTimer;
		__nodule.function += p_method;
		__nodule.timer = p_time;
        __nodule.framesToWait = p_framesToWait;
		__nodule.repeatTime = p_repeatTime;
		__nodule.usingRealTime = !p_useUnityTime;
		
		//Add the nodule to the list
		CoreKTimer.kTimerCore.AddNodule(__nodule);
		
		//Return it to be used to set listerners and such
		return CoreKTimer.kTimerCore.GetPointerTo(__nodule);
	}
	
	#endregion
}