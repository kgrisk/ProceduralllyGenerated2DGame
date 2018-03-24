using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeCounting : MonoBehaviour {

	// Use this for initialization
	public void StartCounting(){
		GetComponentInChildren<LevelManager> ().StartCounting ();
	}

	public void StopCounting(){
		GetComponentInChildren<LevelManager> ().StopCounting ();
	}

	public void LoadScene(){
		SceneManager.LoadScene (3);
	}
}
