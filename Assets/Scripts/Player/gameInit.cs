using UnityEngine;
using System.Collections;

public class gameInit : MonoBehaviour {

	public static GameObject curLevObject;

	private GameObject[] cleaner;
	private GameObject elevator;
	private GameObject currentLevel;
	private int levelNum;
	private float levelHeight;
	private float levelRot;
	private int prevRandomNumber;
	private int random;

	public static bool loadNextLevel = false;
	public static bool removeCurrentLevel = false;

	// Use this for initialization
	void Start () {

		//Fix variables
		levelNum = 1;
		levelHeight = 0.0f;
		levelRot = 0.0f;
		prevRandomNumber = -1;
		random = -1;

		//Instantiate the player
		GameObject player = Instantiate (Resources.Load ("Prefabs/Player"), new Vector3 (-15f, 3f, 5f), Quaternion.identity) as GameObject;
		player.name = "Player";

		///
		//SPAWN 1 LEVEL
		///

		LoadLevel (levelNum, levelHeight, levelRot);

	}

	void Update(){

		//Load Next Level On Demand
		if (loadNextLevel) {
			loadNextLevel = false;
			levelNum++;
			LoadLevel(levelNum, 0.0f, 0.0f);
		}

		//Remove old level
		if(removeCurrentLevel){
			removeCurrentLevel = false;
			currentLevel.tag = null;
			GameObject.Destroy (currentLevel);
		}

		//Remove any object with no name = trash
		//if (GameObject.Find ("")) {
			//GameObject.Destroy(GameObject.FindGameObjectsWithTag("thrash"));
		//GameObject.D
		//}
	}


