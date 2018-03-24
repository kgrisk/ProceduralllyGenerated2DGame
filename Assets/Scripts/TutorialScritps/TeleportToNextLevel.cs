using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToNextLevel : MonoBehaviour {
	public bool entered = false;
	public Animator anim;
	public static TeleportToNextLevel instance;

	// Use this for initialization
	Vector3 focusPosition;
	bool smoth = false;

	float smoothLookVelocityX = 1f;
	void Start(){
		instance = GameObject.FindObjectOfType<TeleportToNextLevel> ();
	}
	void Update () {
		Debug.Log (Input.GetKeyDown (KeyCode.F));
		if (Input.GetKeyDown (KeyCode.F) && entered) {
			smoth = true;
			//PlayerMovement.Instance.GetComponent<PlayerMovement> ().enabled = false;
		}
	}
	void LateUpdate(){
		
		if (smoth) {
			
			if (Mathf.Abs(PlayerMovement.Instance.transform.position.x - anim.transform.position.x) > 0.05f) {
				anim.transform.Translate (new Vector3((PlayerMovement.Instance.transform.position.x - anim.transform.position.x),0,0).normalized * Time.deltaTime * 10);
			}else {
				anim.SetTrigger ("startTeleportation");
			}
		} 
	}
	
	// Update is called once per frame
	public void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player")
		entered = true;
	}
	public void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player")
		entered = false;
	}


}
