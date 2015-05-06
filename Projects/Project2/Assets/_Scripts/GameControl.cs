using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour 
{
	public static GameControl control;

	//Player
	public int health = 100;
	public int mana = 50;
	public int xp = 0;
	public int coins = 0;
	public int dmg = 10;

	//Todo
	//Items, Badges, Equipped badges

	public ArrayList enemyList = new ArrayList();
	public ArrayList sceneActors = new ArrayList();

	void Awake () 
	{
		if(control == null)
		{
			DontDestroyOnLoad (gameObject);
			control = this;
		}
		else if(control != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		CacheActors(Application.loadedLevel);
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData();
		data.health = health;
		data.mana = mana;
		data.xp = xp;
		data.coins = coins;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			health = data.health;
			mana = data.mana;
			xp = data.xp;
			coins = data.coins;
		}
	}

	void OnLevelWasLoaded(int level)
	{
		CacheActors(level);
	}
	void CacheActors(int level)
	{
		if(level != 0)
		{
			if(sceneActors.Count == 0)
			{
				sceneActors.Add(GameObject.FindGameObjectWithTag("Player"));
				GameObject[] actors = GameObject.FindGameObjectsWithTag("Enemy");
				
				foreach(GameObject actor in actors)
				{
					sceneActors.Add(actor);
				}
			}
			else
			{
				GameObject[] actors = GameObject.FindGameObjectsWithTag("Enemy");
				
				foreach(GameObject actor in actors)
				{
					Destroy(actor);
				}
				
				foreach(GameObject obj in sceneActors)
				{
					Instantiate(obj);
				}
				
			}
		}
	}
}
	
[Serializable]
class PlayerData
{
	public int health;
	public int mana;
	public int xp;
	public int coins;
}
