using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	private GameObject coinPrfb;

	[SerializeField]
	private static int amountOfCoin;
	[SerializeField]
	private Text coinText;

	[SerializeField]
	private Text TimeText;

	private static LevelManager instance;
	public static int time;
	private int minutes;
	private int seconds;
	public  float Time {
		get {
			return time;
		}
	}
	public void StartCounting(){
		StartCoroutine (CountSeconds ());
	}
	public void StopCounting(){
		StopCoroutine (CountSeconds());
	}
	public IEnumerator CountSeconds(){
		while (true) {
			time++;
			structureTime ();
			yield return new WaitForSeconds (1);
		}
	}
	void structureTime(){
		minutes = time / 60;
		seconds = (int)(time) % 60;
		if(minutes > 0)
			TimeText.text = "Time : 0" + minutes + " : " + seconds;
		else
			TimeText.text = "Time : " + seconds;
	}
	public  int AmountOfCoin {
		get {
			return amountOfCoin;
		}
		set {
			coinText.text = "Cash : " + value.ToString ();
			amountOfCoin = value;
		}
	}

	public GameObject CoinPrfb {
		get {
			return coinPrfb;
		}
	}

	public static LevelManager Instance{ 
		get{
			if (instance == null) {
				instance = FindObjectOfType<LevelManager> ();	
			}
			return instance;

		}}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
