using UnityEngine;
using System.Collections;

public class consoleScript : MonoBehaviour {

	private bool elevator = false;
	private bool door = false;
	private bool light = false;
	private bool enterPressed = false;
	private bool spamshield = false;
	private string consoleStr = "Basic>";
	private string textInput = "";
	private string textOutput = "";
	private string mode = "Basic";
	private string modeOptions = "";
	private float cooldown = 2;

	// Detects what is child of the console and what options it should have
	void Start () {
		foreach(Transform t in transform){
			if (t.tag == "elevator") {
				Debug.Log ("Elevator on console");
				elevator = true;
				modeOptions = modeOptions + "Elevator\n";
			}
			if (t.tag == "door"){
				Debug.Log ("Door on console");
				door = true;
				modeOptions = modeOptions + "Door\n";
			}
			if(t.tag == "light"){
				Debug.Log ("Light on console");
				modeOptions = modeOptions + "Light\n";
			}
		}
	}
	// Update is called once per frame
	void Update () {
		//To prevent the GUI input detection from spamming 4 times
		if (enterPressed == true) {
			cooldown -= Time.deltaTime;
			if (cooldown < 1){
				enterPressed = false;
				cooldown = 2;
			}
		}
	}
	void OnGUI(){
		Rect textWindow = new Rect (200, 10, Screen.width - 210, Screen.height - 20);
		GUI.Window(0, textWindow, windowFunc, "Console.Log");
	}

	private void inputFunc(){
		textInput = textInput.ToLower ();

		//Basic level input check
		if (mode == "Basic") {
			if (textInput == "?") {
				textOutput = modeOptions;
			} else if (textInput == "door" && door == true && mode == "Basic") {
				mode = mode + "/Door";
				modeOptions = "Power\nLock\nExit\n";
				textOutput = "";
				spamshield = true;
			} else if (textInput == "light" && light == true && mode == "Basic") {
				mode = mode + "/Light";
				textOutput = "";
			} else if (textInput == "elevator" && elevator == true && mode == "Basic") {
				mode = mode + "/Elevator";
				textOutput = "";
			} else{
				textOutput = "Unrecognised command\n";
			}
		}

		//Door level input check
		if (mode == "Basic/Door") {
			if (textInput == "?"){
				textOutput = modeOptions;
			} else if (textInput == "exit"){
				mode = "Basic";
				textOutput = "";
				spamshield = true;
			} else if (textInput == "power" || textInput == "lock"){
				textOutput = "Incomplete command\n";
			} else if (textInput == "power ?" || textInput == "lock ?"){
				textOutput = "On\nOff\n";
			} else if (textInput == "power on"){
				textOutput = "Door is now powered\n";
			} else if (textInput == "power off"){
				textOutput = "Door is no longer powered\n";
			} else if (textInput == "lock on"){
				textOutput = "Door is now locked\n";
			} else if (textInput == "lock off"){
				textOutput = "Door is now unlocked\n";
			} else if (spamshield == true){
				textOutput = "";
			}else {
				textOutput = "Unrecognised command\n";
			}
		}

		//Light level input check
		if (mode == "Basic/Light") {
			if (textInput == "?"){
				textOutput = modeOptions;
			} else if (textInput == "exit"){
				mode = "Basic";
				textOutput = "";
				spamshield = true;
			} else if (textInput == "power" || textInput == "color"){
				textOutput = "Incomplete command\n";
			} else if (textInput == "power ?"){
				textOutput = "On\nOff\nSexy\n";
			} else if (textInput == "color ?"){
				textOutput = "White\nGreen\nSexy\n";
			} else if (textInput == "power on"){
				textOutput = "Lights are powered on\n";
			} else if (textInput == "power off"){
				textOutput = "Lights are powered off\n";
			} else if (textInput == "power sexy"){
				textOutput = "Lights are set to SEXY!\n";
			} else if (textInput == "color white"){
				textOutput = "Light color set to white";
			} else if (textInput == "color green"){
				textOutput = "Light color set to green";
			} else if (textInput == "color sexy"){
				textOutput = "BOOM BABY SEXY LIGHTS!\n";
			} else if (spamshield == true){
				textOutput = "";
			}else {
				textOutput = "Unrecognised command\n";
			}
		}
		//Other part to prevent the GUI input from spamming 4 times.
		if (enterPressed == false){
			consoleStr = consoleStr + " " + textInput + "\n" + textOutput + mode + ">";
			enterPressed = true;
		}
		textInput = "";
	}
	private void windowFunc(int id){
		//Aligning and organising the input and output fields
		GUI.skin.label.alignment = TextAnchor.UpperLeft;
		GUI.Label(new Rect(10,20, Screen.width - 230, Screen.height - 70), consoleStr);
		textInput = GUI.TextField (new Rect (10, Screen.height - 52, Screen.width - 230, 22), textInput);

		//Checking if enter is pressed
		Event e = Event.current;
		if (e.keyCode == KeyCode.Return) {
			inputFunc ();
		}
	}
}
