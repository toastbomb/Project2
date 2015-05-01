using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public ArrayList enemyList = new ArrayList();

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			BuildEnemyList();
			GameControl.control.enemyList = this.enemyList;
			GameControl.control.sceneActors.Remove(this);
			Application.LoadLevel("battle");
		}
	}

	void BuildEnemyList()
	{
		int rand = Mathf.FloorToInt(Random.Range(1.0f, 5.0f));

		for(int i = 0; i < rand; i++)
		{
			enemyList.Add(GameObject.Instantiate(this));
		}
	}
}
