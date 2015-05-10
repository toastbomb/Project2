using UnityEngine;
using System.Collections;

public class SceneTrigger : MonoBehaviour 
{
	public string sceneName;

	void OnTriggerEnter(Collider other)
	{
		//If we touch something that is the player then load a new scene
		if(other.tag == "Player")
		{
			if(sceneName != null)
			{
				GameControl.control.sceneActors.Clear();
				Application.LoadLevel(sceneName);
			}
		}
	}
}
