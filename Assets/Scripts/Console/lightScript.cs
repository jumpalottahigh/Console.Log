using UnityEngine;
using System.Collections;

public class lightScript : MonoBehaviour {

	public void light (string textInput){

		consoleScript console = (consoleScript)this.GetComponent("consoleScript");

		if (textInput == "?") {
			console.textOutput = console.modeOptions;
		} else if (textInput == "exit") {
			console.mode = "Basic";
			console.textOutput = "";
			console.modeOptions = console.defaultOptions;
			console.spamshield = true;
		} else if (textInput == "power" || textInput == "color") {
			console.textOutput = "Incomplete command\n";
		} else if (textInput == "power ?") {
			console.textOutput = "On\nOff\nDisco\n";
		} else if (textInput == "color ?") {
			console.textOutput = "White\nGreen\nSexy\n";
		} else if (textInput == "power on") {
			interaction.lightsWhite = true;
			console.textOutput = "Lights are powered on\n";
		} else if (textInput == "power off") {
			interaction.lightsOff = true;
			console.textOutput = "Lights are powered off\n";
		} else if (textInput == "power disco") {
			interaction.lightsSexy = true;
			console.textOutput = "Lights are set to PARTY!\n";
		} else if (textInput == "color white") {
			interaction.lightsWhite = true;
			console.textOutput = "Light color set to white";
		} else if (textInput == "color green") {
			interaction.lightsGreen = true;
			console.textOutput = "Light color set to green";
		} else if (textInput == "color sexy") {
			interaction.lightsSexy = true;
			console.textOutput = "BOOM BABY SEXY LIGHTS!\n";
		} else if (console.spamshield == true) {
			console.textOutput = "";
			console.spamshield = false;
		} else {
			console.textOutput = "Unrecognised command\n";
		}
	}
}
