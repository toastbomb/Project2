using UnityEngine;
using System.Collections;

public class SceneTrigger : MonoBehaviour 
{
	public string sceneName;

	void OnTriggerEnter(Collider other)
	{
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
