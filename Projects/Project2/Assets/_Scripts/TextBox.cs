using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class TextBox : MonoBehaviour 
{
	public static TextBox textBox;
	private List<string> dialogues = new List<string>();
	public Text dialogue;

	void Awake () 
	{
		if(textBox == null)
		{
			textBox = this;
		}
		else if(textBox != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		HideTextBox();
	}

	void OnDisable()
	{
		DialogueLoop();
		PlayerManager.player.playerState = PlayerManager.PlayerState.Walking;
	}

	public void NextText()
	{
		DialogueLoop();
	}

	private void DialogueLoop()
	{
		if(dialogues != null)
		{
			if(dialogues.Count > 0)
			{
				GiveNewDialogue(dialogues[0]);
				dialogues.RemoveAt(0);
				ShowTextBox();
			}
			else
			{
				HideTextBox();
			}
		}
	}

	private void GiveNewDialogue(string someText)
	{
		dialogue.text = someText;
	}

	private void ShowTextBox()
	{
		this.gameObject.SetActive(true);
	}

	private void HideTextBox()
	{
		this.gameObject.SetActive(false);
	}

	public void StartTalking(List<string> dial)
	{
		PlayerManager.player.playerState = PlayerManager.PlayerState.Talking;
		dialogues.Clear();
		dialogues = dial;
		DialogueLoop();
	}
}
