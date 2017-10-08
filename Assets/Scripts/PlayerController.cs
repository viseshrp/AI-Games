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

		obstacle.SetActive (false);
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
				towards = newPosition - transform.position; // find the vector to take
				//Debug.Log(newPosition);
				transform.rotation = Quaternion.LookRotation (towards, Vector3.right); //perform rotation towards click point

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



		// create obstacle on right click
		if (Input.GetMouseButtonDown (1)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition); //point camera in direction of mouse
			if (Physics.Raycast (ray, out hit)) {
				obstacle.SetActive (true);
				GameObject obj = Instantiate (obstacle, new Vector3 (hit.point.x, hit.point.y, hit.point.z), Quaternion.identity) as GameObject;
				obj.transform.position = new Vector3 (obj.transform.position.x, 0.5f, obj.transform.position.z);
			}
		}

		//current velocity of rigid body
		rb.velocity = newPosition - rb.position;
		//if the ball is within a particular radius, stop it
		if (rb.velocity.magnitude < satisfactionRadius) {
			rb.velocity = Vector3.zero;
		}

		AvoidObstacle ();
	}

	void AvoidObstacle ()
	{
		//CHECKING AND AVOIDING OBSTACLES
		RaycastHit hit2; //stores anything that is hit with the ray cast forward.
		Ray checkingRay = new Ray (transform.position, Vector3.forward);
		Debug.DrawRay (transform.position, Vector3.forward);
		if (Physics.Raycast (checkingRay, out hit2, 3.3f)) {
			if (hit2.transform != transform && hit2.collider.tag != "wall" && hit2.collider.tag != "ground" && hit2.collider.tag != "dudes") {
				// avoid obstacle
				Vector3 movement = new Vector3 (550.0f, 0.0f, 50.0f);
				rb.AddForce (movement * speed);
			}
		}

	}

	//call on colliding
	void OnCollisionEnter (Collision collision)
	{
		if (!collision.gameObject.CompareTag ("wall") && !collision.gameObject.CompareTag ("ground") && !collision.gameObject.CompareTag ("dudes")) { 
			Debug.Log (collision.gameObject.name);
			Vector3 movement = new Vector3 (300.0f, 0.0f, 0.0f);
			rb.AddForce (movement * speed);

		}
	}
}