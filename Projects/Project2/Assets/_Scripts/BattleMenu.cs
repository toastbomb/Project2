using UnityEngine;
using System.Collections;

public class BattleMenu : MonoBehaviour 
{
	public static BattleMenu instance;

	void Awake () 
	{
		if(instance == null)
		{
			DontDestroyOnLoad (gameObject);
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		this.gameObject.SetActive(false);
	}

	public void EnterBattle()
	{
		this.gameObject.SetActive(true);
	}

	public void ExitBattle()
	{
		this.gameObject.SetActive(false);
	}

	public void Melee()
	{
		BattleControl.battleControl.OnMeleeButton();
	}

	public void Ranged()
	{
		BattleControl.battleControl.OnRangedButton();
	}

	public void Double()
	{
		BattleControl.battleControl.OnButtonDouble();
	}

	public void Heavy()
	{
		BattleControl.battleControl.OnHeavyButton();
	}
}
