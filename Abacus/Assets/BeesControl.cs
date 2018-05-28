using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeesControl : MonoBehaviour {

	public GameObject beePrefab;
	public int amountOfBee = 1;

	private List<GameObject> bees;

	// Use this for initialization
	void Start () {
		if (amountOfBee < 1)
			amountOfBee = 1;

		Vector3 edgeScreen = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0.0f));
		Vector3 edgeZero =  Camera.main.ScreenToWorldPoint (new Vector3(0.0f, 0.0f, 0.0f));
		Vector3 position = Vector3.zero;

		GameObject obj = null;
		for (int i = 0; i < amountOfBee; i++) {
			position.x = Random.Range (edgeZero.x, edgeScreen.x);
			position.y = Random.Range (edgeZero.y, edgeScreen.x);
			obj = Instantiate (beePrefab, position, Quaternion.identity) as GameObject;
			obj.transform.SetParent (this.transform);
		}
	}
	
}
