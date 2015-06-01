using UnityEngine;
using System.Collections;

public class EquipButton : MonoBehaviour {

	public void Buy(int item)
	{
		GameControl.control.Acquire(item);

	}
}
