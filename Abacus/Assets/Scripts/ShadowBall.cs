using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowBall : MonoBehaviour {

	public float deltaStep = 0.02f;

	private Vector3 newPosition;

	private SpriteRenderer beeRender;
	private WingControl wingControl;

	private Vector3 edgeZero;
	private Vector3 edgeScreen;

	private int direction = 0;
	private bool isStop = false;

	void Awake(){
		beeRender = GetComponent<SpriteRenderer> ();
		wingControl = GetComponentInChildren<WingControl>();
	}

	void Start () {
		newPosition = transform.position;
		wingControl.WingInit (deltaStep * 0.01f);
		StartCoroutine ("ScreenVerify");
		StartCoroutine ("Direction");
		StartCoroutine ("Movement");
	}

	IEnumerator ScreenVerify(){
		Vector3 tmpVector = Vector3.zero;
		Vector3 lastVector = Vector3.zero;
		edgeZero = tmpVector;
		edgeScreen = tmpVector;
		float delta = 0.0f;
		while (true) {
			tmpVector = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0.0f));
			if (tmpVector.x != lastVector.x) {
				Debug.Log ("Changed! - " + tmpVector + " - " + lastVector);
				edgeScreen = tmpVector;
				edgeZero = Camera.main.ScreenToWorldPoint (new Vector3 (0.0f, 0.0f, 0.0f));
				lastVector = edgeScreen;
				delta = beeRender.bounds.extents.x > beeRender.bounds.extents.y ? beeRender.bounds.extents.x : beeRender.bounds.extents.y;
				edgeScreen.x -= delta; 
				edgeScreen.y -= delta;
				edgeZero.x += delta;
				edgeZero.y += delta;
			}
			yield return new WaitForSeconds (0.5f);
		}
	}

	IEnumerator Direction(){
		yield return new WaitForSeconds (0.1f);
		direction = 0;
		GameObject obj = null;
		while (true) {
			isStop = true;
			wingControl.JustLanded();
			yield return new WaitForSeconds (1.0f);
			obj = StageControl.sharedInstance.GetPooledObject ("Ball");
			if (obj != null) {
				obj.transform.position = this.transform.position;
				obj.SetActive (true);
			}
			isStop = false;
			wingControl.LetItFly();
			direction = Random.Range (0, 8);
			yield return new WaitForSeconds (Random.Range(2.0f, 4.0f));
		}
	}

	IEnumerator Movement(){
		yield return new WaitForSeconds (0.1f);
		float deltaX = 0.0f; 
		float deltaY = 0.0f;
		while (true) {
			if (!isStop) {
				if (transform.position.x >= edgeScreen.x) {
					direction = direction == 1 ? 3 : direction;
					direction = direction == 0 ? 4 : direction;
					direction = direction == 7 ? 5 : direction;
				}
				if (transform.position.x <= edgeZero.x) {
					direction = direction == 3 ? 1 : direction;
					direction = direction == 4 ? 0 : direction;
					direction = direction == 5 ? 7 : direction;
				}
				if (transform.position.y >= edgeScreen.y) {
					direction = direction == 1 ? 7 : direction;
					direction = direction == 2 ? 6 : direction;
					direction = direction == 3 ? 5 : direction;
				}
				if (transform.position.y <= edgeZero.y) {
					direction = direction == 7 ? 1 : direction;
					direction = direction == 6 ? 2 : direction;
					direction = direction == 5 ? 3 : direction;
				}
				switch (direction) {
				case 0:
					deltaX = 0.1f;
					deltaY = 0.0f;
					this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 270.0f);
					break;
				case 1:
					deltaX = 0.1f;
					deltaY = 0.1f;
					this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 315.0f);
					break;
				case 2:
					deltaX = 0.0f;
					deltaY = 0.1f;
					this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
					break;
				case 3:
					deltaX = -0.1f;
					deltaY = 0.1f;
					this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 45.0f);
					break;
				case 4:
					deltaX = -0.1f;
					deltaY = 0.0f;
					this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
					break;
				case 5:
					deltaX = -0.1f;
					deltaY = -0.1f;
					this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 135.0f);
					break;
				case 6:
					deltaX = 0.0f;
					deltaY = -0.1f;
					this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
					break;
				case 7:
					deltaX = 0.1f;
					deltaY = -0.1f;
					this.transform.eulerAngles = new Vector3(0.0f, 0.0f, 225.0f);
					break;
				}
				newPosition = new Vector3 (transform.position.x + deltaX, transform.position.y + deltaY, 0.0f);
				transform.position = newPosition;
			}
			yield return new WaitForSeconds (deltaStep);
		}
	}

}
