﻿using UnityEngine;
using System.Collections;

public class interaction : MonoBehaviour {

	//VARIABLES
	public GameObject player;
	public GameObject closestConsole;
	public bool foundFirstCon = false;



	//I wanna kick the following function out soon, reworking my 2.0 rework of it now
	//Function that finds the closest to player console and returns GameObject with it
	GameObject FindClosestConsole() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("console");

		GameObject closest = GameObject.FindGameObjectWithTag ("console");

		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}








	//STATIC VARIABLE FOR USE WITH CONSOLE FUNCTIONALITY
	//UI and Keyboard
	public bool toggleHelp = true;
	public static bool escPressed = false;

	//DOORS
	public static bool doorUnlocked = false;
	public static bool doorLocked = false;

	//lights interaction
	public static bool lightsSexy = false;
	public static bool lightsWhite = false;
	public static bool lightsGreen = false;
	public static bool lightsOff = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		Cursor.visible = false;

		FindConsole ();
	}
	
	// Update is called once per frame
	void Update () {

		if(!foundFirstCon){
			FindConsole();
			foundFirstCon = true;
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			//E was pressed, find closest console

			FindConsole();

			//if player is close enough to interact, bring up the UI
			if (Vector3.Distance (player.transform.position, closestConsole.transform.position) < 5f && !closestConsole.GetComponentInChildren<consoleScript> ().enabled) {
				closestConsole.GetComponentInChildren<consoleScript> ().enabled = true;
				toggleHelp = false;
			} else {
				closestConsole.GetComponentInChildren<consoleScript> ().enabled = false;
				//toggleHelp = true;
			}
		}

		//close console window if user pressed ESC
		if (escPressed) {
			closestConsole.GetComponentInChildren<consoleScript> ().enabled = false;
			escPressed = false;

			toggleHelp = true;
		}


		//Monitor for changes in booleans which are primarily changed from consoleScript
		//If console script changes static booleans these changes will be reflected below on update and send on to be taken care of

		if (doorUnlocked) {
			closestConsole.GetComponentInChildren<door1_open> ().SendMessage ("Unlock");
			doorUnlocked = false;
		} 

		if (doorLocked) {
			closestConsole.GetComponentInChildren<door1_open>().SendMessage("Lock");
			doorLocked = false;
		}

		//Lights interaction from console comes in through the boolean
		if (lightsGreen) {
			closestConsole.GetComponentInChildren<Lights>().SendMessage("greenLights");
			Debug.Log ("Green LIGHTS ON");
			lightsGreen = false;
		}

		if (lightsSexy) {
			closestConsole.GetComponentInChildren<Lights>().SendMessage("sexyLights");
			Debug.Log ("SEXY LIGHTS ON");
			lightsSexy = false;
		}

		if (lightsWhite) {
			closestConsole.GetComponentInChildren<Lights>().SendMessage("whiteLights");
			Debug.Log ("WHITE LIGHTS ON");
			lightsWhite = false;
		}

		if (lightsOff) {
			closestConsole.GetComponentInChildren<Lights>().SendMessage("offLights");
			Debug.Log ("LIGHTS OFF");
			lightsOff = false;
		}



	}

	void OnGUI(){
		//SHIT FIX FOR THE ANNOYING ERROR
		GUI.SetNextControlName ("inputField");
		GUI.Label (new Rect (-100, -100, 1, 1), "");

		//Here is a small bug which throws 1 error, to be fixed in the future
		if (closestConsole != null) {

			if (Vector3.Distance (player.transform.position, closestConsole.transform.position) < 5f && toggleHelp) {
				GUI.Label (new Rect (20, Screen.height / 2, Screen.width / 4, Screen.height / 3), "<color=orange><size=40>Press E to interact with consoles</size></color>");
			}

			if (Vector3.Distance (player.transform.position, closestConsole.transform.position) < 5f && !toggleHelp) {
				GUI.Label (new Rect (20, Screen.height / 2, Screen.width / 4, Screen.height / 3), "<color=red><size=40>Press ESC to exit</size></color>");
			}
		}

	}



	//New Find closest console function should be more efficient
	void FindConsole(){
		
		GameObject [] allCons = GameObject.FindGameObjectsWithTag("console");
		float maxDistance = Mathf.Infinity;
		
		foreach (GameObject c in allCons) {
			float curDifference = Vector3.Distance(c.transform.position, player.transform.position);
			if(curDifference<maxDistance){
				closestConsole = c;
				maxDistance = curDifference;
			}
			
		}
	}
			
}
