using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour {

	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey(KeyCode.Escape))
				MenuPlay ();
		}
	}

	public void MenuPlay(){
		SceneManager.LoadScene ("Menu");
	}

	public void ShutGame(){
		Application.Quit();
	}
}
