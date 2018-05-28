using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TriggerControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public Color pressed;
	public Color released;

	public LayerMask impactLayer;


	private Image imageHandler;

	private bool isPressed = false;

	void Awake () {
		imageHandler = GetComponent<Image> ();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown(PointerEventData data){
		if (isPressed)
			return;
		isPressed = true;
		imageHandler.color = pressed;
		Vector3 initVect = Camera.main.ScreenToWorldPoint (data.position);
		initVect.z = -10.0f;
		Vector3 endVect = initVect;
		endVect.z = 10.0f;
		RaycastHit coverHit;
		if(Physics.Linecast(initVect, endVect, out coverHit, impactLayer))
			Debug.Log("YES - " + coverHit.transform.name);
		else
			Debug.Log("Nah");
//		Debug.Log (initVect + " - " + endVect);
	}

	public void OnPointerUp(PointerEventData data){
		isPressed = false;
		imageHandler.color = released;
	}

}