using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Vector3 targetObject;
	public Vector3 offset;

	public static CameraFollow instance;

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

	void Update () 
	{
		//Calc the position that we want the camera to be at
		Vector3 target = new Vector3 (PlayerManager.player.transform.position.x + offset.x, PlayerManager.player.transform.position.y + offset.y, PlayerManager.player.transform.position.z + offset.z);
		//Make the camera postion equal the target position
		transform.position = target;
		//Always rotate to look at the player
		transform.LookAt (PlayerManager.player.transform);
	}
}
