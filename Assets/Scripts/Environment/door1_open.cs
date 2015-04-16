using UnityEngine;
using System.Collections;

public class door1_open : MonoBehaviour {

	public bool locked = true;
	public bool playerNear = false;

	public Transform[] doorTransforms;

	public Transform door_left;
	public Transform door_right;
	public Transform leftEnd;
	public Transform rightEnd;
	public Transform leftStart;
	public Transform rightStart;

	// Use this for initialization
	void Start () {
		doorTransforms = this.GetComponentsInChildren<Transform>();
		door_left = doorTransforms[1];
		door_right = doorTransforms[2];
		leftEnd = doorTransforms [3];
		rightEnd = doorTransforms [4];
		leftStart = doorTransforms [5];
		rightStart = doorTransforms [6];
	}
	
	// Update is called once per frame
	void Update () {
		if (!locked) {
			if (playerNear) {
				door_left.position = Vector3.Lerp (door_left.position, leftEnd.position, Time.deltaTime);
				door_right.position = Vector3.Lerp (door_right.position, rightEnd.position, Time.deltaTime);
			} else {
				door_left.position = Vector3.Lerp (door_left.position, leftStart.position, Time.deltaTime);
				door_right.position = Vector3.Lerp (door_right.position, rightStart.position, Time.deltaTime);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player")){
			//Debug.Log("Player enter");
			playerNear = true;
		}

	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")){
			//Debug.Log("Player LEFT");
			playerNear = false;
		}
	}

	void Lock(){
		locked = true;
	}

	void Unlock(){
		locked = false;
	}


}
