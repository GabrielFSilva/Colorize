using UnityEngine;
using System.Collections;
using System;

public class KTweenNodule
{
	public bool 			isAlive
	{
		get { return !finished; }
	}
	public bool 			finished;
	public bool 			paused;
    public bool             onLoop;
	public float[] 			args;
	public event Action 	toDo;
	public event Action 	onFinished;
	public event Action		onStop;
	public event Action		onPause;
	public event Action		onResume;
	public bool 			stoped = false;
	
	public KTweenNodule()
	{
		finished = false;
		paused = false;
		args = new float[]{};
	}
	 
	/// <summary>
	/// Update the nodule, this function is called by the framework and should not be called by the user.
	/// </summary>
	public void ATNUpdate()
	{
		if(!finished)
			toDo();
	}
	 
	/// <summary>
	/// Pause the nodule.
	/// </summary>
	/// <remarks>
	/// You can use this method to pause the nodule functionality.<BR>
	/// It will not be deleted from the nodules list iniside the framework core.<BR>
	/// When you use this method your are able to resume this nodule at any time.<BR>
	/// <BR>
	/// <code>
	/// public Transform car;
	/// private KTweenNodule _carTweenNodule;
	/// 
	/// void Start()
	/// {
	/// 	//Move a little car forward
	/// 	_carTweenNodule = KTween.Vector3To(car.position, car.position + (car.forward * 5f), 60f, KTweenEase.LINEAR, delegate(Vector3 p_callBack)
	///		{
	///			car.transform.position = p_callBack;
	///		});
	/// }
	/// 
	/// private void RedTrafficLight()
	/// {
	/// 	_carTweenNodule.Pause();
	/// }
	/// 
	/// private void GreenTrafficLight()
	/// {
	/// 	_carTweenNodule.Resume();
	/// }
	/// 
	/// /* 
	/// This way you can pause the little car every time it gets in a red traffic light.
	/// And resume it later when the traffic light becomes green.
	/// */
	/// </code>
	/// </remarks>
	public void Pause()
	{
		if(onPause != null)
			onPause();
		
		paused = true;
	}
	 
	/// <summary>
	/// Resume this nodule functionality.
	/// </summary>
	/// <remarks>
	/// Use this method to resume the nodule if it was paused before.<BR>
	/// </remarks>
	public void Resume()
	{
		if(onResume != null)
			onResume();
		
		paused = false;
	}
	 
	/// <summary>
	/// Stop this nodule.
	/// </summary>
	/// <remarks>
	/// This method is used to remove the current nodule from the framework's nodule list.<BR>
	/// Please note that Stop() method removes the nodule from framework's core list. So it can not be resumed again.<BR>
	/// 
	/// <code>
	/// private KTweenNodule _tween;
	/// 
	/// void Start()
	/// {
	/// 	_tween = KTween.Vector3To(transform.position, transform.position + (transform.forward * 2f), 60f, KTweenEase.LINEAR, delegate(Vector3 p_callBack)
	/// 	{
	/// 		transform.position = p_callBack;
	/// 	});
	/// }
	/// 
	/// //When this object is destroyed we do not need the tween anymore. So we can stop it.
	/// void OnDestroy()
	/// {
	/// 	if(_tween.isAlive)
	/// 		_tween.Stop();
	/// }
	/// </code>
	/// 
	/// Again, note that once you stop a nodule it cannot be resumed later.<BR> 
	/// If you wanna to resume it later you should use the Pause() method instead of Stop().
	/// </remarks>
	public void Stop()
	{
		if(onStop != null)
			onStop();
		
		stoped = true;
		finished = true;
        onLoop = false;
	}
	 
	/// <summary>
	/// Dispatch the specified event type to the nodule.
	/// </summary>
	/// <param name='p_type'>
	/// The event type you wanna to dispatch.
	/// </param>
	public void Dispatch(KTweenEventType p_type)
	{
		switch(p_type)
		{
		case KTweenEventType.FINISHED:
			if(onFinished != null)
				onFinished();
			break;
		case KTweenEventType.STOP:
			if(onStop != null)
				onStop();
			break;
		case KTweenEventType.PAUSE:
			if(onPause != null)
				onPause();
			break;
		case KTweenEventType.RESUME:
			if(onResume != null)
				onResume();
			break;
		}
	}
	 
}
