using UnityEngine;
using System.Collections;

public class con5MainEvent : MonoBehaviour {

	private Vector3 levelExit;
	private GameObject player;
	private GameObject elevator;
	private GameObject currentLevel;
	private float timer;
	private bool eventSolved;
	private bool weAreDoneHere;
	private bool toggleEventExplaination;
	
	// Use this for initialization
	void StartEvent () {
		Debug.Log ("Console 5 has the main event");
		//Level Exit and player
		currentLevel = GameObject.FindGameObjectWithTag("level");
		//levelExit = GameObject.FindGameObjectWithTag("exit").transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");
		eventSolved = false;
		toggleEventExplaination = true;
		weAreDoneHere = false;
		
		//Event
		timer = 10f;
		
		//Instantiate button and elevator
		GameObject[] gos = GameObject.FindGameObjectsWithTag("consolePositions");
		
		foreach (GameObject go in gos) {
			if(go.name.Contains("con4Pos"))
				levelExit = go.transform.position;
		}
		
		levelExit = levelExit + new Vector3(-4f,2.7f,5.5f);

		Debug.Log("DO WE GET HERE");
		//Spawn elevator to next level;
		elevator = Instantiate(Resources.Load("Prefabs/Elevator"), levelExit, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
		elevator.name = "exitElevator";
		elevator.transform.parent = currentLevel.transform;
	}
	
	// Update is called once per frame
	void Update () {
		//Fire off event timer and count down
		timer -= 1 * Time.deltaTime;
		
		//Autosolve event in 10 secs
		if(timer <=0){
			if(!weAreDoneHere){
				victory ();
			}
		}
		
	}
	
	void OnGUI(){
		//Hide event explaination after 7 secs
		Invoke ("hideExplaination", 7);
		if (toggleEventExplaination) {
			GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2, Screen.width / 3, Screen.height / 3), "<color=yellow><size=22>-Empty Event 5\n-Autocomplete in 10 secs</size></color>");
		}
		
		if(!eventSolved && timer >=0)
			GUI.Label (new Rect (Screen.width / 2 - 170, 30, Screen.width / 2, Screen.width / 4), "<color=red><size=24>Timer of Nothing: " + (int)timer + "</size></color>");
		
		if (eventSolved && weAreDoneHere){
			Invoke ("hideSuccess", 7);
			GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2, Screen.width / 3, Screen.height / 3), "<color=green><size=34>Mission Succesful!</size></color>");
		}
	}
	
	//Award the player score for completing this major event
	void victory(){
		eventSolved = true;
		weAreDoneHere = true;
		
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
