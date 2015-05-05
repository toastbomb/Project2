using UnityEngine;
using System.Collections;

public class BattleControl : MonoBehaviour 
{
	public static BattleControl battleControl;
	
	public Vector3 prevPlayerPos;
	public Vector3 playerBattlePos;
	
	void Awake () 
	{
		if(battleControl == null)
		{
			battleControl = this;
		}
		else if(battleControl != this)
		{
			Destroy(gameObject);
		}
	}
	
	void Start()
	{
		prevPlayerPos = PlayerManager.player.transform.position;
		PlayerManager.player.transform.position = playerBattlePos;
	}

	void OnGUI() 
	{
		if (GUI.Button(new Rect(10, 10, 150, 100), "End fight"))
		{
			EndOfFight();
		}
	}
	
	public void EndOfFight()
	{
		PlayerManager.player.transform.position = prevPlayerPos;
		PlayerManager.player.OnExitBattle();
		Destroy(this.transform.parent.gameObject);
	}
}
