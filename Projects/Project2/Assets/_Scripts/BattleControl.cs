using UnityEngine;
using System.Collections;

public class BattleControl : MonoBehaviour 
{
	public static BattleControl battleControl;
	
	public Vector3 prevPlayerPos;
	public Vector3 playerBattlePos;
	public ArrayList enemies = new ArrayList ();
	int enemyXp = 0;
	int coins = 0;
	bool choosing = false;
	//temp
	public Enemy enemy;
	public int enemyNum;

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
		PlayerManager.player.enemy.BuildEnemyList ();
		enemies = PlayerManager.player.enemy.enemyList;
		Destroy (PlayerManager.player.enemy.gameObject);
		enemy = (Enemy)enemies [0];
		
		enemyNum = enemies.Count;
		for (int i=0; i < enemyNum; i++) {
			Enemy e = (Enemy)enemies[i];
			enemyXp += e.xp;
			coins += e.coins;
			e.transform.position = playerBattlePos;
			Vector3 temp = new Vector3((e.transform.position.x + (i+1)*2), e.transform.position.y, e.transform.position.z);
			e.transform.position = temp;
		}
		
		//enemy.BuildEnemyList ();
		//enemies = enemy.enemyList;
	}
	
	void OnGUI() 
	{
		if (choosing == true) {
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
					choosing = false;
				}
			}
		}
		else {
			if (GUI.Button(new Rect(10, 10, 150, 100), "End fight"))
			{
				EndOfFight();
			}
			if (GUI.Button(new Rect(10, Screen.height - 110, 150, 100), "Melee"))
			{
				Melee();
			}
			if (GUI.Button(new Rect(170, Screen.height - 110, 150, 100), "Ranged"))
			{
				choosing = true;
			}
			Vector3 pos = Camera.main.WorldToScreenPoint (PlayerManager.player.transform.position);
			GUI.Box (new Rect (pos.x - 40, pos.y - 20, 80, 20), "Health: " + GameControl.control.health);
			//enemy = (Enemy)enemies[0];
		}
	}
	
	void EndOfFight()
	{
		Destroy (enemy.gameObject);
		PlayerManager.player.transform.position = prevPlayerPos;
		GameControl.control.xp += enemyXp;
		GameControl.control.coins += coins;
		PlayerManager.player.OnExitBattle();
		Destroy(this.transform.parent.gameObject);
	}
	
	void Ranged(){
		int playerDmg = (int)(.8*((double)GameControl.control.dmg));//Later set to player damage value
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
		else {
			EnemyTurn ();
		}
	}
	
	void Melee(){
		
		enemy = (Enemy)enemies[0];
		int playerDmg = (int)(1.5*((double)GameControl.control.dmg));//Later set to player damage value
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
		else {
			EnemyTurn ();
		}
	}
	
	void EnemyTurn(){
		
		
		enemyNum = enemies.Count;
		for (int i=0; i < enemyNum; i++) {
			int enemyDmg = ((Enemy)enemies[i]).dmg;
			GameControl.control.health -= enemyDmg;
		}
	}
}
