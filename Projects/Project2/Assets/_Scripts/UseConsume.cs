using UnityEngine;
using System.Collections;

public class UseConsume : MonoBehaviour 
{
	public static UseConsume instance;
	
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
			print ("ba");
		}
	}

	void Start () 
	{
		this.gameObject.SetActive(false);
	}

	public void TurnOffMenu()
	{
		this.gameObject.SetActive(false);
	}

	public void TurnOnMenu()
	{
		this.gameObject.SetActive(true);
	}
}
