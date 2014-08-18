using UnityEngine;
using System.Collections;
using System;

public class KSoundNodule 
{
	public event Action onTerminated;
	
	private KTimerNode _timer;
	public KTimerNode timer
	{
		get { return _timer; }
		set { _timer = value; }
	}
	
	private bool _disablePitchCorrection = true;
	public bool disablePitchCorrection
	{
		get { return _disablePitchCorrection; }
		set { _disablePitchCorrection = value; }
	}
	
	private AudioSource _audioSource;
	public AudioSource audioSource
	{
		get { return _audioSource; }
	}
	
	private AudioClip _audioClip;
	public AudioClip audioClip
	{
		get { return _audioClip; }
	}
	
	private SoundType _type;
	public SoundType type	
	{
		get { return _type; }
	}
	
	private int _audioLayer;
	public int audioLayer
	{
		get { return _audioLayer; }
	}
	
	public float volume
	{
		get { return _audioSource.volume; }
	}
	
	public bool isLoop
	{
		get { return _audioSource.loop; }
	}
	
	public float pitch	
	{
		get { return _audioSource.pitch; }
		set { _audioSource.pitch = value; }
	}
	
	public bool isPlaying
	{
		get { return _audioSource.isPlaying; }
	}
	
	public GameObject gameObject
	{
		get { return _audioSource.gameObject; }
	}
	
	public KSoundNodule()
	{
	}
	public KSoundNodule(AudioSource source, AudioClip clip, SoundType type, bool loop, int layer)
	{
		_audioClip = clip;
		_audioSource = source;
		_type = type;
		_audioSource.loop = loop;
		_audioLayer = layer;
		
		if(!loop)
		{
			_timer = KTimer.WaitSeconds(clip.length, false, StopAction);
		}
	}
	
	private void StopAction()
	{
		if(onTerminated != null) onTerminated();
		
		CheckAndDestroy();
	}
	
	private void CheckAndDestroy()
	{
		if(_audioSource != null && _audioSource.isPlaying)
		{
			KTimer.WaitSeconds(0.3f, CheckAndDestroy);
		}
		else
		{
			AutoDestroy();
		}
	}
	
	private void AutoDestroy()
	{
		CoreKSound.instance.DeleteNodule(this);
	}
	
	/// <summary>
	/// Pause the sound.
	/// </summary>
	public void Pause()
	{
		if(_audioSource != null)
		{
			if(_timer != null)
				_timer.Pause();
			
			_audioSource.Pause();
		}
	}

	public void Resume()
	{
		Play();
	}
	
	public void Play()
	{
		if(_audioSource != null)
		{
			if(_timer != null)
				_timer.Resume();
			
			if(_audioSource != null)
				_audioSource.Play();
			else
				AutoDestroy();
		}
	}
	
	public void Stop()
	{
		if(_audioSource != null)
		{
			_audioSource.Stop();
			
			if(_timer != null)	
				_timer.Stop();

			AutoDestroy();
		}
	}
	
	public void SetVolume(float p_volumeToSet)
	{
		if(_audioSource != null)
			_audioSource.volume = p_volumeToSet;
	}
}
