using UnityEngine;
using System.Collections;

public class EquipmentButton : MonoBehaviour {

	public void Equip(int item)
	{
		GameControl.control.Equip(item);
		PauseScreen.instance.Refresh();
	}

	public void Dequip(int item)
	{
		GameControl.control.Unequip(item);
		PauseScreen.instance.Refresh();
	}
}
