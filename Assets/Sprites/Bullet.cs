using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Rigidbody2D rgb;
	public string targetTag;
	public float speed;
	private Vector2 direction;
	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void FixedUpdate(){
		rgb.velocity = direction * speed;
	}
	public void Initalize(Vector2 direction){
		this.direction = direction;
	}
	void Destroy(){
		Destroy (gameObject);
	}
	void  OnCollisionEnter2D(Collision2D other){
		Destroy (gameObject);
	}
	void  OnTriggerEnter2D(Collider2D other){
		
		if ((targetTag == other.tag) || (targetTag == "Enemy" && other.tag == "Boss")) {
			GetComponent<Collider2D> ().enabled = false;
			GetComponent<SpriteRenderer> ().enabled = false;
			Destroy (gameObject);
		}
	}
}
