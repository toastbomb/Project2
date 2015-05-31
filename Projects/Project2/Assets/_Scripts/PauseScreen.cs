using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class PauseScreen : MonoBehaviour 
{
	public static PauseScreen instance;

	public Transform contentPanelConsume;
	public GameObject HP5;
	public GameObject Mana5;
	public GameObject HPFull;
	public GameObject ManaFull;

	public Transform contentPanelEquipment;
	public GameObject MHP5;
	public GameObject MMP5;
	public GameObject DMGP1;
	public GameObject DEFP1;

	public Transform contentPanelEquip;
	public GameObject MHP5e;
	public GameObject MMP5e;
	public GameObject DMGP1e;
	public GameObject DEFP1e;
	
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
		this.gameObject.SetActive (false);
	}

	void OnEnable()
	{
		PlayerManager.player.playerState = PlayerManager.PlayerState.Paused;
	}
	
	void OnDisable()
	{
		PlayerManager.player.playerState = PlayerManager.PlayerState.Walking;
		foreach(Transform child in contentPanelConsume)
		{
			GameObject.Destroy(child.gameObject);
		}
		//foreach(Transform child in contentPanelEquipment)
		//{
		//	GameObject.Destroy(child.gameObject);
		//}
		//foreach(Transform child in contentPanelEquip)
		//{
		//	GameObject.Destroy(child.gameObject);
		//}
	}

	public void Refresh()
	{
		print ("bla");
		foreach(Transform child in contentPanelConsume)
		{
			GameObject.Destroy(child.gameObject);
		}
		//foreach(Transform child in contentPanelEquipment)
		//{
		//	GameObject.Destroy(child.gameObject);
		//}
		//foreach(Transform child in contentPanelEquip)
		//{
		//	GameObject.Destroy(child.gameObject);
		//}
		PopulateListConsume();
		//PopulateListEquipment();
		//PopulateListEquipped();
	}

	public void OnStartPause()
	{
		this.gameObject.SetActive (true);
		Refresh();
	}
	
	public void OnEndPause()
	{
		this.gameObject.SetActive (false);
	}
	
	public void PopulateListConsume () 
	{
		print (GameControl.control.consumables.Count);
		for(int i = 0; i < GameControl.control.consumables.Count; i++)
		{
			print (i);
			if(GameControl.control.consumables[i].Equals(1))
			{
				GameObject newButton = Instantiate (HP5) as GameObject;
				newButton.transform.SetParent(contentPanelConsume);
			}
			if(GameControl.control.consumables[i].Equals(2))
			{
				GameObject newButton = Instantiate (Mana5) as GameObject;
				newButton.transform.SetParent(contentPanelConsume);
			}
			if(GameControl.control.consumables[i].Equals(3))
			{
				GameObject newButton = Instantiate (HPFull) as GameObject;
				newButton.transform.SetParent(contentPanelConsume);
			}
			if(GameControl.control.consumables[i].Equals(4))
			{
				GameObject newButton = Instantiate (ManaFull) as GameObject;
				newButton.transform.SetParent(contentPanelConsume);
			}
		}
	}

	public void PopulateListEquipment () 
	{
		for(int i = 0; i < GameControl.control.equipment.Count; i++)
		{
			if(GameControl.control.equipment[i].Equals(1))
			{
				GameObject newButton = Instantiate (MHP5) as GameObject;
				newButton.transform.SetParent(contentPanelEquipment);
			}
			if(GameControl.control.equipment[i].Equals(2))
			{
				GameObject newButton = Instantiate (MMP5) as GameObject;
				newButton.transform.SetParent(contentPanelEquipment);
			}
			if(GameControl.control.equipment[i].Equals(3))
			{
				GameObject newButton = Instantiate (DMGP1) as GameObject;
				newButton.transform.SetParent(contentPanelEquipment);
			}
			if(GameControl.control.equipment[i].Equals(4))
			{
				GameObject newButton = Instantiate (DEFP1) as GameObject;
				newButton.transform.SetParent(contentPanelEquipment);
			}
		}
	}

	public void PopulateListEquipped () 
	{
		for(int i = 0; i < GameControl.control.equiped.Count; i++)
		{
			if(GameControl.control.equiped[i].Equals(1))
			{
				GameObject newButton = Instantiate (MHP5e) as GameObject;
				newButton.transform.SetParent(contentPanelEquip);
			}
			if(GameControl.control.equiped[i].Equals(2))
			{
				GameObject newButton = Instantiate (MMP5e) as GameObject;
				newButton.transform.SetParent(contentPanelEquip);
			}
			if(GameControl.control.equiped[i].Equals(3))
			{
				GameObject newButton = Instantiate (DMGP1e) as GameObject;
				newButton.transform.SetParent(contentPanelEquip);
			}
			if(GameControl.control.equiped[i].Equals(4))
			{
				GameObject newButton = Instantiate (DEFP1e) as GameObject;
				newButton.transform.SetParent(contentPanelEquip);
			}
		}
	}
}
