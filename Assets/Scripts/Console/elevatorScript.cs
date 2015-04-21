using UnityEngine;
using System.Collections;

public class elevatorScript : MonoBehaviour {

	// Use this for initialization
	public void elevator (string textInput) {

		consoleScript console = (consoleScript)this.GetComponent("consoleScript");

		if (textInput == "?") {
			console.textOutput = console.modeOptions;
		} else if (textInput == "exit") {
			console.mode = "Basic";
			console.textOutput = "";
			console.modeOptions = console.defaultOptions;
			console.spamshield = true;
		} else if (textInput == "power" || textInput == "speed") {
			console.textOutput = "Incomplete command\n";
		} else if (textInput == "power ?") {
			console.textOutput = "On\nOff\n";
		} else if (textInput == "speed ?") {
			console.textOutput = "Low\nMedium\nHigh\nLudicrous\n";
		} else if (textInput == "power on") {
			console.textOutput = "Elevator is powered on\n";
		} else if (textInput == "power off") {
			console.textOutput = "Elevator is powered off\n";
		} else if (textInput == "speed low"){
			console.eleSpeed = 1;
			console.textOutput = "Elevator speed set to low\n";
		} else if (textInput == "speed medium"){
			console.eleSpeed = 2;
			console.textOutput = "Elevator speed set to medium\n";
		} else if (textInput == "speed high"){
			console.eleSpeed = 3;
			console.textOutput = "Elevator speed set to high\n";
		} else if (textInput == "speed ludicrous"){
			console.eleSpeed = 4;
			console.textOutput = "Elevator speed set to LUDICROUS!\n";
		} else if (console.spamshield == true) {
			console.textOutput = "";
			console.spamshield = false;
		} else {
			console.textOutput = "Unrecognised command\n";
		}
	}
}
