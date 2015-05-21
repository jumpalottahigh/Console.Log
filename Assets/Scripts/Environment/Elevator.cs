using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	private GameObject player;
	private GameObject elevBtn;
	private GameObject elevator;
	private bool inTransition;
	private bool haveArrived;
	private bool elevBtnFound;

	//Elevator animation clips
	public Animation leftDoor;
	public Animation rightDoor;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		elevBtn = GameObject.Find ("button");
		inTransition = false;
		haveArrived = false;
		elevBtnFound = false;
	}

	void Update(){
		//Find the right elevator button
		if (!elevBtnFound) {
			elevBtn = GameObject.Find ("button");
			if (elevBtn != null)
				elevBtnFound = true;
		} else {
			if(elevBtn.name !="button")
				elevBtnFound = false;
		}

		if (Vector3.Distance (elevBtn.transform.position, player.transform.position) < 2f && Input.GetKeyDown(KeyCode.E) && !inTransition) {
			//Simple button in animation
			while(elevBtn.transform.localPosition.x > -2.165f){
				elevBtn.transform.localPosition -= Vector3.right * Time.deltaTime;
				elevBtn.GetComponent<Renderer>().material.mainTexture = (Texture) Resources.Load("button_red");

				//Play elevator button pressed sound
				AudioClip ebp = (AudioClip) Resources.Load("Sounds/Environment/ElevatorButtonPressed");
				AudioSource.PlayClipAtPoint(ebp, transform.position, 0.1f);
			}

			inTransition = true;
			CloseDoors();
		}

		if (Vector3.Distance (transform.position, player.transform.position) > 10f && haveArrived) {
			haveArrived = false;
			HaveArrived();
		}

	}

	void OpenDoors(){
		//Fix parenting and open doors
		if (!haveArrived) {
			transform.parent = null;
		}
	
		if (haveArrived) {
			//Play elevator arrived sound
			AudioClip arr = (AudioClip)Resources.Load ("Sounds/Environment/ElevatorArrived");
			AudioSource.PlayClipAtPoint (arr, transform.position, 0.1f);
		}

		rightDoor.Play ("doorRightOpen");
		leftDoor.Play ("doorLeftOpen");
	}

	void CloseDoors(){
		//Close doors
		rightDoor.Play ("doorRightClose");
		leftDoor.Play ("doorLeftClose");

		//Destroy current level
		if (!leftDoor.isPlaying) {
			gameInit.removeCurrentLevel = true;
			//transform.rotation.eulerAngles = Vector3.zero;
		}
		Invoke ("LoadNext", 2);
	}

	void LoadNext(){
		gameInit.loadNextLevel = true;
		haveArrived = true;
		Invoke ("OpenDoors", 2);
	}

	void HaveArrived(){
		rightDoor.Play ("doorRightClose");
		leftDoor.Play ("doorLeftClose");
		transform.tag = "Untagged";
		transform.name = "";
		elevBtn.name = "";
		transform.parent = GameObject.FindGameObjectWithTag ("level").transform;
		GetComponent<Elevator> ().enabled = false;

		//boolean resets
		//elevBtnFound = false;
	}

}
