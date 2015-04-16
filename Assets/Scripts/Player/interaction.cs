using UnityEngine;
using System.Collections;

public class interaction : MonoBehaviour {

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

	//VARIABLES
	public GameObject player;
	public GameObject closestConsole;

	public bool toggleHelp = true;
	public static bool escPressed = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.E)) {
			//Debug.Log ("E pressed");

			//E was pressed, find closest console
			closestConsole = FindClosestConsole ();

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

	}

	void OnGUI(){
		//SHIT FIX FOR THE ANNOYING ERROR
		GUI.SetNextControlName ("inputField");
		GUI.Label (new Rect (-100, -100, 1, 1), "");
		//

		closestConsole = FindClosestConsole ();

		if (Vector3.Distance (player.transform.position, closestConsole.transform.position) < 5f && toggleHelp) {
			GUI.Label(new Rect(20,Screen.height/2,Screen.width/4,Screen.height/3), "<color=orange><size=40>Press E to interact with consoles</size></color>");
		}

		if (Vector3.Distance (player.transform.position, closestConsole.transform.position) < 5f && !toggleHelp) {
			GUI.Label(new Rect(20,Screen.height/2,Screen.width/4,Screen.height/3), "<color=red><size=40>Press ESC to exit</size></color>");
		}

	}
			
}
