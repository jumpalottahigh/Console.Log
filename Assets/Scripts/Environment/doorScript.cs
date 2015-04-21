using UnityEngine;
using System.Collections;

//Future idea
//[RequireComponent (consoleSCript))]

public class doorScript : MonoBehaviour {


	public void door(string textInput, string modeOptions, string defaultOptions, bool spamshield){

		consoleScript console = (consoleScript)this.GetComponent("consoleScript");

		if (textInput == "?") {
			console.textOutput = modeOptions;
		} else if (textInput == "exit") {
			console.mode = "Basic";			
			console.textOutput = "";
			console.modeOptions = console.defaultOptions;
			console.spamshield = true;
			spamshield = true;
		} else if (textInput == "power" || textInput == "lock") {
			console.textOutput = "Incomplete command\n";
		} else if (textInput == "power ?" || textInput == "lock ?") {
			console.textOutput = "On\nOff\n";
		} else if (textInput == "power on") {
			console.textOutput = "Door is now powered\n";
		} else if (textInput == "power off") {
			console.textOutput = "Door is no longer powered\n";
		} else if (textInput == "lock on") {
			interaction.doorLocked = true;
			console.textOutput = "Door is now locked\n";
		} else if (textInput == "lock off") {
			//send message to door script
			interaction.doorUnlocked = true;
			console.textOutput = "Door is now unlocked\n";
		} else if (console.spamshield == true) {
			console.textOutput = "";
			console.spamshield = false;
		} else {
			console.textOutput = "Unrecognised command\n";
		}
	}
}
