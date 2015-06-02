using UnityEngine;
using System.Collections;

public class ConsumeButton : MonoBehaviour 
{
	public void Use(int item)
	{
		GameControl.control.ConsumeItem(item);
		PauseScreen.instance.Refresh();

		if(GameControl.control.isInBattle)
		{
			BattleControl.battleControl.ChangeToEnemyTurn();
			UseConsume.instance.TurnOffMenu();
		}
	}
}
