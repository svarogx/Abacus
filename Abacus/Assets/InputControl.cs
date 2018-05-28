using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler{

	private RectTransform myRect;

	private Vector2 minLimits;
	private Vector2 maxLimits;

	private bool touchDown = false;
	private int pointerID = -1;

	// Use this for initialization
	void Awake(){
		myRect = GetComponent<RectTransform> ();

	}

	void Start () {
		minLimits = new Vector2 (myRect.rect.width / 2, myRect.rect.height/2);
		maxLimits = new Vector2 (Screen.width - (myRect.rect.width / 2), Screen.height - (myRect.rect.height/2));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerDown(PointerEventData data){
		if (touchDown)
			return;
		touchDown = true;
		pointerID = data.pointerId;
	}

	public void OnDrag(PointerEventData data){
		if (!touchDown) 
			return;
		if (data.pointerId != pointerID)
			return;

		Vector3 vecttmp = new Vector3(data.position.x, transform.position.y, 0.0f);
		if (data.position.x < minLimits.x)
			vecttmp.x = minLimits.x;
//		if (data.position.y < minLimits.y)
//			vecttmp.y = minLimits.y;
		if (data.position.x > maxLimits.x)
			vecttmp.x = maxLimits.x;
//		if (data.position.y > maxLimits.y)
//			vecttmp.y = maxLimits.y;
		transform.position = vecttmp;       
	}

	public void OnPointerUp(PointerEventData data){
		if (!touchDown)
			return;
		touchDown = false;
		pointerID = -1;
	}

}
