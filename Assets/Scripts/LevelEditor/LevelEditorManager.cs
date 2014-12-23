using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class LevelEditorManager : MonoBehaviour {
	
	public int 			chapterIndex;
	public int			stageIndex;
	
	public GameObject		platPrefab;
	public StageDescriptor	stageDescriptor;
	
	void Start()
	{
		//LoadLevel ();
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
			SaveLevelToXML();
		if (Input.GetKeyDown(KeyCode.L))
			LoadLevelFromXML();
	}
	void OnGUI()
	{	
		string __saveButtonText = "Press S to Save Level " + chapterIndex.ToString() + "-" + stageIndex.ToString();
		string __loadButtonText = "Press L to Load Level: " + chapterIndex.ToString() + "-" + stageIndex.ToString();
		
		GUI.Label(new Rect(Screen.width * 0.8f, 0f, Screen.width * 0.2f, Screen.height * 0.1f), __saveButtonText);
		GUI.Label(new Rect(Screen.width * 0.6f, 0f, Screen.width * 0.2f, Screen.height * 0.1f), __loadButtonText);
		
	}
	
	public void SaveLevelToXML()
	{
		List<Platform> __platformsList = new List<Platform>(GameObject.FindObjectsOfType<Platform>());
		Transform __playerTransfom = GameObject.Find("Player").transform;
		List<TutorialTrigger>  __tutorialTriggersList = new List<TutorialTrigger>(GameObject.FindObjectsOfType<TutorialTrigger>());

		EnergySphere __energySphere = GameObject.Find ("EnergySphere").GetComponent<EnergySphere> ();
		string filepath = Application.dataPath + "/Resources/LevelsXML/Stage" + chapterIndex.ToString() + "-" + stageIndex.ToString() + ".xml";
		//string filepath = "C:/Users/Pelôn/Desktop/Lala/Stage" + chapterIndex.ToString() + "-" + stageIndex.ToString() + ".xml";
			//Delete a file that contains the same name
		if (File.Exists(filepath))
			File.Delete(filepath);
		
		using (FileStream fs = new FileStream (filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite,FileShare.ReadWrite))
		{
			//Create de xml file
			XmlDocument xmlDoc = new XmlDocument();
			//Set the root node
			XmlNode rootNode = xmlDoc.CreateElement("Level");
			xmlDoc.AppendChild(rootNode);

			/////////////////////////////////////////////////////////////////////
			//Save some info
			XmlNode levelInfo = xmlDoc.CreateElement("LevelInfo");

			XmlAttribute levelEditorAttrib = xmlDoc.CreateAttribute("LevelEditorVersion");
			XmlAttribute chapterAttrib = xmlDoc.CreateAttribute("Chapter");
			XmlAttribute stageAttrib = xmlDoc.CreateAttribute("Stage");

			levelEditorAttrib.Value = "1";
			chapterAttrib.Value = chapterIndex.ToString();
			stageAttrib.Value = stageIndex.ToString();

			levelInfo.Attributes.Append(levelEditorAttrib);
			levelInfo.Attributes.Append(chapterAttrib);
			levelInfo.Attributes.Append(stageAttrib);

			rootNode.AppendChild(levelInfo);

			/////////////////////////////////////////////////////////////////////
			//Save the ammo info
			XmlNode shootsInfoNode = xmlDoc.CreateElement("ShootsInfo");
			rootNode.AppendChild(shootsInfoNode);
			
			for(int i = 0; i < stageDescriptor.shootTypesList.Count; i ++)
			{
				XmlNode shootInfo = xmlDoc.CreateElement("ShootInfo");
				
				XmlAttribute shootType = xmlDoc.CreateAttribute("type");
				XmlAttribute shootWillBeUsed = xmlDoc.CreateAttribute("isUsed");
				XmlAttribute shootInfinite = xmlDoc.CreateAttribute("isInfinite");
				XmlAttribute shootAmmo = xmlDoc.CreateAttribute("ammo");
				XmlAttribute shootTutorialFocus = xmlDoc.CreateAttribute("tutFocus");
				
				shootType.Value = i.ToString();
				
				if (stageDescriptor.shootAmmoList[i] > 0 || stageDescriptor.shootInfiniteAmmoList[i])
				{
					shootWillBeUsed.Value = "true";
					shootInfinite.Value = stageDescriptor.shootInfiniteAmmoList[i].ToString();
					shootAmmo.Value = stageDescriptor.shootAmmoList[i].ToString();
					shootTutorialFocus.Value = stageDescriptor.shootTutorialFocus[i].ToString();
				}
				else
				{
					shootWillBeUsed.Value = "false";
					shootInfinite.Value = "false";
					shootAmmo.Value = "0";
					shootTutorialFocus.Value = "0";
				}
				shootInfo.Attributes.Append(shootType);
				shootInfo.Attributes.Append(shootWillBeUsed);
				shootInfo.Attributes.Append(shootInfinite);
				shootInfo.Attributes.Append(shootAmmo);
				shootInfo.Attributes.Append(shootTutorialFocus);
				
				
				shootsInfoNode.AppendChild(shootInfo);
			}

			/////////////////////////////////////////////////////////////////////
			//Save the platforms info - position/type
			XmlNode platformsNode = xmlDoc.CreateElement("Platforms");
			rootNode.AppendChild(platformsNode);
			
			foreach(Platform plat in __platformsList)
			{
				XmlNode platNode = xmlDoc.CreateElement("Platform");
				
				XmlAttribute platformX = xmlDoc.CreateAttribute("x");
				XmlAttribute platformY = xmlDoc.CreateAttribute("y");
				XmlAttribute platformZ = xmlDoc.CreateAttribute("z");
				XmlAttribute platformType = xmlDoc.CreateAttribute("type");
				XmlAttribute platformTutorialFocus = xmlDoc.CreateAttribute("tutFocus");

				platformX.Value = plat.transform.position.x.ToString();
				platformY.Value = plat.transform.position.y.ToString();
				platformZ.Value = plat.transform.position.z.ToString();
				platformType.Value = ((int)plat.platformType).ToString();
				platformTutorialFocus.Value = plat.tutorialFocusIndex.ToString();
				
				platNode.Attributes.Append(platformX);
				platNode.Attributes.Append(platformY);
				platNode.Attributes.Append(platformZ);
				platNode.Attributes.Append(platformType);
				platNode.Attributes.Append(platformTutorialFocus);

				platformsNode.AppendChild(platNode);
			}

			/////////////////////////////////////////////////////////////////////
			//Sabe the tutorial triggers
			XmlNode tutorialTriggersNode = xmlDoc.CreateElement("TutorialTriggers");
			rootNode.AppendChild(tutorialTriggersNode);

			foreach (TutorialTrigger trigger in __tutorialTriggersList)
			{
				XmlNode triggerNode = xmlDoc.CreateElement("TutorialTrigger");

				XmlAttribute triggerPosX = xmlDoc.CreateAttribute("x");
				XmlAttribute triggerPosY = xmlDoc.CreateAttribute("y");
				XmlAttribute triggerPosZ = xmlDoc.CreateAttribute("z");
				XmlAttribute triggerImageName = xmlDoc.CreateAttribute("img");
				XmlAttribute triggerCamPosX = xmlDoc.CreateAttribute("xCam");
				XmlAttribute triggerCamPosY = xmlDoc.CreateAttribute("yCam");
				XmlAttribute triggerCamPosZ = xmlDoc.CreateAttribute("zCam");
				XmlAttribute triggerIndex = xmlDoc.CreateAttribute("index");
				XmlAttribute triggerColliderX = xmlDoc.CreateAttribute("colliderX");
				XmlAttribute triggerColliderY = xmlDoc.CreateAttribute("colliderY");

				triggerPosX.Value = trigger.transform.position.x.ToString();
				triggerPosY.Value = trigger.transform.position.y.ToString();
				triggerPosZ.Value = trigger.transform.position.z.ToString();
				triggerImageName.Value = trigger.imageName;
				triggerCamPosX.Value = trigger.cameraPosition.x.ToString();
				triggerCamPosY.Value = trigger.cameraPosition.y.ToString();
				triggerCamPosZ.Value = trigger.cameraPosition.z.ToString();
				triggerIndex.Value = trigger.tutorialIndex.ToString();
				triggerColliderX.Value = trigger.GetComponent<BoxCollider2D>().size.x.ToString();
				triggerColliderY.Value = trigger.GetComponent<BoxCollider2D>().size.y.ToString();

				triggerNode.Attributes.Append(triggerPosX);
				triggerNode.Attributes.Append(triggerPosY);
				triggerNode.Attributes.Append(triggerPosZ);
				triggerNode.Attributes.Append(triggerImageName);
				triggerNode.Attributes.Append(triggerCamPosX);
				triggerNode.Attributes.Append(triggerCamPosY);
				triggerNode.Attributes.Append(triggerCamPosZ);
				triggerNode.Attributes.Append(triggerIndex);
				triggerNode.Attributes.Append(triggerColliderX);
				triggerNode.Attributes.Append(triggerColliderY);


				tutorialTriggersNode.AppendChild(triggerNode);
			}
			/////////////////////////////////////////////////////////////////////
			//Save the Energy Sphere info
			XmlNode energySphereNode = xmlDoc.CreateElement("EnergySphere");

			XmlAttribute xEnergySphere = xmlDoc.CreateAttribute("x");
			XmlAttribute yEnergySphere = xmlDoc.CreateAttribute("y");
			XmlAttribute zEnergySphere = xmlDoc.CreateAttribute("z");
			XmlAttribute focusEnergySphere = xmlDoc.CreateAttribute("tutFocus");
			XmlAttribute typeEnergySphere = xmlDoc.CreateAttribute("type");

			xEnergySphere.Value = __energySphere.transform.position.x.ToString();
			yEnergySphere.Value = __energySphere.transform.position.y.ToString();
			zEnergySphere.Value = __energySphere.transform.position.z.ToString();
			focusEnergySphere.Value = __energySphere.tutorialFocusIndex.ToString();
			typeEnergySphere.Value = ((int)__energySphere.sphereType).ToString();

			energySphereNode.Attributes.Append(xEnergySphere);
			energySphereNode.Attributes.Append(yEnergySphere);
			energySphereNode.Attributes.Append(zEnergySphere);
			energySphereNode.Attributes.Append(focusEnergySphere);
			energySphereNode.Attributes.Append(typeEnergySphere);

			rootNode.AppendChild(energySphereNode);

			/////////////////////////////////////////////////////////////////////
			//Save the player info - spawnPoint
			XmlNode playerNode = xmlDoc.CreateElement("Player");
			
			XmlAttribute xPlayer = xmlDoc.CreateAttribute("x");
			XmlAttribute yPlayer = xmlDoc.CreateAttribute("y");
			XmlAttribute zPlayer = xmlDoc.CreateAttribute("z");
			
			xPlayer.Value = __playerTransfom.position.x.ToString();
			yPlayer.Value = __playerTransfom.position.y.ToString();
			zPlayer.Value = __playerTransfom.position.z.ToString();
			
			playerNode.Attributes.Append(xPlayer);
			playerNode.Attributes.Append(yPlayer);
			playerNode.Attributes.Append(zPlayer);
			
			rootNode.AppendChild(playerNode);

			//Save the xml file
			xmlDoc.Save(fs);
			
		}
	}
	
	public void LoadLevelFromXML()
	{
		string filepath = Application.dataPath + "/LevelsXML/Stage" + chapterIndex.ToString() + "-" + stageIndex.ToString() + ".xml";
		
		if (!File.Exists(filepath))
		{
			Debug.Log("The File Don't Exist");
			return;
		}
		
		
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(filepath);
		XmlNodeList userNodes = xmlDoc.SelectNodes("//Platforms/Platform");
		foreach(XmlNode userNode in userNodes)
		{
			Platform __tempPlat = ((GameObject) GameObject.Instantiate(platPrefab)).GetComponent<Platform>();
			__tempPlat.transform.position = new Vector3(float.Parse(userNode.Attributes["x"].Value),float.Parse(userNode.Attributes["y"].Value),float.Parse(userNode.Attributes["z"].Value));
			__tempPlat.platformType = (GlobalInfo.PlaformType)int.Parse(userNode.Attributes["type"].Value);
			__tempPlat.platformStartType = __tempPlat.platformType;
			__tempPlat.name = "Platform";
		}
	}
}
