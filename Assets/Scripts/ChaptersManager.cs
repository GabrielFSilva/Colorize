using UnityEngine;
using System.Collections;

public class ChaptersManager 
{
	#region Singleton
	private static ChaptersManager _instance;
	public static ChaptersManager GetInstance()
	{
		if (_instance == null)
		{
			_instance = new ChaptersManager();
			_instance.StartUp();
		}
		return _instance;
	}
	#endregion

	public ChaptersList chaptersList;


	private void StartUp()
	{
		if (chaptersList == null)
		{
			chaptersList = ((GameObject)GameObject.Instantiate(Resources.Load<GameObject> ("Code Structures/ChaptersList"))).GetComponent<ChaptersList>();
			chaptersList.name = "ChaptersList";
			GameObject.DontDestroyOnLoad(chaptersList.gameObject);
		}
		if (!PlayerPrefs.HasKey("CHAPTER_LIST_ON_PREFS"))
			SaveOnPlayerPrefs();
		else
			LoadFromPlayerPrefs();

	}

	public void UnlockChapter(int p_chapterIndex, bool p_isUnlocked = true)
	{
		chaptersList.chapters [p_chapterIndex].isUnlocked = p_isUnlocked;
		PlayerPrefs.SetInt("CHAPTER_" + p_chapterIndex.ToString() + "_IS_UNLOCKED", chaptersList.chapters[p_chapterIndex].isUnlocked ? 1:0);
		PlayerPrefs.Save ();
	}

	public void UnlockStage(int p_chapterIndex, int p_stageIndex, bool p_isUnlocked = true)
	{
		chaptersList.chapters [p_chapterIndex].stages [p_stageIndex].isUnlocked = p_isUnlocked;
		PlayerPrefs.SetInt("CHAPTER_" + p_chapterIndex.ToString() + "_STAGE_" + p_stageIndex + "_IS_UNLOCKED", chaptersList.chapters[p_chapterIndex].stages[p_stageIndex].isUnlocked ? 1:0);
		PlayerPrefs.Save ();
	}

	public void CompleteStage(int p_chapterIndex, int p_stageIndex, bool p_isUnlocked = true)
	{
		chaptersList.chapters [p_chapterIndex].stages [p_stageIndex].isCompleted = p_isUnlocked;
		PlayerPrefs.SetInt("CHAPTER_" + p_chapterIndex.ToString() + "_STAGE_" + p_stageIndex + "_IS_COMPLETED", chaptersList.chapters[p_chapterIndex].stages[p_stageIndex].isCompleted ? 1:0);
		PlayerPrefs.Save ();
		CheckCompleteChapter (p_chapterIndex);
	}

	private void CheckCompleteChapter(int p_chapterIndex)
	{
		bool __isCompleted = true;

		foreach (StageDescriptor stageDescriptor in chaptersList.chapters[p_chapterIndex].stages)
		{
			if (!stageDescriptor.isCompleted)
				__isCompleted = false;
		}
		if (__isCompleted)
		{
			chaptersList.chapters[p_chapterIndex].isCompleted = true;
			PlayerPrefs.SetInt("CHAPTER_" + p_chapterIndex.ToString() + "_IS_COMPLETED", chaptersList.chapters[p_chapterIndex].isCompleted ? 1:0);
		}
	}
	
	private void SaveOnPlayerPrefs()
	{
		Debug.Log("Salvando!");
		for(int __chapterIndex = 0; __chapterIndex < chaptersList.chapters.Count; __chapterIndex ++)
		{
			PlayerPrefs.SetInt("CHAPTER_" + __chapterIndex.ToString() + "_IS_UNLOCKED", chaptersList.chapters[__chapterIndex].isUnlocked ? 1:0);
			PlayerPrefs.SetInt("CHAPTER_" + __chapterIndex.ToString() + "_IS_COMPLETED", chaptersList.chapters[__chapterIndex].isCompleted ? 1:0);
			
			for(int __stageIndex = 0; __stageIndex < chaptersList.chapters[__chapterIndex].stages.Count; __stageIndex ++)
			{
				PlayerPrefs.SetInt("CHAPTER_" + __chapterIndex.ToString() + "_STAGE_" + __stageIndex + "_IS_UNLOCKED", chaptersList.chapters[__chapterIndex].stages[__stageIndex].isUnlocked ? 1:0);
				PlayerPrefs.SetInt("CHAPTER_" + __chapterIndex.ToString() + "_STAGE_" + __stageIndex + "_IS_COMPLETED", chaptersList.chapters[__chapterIndex].stages[__stageIndex].isCompleted ? 1:0);
			}
		}
		PlayerPrefs.SetInt("CHAPTER_LIST_ON_PREFS",1);
		PlayerPrefs.Save ();
	}
	private void LoadFromPlayerPrefs()
	{
		Debug.Log("Carregando!");
		for(int __chapterIndex = 0; __chapterIndex < chaptersList.chapters.Count; __chapterIndex ++)
		{
			chaptersList.chapters[__chapterIndex].isUnlocked = PlayerPrefs.GetInt("CHAPTER_" + __chapterIndex.ToString() + "_IS_UNLOCKED") > 0 ? true : false;
			chaptersList.chapters[__chapterIndex].isCompleted =PlayerPrefs.GetInt("CHAPTER_" + __chapterIndex.ToString() + "_IS_COMPLETED") > 0 ? true : false;
			
			for(int __stageIndex = 0; __stageIndex < chaptersList.chapters[__chapterIndex].stages.Count; __stageIndex ++)
			{
				chaptersList.chapters[__chapterIndex].stages[__stageIndex].isUnlocked = PlayerPrefs.GetInt("CHAPTER_" + __chapterIndex.ToString() + "_STAGE_" + __stageIndex + "_IS_UNLOCKED") > 0 ? true : false;
				chaptersList.chapters[__chapterIndex].stages[__stageIndex].isCompleted = PlayerPrefs.GetInt("CHAPTER_" + __chapterIndex.ToString() + "_STAGE_" + __stageIndex + "_IS_COMPLETED") > 0 ? true : false;
			}
		}
		
	}
	
}
