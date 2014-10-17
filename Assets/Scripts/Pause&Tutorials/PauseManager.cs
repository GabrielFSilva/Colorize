using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour 
{
	public LevelInfo levelInfo;

	public bool isPaused = false;

	public void PauseGame(bool p_willPause)
	{
		LevelInfo.player.PausePlayer (p_willPause);
		LevelInfo.energySphere.PauseEnergySphere (p_willPause);
		isPaused = p_willPause;
	}
}
