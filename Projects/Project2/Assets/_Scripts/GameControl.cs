using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

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
	public int dmg = 2;
	public int def = 0;
	public int max_bp = 3;
	public int bp = 3;
	public int player_level = 1;
	public string current_scene = "Desert";
	public string check_scene = "Desert";
	bool checkpoint = false;
	bool everHitCheckpoint = false;
	public ArrayList consumables = new ArrayList();
	public ArrayList equipment = new ArrayList ();
	public ArrayList equiped = new ArrayList ();
	float checkposx;
	float checkposy;
	float checkposz;

	//Consumables
	const int HEALTHP5 = 1;
	const int MANAP5 = 2;
	const int HEALTHFULL = 3;
	const int MANAFULL = 4;

	//Equipment
	const int MHP5 = 1;
	const int MMP5 = 2;
	const int DMGP1 = 3;
	const int DEFP1 = 4;

	//Item testing var
	bool buying = false;
	public bool isInBattle = false;

	//Todo
	//Items, Badges, Equipped badges

	public ArrayList enemyList = new ArrayList();

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

	public void ClearSave()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
		PlayerData data = new PlayerData();
		data.posx = GameObject.FindGameObjectWithTag ("Player").transform.position.x;
		data.posy = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
		data.posz = GameObject.FindGameObjectWithTag ("Player").transform.position.z;
		if (checkpoint) {
			//print ("check");
			data.checkposx = GameObject.FindGameObjectWithTag ("Player").transform.position.x;
			data.checkposy = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
			data.checkposz = GameObject.FindGameObjectWithTag ("Player").transform.position.z;
			checkposx = data.checkposx;
			checkposy = data.checkposy;
			checkposz = data.checkposz;
			check_scene = Application.loadedLevelName;
			data.check_scene = check_scene;
			health = max_health;
			mana = max_mana;
		} else {
			//print ("check");
			data.checkposx = checkposx;
			data.checkposy = checkposy;
			data.checkposz = checkposz;
			data.check_scene = check_scene;
		}
		data.max_health = 15;
		data.health = 15;
		data.max_mana = 10;
		data.mana = 10;
		data.xp = 0;
		data.coins = 0;
		data.dmg = 2;
		data.def = 0;
		data.bp = 3;
		data.max_bp = 3;
		data.player_level = 1;
		data.consumables = new ArrayList();
		data.equipment = new ArrayList();
		data.equiped = new ArrayList();
		current_scene = "Desert";
		data.current_scene = "Desert";
		
		checkpoint = false;
		bf.Serialize(file, data);
		file.Close();
	}

	public void Save() //Should be used when quiting
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData();
		data.posx = GameObject.FindGameObjectWithTag ("Player").transform.position.x;
		data.posy = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
		data.posz = GameObject.FindGameObjectWithTag ("Player").transform.position.z;
		if (checkpoint) {
			//print ("check");
			data.checkposx = GameObject.FindGameObjectWithTag ("Player").transform.position.x;
			data.checkposy = GameObject.FindGameObjectWithTag ("Player").transform.position.y;
			data.checkposz = GameObject.FindGameObjectWithTag ("Player").transform.position.z;
			checkposx = data.checkposx;
			checkposy = data.checkposy;
			checkposz = data.checkposz;
			check_scene = Application.loadedLevelName;
			data.check_scene = check_scene;
			health = max_health;
			mana = max_mana;
		} else {
			//print ("check");
			data.checkposx = checkposx;
			data.checkposy = checkposy;
			data.checkposz = checkposz;
			data.check_scene = check_scene;
		}
		data.max_health = max_health;
		data.health = health;
		data.max_mana = max_mana;
		data.mana = mana;
		data.xp = xp;
		data.coins = coins;
		data.dmg = dmg;
		data.def = def;
		data.bp = bp;
		data.max_bp = max_bp;
		data.player_level = player_level;
		data.consumables = consumables;
		data.equipment = equipment;
		data.equiped = equiped;
		current_scene = Application.loadedLevelName;
		data.current_scene = current_scene;

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
			dmg = data.dmg;
			def = data.def;
			bp = data.bp;
			max_bp = data.max_bp;
			player_level = data.player_level;
			consumables = data.consumables;
			equipment = data.equipment;
			equiped = data.equiped;
			current_scene = data.current_scene;
			//print(data.check_scene);
			//print (check_scene);
			//check_scene = data.check_scene;
			//print (check_scene);
			Vector3 tempPos;
			string temp_scene;
			if(checkpoint){
				if(everHitCheckpoint){
					tempPos = new Vector3(data.checkposx, data.checkposy, data.checkposz);
					temp_scene = check_scene;
				}
				else{
					tempPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
					temp_scene = current_scene;
				}
			}
			else{
				tempPos = new Vector3(data.posx, data.posy, data.posz);
				temp_scene = current_scene;
			}
			GameObject.FindGameObjectWithTag ("Player").transform.position = tempPos;
			//SceneFadeInOut.sceneFadeInOut.EndScene(temp_scene);
			GameObject.FindGameObjectWithTag ("Player").transform.position = tempPos;
			checkpoint = false;

		}
	}

	public void HitCheckpoint(){ //When you hit a checkpoint
		everHitCheckpoint = true;
		checkpoint = true;
		Save ();
	}
	public void Death(){ //On player death
		Save ();
		checkpoint = true;
		Invoke("Load", 1);
	}
	//ITEMS
	//

	//Consumables
	public void GainItem(int i)
	{
		if(i == 1)
		{
			if(coins >= 5)
			{
				consumables.Add (i);
				coins -= 5;
			}
		}
		else if(i == 2)
		{
			if(coins >= 5)
			{
				consumables.Add (i);
				coins -= 5;
			}
		}
		else if(i == 3)
		{
			if(coins >= 15)
			{
				consumables.Add (i);
				coins -= 15;
			}
		}
		else if(i == 4)
		{
			if(coins >= 15)
			{
				consumables.Add (i);
				coins -= 15;
			}
		}
	}
	public void ConsumeItem(int i){
		consumables.Remove (i);
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

	//Equipment
	public void Acquire(int i)
	{
		if(i == 1)
		{
			if(coins >= 15)
			{
				equipment.Add (i);
				coins -= 15;
			}
		}
		else if(i == 2)
		{
			if(coins >= 15)
			{
				equipment.Add (i);
				coins -= 15;
			}
		}
		else if(i == 3)
		{
			if(coins >= 20)
			{
				equipment.Add (i);
				coins -= 20;
			}
		}
		else if(i == 4)
		{
			if(coins >= 35)
			{
				equipment.Add (i);
				coins -= 35;
			}
		}
	}

	public void Equip(int i)
	{
		if(i == 1)
		{
			if(bp >= 3)
			{
				equipment.Remove (i);
				equiped.Add (i);
				bp -= 3;
				max_health += 5;
				health += 5;
			}
		}
		else if(i == 2)
		{
			if(bp >= 3)
			{
				equipment.Remove (i);
				equiped.Add (i);
				bp -= 3;
				max_mana += 5;
				mana += 5;
			}
		}
		else if(i == 3)
		{
			if(bp >= 6)
			{
				equipment.Remove (i);
				equiped.Add (i);
				bp -= 6;
				dmg++;
			}
		}
		else if(i == 4)
		{
			if(bp >= 9)
			{
				equipment.Remove (i);
				equiped.Add (i);
				bp -= 9;
				def++;
			}
		}
	}

	public void Unequip(int i)
	{
		if(i == 1)
		{
			equiped.Remove (i);
			equipment.Add (i);
			bp += 3;
			max_health -= 5;
			if(health > 5)
			{
				health -= 5;
			}
		}
		else if(i == 2)
		{
			equiped.Remove (i);
			equipment.Add (i);
			bp += 3;
			max_mana -= 5;
			if(mana > 5)
			{
				mana -= 5;
			}
		}
		else if(i == 3)
		{
			equiped.Remove (i);
			equipment.Add (i);
			bp += 6;
			dmg--;
		}
		else if(i == 4)
		{
			equiped.Remove (i);
			equipment.Add (i);
			bp += 9;
			def--;
		}
	}

	//for testing consumables uncommment

	void OnGUI(){
	
		/*
		if (buying) {
			if (GUI.Button (new Rect (Screen.width - 150, 10, 150, 50), "Not Buying")) {
				buying = false;
			}
			if( bp >= bpCost_MHP5){
				if (GUI.Button (new Rect (Screen.width - 150, 60, 150, 50), "Buy: Max Health + 5")) {
					Acquire (MHP5);
					bp -= bpCost_MHP5;
					Equip (MHP5);
				}
			} else{
				GUI.Box (new Rect (Screen.width - 150, 60, 150, 50), "Buy: Max Health + 5");
			}

			if( bp >= bpCost_MMP5){
				if (GUI.Button (new Rect (Screen.width - 150, 110, 150, 50), "Buy: Max Mana + 5")) {
					Acquire (MMP5);
					bp -= bpCost_MMP5;
					Equip (MMP5);
				}
			} else{
				GUI.Box (new Rect (Screen.width - 150, 110, 150, 50), "Buy: Max Mana + 5");
			}

			if( bp >= bpCost_DMGP1){
				if (GUI.Button (new Rect (Screen.width - 150, 160, 150, 50), "Buy: Damage + 1")) {
					Acquire (DMGP1);
					bp -= bpCost_DMGP1;
					Equip (DMGP1);
				}
			} else{
				GUI.Box (new Rect (Screen.width - 150, 160, 150, 50), "Buy: Damage + 1");
			}

			if( bp >= bpCost_DEFP1){
				if (GUI.Button (new Rect (Screen.width - 150, 210, 150, 50), "Buy: Defense + 1")) {
					Acquire (DEFP1);
					bp -= bpCost_DEFP1;
					Equip (DEFP1);
				}
			} else{
				GUI.Box (new Rect (Screen.width - 150, 210, 150, 50), "Buy: Defense + 1");
			}
		} else {
			if (GUI.Button (new Rect (Screen.width - 150, 10, 150, 50), "Buying + Equip")) {
				buying = true;
			}
			int size = consumables.Count;
			ArrayList names = new ArrayList ();
			names.Add ("None");
			names.Add ("Health + 5");
			names.Add ("Mana + 5");
			names.Add ("Full Heal");
			names.Add ("Full Mana");
			for (int i=1; i< (size+1); i++) {
				if (GUI.Button (new Rect (Screen.width - 150, (float)(10+i*50), 150, 50), (string)names[(int)consumables[i-1]])) {
					ConsumeItem ((int)consumables[i-1]);
					size--;
				}
			}
		}
		*/
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
	public int def;
	public int dmg;
	public int max_bp;
	public int bp;
	public int player_level;
	public string current_scene;
	public string check_scene;
	public float posx;
	public float posy;
	public float posz;
	public float checkposx;
	public float checkposy;
	public float checkposz;
	public ArrayList consumables;
	public ArrayList equipment;
	public ArrayList equiped;
}
