using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStarController : Enemy {

	public float StarSpeed = 10F;
	private float initialSpeed;
	private GameObject player;

	// Use this for initialization
	void Start () {
		initialSpeed = StarSpeed;
		print (getScale ());
		if (!throwFlip) {
			StarSpeed = -initialSpeed;
		} else {
			StarSpeed = initialSpeed;
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2 (StarSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}
	
	// Update is called once per frame
	void Update () {

		//Rotate the transform of the game object this is attached to by 45 degrees, taking into account the time elapsed since last frame.
		transform.Rotate (new Vector3 (0, 0, 1260) * Time.deltaTime);


		
	}

	void OnTriggerEnter2D(Collider2D other){
		//print ("Destroyed");
		if (other.gameObject.CompareTag ("Bullet_1") != true) {
			Destroy (gameObject); //Destroys NinjaStar when NinjaStar hits another game object
		}
	}
}
