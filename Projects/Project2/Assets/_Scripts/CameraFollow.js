#pragma strict

var player : GameObject;

function Start () 
{
	player = GameObject.FindWithTag("Player");
}

function Update () 
{
	player = GameObject.FindWithTag("Player");
	transform.position.x = player.transform.position.x;
}