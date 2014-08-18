using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
 
public enum KTweenEventType
{
	FINISHED,
	STOP,
	PAUSE,
	RESUME
}

public enum KTweenEase
{
	LINEAR,
	SPRING,
	QUAD_IN,
	QUAD_OUT,
	QUAD_IN_OUT,
	CUBIC_IN,
	CUBIC_OUT,
	CUBIC_IN_OUT,
	BOUNCE_IN,
	BOUNCE_OUT,
	ELASTIC_IN,
	ELASTIC_OUT,
	ELASTIC_IN_OUT,
	QUART_IN,
	QUART_OUT,
	QUART_IN_OUT,
	QUINT_IN,
	QUINT_OUT,
	QUINT_IN_OUT,
	SINE_IN,
	SINE_OUT,
	SINE_IN_OUT,
	EXPO_IN,
	EXPO_OUT,
	EXPO_IN_OUT,
	CIRC_IN,
	CIRC_OUT,
	CIRC_IN_OUT,
	BACK_IN,
	BACK_OUT,
	BACK_IN_OUT
}
 
public class KTween
{
	#region FloatTo

	public static KTweenNodule FloatTo(float p_startValue, float p_finalValue, float p_duration, KTweenEase p_easeType, Action<float> p_callbackUpdate)
	{
		return FloatTo(p_startValue, p_finalValue, p_duration, p_easeType, 0, p_callbackUpdate);
	}
	public static KTweenNodule FloatTo(float p_startValue, float p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay,  Action<float> p_callbackUpdate)
	{
		return FloatTo(p_startValue, p_finalValue, p_duration, p_easeType, p_delay, true, p_callbackUpdate);
	}
	public static KTweenNodule FloatTo(float p_startValue, float p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, bool p_useUnityTime,  Action<float> p_callbackUpdate)
	{
		return FloatTo(p_startValue, p_finalValue, p_duration, p_easeType, p_delay, p_useUnityTime, false, p_callbackUpdate);
	}
    private static KTweenNodule FloatTo(float p_startValue, float p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, bool p_useUnityTime, bool p_isLoop, Action<float> p_callbackUpdate)
	{
		KTweenNodule __nodule = new KTweenNodule();
		
		float __startValue = p_startValue;
		float __counter = 0f;
		float __timeNow = Time.time;
		float __ableToStartIn = __timeNow + p_delay;

		__nodule.args = new float[] { __counter };
        __nodule.onLoop = p_isLoop;
		
		if(p_useUnityTime)
			__nodule.onResume 	+= delegate() 
								{
									if(__nodule.args.Length > 1)
										__nodule.args[1] = Time.realtimeSinceStartup;	
								};
		
		__nodule.toDo 	+= delegate()
						{
							if(__ableToStartIn <= Time.time)
							{
                                if (!__nodule.paused)
                                {
                                    float ___counter = __nodule.args[0];

                                    float __currentValue;

                                    if (___counter < p_duration)
                                    {
                                        if (!p_useUnityTime)
                                            ___counter += KTimer.realDeltaTime;
                                        else
                                            ___counter += Time.deltaTime;
                                        float __normalizedTime = Mathf.Min(___counter / p_duration, 1f);

                                        __currentValue = EaseMathsKTween.GetTransaction(__startValue, p_finalValue, __normalizedTime, p_easeType);
						
                                        p_callbackUpdate(__currentValue);

                                        __nodule.args[0] = ___counter;
                                    }
                                    else
                                    {
                                        if (__nodule.onLoop)
                                        {
                                            __nodule.args[0] = 0;
                                            __ableToStartIn = Time.realtimeSinceStartup + p_delay;
                                        }
                                        else
                                            __nodule.finished = true;
                                    }
                                }
							}
						};

		CoreKTween.kTweenInstance.AddNodule(__nodule);
		
		return __nodule;
	}
	 
	#endregion
	
	#region Vector3To
	
	/// <remarks>
	/// It works just like the KTween.FloatTo function but with Vector3 instead of Float.
	/// </remarks>
	public static KTweenNodule Vector3To(Vector3 p_startValue, Vector3 p_finalValue, float p_duration, KTweenEase p_easeType, Action<Vector3> p_callbackUpdate)
	{
		return Vector3To(p_startValue, p_finalValue, p_duration, p_easeType, 0, p_callbackUpdate);
	}
	public static KTweenNodule Vector3To(Vector3 p_startValue, Vector3 p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, Action<Vector3> p_callbackUpdate)
	{
		return Vector3To(p_startValue, p_finalValue, p_duration, p_easeType, p_delay, true, p_callbackUpdate);
	}
	public static KTweenNodule Vector3To(Vector3 p_startValue, Vector3 p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, bool p_useUnityTime, Action<Vector3> p_callbackUpdate)
    {
		KTweenNodule __temp = FloatTo(0, 1, p_duration, p_easeType, p_delay, p_useUnityTime, delegate(float newFloat)
		{
			p_callbackUpdate(Vector3.Lerp(p_startValue, p_finalValue, newFloat));
		});
		
		return __temp;
	}
	
	#endregion
	
	#region QuaternionTo
	
