using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateUIValue : MonoBehaviour 
{
	public Text myText;
	public enum UpdateVal{Health, Mana, Coins};
	public UpdateVal updateVal = UpdateVal.Health;

	void Update () 
	{
		switch(updateVal)
		{
		case UpdateVal.Health:
			myText.text = GameControl.control.health.ToString() + " / " + GameControl.control.max_health.ToString();
			break;
		case UpdateVal.Mana:
			myText.text = GameControl.control.mana.ToString() + " / " + GameControl.control.max_mana.ToString();
			break;
		case UpdateVal.Coins:
			myText.text = "x " + GameControl.control.coins.ToString();
			break;
		}
	}
}
