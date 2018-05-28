using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour {

	private bool[] touchCreate;
	private GameObject[] touchObject;

	void Start(){
		touchCreate = new bool[10];
		for (int i = 0; i < 10; i++) {
			touchCreate [i] = false;
		}
		touchObject = new GameObject[10];
	}

	void Update () {
		RaycastHit2D hit;
		Vector2 ray;
		GameObject casual;
		int indx;
		foreach (Touch touch in Input.touches) {
			indx = (int)touch.fingerId;
			ray = Camera.main.ScreenToWorldPoint (touch.position);
			hit = Physics2D.Raycast (ray, Vector2.zero);
			switch (touch.phase) {
			case TouchPhase.Began:
				if (hit.collider) {
					casual = hit.collider.gameObject;
					switch (hit.collider.tag) {
					case "Player":
						if (indx > 9)
							break;
						touchObject [indx] = casual;
						touchCreate [indx] = true;
						touchObject [indx].transform.position = new Vector3 (hit.transform.position.x, ray.y, hit.transform.position.z);
						break;
					case "Respawn":
						if (indx >= 10)
							break;
						if (!touchCreate[indx]) 
							casual.gameObject.GetComponent<SpawnBall> ().CreateBall ();
						break;
					}
				}
				break;
			case TouchPhase.Canceled:
				if (indx > 9)
					break;
				if (touchCreate [indx]) {
					touchCreate [indx] = false;
					touchObject [indx] = null;
				}
				break;
			case TouchPhase.Ended:
				if (indx > 9)
					break;
				if (touchCreate [indx]) {
					touchCreate [indx] = false;
					touchObject [indx] = null;
				}
				break;
			case TouchPhase.Moved:
				if (indx > 9)
					break;
				if (touchObject [indx]) {
					touchObject [indx].transform.position = new Vector3 (touchObject [indx].transform.position.x, ray.y, touchObject [indx].transform.position.z);
				} else {
					touchCreate [indx] = false;
					touchObject [indx] = null;
				}
				break;
			case TouchPhase.Stationary:
				if (indx > 9)
					break;
				if (touchObject [indx]) {
					touchObject [indx].transform.position = new Vector3 (touchObject [indx].transform.position.x, ray.y, touchObject [indx].transform.position.z);
				} else {
					touchCreate [indx] = false;
					touchObject [indx] = null;
				}
				break;
			}
		}
	}
}
