using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {
	[SerializeField]
	private Collider2D other;
	// Use this for initialization
	void Start () {
		other = GameObject.Find ("Player").GetComponent<BoxCollider2D> ();
		Physics2D.IgnoreCollision (GetComponent<BoxCollider2D>(), other, true);
	}

}
