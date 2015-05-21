using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour 
{
	public static SceneFadeInOut sceneFadeInOut;

	public float fadeSpeed = 1.5f;
	private bool sceneStarting = true;
	public Image fader;

	void Awake()
	{
		if(sceneFadeInOut == null)
		{
			DontDestroyOnLoad (gameObject);
			sceneFadeInOut = this;
		}
		else if(sceneFadeInOut != this)
		{
			Destroy(gameObject);
		}
		fader.color = Color.black;
	}

	void Update()
	{
		if(sceneStarting)
		{
			StartScene();
		}
	}

	void FadeToClear()
	{
		fader.color = Color.Lerp (fader.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack()
	{
		fader.color = Color.Lerp (fader.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	void StartScene()
	{
		FadeToClear ();

		if(fader.color.a <= 0.05f)
		{
			fader.color = Color.clear;
			fader.enabled = false;
			sceneStarting = false;
		}
	}

	public void EndScene(string sceneName)
	{
		fader.enabled = true;
		FadeToBlack();
		print ("bla");
		Application.LoadLevel(sceneName);

		if(fader.color.a >= 0.95f)
		{
			Application.LoadLevel(sceneName);
		}
	}
}
