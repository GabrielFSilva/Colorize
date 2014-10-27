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

	public Transform baseSceneryTransform;
	public Transform platformsContainerTransform;
	public Transform tutorialTriggersContainerTransform;

	//Prefabs
	public GameObject platformPrefab;
	public GameObject playerPrefab;
	public GameObject energySpherePrefab;
	public GameObject tutorialTriggerPrefab;
	
	//References
	public InGameSceneManager	gameSceneManager;
	public ShootsUIManager 		shootsUIManager;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void LoadLevel()
	{
		levelInfo.ClearInfo ();
		levelXML = Resources.Load<TextAsset>("LevelsXML/Stage" + InGameSceneManager.selectedChapter.ToString() + "-" + InGameSceneManager.selectedStage.ToString());

		if (levelXML == null)
		{
			Debug.Log("The File Don't Exist");
			return;
		}
			
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml (levelXML.text);

		////////////////////////////////////////////////////////////////////
		// Load Ammo
		XmlNodeList shootsRootNode = xmlDoc.SelectNodes("//ShootsInfo/ShootInfo");
		foreach(XmlNode shootNode in shootsRootNode)
		{
			if (bool.Parse(shootNode.Attributes["isUsed"].Value))
			{
				shootsUIManager.shootTypesList.Add((GlobalInfo.ShootTypes)int.Parse(shootNode.Attributes["type"].Value));
				shootsUIManager.shootInfiniteAmmoList.Add(bool.Parse(shootNode.Attributes["isInfinite"].Value));
				shootsUIManager.shootAmmoList.Add(int.Parse(shootNode.Attributes["ammo"].Value));
				shootsUIManager.shootTutorialFocus.Add(int.Parse(shootNode.Attributes["tutFocus"].Value));
			}
		}
		////////////////////////////////////////////////////////////////////
		// Load Platforms
		XmlNodeList platformsRootNode = xmlDoc.SelectNodes("//Platforms/Platform");
		foreach(XmlNode platformNode in platformsRootNode)
		{
			Platform __tempPlat = ((GameObject) GameObject.Instantiate(platformPrefab)).GetComponent<Platform>();
			__tempPlat.transform.parent = platformsContainerTransform;
			__tempPlat.transform.position = new Vector3(float.Parse(platformNode.Attributes["x"].Value),float.Parse(platformNode.Attributes["y"].Value),float.Parse(platformNode.Attributes["z"].Value));
			__tempPlat.tutorialFocusIndex = int.Parse(platformNode.Attributes["tutFocus"].Value);
			__tempPlat.platformType = (GlobalInfo.PlaformType)int.Parse(platformNode.Attributes["type"].Value);
			__tempPlat.platformStartType = __tempPlat.platformType;

			__tempPlat.name = "Platform";

			LevelInfo.platformsList.Add(__tempPlat);
		}
		////////////////////////////////////////////////////////////////////
		// Load Tutorial Triggers
		XmlNodeList tutorialTriggersRootNode = xmlDoc.SelectNodes ("//TutorialTriggers/TutorialTrigger");
		foreach(XmlNode tutorialTriggerNode in tutorialTriggersRootNode)
		{
			TutorialTrigger __tempTrigger = ((GameObject) GameObject.Instantiate(tutorialTriggerPrefab)).GetComponent<TutorialTrigger>();
			__tempTrigger.transform.parent = tutorialTriggersContainerTransform;
			__tempTrigger.transform.position = new Vector3(float.Parse(tutorialTriggerNode.Attributes["x"].Value),float.Parse(tutorialTriggerNode.Attributes["y"].Value),float.Parse(tutorialTriggerNode.Attributes["z"].Value));
			__tempTrigger.tutorialIndex = int.Parse(tutorialTriggerNode.Attributes["index"].Value);
			__tempTrigger.GetComponent<BoxCollider2D>().size = new Vector2 (__tempTrigger.GetComponent<BoxCollider2D>().size.x,
			                                                                int.Parse(tutorialTriggerNode.Attributes["colliderY"].Value));
			__tempTrigger.cameraPosition = new Vector3(float.Parse(tutorialTriggerNode.Attributes["xCam"].Value),float.Parse(tutorialTriggerNode.Attributes["yCam"].Value),float.Parse(tutorialTriggerNode.Attributes["zCam"].Value));
			__tempTrigger.gameSceneManager = gameSceneManager;
			__tempTrigger.name = "TutorialTrigger" + __tempTrigger.tutorialIndex.ToString();
			
			LevelInfo.tutorialTriggersList.Add(__tempTrigger);
		}

		////////////////////////////////////////////////////////////////////
		// Load Energy Sphere
		XmlNode energySphereNode = xmlDoc.SelectSingleNode ("//EnergySphere");

		LevelInfo.energySphere = ((GameObject)Instantiate (energySpherePrefab)).GetComponent<EnergySphere> ();
		LevelInfo.energySphere.transform.parent = baseSceneryTransform;
		LevelInfo.energySphere.transform.position = new Vector3 (float.Parse (energySphereNode.Attributes ["x"].Value), float.Parse (energySphereNode.Attributes ["y"].Value), float.Parse (energySphereNode.Attributes ["z"].Value));
		LevelInfo.energySphere.name = "EnergySphere";
		LevelInfo.energySphere.tutorialFocusIndex = int.Parse(energySphereNode.Attributes["tutFocus"].Value);
		LevelInfo.energySphere.gameSceneManager = gameSceneManager;
		////////////////////////////////////////////////////////////////////
		// Load Player
		XmlNode playerNode = xmlDoc.SelectSingleNode ("//Player");

		LevelInfo.playerSpawnPosition = new Vector3 (float.Parse (playerNode.Attributes ["x"].Value), float.Parse (playerNode.Attributes ["y"].Value), float.Parse (playerNode.Attributes ["z"].Value));

		LevelInfo.player = ((GameObject)Instantiate (playerPrefab)).GetComponent<Player> ();
		LevelInfo.player.transform.parent = baseSceneryTransform;
		LevelInfo.player.transform.position = LevelInfo.playerSpawnPosition;
		LevelInfo.player.name = "Player";
		LevelInfo.player.shootsUIManager = shootsUIManager;

	}


}






