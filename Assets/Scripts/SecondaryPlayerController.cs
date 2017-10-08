using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryPlayerController : MonoBehaviour
{

	//reference to player to relate him with the camera
	public GameObject player;

	private Vector3 offset;

	public float speed;
	//public vars shows up in inspector as property
	private Rigidbody rb;
	//the rigid body to be attached to the sphere/object to perform physical actions on it.
	private Transform target;
	//holds static data for the target character

	private float maxSpeed;
	//max speed character can travel

	public float satisfactionRadius = .1f;
	//satisfaction radius

	public float timeToTarget = 0.25f;

	Vector3 newPosition;

	Vector3 towards;

	public GameObject obstacle;

	public int range;

	// Use this for initialization
	// called on the first frame when the script is active
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		maxSpeed = 10.0f;

		speed = 10.0f;
		range = 80;

		offset = transform.position// position of camera
		- player.transform.position; // position of player
	}

	void Update() {
		transform.position = player.transform.position + offset;
		AvoidObstacle ();
	}

	// Update Not appropriate or perfect for this purpose, so we switch to late update.
	// Guaranteed to run after all items have been processed in Update(), i.e player has completely moved.
	void LateUpdate ()
	{
		// make sure any movement in player is followed by movement in camera and offset/gap is maintained
		// A.K.A Follow camera
		transform.position = player.transform.position + offset;
	}

	void AvoidObstacle ()
	{
		//CHECKING AND AVOIDING OBSTACLES
		RaycastHit hit2; //stores anything that is hit with the ray cast forward.
		Ray checkingRay = new Ray (transform.position, Vector3.forward);
		Debug.DrawRay (transform.position, Vector3.forward);
		if (Physics.Raycast (checkingRay, out hit2, 3.3f)) {
			// also, do not avoid if the object is a wall or ground or other balls
			if (hit2.transform != transform && hit2.collider.tag != "wall" && hit2.collider.tag != "ground" && hit2.collider.tag != "dudes") {
				//avoid obstacle
				Vector3 movement = new Vector3 (150.0f, 0.0f, 0.0f);
				rb.AddForce (movement * speed);
			}
		}

	}

	//called when entering
	void OnCollisionEnter (Collision collision)
	{
		if (!collision.gameObject.CompareTag ("wall") && !collision.gameObject.CompareTag ("ground") && !collision.gameObject.CompareTag ("dudes")) { 
			Debug.Log (collision.gameObject.tag);
			Vector3 movement = new Vector3 (200.0f, 0.0f, 0.0f);
			rb.AddForce (movement * speed);

		}
	}

		
}
