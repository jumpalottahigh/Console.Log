using UnityEngine;
using System.Collections;

public class con1MainEvent : MonoBehaviour {
	
	private Vector3 levelExit;
	private GameObject player;
	private GameObject elevBtn;
	private Vector3 con5pos;
	private bool btnPressed;
	private GameObject gravityButton;
	private GameObject elevator;
	private GameObject currentLevel;
	private float timer;
	private bool eventSolved;
	private bool toggleEventExplaination = true;

	//EVENT SCRIPT FOR WHEN CONSOLE 1 is main does the following:
	//Event fires off right away if con1 is the main event con;
	//Space station gravity systems start tilting the station
	//Player must get to console5 room and manually override the gravity switch
	//then come back to con1 and reenable the gravity systems
	//This is a timed event

	// Use this for initialization
	void StartEvent () {
		Debug.Log ("Console 1 has the main event");
		//Level Exit and player
		currentLevel = GameObject.FindGameObjectWithTag("level");
		levelExit = GameObject.FindGameObjectWithTag("exit").transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");
		btnPressed = false;
		eventSolved = false;

		//Event
		//timer = 120;//120 seconds to complete
		timer = 120f;

		//Instantiate button and elevator
		GameObject[] gos = GameObject.FindGameObjectsWithTag("consolePositions");

		foreach (GameObject go in gos) {
			if(go.name == "con5Pos")
				con5pos = go.transform.position;
		}

		con5pos = con5pos + new Vector3(8.2f,1.5f,8f);

		//Instantiate button
		gravityButton = Instantiate (Resources.Load ("Prefabs/Events/con1Event/gravityButton"), con5pos, Quaternion.Euler(new Vector3(0,0,90))) as GameObject;
		gravityButton.name = "gravityButton";
		gravityButton.transform.parent = currentLevel.transform;

		Debug.Log("DO WE GET HERE");
		//Spawn elevator to next level;
		elevator = Instantiate(Resources.Load("Prefabs/Elevator"), levelExit + new Vector3(-2.2f, 2.64f, -2f), Quaternion.Euler(Vector3.up)) as GameObject;
		elevator.name = "exitElevator";
		elevator.transform.parent = currentLevel.transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (!btnPressed) {

			//Fire off event timer and count down
			timer -= 1 * Time.deltaTime;

			//Rotate the level here
			if((int)timer % 10 == 0 && !eventSolved){
				currentLevel.transform.Rotate(new Vector3(Random.value / 10f,Random.value/10f,0));
			}
			//Player presses button
			if(Vector3.Distance(player.transform.position, gravityButton.transform.position) < 3f && Input.GetKeyDown(KeyCode.E)){

				while(gravityButton.transform.localPosition.x < -11.45f){
					gravityButton.transform.localPosition += Vector3.right * Time.deltaTime;
				}

				btnPressed = true;
				victory ();
			}
		}
	
	}

	void OnGUI(){
		//Hide event explaination after 7 secs
		Invoke ("hideExplaination", 7);
		if (toggleEventExplaination) {
			GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2, Screen.width / 3, Screen.height / 3), "<color=yellow><size=22>-Get to the gravity controls\n-Manually override the system</size></color>");
		}

		if(!btnPressed)
			GUI.Label (new Rect (Screen.width / 2 - 170, 30, Screen.width / 2, Screen.width / 4), "<color=red><size=34>Space station tilt in: " + (int)timer + "</size></color>");

		if (eventSolved){
			Invoke ("hideSuccess", 7);
			GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2, Screen.width / 3, Screen.height / 3), "<color=green><size=34>Mission Succesful!</size></color>");
		}
	}

	//Award the player score for completing this major event
	void victory(){
		timer = 0f;
		eventSolved = true;

		//give points
		score.majorEventCompleted = true;

		//open elevator doors
		elevator.SendMessage ("OpenDoors");
	}

	//Hides initial mission explaination after 7 secs
	void hideExplaination(){
		toggleEventExplaination = false;
	}

	void hideSuccess(){
		eventSolved = false;
	}

}