	//This function takes care of loading multiple levels, selecting a main con and spawning other cons
	void LoadLevel(int levelNum, float levelHeight, float levelRot){
			//Load up level backbone at 0,0,0 for first level and fix the name in the Hierarchy
			//All follow up levels have a random 90 degree around the Y axis rotation	

			GameObject.Destroy (currentLevel);
			currentLevel = Instantiate (Resources.Load ("Prefabs/Final/Level_Backbone"), new Vector3(0, levelHeight, 0), Quaternion.Euler(0,levelRot,0)) as GameObject;
			currentLevel.name = "level" + levelNum;
			curLevObject = currentLevel;
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
			
			random = Random.Range (0, 5);
			//Spawn the main console
			//Prevent same event from spawning twice in a row
			if (prevRandomNumber >= 0) {

			if (random == prevRandomNumber) {
				if(Random.value > 0.5f){
					random = Random.Range (0, prevRandomNumber);// pick a number from 1 to 5
				} else {
					random = Random.Range(prevRandomNumber+1, 5);
				}
				}
			}

			//Update prev random
			prevRandomNumber = random;

			//replace hardcoded 0 with random var;
			Transform mainPos = (Transform)consoleTransforms [random]; //Get the transform we picked
			
			//Instantiate the main console and immediately fix the name
			GameObject mainCon = Instantiate (Resources.Load ("Prefabs/Consoles/conGeneric"), mainPos.position, mainPos.rotation) as GameObject;
			mainCon.name = currentLevel.name + "_MAINEVENT_" + mainPos.name;
			mainCon.transform.parent = consolesInLevel.transform;
			AttachMainEventScript (mainCon);
			SetParenting (mainCon, currentLevel);	

			//Remove main console from arraylist
			//replace 0 with random
			consoleTransforms.RemoveAt (random);
			
			//Spawn 0-4 secondary consoles (chance of spawning secondary is 66%
			
			foreach (Transform pos in consoleTransforms) {
				//Give secondary consoles 66% chance to spawn
				if(Random.value < 0.66f){
					GameObject buffer = Instantiate (Resources.Load ("Prefabs/Consoles/conGeneric"), pos.position, pos.rotation) as GameObject;
					buffer.name = "level" + levelNum + "_" + pos.name;
					buffer.transform.parent = consolesInLevel.transform;
					SetParenting(buffer, currentLevel);
				}
			}
			
			//Clear the array list
			consoleTransforms.Clear ();
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
			light = GameObject.Find ("Light5");
			if(light.transform.IsChildOf(curLevel.transform)){
				light.transform.parent = con.transform;
			}
		}

	}

	//Here we pass only the main event console and depending on its numbering assign the proper event script
	void AttachMainEventScript(GameObject con){

		//Variables
		Vector3 levelExit = Vector3.zero;
		GameObject[] gos = GameObject.FindGameObjectsWithTag("consolePositions");


		//CASE CONSOLE 1
		if (con.name.Contains ("con1")) {
			con.AddComponent<con1MainEvent> ();
			con.SendMessage("StartEvent");


			//Spawn elevator to next level
			levelExit = GameObject.FindGameObjectWithTag("exit").transform.position;
			levelExit = levelExit + new Vector3(-2.2f, 2.64f, -2f);
			spawnElevator(levelExit, 0);

			//Spawn gravity button
			Vector3 gravityBtnPos = Vector3.zero;
			GameObject gravityButton;
			foreach (GameObject go in gos) {
				if(go.name == "con5Pos")
					gravityBtnPos = go.transform.position;
			}
			
			gravityBtnPos = gravityBtnPos + new Vector3(8.2f,1.5f,8f);
			
			//Instantiate button
			gravityButton = Instantiate (Resources.Load ("Prefabs/Events/con1Event/gravityButton"), gravityBtnPos, Quaternion.Euler(new Vector3(0,0,90))) as GameObject;
			gravityButton.name = "gravityButton";
			gravityButton.transform.parent = currentLevel.transform;
		}


		//CASE CONSOLE 2
		if (con.name.Contains ("con2")) {
			con.AddComponent<con2MainEvent> ();
			con.SendMessage("StartEvent");

			//Spawn elevator to next level
			foreach (GameObject go in gos) {
				if(go.name.Contains("con2Pos"))
					levelExit = go.transform.position;
			}
			levelExit = levelExit + new Vector3(-7f,2.7f,-3f);
			spawnElevator(levelExit, -180f);
		}


		//CASE CONSOLE 3
		if (con.name.Contains ("con3")) {
			con.AddComponent<con3MainEvent> ();
			con.SendMessage("StartEvent");

			//Spawn elevator to next level
			foreach (GameObject go in gos) {
				if(go.name.Contains("con2Pos"))
					levelExit = go.transform.position;
			}
			levelExit = levelExit + new Vector3(-6f,2.7f,17f);
			spawnElevator(levelExit, 90f);
		}


		//CASE CONSOLE 4
		if (con.name.Contains ("con4")) {
			con.AddComponent<con4MainEvent> ();
			con.SendMessage("StartEvent");

			//Spawn elevator to next level		
			foreach (GameObject go in gos) {
				if(go.name.Contains("con4Pos"))
					levelExit = go.transform.position;
			}
			levelExit = levelExit + new Vector3(-5f,2.7f,-9f);
			spawnElevator(levelExit, 90f);
		}


		//CASE CONSOLE 5
		if (con.name.Contains ("con5")) {
			con.AddComponent<con5MainEvent> ();
			con.SendMessage("StartEvent");

			//Spawn elevator to next level
			foreach (GameObject go in gos) {
				if(go.name.Contains("con4Pos"))
					levelExit = go.transform.position;
			}
			
			levelExit = levelExit + new Vector3(-4f,2.7f,5.5f);
			spawnElevator(levelExit, 0);
		}

	}

	/*
	public static GameObject getCurrentLevel(){
		return curLevObject;
	}
	*/

	void spawnElevator(Vector3 levelExit, float yRot){
		//Spawn elevator to next level;
		//elevator = null;
		elevator = Instantiate(Resources.Load("Prefabs/Elevator"), levelExit, Quaternion.Euler(new Vector3(0, yRot, 0))) as GameObject;
		elevator.name = "exitElevator";
		elevator.transform.parent = currentLevel.transform;
	}

	/*
	GameObject getElevator(){
		return elevator;
	}
	*/

}