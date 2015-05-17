﻿using UnityEngine;
using System.Collections;

public class BattleControl : MonoBehaviour 
{
	public static BattleControl battleControl;
	
	public Vector3 prevPlayerPos;
	public Vector3 playerBattlePos;
	public ArrayList enemies = new ArrayList ();
	int enemyXp = 0;//total xp awarded at end of fight
	int coins = 0;//total coins awarded at end of fight
	string choosing = "";//choosing an enemy to attack
	bool leveling = false;//leveling up

	public Enemy enemy;
	public int enemyNum;
	double HSmult = 1.5;

	//Make sure we only ever have one BattleControl at a time
	void Awake () 
	{
		if(battleControl == null)
		{
			battleControl = this;
		}
		else if(battleControl != this)
		{
			Destroy(gameObject);
		}
	}
	
	void Start()
	{
		prevPlayerPos = PlayerManager.player.transform.position;
		PlayerManager.player.transform.position = playerBattlePos;
		//Build enemies
		PlayerManager.player.enemy.BuildEnemyList ();
		enemies = PlayerManager.player.enemy.enemyList;
		Destroy (PlayerManager.player.enemy.gameObject);
		enemy = (Enemy)enemies [0];
		
		enemyNum = enemies.Count; //Position enemies
		for (int i=0; i < enemyNum; i++) {
			Enemy e = (Enemy)enemies[i];
			enemyXp += e.xp;
			coins += e.coins;
			e.transform.position = playerBattlePos;
			Vector3 temp = new Vector3((e.transform.position.x + (i+1)*2), e.transform.position.y, e.transform.position.z);
			e.transform.position = temp;
		}
	}
	
	void OnGUI() 
	{
		if (choosing == "basic") { //Ranged attack UI
			enemyNum = enemies.Count;
			ArrayList tempList = new ArrayList();
			for(int i=0; i < enemyNum; i++){
				Vector3 pos = Camera.main.WorldToScreenPoint (((Enemy)enemies[i]).transform.position);
				tempList.Add(pos);
			}
			
			for (int i=0; i < enemyNum; i++) {
				if (GUI.Button (new Rect (((Vector3)tempList[i]).x - 40, ((Vector3)tempList[i]).y - 40, 80, 20), "Enemy " + i)) {
					enemy = (Enemy)enemies [i];
					Ranged ();
					choosing = "";
				}
			}
		}
		else if (choosing == "DT") { //Ranged attack UI
			enemyNum = enemies.Count;
			ArrayList tempList = new ArrayList();
			for(int i=0; i < enemyNum; i++){
				Vector3 pos = Camera.main.WorldToScreenPoint (((Enemy)enemies[i]).transform.position);
				tempList.Add(pos);
			}
			
			for (int i=0; i < enemyNum; i++) {
				if (GUI.Button (new Rect (((Vector3)tempList[i]).x - 40, ((Vector3)tempList[i]).y - 40, 80, 20), "Enemy " + i)) {
					enemy = (Enemy)enemies [i];
					DoubleTap (i);
					choosing = "";
				}
			}
		}
		else {
			if(leveling){ //LevelUp UI
				GUI.Box(new Rect(Screen.width/2, 10, 150, 20), "Level Up!");
				if(GUI.Button(new Rect(Screen.width/4, Screen.height/2, 150, 100), "Health + 5")){
					leveling = false;
					GameControl.control.max_health += 5;
					MaxEverything();
					if (GameControl.control.xp >= 100) {
						GameControl.control.xp -= 100;
						LevelUp ();
					} else {
						Leave ();
					}
				}
				if(GUI.Button(new Rect(Screen.width/2, Screen.height/2, 150, 100), "Mana + 5")){
					leveling = false;
					GameControl.control.max_mana += 5;
					MaxEverything();
					if (GameControl.control.xp >= 100) {
						GameControl.control.xp -= 100;
						LevelUp ();
					} else {
						Leave ();
					}
				}
				if(GUI.Button(new Rect((Screen.width*3)/4, Screen.height/2, 150, 100), "Something else")){
					leveling = false;
					//GameControl.control.whatever
					MaxEverything();
					if (GameControl.control.xp >= 100) {
						GameControl.control.xp -= 100;
						LevelUp ();
					} else {
						Leave ();
					}
				}
			}
			else{
				if (GUI.Button(new Rect(10, 10, 150, 100), "End fight"))
				{
					EndOfFight();
				}
				if (GUI.Button(new Rect(10, Screen.height - 110, 150, 100), "Melee: \n" + (int)(1.5*((double)GameControl.control.dmg))))
				{
					Melee();
				}
				int manaCostHS = 3;
				if(GameControl.control.mana > manaCostHS){
					if (GUI.Button(new Rect(10, Screen.height - 160, 150, 50), "Heavy Strike: " + manaCostHS +  " mana: \n" + (int)(1.5*((double)GameControl.control.dmg)*HSmult))){
						GameControl.control.mana -= manaCostHS;
						HeavyStrike();
					}
				}
				if (GUI.Button(new Rect(170, Screen.height - 110, 150, 100), "Ranged: \n" + (int)(.8*((double)GameControl.control.dmg))))
				{
					choosing = "basic";
				}
				int manaCostDT = 3;
				if(GameControl.control.mana > manaCostHS){
					if (GUI.Button(new Rect(170, Screen.height - 160, 150, 50), "Double Tap: " + manaCostDT +  " mana: \n" + (int)(.8*((double)GameControl.control.dmg)))){
						GameControl.control.mana -= manaCostDT;
						choosing = "DT";
					}
				}
				Vector3 pos = Camera.main.WorldToScreenPoint (PlayerManager.player.transform.position);
				GUI.Box (new Rect (pos.x - 40, pos.y - 20, 80, 20), "Health: " + GameControl.control.health + "/" + GameControl.control.max_health);
				//Enemy hp
				enemyNum = enemies.Count;
				ArrayList tempList = new ArrayList();
				for(int i=0; i < enemyNum; i++){
					pos = Camera.main.WorldToScreenPoint (((Enemy)enemies[i]).transform.position);
					tempList.Add(pos);
				}
				for (int i=0; i < enemyNum; i++) {
					GUI.Box (new Rect (((Vector3)tempList[i]).x - 40, ((Vector3)tempList[i]).y - 40, 80, 20), "Health: " + ((Enemy)enemies[i]).health);
				}
			}
		}
	}
	
