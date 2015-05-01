using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
	public PlayerControllerMove playerControllerMove;
	public enum PlayerState{Walking, Talking, Fighting, Dead};
	public PlayerState playerState = PlayerState.Walking;

	public float health;
	public float xp;
	public float mana;

	void Update () 
	{
		if(playerState == PlayerState.Walking)
		{
			playerControllerMove.UpdateMoveController();
		}
	}
}
