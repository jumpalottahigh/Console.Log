using UnityEngine;
using System.Collections;

public class gameInit : MonoBehaviour {

	private GameObject currentLevel;
	private int levelNum;
	private float levelHeight;
	private float levelRot;

	public static bool loadNextLevel = false;
	public static bool removeCurrentLevel = false;


	// Use this for initialization
	void Start () {

		//Fix variables
		levelNum = 0;
		levelHeight = 0.0f;
		levelRot = 0.0f;

		//Instantiate the player
		GameObject player = Instantiate (Resources.Load ("Prefabs/Player"), new Vector3 (-15f, 3f, 5f), Quaternion.identity) as GameObject;
		player.name = "Player";

		///
		//SPAWN 1 LEVEL
		///

		LoadLevel (levelNum, levelHeight, levelRot);




	}

	void Update(){

		if (loadNextLevel) {
			loadNextLevel = false;
			LoadLevel(levelNum, 0.0f, 0.0f);
		}

		if(removeCurrentLevel){
			removeCurrentLevel = false;
			currentLevel.tag = null;
			GameObject.Destroy (currentLevel);
		}
	}


	//This function takes care of loading multiple levels, selecting a main con and spawning other cons
	void LoadLevel(int levelNum, float levelHeight, float levelRot){
			//Load up level backbone at 0,0,0 for first level and fix the name in the Hierarchy
			//All follow up levels have a random 90 degree around the Y axis rotation
			GameObject.Destroy (currentLevel);
			levelNum++;
			currentLevel = Instantiate (Resources.Load ("Prefabs/Final/Level_Backbone"), new Vector3(0, levelHeight, 0), Quaternion.Euler(0,levelRot,0)) as GameObject;
			currentLevel.name = "level" + levelNum;
			GameObject consolesInLevel = new GameObject ();
			consolesInLevel.name = currentLevel.name + "_consoles";
			consolesInLevel.transform.parent = currentLevel.transform;	

			//Get all 5 console positions
			GameObject[] conPos = GameObject.FindGameObjectsWithTag ("consolePositions");
			ArrayList consoleTransforms = new ArrayList ();
			
			for (int j=0; j<conPos.Length; j++) {
				if(conPos[j].transform.parent.IsChildOf(currentLevel.transform))
					consoleTransforms.Add (conPos [j].transform);
			}
			
			//Spawn the main console
			int random = Random.Range (0, 5); // pick a number from 1 to 5
			
			//replace hardcoded 0 with random var;
			Transform mainPos = (Transform)consoleTransforms [0]; //Get the transform we picked
			
			//Instantiate the main console and immediately fix the name
			GameObject mainCon = Instantiate (Resources.Load ("Prefabs/Consoles/conGeneric"), mainPos.position, mainPos.rotation) as GameObject;
			mainCon.name = currentLevel.name + "_MAINEVENT_" + mainPos.name;
			mainCon.transform.parent = consolesInLevel.transform;
			AttachMainEventScript (mainCon);
			SetParenting (mainCon, currentLevel);	

			//Remove main console from arraylist
			//replace 0 with random
			consoleTransforms.RemoveAt (0);
			
			//Spawn 0-4 secondary consoles (chance of spawning secondary is 66%
			
			foreach (Transform pos in consoleTransforms) {
				//Give secondary consoles 66% chance to spawn
				if(Random.value < 0.66f){
					Debug.Log ("Secondary consoles are: " + pos);
					GameObject buffer = Instantiate (Resources.Load ("Prefabs/Consoles/conGeneric"), pos.position, pos.rotation) as GameObject;
					buffer.name = "level" + levelNum + "_" + pos.name;
					buffer.transform.parent = consolesInLevel.transform;
					SetParenting(buffer, currentLevel);
				}
			}
			
			//Clear the array list
			consoleTransforms.Clear ();
	
			//Increment level number
			//levelNum++;
			//levelHeight += 23f;

		}

	//This function takes a console game object as an argument and parents to it all relative doors, lights, elevators etc.
	void SetParenting(GameObject con, GameObject curLevel){
		GameObject door;
		GameObject light;

		if (con.name.Contains ("con1")) {
			door = GameObject.Find("Door1");
			light = GameObject.Find("Light1");
			if(door.transform.IsChildOf(curLevel.transform) && light.transform.IsChildOf(curLevel.transform)){
				door.transform.parent = con.transform;
				light.transform.parent = con.transform;

				//Give it 50/50 for the doors to be locked
				//if(Random.value > 0.5f)
				//	door.SendMessage("Lock");
			}
		}
		
		if (con.name.Contains ("con2")) {
			door = GameObject.Find("Door2");
			if(door.transform.IsChildOf(curLevel.transform)){
				door.transform.parent = con.transform;

				//Give it 50/50 for the doors to be locked
				//if(Random.value > 0.5f)
				//	door.SendMessage("Lock");
			}
		}
		
		if (con.name.Contains ("con3")) {
			door = GameObject.Find("Door3");
			light = GameObject.Find("Light3");
			if(door.transform.IsChildOf(curLevel.transform) && light.transform.IsChildOf(curLevel.transform)){
				door.transform.parent = con.transform;
				light.transform.parent = con.transform;

				//Give it 50/50 for the doors to be locked
				//if(Random.value > 0.5f)
				//	door.SendMessage("Lock");
			}
		}
		
		if (con.name.Contains ("con4")) {
			door = GameObject.Find("Door4");
			light = GameObject.Find("Light4");
			if(door.transform.IsChildOf(curLevel.transform) && light.transform.IsChildOf(curLevel.transform)){
				door.transform.parent = con.transform;
				light.transform.parent = con.transform;

				//Give it 50/50 for the doors to be locked
				//if(Random.value > 0.5f)
				//	door.SendMessage("Lock");
			}
		}
		
		if (con.name.Contains ("con5")) {
			door = GameObject.Find("Elevator");
			light = GameObject.Find ("Light5");
			if(door.transform.IsChildOf(curLevel.transform) && light.transform.IsChildOf(curLevel.transform)){
				door.transform.parent = con.transform;
				light.transform.parent = con.transform;
			}
		}

	}

	//Here we pass only the main event console and depending on its numbering assign the proper event script
	void AttachMainEventScript(GameObject con){

		if (con.name.Contains ("con1")) {
			con.AddComponent<con1MainEvent> ();
			con.SendMessage("StartEvent");
		}

		if (con.name.Contains ("con2")) {
			con.AddComponent<con2MainEvent> ();
		}

		if (con.name.Contains ("con3")) {
			con.AddComponent<con3MainEvent> ();
		}

		if (con.name.Contains ("con4")) {
			con.AddComponent<con4MainEvent> ();
		}

		if (con.name.Contains ("con5")) {
			con.AddComponent<con5MainEvent> ();
		}

	}

}