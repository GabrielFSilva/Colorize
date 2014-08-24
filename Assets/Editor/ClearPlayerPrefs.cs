using UnityEngine;
using UnityEditor;
using System.Collections;

public class ClearPlayerPrefs : MonoBehaviour 
{
	[MenuItem("Colorize/ClearPlayerPrefs")] 
	static void DeleteMyPlayerPrefs() 
	{ 
		PlayerPrefs.DeleteAll();
	} 
}
