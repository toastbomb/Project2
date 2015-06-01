using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class NPC : MonoBehaviour 
{
	public string[] conversation;
	private List<string> conversationList = new List<string>();
	private bool inRangeOfPlayer = false;

	public enum StoreType{StoreConsumable, StoreBadges, Conversation};
	public StoreType storeType = StoreType.Conversation;

	public Store consumableStore;
	public Store badgeStore;

	void Start () 
	{
		foreach(string text in conversation)
		{
			conversationList.Add(text);
		}
	}
	void Update () 
	{
		if(inRangeOfPlayer && PlayerManager.player.playerState == PlayerManager.PlayerState.Walking)
		{
			if(Input.GetKeyUp(KeyCode.E))
			{
				switch(storeType)
				{
				case StoreType.Conversation:
					TextBox.textBox.StartTalking(new List<string>(conversationList));
					break;
				case StoreType.StoreBadges:
					badgeStore.EnterStore();
					break;
				case StoreType.StoreConsumable:
					consumableStore.EnterStore();
					break;
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			inRangeOfPlayer = true;
			InteractNotification.instance.ActivateMe();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			inRangeOfPlayer = false;
			InteractNotification.instance.DeactivateMe();
		}
	}
}
