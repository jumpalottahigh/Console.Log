using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {

	//SETTINGS Public variables
	public Light lt;
	public Color off = Color.black;
	public Color white = Color.white;
	public Color sexy = Color.red;
	public Color green = Color.green;
	public Color current;

	//Public static bools to control stuff


	// Use this for initialization
	void Start () {
		lt = GetComponent<Light> ();
		current = lt.color;
	}

	void sexyLights(){
		current = lt.color;
		lt.color = Color.Lerp (current, sexy, 5f);
	}

	void greenLights(){
		current = lt.color;
		lt.color = Color.Lerp (current, green, 5f);
	}

	void whiteLights(){
		current = lt.color;
		lt.color = Color.Lerp (current, white, 5f);
	}

	void offLights(){
		current = lt.color;
		lt.color = Color.Lerp (current, off, 5f);
	}
}
