﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
	public Animator Anim{ get; set;}
	public Transform bulletPos;
	public float speed;
	protected bool facingRight = true;
	public GameObject bullet;
	[SerializeField]
	private EdgeCollider2D weaponCollider;
	public List<string> demageSource;
	public Vector3 startPos;

	public abstract bool IsDead{ get; }
	public abstract void Dead ();

	[SerializeField]
	protected PlayerStats healthStat;

	public EdgeCollider2D WeaponCollider {
		get {
			return weaponCollider;
		}
	}

	public bool TakingDemage{ get; set;}
	public bool Attack{ get; set;}

	// Use this for initialization
	public virtual void Start () {
		startPos = transform.position;
		Anim = GetComponent<Animator> ();
		healthStat.GiveValues ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void MeleeAttack(){
		weaponCollider.enabled = !weaponCollider.enabled;
	}
	public virtual void OnTriggerEnter2D(Collider2D other){
		if (demageSource.Contains(other.tag)) {
			StartCoroutine (TakeDemage ());
		}
	}

	public  virtual void DirectionChange(){
		facingRight = !facingRight;
		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	public virtual void ShootBullet(int value){
		if (facingRight) {
			GameObject obj = (GameObject)Instantiate (bullet, bulletPos.position, bulletPos.rotation);
			obj.GetComponent<Bullet> ().Initalize (Vector2.right);
		} else {

			GameObject obj =(GameObject) Instantiate (bullet, bulletPos.position, bulletPos.rotation);
			obj.GetComponent<Bullet> ().Initalize (Vector2.left);
		}
	}
	public abstract IEnumerator TakeDemage ();
}
