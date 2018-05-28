using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingControl : MonoBehaviour {

	private float stepTime = 0.02f;
	private bool isLanded = false;

	IEnumerator FlightControl(){
		int delta = -1;
		Vector3 scale = Vector3.zero;
		while (true) {
			if (!isLanded) {
				scale = transform.localScale;
				if (delta > 0)
					scale.x += 0.1f;
				else
					scale.x -= 0.1f;
				transform.localScale = scale;
				if (scale.x >= 1.0f)
					delta = -1;
				if (scale.x <= 0.5f)
					delta = 1;
			} else {
				scale = transform.localScale;
				scale.x = 1.0f;
				transform.localScale = scale;
				delta = -1;
			}
			yield return new WaitForSeconds (stepTime);
		}
	}

	public void WingInit(float deltaStep){
		stepTime = deltaStep;
		StartCoroutine ("FlightControl");
	}

	public void LetItFly(){
		isLanded = false;
	}

	public void JustLanded(){
		isLanded = true;
	}
}
