﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {
	public float waitingTime;
	Rigidbody2D rgb;
	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		//rgb.isKinematic = true;
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D other){
		
		if (other.gameObject.CompareTag("Player")) {
			StartCoroutine (IsFalling());
		}
	}


	IEnumerator IsFalling(){
		yield return new WaitForSeconds (waitingTime);
		rgb.isKinematic = false;
		GetComponent<Collider2D> ().isTrigger = true;
		Destroy (gameObject, 2);
	}
} 
