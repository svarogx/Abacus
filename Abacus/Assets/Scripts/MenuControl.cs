using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey(KeyCode.Escape))
				ShutGame ();
		}
	}

	public void LetsPlay(){
		SceneManager.LoadScene ("Main");
	}

	public void ShutGame(){
		Application.Quit();
	}
}
