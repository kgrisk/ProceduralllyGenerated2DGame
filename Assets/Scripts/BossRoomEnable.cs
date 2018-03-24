using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEnable : MonoBehaviour {
	public GameObject doors;
	public GameObject enemy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			doors.SetActive (true);
			enemy.SetActive (true);
		}
	}
}
