using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoreKTween : MonoBehaviour
{
	private static CoreKTween 			_kTweenInstance;
    public static CoreKTween 			kTweenInstance
    {
        get 
        {
            if(_kTweenInstance == null ) 
            {
                _kTweenInstance = FrameworkCore.instance.AddComponent<CoreKTween>() ;
            }
            return _kTweenInstance; 
        }
    }
	    
	private List<KTweenNodule>		_nodules = new List<KTweenNodule>();

	void Start()
	{
		_kTweenInstance = this;
	}
	 
	void Update()
	{
		for(int i = 0; i < _nodules.Count; i++)
		{
			if(_nodules[i].finished)
			{
				if(!_nodules[i].stoped)
					_nodules[i].Dispatch(KTweenEventType.FINISHED);
				
				_nodules.Remove(_nodules[i]);
			}
			else
			{
				_nodules[i].ATNUpdate();
			}
		}
	}
	 
	public void AddNodule(KTweenNodule nodule)
	{
		if(_nodules == null)
			_nodules = new List<KTweenNodule>();
		_nodules.Add(nodule);
	}
}