	/// <remarks>
	/// It works just like the KTween.FloatTo function but with Quaternion instead of Float.
	/// </remarks>
	public static KTweenNodule QuaternionTo(Quaternion p_startValue, Quaternion p_finalValue, float p_duration, KTweenEase p_easeType, Action<Quaternion> p_callbackUpdate)
	{
		return QuaternionTo(p_startValue, p_finalValue, p_duration, p_easeType, 0, p_callbackUpdate);
	}
	public static KTweenNodule QuaternionTo(Quaternion p_startValue, Quaternion p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, Action<Quaternion> p_callbackUpdate)
	{
		return QuaternionTo(p_startValue, p_finalValue, p_duration, p_easeType, p_delay, true, p_callbackUpdate);
	}
	public static KTweenNodule QuaternionTo(Quaternion p_startValue, Quaternion p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, bool p_useUnityTime, Action<Quaternion> p_callbackUpdate)
	{
		KTweenNodule nodule = KTween.FloatTo(0, 1, p_duration, p_easeType, p_delay, p_useUnityTime, delegate(float newFloat)
		{
			p_callbackUpdate(Quaternion.Lerp(p_startValue, p_finalValue, newFloat));
		});
		
		return nodule;
	}
	
	#endregion
	
	#region ColorTo
	
	public static KTweenNodule ColorTo(Color p_startColor, Color p_finalColor, float p_duration, KTweenEase p_easeType, Action<Color> p_callbackUpdate)
	{
		return ColorTo(p_startColor, p_finalColor, p_duration, p_easeType, 0, p_callbackUpdate);
	}
	public static KTweenNodule ColorTo(Color p_startColor, Color p_finalColor, float p_duration, KTweenEase p_easeType, float p_delay,  Action<Color> p_callbackUpdate)
	{
		return ColorTo(p_startColor, p_finalColor, p_duration, p_easeType, p_delay, true, p_callbackUpdate);
	}
	public static KTweenNodule ColorTo(Color p_startColor, Color p_finalColor, float p_duration, KTweenEase p_easeType, float p_delay, bool p_useUnityTime, Action<Color> p_callbackUpdate)
	{
		KTweenNodule __temp = KTween.FloatTo(0, 1, p_duration, p_easeType, p_delay, p_useUnityTime, delegate(float newFloat)
		{
			p_callbackUpdate(Color.Lerp(p_startColor, p_finalColor, newFloat));
		});
		
		return __temp;
	}
	 
	#endregion
	
	#region RectoTo
	
	public static KTweenNodule RectTo(Rect p_startValue, Rect p_finalValue, float p_duration, KTweenEase p_easeType, Action<Rect> p_callbackUpdate)
	{
		return RectTo(p_startValue, p_finalValue, p_duration, p_easeType, 0, p_callbackUpdate);
	}
	public static KTweenNodule RectTo(Rect p_startValue, Rect p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, Action<Rect> p_callbackUpdate)
	{
		return RectTo(p_startValue, p_finalValue, p_duration, p_easeType, p_delay, true, p_callbackUpdate);
	}
	public static KTweenNodule RectTo(Rect p_startValue, Rect p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, bool p_useUnityTime, Action<Rect> p_callbackUpdate)
	{
		KTweenNodule __temp = KTween.FloatTo(0, 1, p_duration, p_easeType, p_delay, p_useUnityTime, delegate(float newFloat)
		{
			p_callbackUpdate(new Rect(
				Mathf.Lerp(p_startValue.x, p_finalValue.x, newFloat),
				Mathf.Lerp(p_startValue.y, p_finalValue.y, newFloat),
				Mathf.Lerp(p_startValue.width, p_finalValue.width, newFloat),
				Mathf.Lerp(p_startValue.height, p_finalValue.height, newFloat)
				));
		});
		
		return __temp;
	}
	
	#endregion
	
	#region Vector2
	
	public static KTweenNodule Vector2To(Vector2 p_startValue, Vector2 p_finalValue, float p_duration, KTweenEase p_easeType, Action<Vector2> p_callbackUpdate)
	{
		return Vector2To(p_startValue, p_finalValue, p_duration, p_easeType, 0, p_callbackUpdate);
	}
	public static KTweenNodule Vector2To(Vector2 p_startValue, Vector2 p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, Action<Vector2> p_callbackUpdate)
	{
		return Vector2To(p_startValue, p_finalValue, p_duration, p_easeType, p_delay, true, p_callbackUpdate);
	}
	public static KTweenNodule Vector2To(Vector2 p_startValue, Vector2 p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, bool p_useUnityTime, Action<Vector2> p_callbackUpdate)
	{
		KTweenNodule __temp = KTween.FloatTo(0,1, p_duration, p_easeType, p_delay, p_useUnityTime, delegate(float newFloat)
		{
			p_callbackUpdate(Vector2.Lerp(p_startValue, p_finalValue, newFloat));
		});
		
		return __temp;
	}
	
	#endregion
	
	#region Vector4
	
	public static KTweenNodule Vector4To(Vector4 p_startValue, Vector4 p_finalValue, float p_duration, KTweenEase p_easeType, Action<Vector4> p_callbackUpdate)
	{
		return Vector4To(p_startValue, p_finalValue, p_duration, p_easeType, p_callbackUpdate);
	}
	public static KTweenNodule Vector4To(Vector4 p_startValue, Vector4 p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, Action<Vector4> p_callbackUpdate)
	{
		return Vector4To(p_startValue,p_finalValue, p_duration, p_easeType, p_delay, true, p_callbackUpdate);
	}
	public static KTweenNodule Vector4To(Vector4 p_startValue, Vector4 p_finalValue, float p_duration, KTweenEase p_easeType, float p_delay, bool p_useUnityTime, Action<Vector4> p_callbackUpdate)
	{
		KTweenNodule __temp = KTween.FloatTo(0,1, p_duration, p_easeType, p_delay, p_useUnityTime, delegate(float newFloat)
		{
			p_callbackUpdate(Vector4.Lerp(p_startValue, p_finalValue, newFloat));	
		});
		
		return __temp;
	}
	
	#endregion
}
