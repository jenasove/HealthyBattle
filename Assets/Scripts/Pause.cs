using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour 
{
	private bool paused;
	public GameObject hud;
	//private GameObject winPanel;

	void Start () 
	{
		paused = false;
		//hud = GameObject.Find ("HUD");
		//winPanel = GameObject.Find ("WinPanel");
	}

	public void PauseScenes ()
	{
		paused = !paused;
	
		if (paused) {
			Time.timeScale = 0;
		} else if(!paused){
			Time.timeScale = 1;
		}
	}

	public void WinExit ()
	{
		Application.Quit ();
	}

	public void WinHome ()
	{
		hud.SetActive (false);
		//winPanel.SetActive (false);
		SceneManager.LoadScene (0, LoadSceneMode.Single);
	}
}
