#pragma strict

var player : GameObject;

function Start () 
{
	player = GameObject.FindWithTag("Player");
}

function Update () 
{
	transform.position.x = player.transform.position.x;
}