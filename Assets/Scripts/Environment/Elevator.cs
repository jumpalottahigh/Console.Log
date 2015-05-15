using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	private GameObject player;
	private GameObject elevBtn;
	private bool inTransition;
	private bool haveArrived;

	//Elevator animation clips
	public Animation leftDoor;
	public Animation rightDoor;
	//public Animation leftDoorClose;
	//public Animation rightDoorClose;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		elevBtn = GameObject.Find ("button");
		inTransition = false;
		haveArrived = false;
	}

	void Update(){
		if (Vector3.Distance (elevBtn.transform.position, player.transform.position) < 2f && Input.GetKeyDown(KeyCode.E) && !inTransition) {
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
		transform.parent = null;
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
		Invoke ("OpenDoors", 3);
	}

	void HaveArrived(){
		rightDoor.Play("doorRightClose");
		leftDoor.Play ("doorLeftClose");
		this.name = "";
		this.tag = "Untagged";
	}
}
