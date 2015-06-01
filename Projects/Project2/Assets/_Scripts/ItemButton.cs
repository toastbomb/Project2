using UnityEngine;
using System.Collections;

public class ItemButton : MonoBehaviour {

	public void Gain(int item)
	{
		GameControl.control.GainItem(item);
	}
}
