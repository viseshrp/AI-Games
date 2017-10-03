using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

	//reference to player to relate him with the camera
	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position // position of camera
			- player.transform.position; // position of player
	}
	
//	// Update is called once per frame
//	// Not appropriate or perfect for this purpose, so we switch to late update.
//	void Update () {
//		// make sure any movement in player is followed by movement in camera and offset/gap is maintained
//		// A.K.A Follow camera
//		transform.position = player.transform.position + offset;
//	}

	// Update Not appropriate or perfect for this purpose, so we switch to late update.
	// Guaranteed to run after all items have been processed in Update(), i.e player has completely moved.
	void LateUpdate () {
		// make sure any movement in player is followed by movement in camera and offset/gap is maintained
		// A.K.A Follow camera
		transform.position = player.transform.position + offset;
	}

}
