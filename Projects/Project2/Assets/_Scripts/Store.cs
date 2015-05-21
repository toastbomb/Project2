using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour 
{
	void Start()
	{
		this.gameObject.SetActive (false);
	}

	void OnEnable()
	{
		PlayerManager.player.playerState = PlayerManager.PlayerState.Buying;
	}

	void OnDisable()
	{
		PlayerManager.player.playerState = PlayerManager.PlayerState.Walking;
	}

	public void EnterStore()
	{
		this.gameObject.SetActive (true);
	}

	public void ExitStore()
	{
		this.gameObject.SetActive (false);
	}
}
