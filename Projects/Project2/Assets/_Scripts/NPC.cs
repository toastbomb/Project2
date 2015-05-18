using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPC : MonoBehaviour 
{
	public string[] conversation;
	private List<string> conversationList = new List<string>();
	private bool inRangeOfPlayer = false;

	void Start () 
	{
		foreach(string text in conversation)
		{
			conversationList.Add(text);
		}
	}
	void Update () 
	{
		if(inRangeOfPlayer)
		{
			if(Input.GetKeyUp(KeyCode.E))
			{
				TextBox.textBox.StartTalking(new List<string>(conversationList));
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			inRangeOfPlayer = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			inRangeOfPlayer = false;
		}
	}
}
