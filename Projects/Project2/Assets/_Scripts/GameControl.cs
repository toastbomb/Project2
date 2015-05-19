using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour 
{
	public static GameControl control;

	//Player
	public int max_health = 15;
	public int health = 15;
	public int max_mana = 10;
	public int mana = 10;
	public int xp = 0;
	public int coins = 0;
	public int dmg = 10;
	public int player_level = 1;
	bool checkpoint = false;
	bool everHitCheckpoint = false;
	public ArrayList items = new ArrayList();

	//Items
	const int HEALTHP5 = 1;
	const int MANAP5 = 2;
	const int HEALTHFULL = 3;
	const int MANAFULL = 4;

	//Item testing var
	bool buying = false;

	//Todo
	//Items, Badges, Equipped badges

	public ArrayList enemyList = new ArrayList();
	public ArrayList sceneActors = new ArrayList();

	//Make sure that we only ever have 1 GameControl
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

	public void Save() //Should be used when quiting
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData();
		data.max_health = max_health;
		data.health = health;
		data.max_mana = max_mana;
		data.mana = mana;
		data.xp = xp;
		data.coins = coins;
		data.player_level = player_level;
		data.items = items;
		data.posx = GameObject.FindGameObjectWithTag ("Player").transform.position.x;
		data.posy = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
		data.posz = GameObject.FindGameObjectWithTag ("Player").transform.position.z;
		if (checkpoint) {
			data.checkposx = GameObject.FindGameObjectWithTag ("Player").transform.position.x;
			data.checkposy = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
			data.checkposz = GameObject.FindGameObjectWithTag ("Player").transform.position.z;
		}
		checkpoint = false;
		bf.Serialize(file, data);
		file.Close();
	}

	public void Load() //Should be used when continuing
	{
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			max_health = data.max_health;
			health = data.health;
			max_mana = data.max_mana;
			mana = data.mana;
			xp = data.xp;
			coins = data.coins;
			player_level = data.player_level;
			items = data.items;
			Vector3 tempPos;
			if(checkpoint){
				if(everHitCheckpoint){
					tempPos = new Vector3(data.checkposx, data.checkposy, data.checkposz);
				}
				else{
					tempPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
				}
			}
			else{
				tempPos = new Vector3(data.posx, data.posy, data.posz);
			}
			GameObject.FindGameObjectWithTag ("Player").transform.position = tempPos;
			checkpoint = false;

		}
	}

	public void HitCheckpoint(){ //When you hit a checkpoint
		checkpoint = true;
		Save ();
	}
	public void Death(){ //On player death
		Save ();
		checkpoint = true;
		Load ();
	}

	//Items
	public void GainItem(int i){
		items.Add (i);
	}
	public void ConsumeItem(int i){
		items.Remove (i);
		if (i == HEALTHP5) {
			health += 5;
			if(health > max_health){
				health = max_health;
			}
		} else if (i == MANAP5) {
			mana += 5;
			if(mana > max_mana){
				mana = max_mana;
			}
		} else if (i == HEALTHFULL) {
			health = max_health;
		} else if (i == MANAFULL) {
			mana = max_mana;
		}
	}

	//for testing items uncommment
	///*
	void OnGUI(){


		if (buying) {
			if (GUI.Button (new Rect (Screen.width - 150, 10, 150, 50), "Not Buying")) {
				buying = false;
			}
			if (GUI.Button (new Rect (Screen.width - 150, 60, 150, 50), "Buy: Health + 5")) {
				GainItem (HEALTHP5);
			}
			if (GUI.Button (new Rect (Screen.width - 150, 110, 150, 50), "Buy: Mana + 5")) {
				GainItem (MANAP5);
			}
			if (GUI.Button (new Rect (Screen.width - 150, 160, 150, 50), "Buy: Full Heal")) {
				GainItem (HEALTHFULL);
			}
			if (GUI.Button (new Rect (Screen.width - 150, 210, 150, 50), "Buy: Full Mana")) {
				GainItem (MANAFULL);
			}
		} else {
			if (GUI.Button (new Rect (Screen.width - 150, 10, 150, 50), "Buying")) {
				buying = true;
			}
			int size = items.Count;
			ArrayList names = new ArrayList ();
			names.Add ("None");
			names.Add ("Health + 5");
			names.Add ("Mana + 5");
			names.Add ("Full Heal");
			names.Add ("Full Mana");
			for (int i=1; i< (size+1); i++) {
				if (GUI.Button (new Rect (Screen.width - 150, (float)(10+i*50), 150, 50), (string)names[(int)items[i-1]])) {
					ConsumeItem ((int)items[i-1]);
					size--;
				}
			}
		}
	}

	 //*/


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
	public int max_health;
	public int health;
	public int max_mana;
	public int mana;
	public int xp;
	public int coins;
	public int player_level;
	public float posx;
	public float posy;
	public float posz;
	public float checkposx;
	public float checkposy;
	public float checkposz;
	public ArrayList items;
}
