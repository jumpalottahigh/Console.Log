using UnityEngine;
using System.Collections;

public class con1MainEvent : MonoBehaviour {

	//public variable


	//privates
	private GameObject player;
	private GameObject currentLevel;
	private GameObject[] allLevels;
	private GameObject gravityButton;

	private Transform con5pos;

	private float timer;

	private bool success = false;
	private bool btnPressed = false;
	private bool playerIsOnThisLevel = false;
	private bool eventSolved = false;
	private bool toggleEventExplaination = true;

	//EVENT SCRIPT FOR WHEN CONSOLE 1 is main does the following:
	//Event fires off right away if con1 is the main event con;
	//Space station gravity systems start tilting the station
	//Player must get to console5 room and manually override the gravity switch
	//then come back to con1 and reenable the gravity systems
	//This is a timed event

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		allLevels = GameObject.FindGameObjectsWithTag ("level");

		//Get current level by checking y coordinate difference between player and level
		foreach(GameObject i in allLevels){
			if(i.transform.position.y - player.transform.position.y < 3f){
				currentLevel = i;
			}
		}

		//Parent the player to the current level for testing purposes to stop him from constantly falling
		//player.transform.parent = currentLevel.transform;

		//Check if player is on this console's level
		if (transform.parent.IsChildOf (currentLevel.transform)) {
			playerIsOnThisLevel = true;
			timer = 120;//120 seconds to complete
		}

		//If player is on this level, init event
		if (playerIsOnThisLevel && !eventSolved) {
			//get con5Pos, add vector3 position to it and use it to instantiate the button
			con5pos = currentLevel.transform.Find ("ConsolePositions/con5Pos");
			con5pos.transform.position = con5pos.transform.position + new Vector3(8.2f,1.5f,8f);

			gravityButton = Instantiate (Resources.Load ("Prefabs/Events/con1Event/gravityButton"), con5pos.transform.position, Quaternion.Euler(new Vector3(0,0,90))) as GameObject;
			gravityButton.name = "gravityButton";
			gravityButton.transform.parent = currentLevel.transform;

			con5pos = gravityButton.transform;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (playerIsOnThisLevel && !btnPressed) {

			//Fire off event timer and count down
			timer -= 1 * Time.deltaTime;

			//Rotate the level here
			currentLevel.transform.Rotate(new Vector3(10,10,0) * Time.deltaTime * 0.1f);

			//Player presses button
			if(Vector3.Distance(player.transform.position, gravityButton.transform.position) < 3f && Input.GetKeyDown(KeyCode.E)){

				while(gravityButton.transform.localPosition.x < -11.45f){
					gravityButton.transform.localPosition += Vector3.right * Time.deltaTime;
				}
				btnPressed = true;
			}

			//Button pressed, normalise the station
			if(btnPressed) {
				//BUGGY THERE MUST BE A BETTER WAY
				//currentLevel.transform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.Euler(Vector3.zero),0.5f * Time.deltaTime) ; 
				currentLevel.transform.rotation = Quaternion.Euler(Vector3.zero);
			}

			//Player came back to room with con1MainEvent after pressing the button
			if (Vector3.Distance (player.transform.position, transform.position) < 3f && btnPressed) {
				eventSolved = true;
				victory ();
			}
		}
	
	}

	void OnGUI(){
		if (playerIsOnThisLevel) {
			//Hide event explaination after 5 secs
			Invoke ("hideExplaination", 7);
			if (toggleEventExplaination) {
				GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2, Screen.width / 3, Screen.height / 3), "<color=yellow><size=22>-Get to the gravity controls\n-Manually override the system</size></color>");
			}

			GUI.Label (new Rect (Screen.width / 2 - 170, 30, Screen.width / 2, Screen.width / 4), "<color=red><size=34>Space station tilt in: " + (int)timer + "</size></color>");
		}

		if (eventSolved) {
			Invoke ("hideSuccess", 3);
			if (success) {
				GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2, Screen.width / 3, Screen.height / 3), "<color=green><size=34>Mission Succesful!</size></color>");
			}
		}
	}

	//Award the player score for completing this major event
	void victory(){
		//give points
		score.majorEventCompleted = true;
		//get player to next level?

		success = true;

		playerIsOnThisLevel = false;
	}

	//Hides initial mission explaination after 7 secs
	void hideExplaination(){
		toggleEventExplaination = false;
	}

	void hideSuccess(){
		success = false;
	}

}
