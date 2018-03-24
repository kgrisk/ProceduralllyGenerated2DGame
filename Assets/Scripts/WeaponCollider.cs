using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour {
	[SerializeField]
	private string targetTag;
	public string objectTag = "no";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == targetTag) {
			GetComponent<Collider2D> ().enabled = false;
			if (objectTag == "bullet") {
				GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
	}
}
