using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppear : MonoBehaviour {

	public GameObject player;


	public void AllienEnabled(){
		player.GetComponent<SpriteRenderer> ().enabled = true;
	}
	public void AllienDisabled(){
		player.GetComponent<SpriteRenderer> ().enabled = true;
	}

	public void MovementScriptEnabled(){
		player.GetComponent<PlayerMovement>().enabled = true;
	}
	public void MovementScriptDisabled(){
		player.GetComponent<PlayerMovement>().enabled = false;
	}
}
