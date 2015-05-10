using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	private GameObject player;
	public Vector3 offset;
	public Vector3 fightPos;

	void Update () 
	{
		//Get a reference to the player
		player = GameObject.FindWithTag ("Player");
		//Calc the position that we want the camera to be at
		Vector3 target = new Vector3 (player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
		//Make the camera postion equal the target position
		transform.position = target;
		//Always rotate to look at the player
		transform.LookAt (player.transform);
	}
}
