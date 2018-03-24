using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDoorsSwitch : MonoBehaviour, IObjectsUse{
	#region IObjectsUse implementation
	[SerializeField]
	private GameObject lockedDoors;
	[SerializeField]
	private GameObject unlockedDoors;

	[SerializeField]
	private GameObject OffSwitch;

	[SerializeField]
	private GameObject OnSwitch;


	public void Use ()
	{
		if (!OnSwitch.activeSelf) {
			OffSwitch.SetActive (false);
			OnSwitch.SetActive (true);
			lockedDoors.SetActive (false);
			unlockedDoors.GetComponent<BoxCollider2D> ().enabled = true;
		}
		else{
			OffSwitch.SetActive (true);
			OnSwitch.SetActive (false);
			lockedDoors.SetActive (true);
			unlockedDoors.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
