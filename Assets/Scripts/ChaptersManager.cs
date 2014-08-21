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
	}
}
