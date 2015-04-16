using UnityEngine;
using System.Collections;

public class gameInit : MonoBehaviour {

	private GameObject currentLevel;
	private int levelNum;
	private float levelHeight;

	// Use this for initialization
	void Start () {
		//Lets spawn 3 levels to start with
		levelNum = 1;
		levelHeight = 0.0f;

		while (levelNum<4) {
			LoadLevel(levelNum, levelHeight);

			levelNum++;
			levelHeight += 22f;
		}


	}

	//This function takes care of loading multiple levels, selecting a main con and spawning other cons
	void LoadLevel(int levelNum, float levelHeight){
			//Load up level backbone at 0,0,0 for first level and fix the name in the Hierarchy
			currentLevel = Instantiate (Resources.Load ("Prefabs/Final/Level_Backbone"), new Vector3(0, levelHeight, 0), Quaternion.identity) as GameObject;
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
			
			Transform mainPos = (Transform)consoleTransforms [random]; //Get the transform we picked
			Debug.Log ("Randomly picked: " + mainPos + "for the main console!");
			
			//Instantiate the main console and immediately fix the name
			GameObject mainCon = Instantiate (Resources.Load ("Prefabs/Consoles/conMain" + (random + 1)), mainPos.position, mainPos.rotation) as GameObject;
			mainCon.name = "level" + levelNum + "_conMain" + (random + 1);
			mainCon.transform.parent = consolesInLevel.transform;
			
			//Remove main console from arraylist
			consoleTransforms.RemoveAt (random);
			
			//Spawn 0-4 secondary consoles (chance of spawning secondary is 66%
			
			foreach (Transform pos in consoleTransforms) {
				//Give secondary consoles 66% chance to spawn
				if(Random.value < 0.66f){
					Debug.Log ("Secondary consoles are: " + pos);
					GameObject buffer = Instantiate (Resources.Load ("Prefabs/Consoles/conGeneric"), pos.position, pos.rotation) as GameObject;
					buffer.name = "level" + levelNum + "_" + pos.name;
					buffer.transform.parent = consolesInLevel.transform;
				}
			}
			
			//Clear the array list
			consoleTransforms.Clear ();
	

		}
	}
