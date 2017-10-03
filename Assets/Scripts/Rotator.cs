using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	// to rotate the cubes, dont set the transform's rotation,
	// instead rotate it.
	void Update () {
		transform.Rotate (new Vector3(15,30,45) * Time.deltaTime); //Pass euler angles,
		// to smooth rotation and make it frame-rate independent, multiply by Time.deltatime
	}
}
