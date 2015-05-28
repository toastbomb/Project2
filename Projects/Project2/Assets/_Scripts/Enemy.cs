using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public ArrayList enemyList = new ArrayList();
	public int xp = 20;
	public int coins = 3;
	public int health = 2;
	public int dmg = 1;
	public int def = 0;

	//Generate a list of size 1 to 5
	public void BuildEnemyList()
	{
		int rand = Mathf.FloorToInt(Random.Range(1.0f, 3.0f));

		for(int i = 0; i < rand; i++)
		{
			enemyList.Add(GameObject.Instantiate(this));
		}
	}
}
