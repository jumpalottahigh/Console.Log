using UnityEngine;
using System.Collections;

public class Elevator1 : MonoBehaviour {

	public bool playerNear = false;
	public bool isUp = false;
	//public bool spawnNext = false;
	
	public Transform spawnPoint;
	public GameObject player;

	public Transform elev;
	public Transform down;
	public Transform up;
	public float speed = 0.01f;

	// Use this for initialization
	void Start () {
		elev = this.transform;
		up = GameObject.Find ("el_up").transform;
		down = GameObject.Find ("el_down").transform;
		player = GameObject.FindGameObjectWithTag("Player");
		spawnPoint = GameObject.Find ("spawnPoint").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerNear && !isUp) {
			elev.Translate (Vector3.up * speed);
			if (Vector3.Distance (elev.position, up.position) < 0.05f) {
				isUp = true;
			}
		}

		//if (Vector3.Distance (player.transform.position, up.position) < 5f) {
			//Instantiate("level2", spawnPoint.position);
		//}

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
	}
	
	void spawnNext() {
		if (isUp){
			Instantiate("level2", spawnPoint.position, Quaternion.identity); //as GameObject;
		}
	}

}
