using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	public GameObject player;

	public GameObject ship;
	bool taken = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AfterTaking(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<SpriteRenderer> ().enabled = false;
		}

	public void stopAfterTaking(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ().enabled = false;
	}
}
