using UnityEngine;
using System.Collections;

public class BattleMenu : MonoBehaviour 
{
	public static BattleMenu instance;

	void Awake () 
	{
		if(instance == null)
		{
			DontDestroyOnLoad (gameObject);
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}
}
