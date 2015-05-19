using UnityEngine;
using System.Collections;

public class SceneTrigger : MonoBehaviour 
{
	public string sceneName;
	public PlayerManager.LastExit exitDirection = PlayerManager.LastExit.NULL;

	void OnTriggerEnter(Collider other)
	{
		//If we touch something that is the player then load a new scene
		if(other.tag == "Player")
		{
			if(sceneName != null)
			{
				GameControl.control.sceneActors.Clear();
				PlayerManager.player.lastExit = exitDirection;
				Application.LoadLevel(sceneName);
			}
		}
	}
}
