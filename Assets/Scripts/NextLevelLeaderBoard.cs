using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelLeaderBoard : MonoBehaviour {

	public void NextLevel(){
		SceneManager.LoadScene (0);
	}
}
