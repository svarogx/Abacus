using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	public int columnID;
	private bool veritas = true;

	private GameObject gameControl;

	void Start(){
		gameControl = GameObject.FindGameObjectWithTag ("GameController");
		if (veritas) {
			gameControl.GetComponent<GameControl> ().AddPoint (columnID);
			veritas = false;
		}
	}

	void OnTriggerEnter2D(Collider2D hit){
		if (hit.tag == "Finish" && !veritas) {
			gameControl.GetComponent<GameControl> ().SubPoint (columnID);
			Invoke ("Destruoid", 0.2f);
			veritas = true;
		}
	}



	void Destruoid(){
		Destroy (gameObject);
	}
}
