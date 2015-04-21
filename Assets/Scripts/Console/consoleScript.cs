using UnityEngine;
using System.Collections;
using System.Text;

public class consoleScript : MonoBehaviour {

	//Variables for just this script
	private bool elevator = false;
	private bool door = false;
	private bool light = false;
	private bool pump = false;
	private bool enterPressed = false;
	private float cooldown = 2;
	private int numlines, maxlines;
	private string consoleStr = "Basic>";

	//Variables for all console scripts
	public bool spamshield = false;
	public string textInput = "";
	public string textOutput = "";
	public string mode = "Basic";
	public string modeOptions = "";
	public string defaultOptions = "";
	public int eleSpeed;


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
				light = true;
				modeOptions = modeOptions + "Light\n";
			}
		}

		//Finds out which console has the main event and adds needed options
		if (this.name.Contains("MAIN")){
			if(this.name.Contains("4")) {
				modeOptions = modeOptions + "Pump\n";
				pump = true;
			}
		}
		defaultOptions = modeOptions;
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
			//Draws the text box on screen
			Rect textWindow = new Rect (Screen.width/3, 10, Screen.width/3*2, Screen.height - 20);
			GUI.Window (0, textWindow, windowFunc, "Console.Log");
	}

	private void inputFunc(){
		/*Makes sure that all player input is lower case
		 Then finds out how many lines the string is and how many lines can fit in the screen*/
		textInput = textInput.ToLower ();
		numlines = consoleStr.Split ('\n').Length;
		maxlines = (Screen.height - 52) / 17;
		if (numlines > maxlines) {
			consoleStr = consoleStr.Substring (consoleStr.IndexOf ('\n') + 1);
		}
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
				modeOptions = "Power\nColor\nExit\n";
				textOutput = "";
				spamshield = true;
			} else if (textInput == "elevator" && elevator == true && mode == "Basic") {
				mode = mode + "/Elevator";
				modeOptions = "Power\nSpeed\nExit\n";
				textOutput = "";
				spamshield = true;
			} else if(textInput == "pump" && pump == true && mode == "Basic"){
				mode = mode + "/Pump";
				modeOptions = "Power\nAir\nWater\nExit\n";
				textOutput = "";
				spamshield = true;
			} else {
				textOutput = "Unrecognised command\n";
			}
		}

		//Door level input check
		if (mode == "Basic/Door") {
			//Finds the doorScript for this console
			doorScript doorS = (doorScript)this.GetComponent("doorScript");
			//Runs the needed script
			doorS.door (textInput);

		}

		//Light level input check
		if (mode == "Basic/Light") {

			lightScript lightS = (lightScript)this.GetComponent("lightScript");
			lightS.light (textInput);

		}

		//Elevator level input check
		if (mode == "Basic/Elevator") {

			elevatorScript elevatorS = (elevatorScript)this.GetComponent("elevatorScript");
			elevatorS.elevator(textInput);

		}


		//Main event input checks
		//Event 2 input check
		if (mode == "Basic/Pump") {
			con4MainEvent pumpS = (con4MainEvent)this.GetComponent("con4MainEvent");
			pumpS.event4Console(textInput);
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

		//Give a name to next control so we can identify it
		GUI.SetNextControlName ("inputField");
		textInput = GUI.TextField (new Rect (10, Screen.height - 52, Screen.width - 230, 22), textInput);

		//AUTO FOCUS TEXT FIELD
		GUI.FocusControl ("inputField");

		//Checking if enter is pressed
		Event e = Event.current;
		if (e.keyCode == KeyCode.Return) {
			inputFunc ();
		}

		//If esp pressed disable script and hide console
		if (e.keyCode == KeyCode.Escape) {
			//send message to interaction script to close UI
			interaction.escPressed = true;
		}
	}
}
