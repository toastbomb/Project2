using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour {

	public void ExitGame()
	{
		Application.Quit();
	}

	public void LoadGame()
	{
		GameControl.control.Load();
		Application.LoadLevel(GameControl.control.current_scene);
	}
}
