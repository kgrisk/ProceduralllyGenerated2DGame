using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateObject : MonoBehaviour {

	[SerializeField]
	private GameObject obj;
	[SerializeField]
	private GameObject instanciate;
	// Use this for initialization

	[SerializeField]
	private float creationTime;
	float timeTicking;
	void Start () {
		timeTicking = creationTime- Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (obj == null) {
			
			timeTicking += Time.deltaTime;
			if (timeTicking >= creationTime) {
				obj = Instantiate (instanciate,gameObject.transform);
				timeTicking = 0;
			}
		}
	}
}
