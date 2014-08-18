using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[System.Serializable]	
public class KTimerNode
{
	public bool 		isAlive
	{
		get { return !finished; }
	}
	public bool 		finished;
	public bool 		usingRealTime;
	public bool			isLoopTimer;
	public float		timer;
	public int			repeatTime;
    public int          framesToWait;
	public event Action	function;
	public event Action onPaused;
	public event Action onResumed;
	public event Action	onStop;
	public event Action onFinished;
	 
	private bool		_paused;
	private float 		_oldTime;
	private float		_counter;
    private int         _passedFrames;
	 
	public KTimerNode()
	{
		finished 		= false;
		usingRealTime 	= false;
		isLoopTimer		= false;
		_oldTime 		= Time.realtimeSinceStartup;
		_counter 		= 0;
        _passedFrames   = 0;
		onPaused 		+= delegate()
						{
							_oldTime = Time.realtimeSinceStartup;
						};
	}
	 
	/// <summary>
	/// Method called by the framework core to update this nodule.
	/// </summary>
	public void KTNUpdate()
	{
        if (!finished && !_paused)
        {
            if (_passedFrames >= framesToWait)
            {
                //Increment counter based on time scale
                if (usingRealTime)
                {
                    float __deltaTime = Time.realtimeSinceStartup - _oldTime;
                    _counter += __deltaTime;
                    _oldTime = Time.realtimeSinceStartup;
                }
                else
                    _counter += Time.deltaTime;

                //Check if its time to call the function
                if (_counter >= timer)
                {
                    if (function != null)
                        function();
                    else
                        Debug.LogWarning("No method was set to this timer. Nothing will happen.");

                    if (repeatTime > 1)
                    {
                        repeatTime--;
                        _counter = 0;
                    }
                    else
                    {
                        if (isLoopTimer)
                            _counter = 0;
                        else
						{
                            finished = true;
							
							if(onFinished != null)
								onFinished();
							
							CoreKTimer.kTimerCore.RemoveNodule(this);
						}
                    }
                }
			}	
	        else
	        {
	            _passedFrames++;
	        }
        }
	}
	 
	/// <summary>
	/// Pause this nodule.
	/// </summary>
	/// <remarks>
	/// Use this method if you want to resume this nodule functionality later.
	/// </remarks>
	public void Pause()
	{
		_paused = true;
		
		if(onPaused != null)
			onPaused();
	}
	
	/// <summary>
	/// Resume this nodule.
	/// </summary>
	public void Resume()
	{
		_paused = false;
		
		if(onResumed != null)
			onResumed();
	}
	
	/// <summary>
	/// Stop this instance.
	/// </summary>
	/// <remarks>
	/// Use this method to complete remove the nodule from framework core's list.<BR>
	/// You can not resume the nodule after calling Stop() method.
	/// </remarks>
	public void Stop()
	{
		finished = true;
		
		if(onStop != null)
			onStop();
		
		CoreKTimer.kTimerCore.RemoveNodule(this);
	}
	 
    public void Antecipate()
    {
        if (function != null)
            function();
        else
            return;

        if (isLoopTimer)
            _counter = 0;
        else
		{
			finished = true;
			
			if(onFinished != null)
				onFinished();
			
			CoreKTimer.kTimerCore.RemoveNodule(this);
		}
    }
    
}

