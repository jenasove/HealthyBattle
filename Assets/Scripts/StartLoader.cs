using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartLoader : MonoBehaviour 
{
	public GameObject canvas;

	public void StartLoad()
	{
		canvas.SetActive (false);
		SceneManager.LoadScene ("Scene");
	}

	public void Exit ()
	{
		Application.Quit ();
	}
}
