using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCommands : MonoBehaviour {

	public void sendSeedNextLevel(string seed){
		
		GenerationString.GenerationWord = seed;
		SceneManager.LoadScene (1);
	}
	public void NextLevel(){
		SceneManager.LoadScene (1);
	}
}
