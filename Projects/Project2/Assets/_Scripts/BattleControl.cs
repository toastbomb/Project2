using UnityEngine;
using System.Collections;

public class BattleControl : MonoBehaviour 
{
	public GameObject[] aerialPositions = new GameObject[5];
	public GameObject[] groundPositions = new GameObject[5];
	public GameObject[] enemies = new GameObject[5];

	void Start () 
	{
		int i = 0;
		foreach(GameObject obj in GameControl.control.enemyList)
		{
			if(obj.tag == "Enemy")
			{
				enemies[i] = GameObject.Instantiate(obj);
				//enemies[i].transform.position = groundPositions[i].transform.position;
				i++;
			}
		}
	}

	void Update () 
	{
	
	}
}
