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
	bool checkpoint = false;
	bool everHitCheckpoint = false;
	public ArrayList consumables = new ArrayList();
	public ArrayList equipment = new ArrayList ();
	public ArrayList equiped = new ArrayList ();

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
		data.dmg = dmg;
		data.def = def;
		data.bp = bp;
		data.max_bp = max_bp;
		data.player_level = player_level;
		data.consumables = consumables;
		data.equipment = equipment;
		data.equiped = equiped;
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
			dmg = data.dmg;
			def = data.def;
			bp = data.bp;
			max_bp = data.max_bp;
			player_level = data.player_level;
			consumables = data.consumables;
			equipment = data.equipment;
			equiped = data.equiped;
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
	//ITEMS
	//

	//Consumables
	public void GainItem(int i){
		consumables.Add (i);
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
	public void Acquire(int i){
		equipment.Add (i);
	}

	public void Equip(int i){
		equipment.Remove (i);
		equiped.Add (i);
		if (i == MHP5) {
			max_health += 5;
			health += 5;
		} else if (i == MMP5) {
			max_mana += 5;
			mana += 5;
		} else if (i == DMGP1) {
			dmg++;
		} else if (i == DEFP1) {
			def++;
		}
	}

	public void Unequip(int i){
		equiped.Remove (i);
		equipment.Add (i);
		if (i == MHP5) {
			max_health -= 5;
			if(health > 5){
				health -= 5;
			}
		} else if (i == MMP5) {
			max_mana -= 5;
			if(mana > 5){
				mana -= 5;
			}
		} else if (i == DMGP1) {
			dmg--;
		} else if (i == DEFP1) {
			def--;
		}
	}

	//for testing consumables uncommment

	void OnGUI(){

		int bpCost_MHP5 = 3;
		int bpCost_MMP5 = 3;
		int bpCost_DMGP1 = 6;
		int bpCost_DEFP1 = 9;
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
