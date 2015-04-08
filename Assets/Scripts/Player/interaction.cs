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

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		//closestConsole = GameObject.FindGameObjectWithTag ("console");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log ("E pressed");
			//E was pressed, find closest console
			closestConsole = FindClosestConsole ();
			//if player is close enough to interact, bring up the UI
			if (Vector3.Distance (player.transform.position, closestConsole.transform.position) < 5f && !closestConsole.GetComponentInChildren<consoleScript> ().enabled) {

				Debug.Log ("Player is within 5m of: ");
				print (closestConsole.name);
				closestConsole.GetComponentInChildren<consoleScript> ().enabled = true;
			} else {
				closestConsole.GetComponentInChildren<consoleScript> ().enabled = false;
			}
		}
	}
			
}
