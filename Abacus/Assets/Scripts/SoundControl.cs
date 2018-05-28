using UnityEngine;
using System.Collections;

public class SoundControl : MonoBehaviour {

	public AudioClip[] units;
	public AudioClip[] tens;
	public AudioClip[] hundreds;
	public AudioClip[] thousands;
	public AudioClip[] millions;

	private AudioSource soundManager;
	private AudioClip soundQueue;
	private bool isQueue = false;

//	public int proof = 0;
	// Use this for initialization
	void Start () {
		soundManager = GetComponent<AudioSource> ();
//		PlayNumber (proof);
	}
	
	// Update is called once per frame
	void Update () {
		if (!soundManager.isPlaying && isQueue) {
			isQueue = false;
			soundManager.Stop ();
			soundManager.loop = false;
			soundManager.clip = soundQueue;
			soundManager.Play ();
		}
	}

	public void PlayNumber(int number){
		if (number >= 1000000) {
			if (number >= 1000000 && number < 2000000) {
				isQueue = false;
				soundManager.Stop ();
				soundManager.loop = false;
				soundManager.clip = millions [0];
				soundManager.Play ();
			} else {
				isQueue = true;
				soundQueue = millions [1];
				PlayBasicNumber(number/1000000);
			}
		}
		if (number >= 1000 && number < 1000000) {
			if (number >= 1000 && number < 2000) {
				isQueue = false;
				soundManager.Stop ();
				soundManager.loop = false;
				soundManager.clip = thousands[0];
				soundManager.Play ();
			} else {
				isQueue = true;
				soundQueue = thousands[0];
				PlayBasicNumber(number/1000);
			}
		}
		if (number < 1000)
			PlayBasicNumber (number);
	}

	void PlayBasicNumber(int number){
		if (number <= 0)
			return;
		if (number < 10)
			PlayUnits (number);
		if (number >= 10 && number < 100) {
			number /= 10;
			PlayTens (number);
		}
		if (number >= 100 && number < 1000) {
			number /= 100;
			PlayHundreds (number);
		}
	}

	void PlayUnits(int number){
		soundManager.Stop ();
		soundManager.loop = false;
		soundManager.clip = units[number - 1];
		soundManager.Play ();
	}

	void PlayTens(int number){
		soundManager.Stop ();
		soundManager.loop = false;
		soundManager.clip = tens[number - 1];
		soundManager.Play ();
	}

	void PlayHundreds(int number){
		soundManager.Stop ();
		soundManager.loop = false;
		soundManager.clip = hundreds[number - 1];
		soundManager.Play ();
	}
}
