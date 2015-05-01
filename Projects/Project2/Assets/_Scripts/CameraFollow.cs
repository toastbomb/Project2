using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	private GameObject player;
	public Vector3 offset;
	public Transform battleTarget;

	void Update () 
	{
		player = GameObject.FindWithTag ("Player");
		if(player.GetComponent<PlayerManager>().playerState == PlayerManager.PlayerState.Walking)
		{
			Vector3 target = new Vector3 (player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
			transform.position = target;
			transform.LookAt (player.transform);
		}

		if(player.GetComponent<PlayerManager>().playerState == PlayerManager.PlayerState.Fighting)
		{
			if(battleTarget != null)
			{
				transform.LookAt (battleTarget);
			}
		}
	}
}
