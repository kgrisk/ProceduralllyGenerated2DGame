using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomGeneratorius : MonoBehaviour {
	public GameObject[] roomTeplates;
	public GameObject[] roomTeplatesLeft;
	public GameObject[] roomTeplatesRight;
	public GameObject[] roomTeplatesEndings;
	public GameObject[] roomTeplatesSideEndings;
	public GameObject[] roomTeplatesStarts;
	GameObject[,] generatedRooms = new GameObject[4,8];

	string random;
	System.Random rand;
	bool ending = true;
	public Text txt;

	GameObject[] ThreeExitRooms;
	GameObject[] TwoExitHighRooms = null;
	GameObject[] TwoExitNormalRooms;
	GameObject[] TwoExitCorridorRooms;


	// Use this for initialization
	int[,] rooms = new int[4,8];
	public GameObject obj;
	Transform childPosition;
	void Awake () {
		random = GenerationString.GenerationWord;
		rand = new System.Random (random.GetHashCode());
		RoomTemplateGenerator ();
		LevelDesignGenerator ();
		obj.SetActive (true);


	}
	public void DesignSelector (GameObject[] designs){
		int numberOfDesigns, designsLeft;
		numberOfDesigns = designs[0].transform.childCount;
		designsLeft = numberOfDesigns;
		int[] designNumbers = new int[designsLeft];
		for (int a = 0; a < designNumbers.Length; a++) {
			designNumbers [a] = a;
		}
			for(int i =0 ; i < designs.Length;i++){
			if (designsLeft > 0) {
				int number = rand.Next (0, designsLeft);
				designs [i].transform.GetChild (designNumbers[number]).gameObject.SetActive (true);
				//rooms.transform.position = new Vector3 (0,0,0);
				if (number != designsLeft - 1) {
					designNumbers [i] = designNumbers [designsLeft - 1];
				}
				designsLeft--;
			} else {
				int number = rand.Next (0, numberOfDesigns);
				designs [i].transform.GetChild (number).gameObject.SetActive (true);
			}
				
		}
	}
	public void LevelDesignGenerator(){
		ThreeExitRooms = GameObject.FindGameObjectsWithTag ("ThreeExitsRoom");
		TwoExitHighRooms = GameObject.FindGameObjectsWithTag ("TwoExitHighRoom");
		TwoExitNormalRooms = GameObject.FindGameObjectsWithTag ("TwoExitNormalRoom");
		TwoExitCorridorRooms = GameObject.FindGameObjectsWithTag ("TwoExitCorridorRoom");

		if(ThreeExitRooms.Length>0){
			DesignSelector (ThreeExitRooms);
		}
		if (TwoExitHighRooms.Length>0) {
			DesignSelector (TwoExitHighRooms);
		}
		if (TwoExitNormalRooms.Length>0) {
			DesignSelector (TwoExitNormalRooms);
		}
		if (TwoExitCorridorRooms.Length>0) {
			DesignSelector (TwoExitCorridorRooms);
		}
	}
	public void RoomTemplateGenerator(){
		
		string lastRoom = "";
		string exitTag="";
		int startroom = rand.Next (1, 7);
		rooms [0,startroom] = 3;
		int solutionDir = rand.Next (1, 5);
		int row = 0;

		int rowBefore = row;
		int currentRoom = startroom;
		int roomBefore = currentRoom;
		if (currentRoom == 1) {
			solutionDir = 1;
			lastRoom = "ToRight";
			generatedRooms[0,startroom] = Instantiate (roomTeplatesStarts [1], roomTeplatesStarts [0].transform.position, roomTeplatesStarts [0].transform.rotation);
		} else if (currentRoom == 6) {
			solutionDir = 3;
			lastRoom = "ToLeft";
			generatedRooms[0,startroom] = Instantiate (roomTeplatesStarts [0], roomTeplatesStarts [0].transform.position, roomTeplatesStarts [0].transform.rotation);

		} else if (solutionDir == 1 || solutionDir == 2) {
			lastRoom = "ToRight";
			generatedRooms[0,startroom] = Instantiate (roomTeplatesStarts [1], roomTeplatesStarts [0].transform.position, roomTeplatesStarts [0].transform.rotation);

		}else if (solutionDir == 3 || solutionDir == 4) {
			lastRoom = "ToLeft";
			generatedRooms[0,startroom] = Instantiate (roomTeplatesStarts [0], roomTeplatesStarts [0].transform.position, roomTeplatesStarts [0].transform.rotation);

		}


		while (ending) {

			if ((solutionDir == 1 || solutionDir == 2) && currentRoom != 6 && rooms [row, currentRoom +1] == 0 && lastRoom == "ToRight") {
				rowBefore = row;
				roomBefore = currentRoom;
				if (rooms [row, currentRoom] == 5) {
					rowBefore--;
					exitTag = "RightUpExit";
					if (currentRoom != 0) {
						rooms [row, currentRoom-1] = 6;
					}
				} else {

					exitTag = "RightExit";

				}currentRoom++;
				rooms [row, currentRoom] = 1;

				foreach(Transform child in generatedRooms [rowBefore, roomBefore].transform){
					if(child.CompareTag(exitTag)){

						childPosition = child;
						generatedRooms [row, currentRoom] = Instantiate (ReturnRandomRoom(roomTeplatesRight), childPosition);
						foreach(Transform exitChild in generatedRooms [row, currentRoom].transform){
							if(exitChild.CompareTag("LeftExit")){
								Vector3 difference = generatedRooms [row, currentRoom].transform.position - exitChild.position;

								generatedRooms [row, currentRoom].transform.position = generatedRooms [row, currentRoom].transform.position + difference;

							}
						}
					}
				}
				lastRoom = "ToRight";
				solutionDir = rand.Next (1, 6);
				continue;

			}else if ((solutionDir == 3 || solutionDir == 4) && currentRoom != 1 && rooms [row, currentRoom - 1] == 0 && lastRoom == "ToLeft") {
				rowBefore = row;
				roomBefore = currentRoom;
				if (rooms [row, currentRoom] == 5) {
					rowBefore--;
					exitTag = "LeftUpExit";
					if (currentRoom != 5) {
						rooms [row, currentRoom+1] = 6;
					}
				} else {
					exitTag = "LeftExit";

				}currentRoom--;
				rooms [row, currentRoom] = 1;


				foreach(Transform child in generatedRooms [rowBefore, roomBefore].transform){
					if(child.CompareTag(exitTag)){

						childPosition = child;
						generatedRooms [row, currentRoom] = Instantiate (ReturnRandomRoom(roomTeplatesLeft), childPosition);
						foreach(Transform exitChild in generatedRooms [row, currentRoom].transform){
							if(exitChild.CompareTag("RightExit")){
								Vector3 difference = generatedRooms [row, currentRoom].transform.position - exitChild.position;

								generatedRooms [row, currentRoom].transform.position = generatedRooms [row, currentRoom].transform.position + difference;

							}
						}
					}
				}
				lastRoom = "ToLeft";
				solutionDir = rand.Next (1, 6);
				continue;

			}else if (solutionDir == 5) {
				if (row != 3) {
					roomBefore = currentRoom;
					rowBefore = row;
					exitTag="";
					if (rooms [row, currentRoom] == 5 && lastRoom == "ToLeft") {
						/*currentRoom++;
						rowBefore--;
						lastRoom = "ToRight";
						exitTag = "LeftUpExit";
						*/
						solutionDir = 1;
						continue;
					}
					else if (rooms [row, currentRoom] == 5 && lastRoom == "ToRight") {
						/*
						currentRoom--;
						rowBefore--;
						lastRoom = "ToLeft";
						exitTag = "RightUpExit";
						*/
						solutionDir = 3;
						continue;
					}else if (rooms [row, currentRoom] != 5 && lastRoom == "ToLeft") {
						roomBefore++;

						lastRoom = "ToRight";
						exitTag = "LeftExit";
					}else if (rooms [row, currentRoom] != 5 && lastRoom == "ToRight") {
						roomBefore--;

						lastRoom = "ToLeft";
						exitTag = "RightExit";
					}
					if (rooms [row, roomBefore] == 5 && lastRoom == "ToRight") {
						exitTag = "LeftUpExit";
						rowBefore--;
					}else if (rooms [row, roomBefore] == 5 && lastRoom == "ToLeft") {
						exitTag = "RightUpExit";
						rowBefore--;
					}
					/*
					if ((currentRoom) != 0 && rooms [row, currentRoom - 1] != 0) {
						

						if (rooms [row, currentRoom - 1] == 5) {
							exitTag = "RightUpExit";
							rowBefore = row - 1;
							currentRoom--;
						} else {
							roomBefore = currentRoom - 1;
							exitTag = "RightExit";
						}

					} else if ((currentRoom) != 5 && rooms [row, currentRoom + 1] != 0) {
						

						if (rooms [row, currentRoom + 1] == 5) {
							exitTag = "LeftUpExit";
							rowBefore = row - 1;

							currentRoom++;
						} else {
							roomBefore = currentRoom + 1;
							exitTag = "LeftExit";
						}
					}*/
					rooms [row, currentRoom] = 2;


					string otherExit = "";
					foreach(Transform child in generatedRooms [rowBefore, roomBefore].transform){
						if(child.CompareTag(exitTag)){

							childPosition = child;
							if (generatedRooms [row, currentRoom] != null) {
								DestroyImmediate (generatedRooms [row, currentRoom]);
							}
							if (child.CompareTag ("LeftUpExit") || child.CompareTag ("LeftExit")) {
								generatedRooms [row, currentRoom] = Instantiate (roomTeplates [2], childPosition);
								otherExit = "RightExit";
							} else {
								generatedRooms [row, currentRoom] = Instantiate (roomTeplates [3], childPosition);
								otherExit = "LeftExit";
							}

							foreach(Transform exitChild in generatedRooms [row, currentRoom].transform){
								if(exitChild.CompareTag(otherExit)){
									Vector3 difference = generatedRooms [row, currentRoom].transform.position - exitChild.position;

									generatedRooms [row, currentRoom].transform.position = generatedRooms [row, currentRoom].transform.position + difference;

								}
								if (otherExit == "LeftExit" && exitChild.CompareTag ("RightExit")) {
									generatedRooms [row, currentRoom + 1] = Instantiate (roomTeplatesSideEndings [0], exitChild);
									foreach (Transform exitChildOther in generatedRooms [row, currentRoom+1].transform) {
										if (exitChildOther.CompareTag ("LeftExit")) {
											Vector3 difference = generatedRooms [row, currentRoom+1].transform.position - exitChildOther.position;

											generatedRooms [row, currentRoom + 1].transform.position = generatedRooms [row, currentRoom + 1].transform.position + difference;

										}
									}
								}else if (otherExit == "RightExit" && exitChild.CompareTag ("LeftExit")) {
									generatedRooms [row, currentRoom - 1] = Instantiate (roomTeplatesSideEndings [1], exitChild);
									foreach (Transform exitChildOther in generatedRooms [row, currentRoom-1].transform) {
										if (exitChildOther.CompareTag ("RightExit")) {
											Vector3 difference = generatedRooms [row, currentRoom-1].transform.position - exitChildOther.position;

											generatedRooms [row, currentRoom - 1].transform.position = generatedRooms [row, currentRoom - 1].transform.position + difference;

										}
									}
								}

							}

						}
					}
					row++;
					rooms [row, currentRoom] = 5;
					solutionDir = rand.Next (1, 6);
					continue;
				}else {

					if ((lastRoom == "ToRight" && currentRoom == 6)) {
						rooms [row, currentRoom] = 4;
						ending = false;
						foreach (Transform child in generatedRooms [row, currentRoom].transform) {
							if (child.CompareTag ("RightExit")) {
								childPosition = child;
								generatedRooms [row, currentRoom] = Instantiate (roomTeplatesEndings[0], childPosition);
								foreach (Transform exitChild in generatedRooms [row, currentRoom].transform) {
									if (exitChild.CompareTag ("LeftExit")) {
										Vector3 difference = generatedRooms [row, currentRoom].transform.position - exitChild.position;

										generatedRooms [row, currentRoom].transform.position = generatedRooms [row, currentRoom].transform.position + difference;

									}
								}
							}
						}
					} else if ((lastRoom == "ToLeft" && currentRoom == 1)) {
						rooms [row, currentRoom] = 4;
						ending = false;
						foreach (Transform child in generatedRooms [row, currentRoom].transform) {
							if (child.CompareTag ("LeftExit")) {
								childPosition = child;
								generatedRooms [row, currentRoom] = Instantiate (roomTeplatesEndings[1], childPosition);
								foreach (Transform exitChild in generatedRooms [row, currentRoom].transform) {
									if (exitChild.CompareTag ("RightExit")) {
										Vector3 difference = generatedRooms [row, currentRoom].transform.position - exitChild.position;

										generatedRooms [row, currentRoom].transform.position = generatedRooms [row, currentRoom].transform.position + difference;

									}
								}
							}
						}
					}

					else {
						solutionDir = (lastRoom == "ToRight")? 1:3;
					}
				}



			}else {
				solutionDir = rand.Next (1, 6);
			}

		}
	}
	public GameObject ReturnRandomRoom(GameObject[] rooms){
		return rooms[rand.Next (0, rooms.Length)];
		
	}

}