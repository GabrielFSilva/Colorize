using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoreKSound : MonoBehaviour 
{
	private static CoreKSound _instance;
	public static CoreKSound instance	
	{
		get 
		{
			if(_instance == null)
			{
				_instance = FrameworkCore.instance.AddComponent<CoreKSound>();
			}
			return _instance;
		}
	}
	
	private List<KSoundNodule> _nodules;
	private bool _pitchCorrection = false;
	private static float[] _soundLayers;
	
	public void SetPitchCorrection(bool enable)
	{
		_pitchCorrection = enable;
	}
	
	private void InitializeLayers()
	{
		_soundLayers = new float[10];
		for(int i = 0; i < _soundLayers.Length; i ++)
		{
			_soundLayers[i] = 1;
		}
	}
	
	void Awake()
	{
		if(_soundLayers == null)
		{
			InitializeLayers();
		}
		
		if(_instance != null)
		{
			if(_instance != this)
			{
				Destroy(this);
			}
		}
		else
		{
			_instance = this;
		}
		
		_nodules = new List<KSoundNodule>();
	}
	
	void Update()
	{
		if(_pitchCorrection)
		{
			foreach(var nodule in _nodules)
			{
				if(!nodule.disablePitchCorrection)
					nodule.audioSource.pitch = Time.timeScale;
			}
		}
	}
	
	public float GetVolume(int layer)
	{
		return _soundLayers[layer];
	}

	public KSoundNodule GetBackgroundMusicNodule()
	{
		foreach(var nodule in _nodules)
		{
			if(nodule.type == SoundType.BACKGROUND_MUSIC)
			{
				return nodule;
			}
		}
		
		return null;
	}
	
	private void ClearAudioSources()
	{
		List<KSoundNodule> tempListForNodules = _nodules;
		for(int i = 0; i < tempListForNodules.Count; i ++)
		{
			tempListForNodules[i].Stop();
		}
	}
	
	public void PrepareCoreToBeDestroyed()
	{
		ClearAudioSources();
	}
	
	public void DeleteNodule(KSoundNodule nodule)
	{
		_nodules.Remove(nodule);
		Destroy(nodule.audioSource);
	}
	
	public void AddNodule(KSoundNodule nodule)
	{
		_nodules.Add(nodule);
	}
	
	public void SetVolume(float volume, int layer)
	{
		_soundLayers[layer] = volume;
		foreach(var nodule in _nodules)
		{
			if(nodule.audioLayer == layer)
			{
				nodule.SetVolume(volume);
			}
		}
	}
	
	public void StopAllSounds(int layer)
	{
		foreach(var nodule in _nodules)
		{
			if(nodule.audioLayer == layer)
			{
				nodule.Stop();
			}
		}
	}
	
	public void StopAllSounds(SoundType type)
	{
		foreach(var nodule in _nodules)
		{
			if(nodule.type == type)
			{
				nodule.Stop();
			}
		}
	}

	public void StopAllSounds()
	{
		ClearAudioSources();
	}
	
	public void StopAllNonBackgroundSounds()
	{
		List<KSoundNodule> toStop = new List<KSoundNodule>();
		foreach(var nodule in _nodules)
		{
			if(nodule.type == SoundType.EFFECT)
			{
				KSoundNodule cache = nodule;
				toStop.Add(cache);
			}
		}
		
		while(toStop.Count > 0)
		{
			KSoundNodule cache = toStop[0];
			toStop.Remove(cache);
			cache.Stop();
		}
	}
}
