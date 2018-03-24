using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingBox : MonoBehaviour {
	public Animator anim;
	public GameObject canvas;
	public Text textBoxText;
	public PlayerMovement player;
	bool startTutorialFinished = false;
	bool finishedWalking = false;
	bool usedLift = false;
	bool finishedEnemy = false;
	bool StartLevel = false;

	public GameObject walking;
	public GameObject spaceTunnel;
	public GameObject EnemyFight;

	public GameObject switchOn;
	public GameObject enemy;
	int first = 0;
	string textStart = "Hey buddy! " +
		"Congratulations for being selected as one of " +
		"those unlucky fellows participating in this " +
		"mess! Now for a starter let me introduce you to " +
		"some controls so that you would not get " +
		"yourself killed  too quickly!!";
	string textWalking = "In Order to Walk you have to use A or D buttons " +
		"and to jump you have to press SPACE";
	string textSpaceTunnel = "To use SpaceTunnel usually you first have to find its switch " +
		"and turn it on with F. Afterwards, you can use it pressing the same button ";
	string textCombat = "You can fight enemy with Q or E. Don't forget to take collectables items, because they " +
		"might be usefull";
	string textTeleport = "Come on the teleport and press F then you are ready to start the game";
	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (startTutorialFinished + "tutorial");
		if (startTutorialFinished) {
			if (first == 0) {
				player.enabled = true;
				anim.SetTrigger ("walking");
				first++;
			}
			if (Input.GetKeyDown (KeyCode.Space) && !finishedWalking) {
				canvas.SetActive (false);
				walking.SetActive (false);
				finishedWalking = true;
				anim.SetTrigger ("lift");
			} else if (switchOn.activeSelf && !usedLift) {
				canvas.SetActive (false);
				spaceTunnel.SetActive (false);
				usedLift = true;
				anim.SetTrigger ("battle");
				enemy.SetActive (true);
			} else if (enemy != null && enemy.activeSelf && enemy.GetComponent<Enemy> ().IsDead && first <2) {
				first++;
				canvas.SetActive (false);
				EnemyFight.SetActive (false);
				anim.SetTrigger ("nextLevel");
			}

		}
	}
	public IEnumerator TextInstructionsIterator(string Text){
		canvas.SetActive(true);
		textBoxText.text = "";

		yield return new WaitForSeconds (1f);
		foreach(char latter in Text){
			textBoxText.text += latter;

			yield return new WaitForSeconds (0.05f);
		}
		yield return new WaitForSeconds (1f);



	}
	public IEnumerator TextITalkingterator(string Text){
		canvas.SetActive(true);
		textBoxText.text = "";
		yield return new WaitForSeconds (1f);
		foreach(char latter in Text){
			textBoxText.text += latter;
			if (latter == '!' || latter == '.') {
				
				yield return new WaitForSeconds (1f);
					textBoxText.text = "";
			
			}

			yield return new WaitForSeconds (0.05f);
		}
		yield return new WaitForSeconds (1f);
		canvas.SetActive (false);
		startTutorialFinished = true;
		StartCoroutine(TextInstructionsIterator(textWalking));
		
	}
	public void StartIntroductionCoroutine(){
		StopAllCoroutines ();
		StartCoroutine (TextITalkingterator (textStart));

	}

	public void StartLiftCoroutine(){
		StopAllCoroutines ();
		StartCoroutine(TextInstructionsIterator(textSpaceTunnel));

	}

	public void StartBattleCoroutine(){
		StopAllCoroutines ();
		StartCoroutine(TextInstructionsIterator(textCombat));

	}
	public void StartTeleportationCoroutine(){
		StopAllCoroutines ();
		StartCoroutine(TextInstructionsIterator(textTeleport));

	}
}
