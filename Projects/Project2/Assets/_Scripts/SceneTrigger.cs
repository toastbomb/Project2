using UnityEngine;
using System.Collections;

public class SceneTrigger : MonoBehaviour 
{
	public string sceneName;
	public Vector3 teleportLocation;

	void OnTriggerEnter(Collider other)
	{
		//If we touch something that is the player then load a new scene
		if(other.tag == "Player")
		{
			if(sceneName != null)
			{
				PlayerManager.player.transform.position = teleportLocation;
				SceneFadeInOut.sceneFadeInOut.EndScene(sceneName);
			}
		}
	}
}
