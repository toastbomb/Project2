using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager player;

	public PlayerControllerMove playerControllerMove;
	public enum PlayerState{Walking, Talking, Fighting, Dead};
	public PlayerState playerState = PlayerState.Walking;
	public string sceneName;

	void Awake () 
	{
		if(player == null)
		{
			DontDestroyOnLoad (gameObject);
			player = this;
		}
		else if(player != this)
		{
			Destroy(gameObject);
		}
	}

	void Update () 
	{
		if(playerState == PlayerState.Walking)
		{
			playerControllerMove.UpdateMoveController();
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(playerState == PlayerState.Walking)
		{
			if(other.tag == "Enemy")
			{
				if(sceneName != null)
				{
					OnEnterBattle();
					Application.LoadLevelAdditive(sceneName);
					Destroy(other.gameObject);
				}
			}
		}
	}

	public void OnEnterBattle()
	{
		playerState = PlayerState.Fighting;
	}

	public void OnExitBattle()
	{
		playerState = PlayerState.Walking;
	}

	public void OnDeath()
	{
		playerState = PlayerState.Dead;
	}
}
