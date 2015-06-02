using UnityEngine;
using System.Collections;

public class ButtonConsume : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActiveWindow()
	{
		UseConsume.instance.TurnOnMenu();
	}
}
