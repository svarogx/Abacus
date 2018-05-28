using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public Text[] scoreText = new Text[6];
	public int[] score = new int[6];
	public string[] alias = new string[6]; 
	public SpawnBall[] ballManage = new SpawnBall[6]; 

	public GameObject tutorialLabel1;
	public GameObject tutorialFig1;
	public GameObject tutorialLabel2;

	private int tutorial = 0;
	private SoundControl soundControl;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < score.Length; i++) {
			score [i] = 0;
		}
		soundControl = GetComponent<SoundControl> ();

		tutorialLabel1.SetActive (true);
		tutorialFig1.SetActive (true);
		tutorial = 1;
		tutorialLabel2.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < score.Length; i++) {
			scoreText [i].text = score [i].ToString () + " " + alias [i];
/*			if (score [i] > 0)
				scoreText [i].text = score [i].ToString () + " " + alias [i];
			else
				scoreText [i].text = "";
*/		}
	}

	public void AddPoint(int columnID){
		if (columnID > score.Length)
			return;
		score [columnID - 1] += 1;
		if (score [columnID - 1] >= 9) {
			score [columnID - 1] = 9;
			ballManage [columnID - 1].veritas = false;
		}
		soundControl.PlayNumber (score[columnID - 1] * (int)Mathf.Pow (10, columnID - 1));
		if (tutorial == 1) {
			tutorialLabel1.GetComponent<Animator> ().SetTrigger ("bye");
			tutorialFig1.GetComponent<Animator> ().SetTrigger ("bye");
			tutorialLabel2.SetActive(true);
			tutorial = 2;
		}
	}

	public void SubPoint(int columnID){
		if (columnID > score.Length)
			return;
		score [columnID - 1] -= 1;
		ballManage [columnID - 1].veritas = true;
		if (score [columnID - 1] <= 0)
			score [columnID - 1] = 0;
//		int tmp = score[columnID - 1] * (int)Mathf.Pow (10, columnID - 1);
		soundControl.PlayNumber (score[columnID - 1] * (int)Mathf.Pow (10, columnID - 1));
		if (tutorial == 2) {
			tutorialLabel2.GetComponent<Animator> ().SetTrigger ("bye");
			tutorial = -1;
		}
	}

}
