using UnityEngine;
using System.Collections;

public class SpawnBall : MonoBehaviour {

	public GameObject ballPrefab;
	public int columnID;
	public bool veritas = true; 

	private GameObject gameControl;

	public void CreateBall(){
		if (veritas) {
			GameObject newBall =  Instantiate (ballPrefab, transform.position, Quaternion.identity) as GameObject;
			newBall.GetComponent<BallControl> ().columnID = columnID;
		}

	}
}
