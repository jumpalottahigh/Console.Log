using UnityEngine;
using System.Collections;

public class score : MonoBehaviour {

	private int playerScore;

	public static bool majorEventCompleted = false;
	public static bool miniEventCompleted = false;



	// Use this for initialization
	void Start () {
		//reset score at init
		playerScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (majorEventCompleted) {
			playerScore += 100;
			majorEventCompleted = false;
		}

		if (miniEventCompleted) {
			playerScore +=20;
			miniEventCompleted = false;
		}
	}

	void OnGUI(){
		GUI.Label(new Rect(Screen.width-200,30,Screen.width/4,Screen.height/4), "<color=orange><size=40>Score: "+playerScore+"</size></color>");
	}
}
