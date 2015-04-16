using UnityEngine;
using System.Collections;

public class Elevator1 : MonoBehaviour {

	public bool playerNear;
	public bool isUp;
	public bool newLevelLoad;
	
	public Transform spawnPoint;
	public GameObject player;
	public GameObject currentLevel;
	public Object nextLevel;

	public Transform elev;
	public Transform down;
	public Transform up;
	public float speed;

	// Use this for initialization
	void Start () {
		playerNear = false;
		isUp = false;
		speed = 0.25f;
		elev = GameObject.Find("Elevator").transform;
		up = GameObject.Find ("el_up").transform;
		down = GameObject.Find ("el_down").transform;
		player = GameObject.FindGameObjectWithTag("Player");
		spawnPoint = GameObject.Find ("spawnPoint").transform;
		currentLevel = GameObject.Find ("currentLevel");
		newLevelLoad = false;
	}



	// Update is called once per frame
	void Update () {
		if (playerNear && !isUp) {
			elev.Translate (Vector3.up * speed);
			if(Vector3.Distance(elev.position, up.position) <0.05f){
				isUp = true;
				spawnNext();
			}
		}

		if (newLevelLoad) {
			Start ();
			newLevelLoad = false;
		}
	}

	void OnTriggerEnter(Collider other){

		if(other.CompareTag("Player")){
			playerNear = true;
			other.transform.parent = gameObject.transform;
		}

	}

	void OnTriggerExit(Collider other) {
		playerNear = false;
		other.transform.parent = null;
		if (!playerNear) {
			newLevelLoad = true;
			GameObject.Destroy (currentLevel);
			nextLevel.name = "currentLevel";
		}
	}
	
	void spawnNext() {
		if (isUp){
			float random = Mathf.Round(Random.Range(1,4)*90);
			Debug.Log(random);
			nextLevel = Instantiate(Resources.Load("Level_Backbone"), spawnPoint.position, Quaternion.Euler(0,random,0));
			currentLevel.name = "oldLevel";
		}
	}

}
