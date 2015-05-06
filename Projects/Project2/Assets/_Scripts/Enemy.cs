using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public ArrayList enemyList = new ArrayList();
	public int xp = 10;
	public int coins = 3;
	public int health = 10;
	public int dmg = 5;
	public int def = 0;

	public void BuildEnemyList()
	{
		int rand = Mathf.FloorToInt(Random.Range(1.0f, 5.0f));

		for(int i = 0; i < rand; i++)
		{
			enemyList.Add(GameObject.Instantiate(this));
		}
	}
}
