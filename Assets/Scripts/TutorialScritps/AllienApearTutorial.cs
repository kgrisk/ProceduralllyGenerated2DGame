using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllienApearTutorial : MonoBehaviour {
	public GameObject Alien;
	public PlayerMovement player;
	public void AllienEnabled(){
		Alien.SetActive (true);
	}
	public void AllienDisabled(){
		Alien.SetActive (false);
	}

	public void MovementScriptEnabled(){
		player.enabled = true;
	}
	public void MovementScriptDisabled(){
		player.enabled = false;
	}
	public void LoadingScene(){

		SceneManager.LoadScene (2);

	}


	// Update is called once per frame	}
}