	void EndOfFight()
	{
		GameControl.control.xp += enemyXp;
		GameControl.control.coins += coins;
		if (GameControl.control.xp >= 100) {
			GameControl.control.xp -= 100;
			LevelUp ();
		} else {
			Leave ();
		}
	}
	
	void Ranged(){ //Basic ranged attack
		int playerDmg = (int)(.8*((double)GameControl.control.dmg));
		enemy.health -= playerDmg;
		if (enemy.health <= 0) {
			enemies.Remove(enemy);
			Destroy (enemy.gameObject);
			if(enemies.Count == 0){
				EndOfFight ();
			}
			else{
				enemy = (Enemy)enemies[0];
			}
		}
		EnemyTurn ();
	}

	void DoubleTap(int i){ //Ranged skill
		int playerDmg = (int)(.8*((double)GameControl.control.dmg));
		if ((i + 1) < enemies.Count) {
			enemy.health -= playerDmg;
			if (enemy.health <= 0) {
				enemies.Remove(enemy);
				Destroy (enemy.gameObject);
				enemy = (Enemy)enemies[i];
			}
			else{
				enemy = (Enemy)enemies[i+1];
			}
		}
		enemy.health -= playerDmg;
		if (enemy.health <= 0) {
			enemies.Remove(enemy);
			Destroy (enemy.gameObject);
			if(enemies.Count == 0){
				EndOfFight ();
			}
			else{
				enemy = (Enemy)enemies[0];
			}
		}
		EnemyTurn ();
	}
	
	void Melee(){ //Basic melee attack
		
		enemy = (Enemy)enemies[0];
		int playerDmg = (int)(1.5*((double)GameControl.control.dmg));
		enemy.health -= playerDmg;
		if (enemy.health <= 0) {
			enemies.Remove(enemy);
			Destroy (enemy.gameObject);
			if(enemies.Count == 0){
				EndOfFight ();
			}
			else{
				enemy = (Enemy)enemies[0];
			}
		}
		EnemyTurn ();
	}

	void HeavyStrike(){ //Melee skill

		enemy = (Enemy)enemies[0];
		int playerDmg = (int)(1.5*((double)GameControl.control.dmg)*HSmult);
		enemy.health -= playerDmg;
		if (enemy.health <= 0) {
			enemies.Remove(enemy);
			Destroy (enemy.gameObject);
			if(enemies.Count == 0){
				EndOfFight ();
			}
			else{
				enemy = (Enemy)enemies[0];
			}
		}
		EnemyTurn ();
	}
	
	void EnemyTurn(){ //Enemies attack
		
		
		enemyNum = enemies.Count;
		for (int i=0; i < enemyNum; i++) {
			int enemyDmg = ((Enemy)enemies[i]).dmg;
			GameControl.control.health -= enemyDmg;
		}
	}

	void LevelUp(){ //Player levels up
		GameControl.control.player_level += 1;
		MaxEverything ();
		leveling = true;
	}

	void Leave(){ //Exiting the battle
		//Destroy (enemy.gameObject);
		PlayerManager.player.transform.position = prevPlayerPos;
		PlayerManager.player.OnExitBattle ();
		Destroy (this.transform.parent.gameObject);
	}

	void MaxEverything(){ //Reset all current values to max (on level up)
		GameControl.control.health = GameControl.control.max_health;
		GameControl.control.mana = GameControl.control.max_mana;
	}
}
