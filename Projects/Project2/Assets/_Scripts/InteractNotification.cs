using UnityEngine;
using System.Collections;

public class InteractNotification : MonoBehaviour 
{
	public static InteractNotification instance;
	
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

	void Start()
	{
		DeactivateMe();
	}

	void Update()
	{
		if(PlayerManager.player.playerState != PlayerManager.PlayerState.Walking)
		{
			DeactivateMe();
		}
	}

	public void ActivateMe()
	{
		this.gameObject.SetActive(true);
	}

	public void DeactivateMe()
	{
		this.gameObject.SetActive(false);
	}
}
