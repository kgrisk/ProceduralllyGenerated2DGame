using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTunnel : MonoBehaviour, IObjectsUse{
	#region IObjectsUse implementation
	public void Use ()
	{
		if (PlayerMovement.Instance.SpaceDoors) {
			UseSpaceDoors (false,1,0, "land");

		} else {
			UseSpaceDoors (true,0,1,"reset");
		}
	}
	#endregion

	private void UseSpaceDoors(bool useDoors, int gravity, int weight, string animTrigger){
		PlayerMovement.Instance.SpaceDoors = useDoors;	
		PlayerMovement.Instance.Rgb.gravityScale = gravity;
		PlayerMovement.Instance.Anim.SetLayerWeight(2,weight);
		PlayerMovement.Instance.Anim.SetTrigger (animTrigger);
	}
	public void OnTriggerExit2D(Collider2D other){

		if (other.tag == "Player") {
			UseSpaceDoors (false, 1,0,"land");
		}

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
