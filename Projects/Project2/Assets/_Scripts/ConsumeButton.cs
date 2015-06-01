using UnityEngine;
using System.Collections;

public class ConsumeButton : MonoBehaviour 
{
	public void Use(int item)
	{
		GameControl.control.consumables.Remove(item);
		PauseScreen.instance.Refresh();
	}
}
