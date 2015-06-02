﻿using UnityEngine;
using System.Collections;

public class ConsumeButton : MonoBehaviour 
{
	public void Use(int item)
	{
		GameControl.control.ConsumeItem(item);
		PauseScreen.instance.Refresh();

		if(GameControl.control.isInBattle)
		{
			this.gameObject.SetActive(false);
			BattleControl.battleControl.ChangeToEnemyTurn();
		}
	}
}
