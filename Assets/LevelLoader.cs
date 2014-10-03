using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class LevelLoader : MonoBehaviour 
{
	public LevelInfo levelInfo;

	public TextAsset levelXML;

	public Transform platformsContainerTransform;


	//Prefabs
	public GameObject platformPrefab;
	public GameObject playerPrefab;


	//References
	public ShootsUIManager shootsUIManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void LoadLevel()
	{
		levelInfo.ClearInfo ();
		//string filepath = Application.dataPath + "/Resources/LevelsXML/Stage" + InGameSceneManager.selectedChapter.ToString() + "-" + InGameSceneManager.selectedStage.ToString() + ".xml";
		levelXML = Resources.Load<TextAsset>("LevelsXML/Stage" + InGameSceneManager.selectedChapter.ToString() + "-" + InGameSceneManager.selectedStage.ToString());

		if (levelXML == null)
		{
			Debug.Log("The File Don't Exist");
			return;
		}
			
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml (levelXML.text);

		XmlNodeList platformsRootNode = xmlDoc.SelectNodes("//Platforms/Platform");
		foreach(XmlNode platformNode in platformsRootNode)
		{
			Platform __tempPlat = ((GameObject) GameObject.Instantiate(platformPrefab)).GetComponent<Platform>();
			__tempPlat.transform.parent = platformsContainerTransform;
			__tempPlat.transform.position = new Vector3(float.Parse(platformNode.Attributes["x"].Value),float.Parse(platformNode.Attributes["y"].Value),float.Parse(platformNode.Attributes["z"].Value));
			__tempPlat.platformType = (GlobalInfo.PlaformType)int.Parse(platformNode.Attributes["type"].Value);
			__tempPlat.platformStartType = __tempPlat.platformType;
			__tempPlat.name = "Platform";

			levelInfo.platformsList.Add(__tempPlat);
		}
		XmlNode playerNode = xmlDoc.SelectSingleNode ("//Player");
		levelInfo.playerSpawnPosition = new Vector3 (float.Parse (playerNode.Attributes ["x"].Value), float.Parse (playerNode.Attributes ["y"].Value), float.Parse (playerNode.Attributes ["z"].Value));

		levelInfo.player = ((GameObject)Instantiate (playerPrefab)).GetComponent<Player> ();
		levelInfo.player.name = "Player";
		levelInfo.player.shootsUIManager = shootsUIManager;

	}


}






