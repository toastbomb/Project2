#pragma strict

var sceneName : String;

function OnTriggerEnter(other : Collider)
{
	if(other.tag == "Player")
	{
		if(sceneName != null)
		{
			Application.LoadLevel(sceneName);
		}
	}
}