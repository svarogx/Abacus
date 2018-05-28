using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {

	[Range(1.0f, 4.0f)]
	public float timeToLive = 1.0f;
	[Range(10.0f, 120.0f)]
	public float framePerSecond = 30.0f;

	private SpriteRenderer spriteRender;
	private Sprite[] sprites;

	// Use this for initialization
	void Awake(){
		spriteRender = GetComponent<SpriteRenderer> ();
		sprites = Resources.LoadAll<Sprite> ("ball");
	}

	void OnEnable(){
		Color tmpColor = Color.white;
		tmpColor.a = 1.0f;
		spriteRender.sprite = sprites[Random.Range(0, sprites.Length)];
		spriteRender.color = tmpColor;
		StartCoroutine ("BallFadeOut");
	}

	IEnumerator BallFadeOut(){
		yield return new WaitForSeconds (timeToLive);
		Color tmpColor = Color.white;
		tmpColor.a = 1.0f;
		float deltaTime = 1.0f / framePerSecond;
		float deltaAlpha = tmpColor.a / (timeToLive / deltaTime); 
		while (tmpColor.a > 0.0f) {
			tmpColor.a -= deltaAlpha;
			spriteRender.color = tmpColor;
			yield return new WaitForSeconds (deltaTime);
		}
		gameObject.SetActive (false);
	}

}
