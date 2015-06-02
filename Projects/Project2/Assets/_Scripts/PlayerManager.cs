using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	public static PlayerManager player;

	public PlayerControllerMove playerControllerMove;

	public enum PlayerState{Walking, Talking, Fighting, Dead, Buying, Paused};
	public PlayerState playerState = PlayerState.Walking;

	public string sceneName;

	//temp
	public Enemy enemy;
	public bool slashing = false;
	public bool throwing = false;

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

			if(Input.GetKeyDown("i"))
			{
				PauseScreen.instance.OnStartPause();
			}
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
					enemy = (Enemy)other.GetComponent(typeof(Enemy));
					enemy.StopInvoke();
					//Destroy(other.gameObject);
				}
			}
			else if(other.tag == "Checkpoint"){
				GameControl.control.HitCheckpoint();
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
