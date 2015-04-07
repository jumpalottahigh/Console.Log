using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public bool playerNear = false;

	public Transform elevator;

	// Use this for initialization
	void Start () {
		elevator = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
