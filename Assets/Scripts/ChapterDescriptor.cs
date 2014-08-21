using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChapterDescriptor : MonoBehaviour 
{
	public string 					chapterName;
	public bool 					isCompleted;
	public bool 					isUnlocked;

	public List<StageDescriptor> 	stages;

}
