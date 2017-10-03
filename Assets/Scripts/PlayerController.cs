using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

	// Use this for initialization
	// called on the first frame when the script is active
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		maxSpeed = 10f;
	}

	// Update is called once per frame and before rendering a frame, where most of our game code will go.
	void Update ()
	{
		//get mouse click and trigger movement
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //point camera in direction of mouse
			if (Physics.Raycast (ray, out hit)) {
				newPosition = hit.point; // set new/target position as mouse click point
				Vector3 towards = newPosition - transform.position; // find the vector to take
				transform.rotation = Quaternion.LookRotation (towards); //perform rotation towards click point

				// get to target in timeToTarget seconds
				towards /= timeToTarget;

				//normalize and reduce speed as ball reaches target point
				if (towards.magnitude > maxSpeed) {
					towards.Normalize ();
					towards *= maxSpeed;
				}

				//set velocity vector of rigid body
				rb.velocity = towards;
			}
		}

		//current velocity of rigid body
		rb.velocity = newPosition - rb.position;
		//if the ball is within a particular radius, stop it
		if (rb.velocity.magnitude < satisfactionRadius) {
			rb.velocity = Vector3.zero;
		}
	}
}