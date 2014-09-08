using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class StageDescriptor : MonoBehaviour 
{
	public string stageName;
	public int stageIndex;
	public bool isCompleted;
	public bool isUnlocked;

	public List<GlobalInfo.ShootTypes> 		shootTypesList;
	public List<int> 						shootAmmoList;
	public List<bool>						shootInfiniteAmmoList;
}
