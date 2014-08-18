using UnityEngine;
using System.Collections;

public enum SoundType
{
	EFFECT,
	BACKGROUND_MUSIC
}

public enum DefaultSoundLayers
{
	BACKGROUND_MUSIC,
	SFX,
	HUD_EFFECTS
}

public class KSound 
{
	private static KSoundNodule PlaySound(AudioClip p_clip, GameObject p_whereToPlay, bool p_isLoop, int p_layer, SoundType p_type)
	{
		AudioSource source = p_whereToPlay.AddComponent<AudioSource>();
		source.clip = p_clip;
		source.loop = p_isLoop;
		source.playOnAwake = false;
		source.volume = CoreKSound.instance.GetVolume(p_layer);
		source.Play();
		
		KSoundNodule nodule = new KSoundNodule(source, p_clip, p_type, p_isLoop, p_layer);
		CoreKSound.instance.AddNodule(nodule);
		
		return nodule;
	}
	
	public static KSoundNodule Play3D(AudioClip p_clip, GameObject p_gameObject)
	{
		return Play3D(p_clip, p_gameObject, 0);
	}
	
	public static KSoundNodule Play3D(AudioClip p_clip, GameObject p_gameObject, int p_layer)
	{
		return Play3D(p_clip, p_gameObject, false, p_layer);
	}

	public static KSoundNodule Play3D(AudioClip p_clip, GameObject p_gameObject, bool p_isLoop, int p_layer)
	{
		return PlaySound(p_clip, p_gameObject, p_isLoop, p_layer, SoundType.EFFECT);
	}

	public static KSoundNodule Play2D(AudioClip p_clip)
	{
		return Play2D(p_clip, 0);
	}
	
	public static KSoundNodule Play2D(AudioClip p_clip, int p_layer)	
	{
		return Play2D(p_clip, false, p_layer);	
	}
	
	public static KSoundNodule Play2D(AudioClip p_clip, bool p_isLoop, int p_layer)
	{
		return PlaySound(p_clip, CoreKSound.instance.gameObject, p_isLoop, p_layer, SoundType.EFFECT);
	}

	public static void SetVolume(float p_volume, int p_layer)
	{
		CoreKSound.instance.SetVolume(p_volume, p_layer);
	}
	
	public static float GetVolume(int p_layer)
	{
		return CoreKSound.instance.GetVolume(p_layer);
	}

	public static void StopAllSounds()
	{
		CoreKSound.instance.StopAllSounds();
	}
	
	public static void StopAllSounds(int p_layer)
	{
		CoreKSound.instance.StopAllSounds(p_layer);
	}
	
	public static void StopAllSounds(SoundType p_type)
	{
		CoreKSound.instance.StopAllSounds(p_type);
	}
}
