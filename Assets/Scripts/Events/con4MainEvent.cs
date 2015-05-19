using UnityEngine;
using System.Collections;

public class con4MainEvent : MonoBehaviour {
	private GameObject elevator;

	private bool showSuccess = true;
	private bool toggleEventExplaination = true;
	private bool eventComplete = false, step2 = false, step3 = false;
	private int eventNum;
	private float globalTimer = 60f;
	private float timer = 10, deathTimer = 20;
	private bool elevatorFound;

	// Use this for initialization
	void StartEvent () {
		eventNum = Random.Range (1, 3);

		GameObject currentLevel;
		GameObject player;
		
		//Level Exit and player
		//currentLevel = GameObject.FindGameObjectWithTag("level");
		//player = GameObject.FindGameObjectWithTag ("Player");

		elevatorFound = false;
	}

	// Update is called once per frame
	void Update () {
		if (step2 == true) {
			timer -= 1*Time.deltaTime;
			deathTimer -= 1*Time.deltaTime;
			if(deathTimer <= 0){
				//Player death stuff
			}
			if (timer <= 0) {
				step3 = true;
				step2 = false;
			}
		}

		//if no steps are done
		if(!step2 && !step3)
			globalTimer -= 1 * Time.deltaTime;

		//Try finding elevator till success
		if (!elevatorFound) {
			elevator = GameObject.FindGameObjectWithTag ("elevator");
			if (elevator != null) {
				elevatorFound = true;
			}
		} else {
			if(elevator.name != "exitElevator")
				elevatorFound = false;
		}
	}

	void OnGUI(){
		Invoke ("hideExplaination", 7);
		if (toggleEventExplaination) {
			if (eventNum == 1) {
				GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2, Screen.width / 3, Screen.height / 3), "<color=yellow><size=22>-Get to the pump controls\n-Turn off the water to prevent flooding</size></color>");
			}
			else if (eventNum == 2){
				GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2, Screen.width / 3, Screen.height / 3), "<color=yellow><size=22>-Get to the pump controls\n-Pump out air to stop fire\n-Pump air in before you asphyxiate</size></color>");
			}
		}

		if (!step2 && !step3) {
			GUI.Label (new Rect (Screen.width / 2 - 170, 30, Screen.width / 2, Screen.width / 4), "<color=red><size=34>Time left: " + (int)globalTimer + "</size></color>");
		}

		//Add here more ifs to facilitate timers for step2 and 3; can look like as follows

		if (step2) {
			GUI.Label (new Rect (Screen.width / 2 - 170, 30, Screen.width / 2, Screen.width / 4), "<color=red><size=34>Time left: " + (int)timer + "</size></color>");
		}


		if (eventComplete && showSuccess) {
			Invoke("hideSuccess", 7);
			GUI.Label (new Rect (Screen.width / 2 - 150, Screen.height / 2, Screen.width / 3, Screen.height / 3), "<color=green><size=34>Event complete!</size></color>");	
		}
	}

	void hideExplaination(){
		toggleEventExplaination = false;
	}

	void hideSuccess(){
		showSuccess = false;
	}

	public void event4Console(string textInput){
		
		consoleScript console = (consoleScript)this.GetComponent ("consoleScript");
		
		if (textInput == "?") {
			console.textOutput = console.modeOptions;
		} else if (textInput == "exit") {
			console.mode = "Basic";			
			console.textOutput = "";
			console.modeOptions = console.defaultOptions;
			console.spamshield = true;
		} else if (textInput == "power" || textInput == "air" || textInput == "water") {
			console.textOutput = "Incomplete command\n";
		} else if (textInput == "power ?" || textInput == "water ?") {
			console.textOutput = "On\nOff\n";
		} else if (textInput == "air ?") {
			console.textOutput = "In\nOut\n";
		} else if (textInput == "power on") {
			console.textOutput = "Pumps are now powered\n";
		} else if (textInput == "power off") {
			console.textOutput = "Pumps are no longer powered\n";
		} else if (textInput == "air in") {
			console.textOutput = "Pressurising room\n";
			if (eventNum == 2 && step3 == true){
				eventComplete = true;
				victory();
			}
		} else if (textInput == "air out") {
			console.textOutput = "Releasing air to space\n";
			if(eventNum == 2 && step2 == false){
				step2 = true;
			}
		} else if (textInput == "water on") { 
			console.textOutput = "Water flow on\n";
		} else if (textInput == "water off") {
			console.textOutput = "Water flow off\n";
			if(eventNum == 1){
				eventComplete = true;
				victory();
			}
		} else if (console.spamshield == true) {
			console.textOutput = "";
			console.spamshield = false;
		} else {
			console.textOutput = "Unrecognised command\n";
		}
		
	}

	public void victory(){
		if (eventComplete) {
			score.majorEventCompleted = true;

			elevator.SendMessage("OpenDoors");
		}
	}



}
