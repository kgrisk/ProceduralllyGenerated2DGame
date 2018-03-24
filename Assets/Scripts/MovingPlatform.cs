using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	[SerializeField]
	private Transform wayPointA;
	[SerializeField]
	private Transform wayPointB;
	private Vector3 nextWayPoint;
	[SerializeField]
	private float speed;

	[SerializeField]
	private Transform child;
	// Use this for initialization
	void Start () {
		nextWayPoint = wayPointA.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (child.localPosition == wayPointB.localPosition) {
			nextWayPoint = wayPointA.localPosition;
		} else if (child.localPosition == wayPointA.localPosition) {
			nextWayPoint = wayPointB.localPosition;
		}
		Movement ();
	}
	private void Movement(){
		child.localPosition = Vector3.MoveTowards (child.localPosition, nextWayPoint,speed * Time.deltaTime);
	}

	private void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.tag == "Player") {
			other.gameObject.layer = 12;
			other.transform.SetParent(child);
		}
	}
	private void OnCollisionExit2D(Collision2D other){


			other.transform.SetParent(null);

	}
}
