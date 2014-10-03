using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class LevelEditorManager : MonoBehaviour {

	public int 			chapterIndex;
	public int			stageIndex;

	public GameObject	platPrefab;

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
		List<Platform> platformsList = new List<Platform>(GameObject.FindObjectsOfType<Platform>());
		Transform __playerTransfom = GameObject.Find("Player").transform;

		string filepath = Application.dataPath + "/LevelsXML/Stage" + chapterIndex.ToString() + "-" + stageIndex.ToString() + ".xml";

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


			//Save the platforms info - position/type
			XmlNode platformsNode = xmlDoc.CreateElement("Platforms");
			rootNode.AppendChild(platformsNode);

			foreach(Platform plat in platformsList)
			{
				XmlNode platNode = xmlDoc.CreateElement("Platform");

				XmlAttribute platformX = xmlDoc.CreateAttribute("x");
				XmlAttribute platformY = xmlDoc.CreateAttribute("y");
				XmlAttribute platformZ = xmlDoc.CreateAttribute("z");
				XmlAttribute platformType = xmlDoc.CreateAttribute("type");

				platformX.Value = plat.transform.position.x.ToString();
				platformY.Value = plat.transform.position.y.ToString();
				platformZ.Value = plat.transform.position.z.ToString();
				platformType.Value = ((int)plat.platformType).ToString();

				platNode.Attributes.Append(platformX);
				platNode.Attributes.Append(platformY);
				platNode.Attributes.Append(platformZ);
				platNode.Attributes.Append(platformType);

				platformsNode.AppendChild(platNode);
			}

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
			xmlDoc.Save(filepath);

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
