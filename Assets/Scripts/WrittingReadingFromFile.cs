using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrittingReadingFromFile : MonoBehaviour {
	public Text[] playerNames;
	public Text[] playerScores;
	string[] names;
	int[] playerTimes;
	public Text newScoreBox;
	int newScore;
	string newName;

	public Animator camera;
	// Use this for initialization
	void Start () {
		names = new string[playerNames.Length];
		playerTimes = new int[playerScores.Length];
		for (int i = 0; i < playerScores.Length; i++) {
			playerTimes [i] = PlayerPrefs.GetInt ("playerTimes" + i);
			names [i] = PlayerPrefs.GetString ("names" + i);
		}
		newScoreBox.text = structureTime(LevelManager.time);
		WriteScores ();
	}
	public void ChangeStats(string namme){
		newName = namme;
		newScore = LevelManager.time;
		CheckHighScores ();
		LevelManager.time = 0;
	}
	void CheckHighScores(){
		for (int i = playerScores.Length - 1; i >= 0; i--) {
			if (playerTimes[i] > newScore) {
				if (i < playerScores.Length - 1) {
					playerTimes [i + 1] = playerTimes [i];
					names [i + 1] = names [i];
				}
				playerTimes [i] = newScore;
				names [i] = newName;
			}
		}
		WriteScores ();
		SaveScores ();
	}
	public void SaveScores(){
		for (int i = 0; i < playerScores.Length; i++) {
			PlayerPrefs.SetInt ("playerTimes" + i, playerTimes [i]);
			PlayerPrefs.SetString ("names" + i,names [i]);
		}
	}
	void WriteScores(){
		for(int i = 0; i< playerScores.Length; i++)
			playerScores[i].text = structureTime(playerTimes[i]);
		for(int i = 0; i< playerNames.Length; i++)
			playerNames[i].text =(i+1) + ". " +  names[i];
	}
	string structureTime(int timee){
		int time = timee;
		int minutes = time / 60;
		int seconds = (int)(time) % 60;
		string text;
		if(minutes > 0)
			text = " 0" + minutes + " : " + seconds;
		else
			if(seconds >=10)
				text = "00 : " + seconds;
			else
				text = "00 : 0" + seconds;
		return text;
	}
	// Update is called once per frame
	public void NextLevel(){
		camera.SetTrigger ("nextLevel");
	}
}
