using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationString : MonoBehaviour {
	private static string generationWord;

	public static string GenerationWord {
		get {
			if (generationWord == null) {
				generationWord = System.DateTime.Now.Millisecond.ToString ();
			}
			return generationWord;
		}
		set{ 
			generationWord = value;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
