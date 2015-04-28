#pragma strict

var playerMoveController : PlayerMoveController;

enum PlayerState{Talking, Walking, Fighting};
var playerState : PlayerState = playerState.Walking;

function Update () 
{
	if(playerState == PlayerState.Walking)
	{
		playerMoveController.MoveUpdate();
	}
}