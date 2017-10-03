using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;

	private int count;
	public Text countText;
	public Text winText;

	// Use this for initialization, called during the first ever frame
	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;
		updateCountText ();
		winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		// get axis of keyboard inputs
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		// determines the direction of the force we add to the player
		// new Vector3(x,y,z) -- x-horizontal, y-axis pointing up, z-- move vertical
		Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);

		// use input to apply forces to rigidbody to make it move in the direction of vector3 'movement'
		rb.AddForce(movement * speed);
		Debug.Log ("yelllo");
	}

	// called when player object comes in contact with "other" object.
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Pickup"))
		{ // need to declare Pickup tag in unity
			other.gameObject.SetActive(false);
			count++;
			updateCountText ();
		}
	}

	void updateCountText(){
		countText.text = "Score: " + count.ToString ();

		if (count >= 12)
			winText.text = "YOU WON!";
	}
}
