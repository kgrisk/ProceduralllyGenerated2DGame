using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPositionApplyinh : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AnimationFinished(){
		transform.parent.position = transform.position;
		transform.localPosition = Vector3.zero;
	}
}
