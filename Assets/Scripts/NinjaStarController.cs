using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStarController : MonoBehaviour {

	public float StarSpeed = 10F;
	private float StarX;
	private float realDir;
	private float StarDir;
	private float PlayerX;
	private GameObject player;

	//Use this for initialization
	void Start () {

		player = GameObject.Find ("PlayerBasic");
		PlayerX = player.transform.position.x;
		StarX = transform.position.x;

		realDir = PlayerX - StarX;

		//print (realDir);

		if (realDir <= 0.05) {
			//print ("minus 3");
			print (StarX);
			StarX = transform.position.x - 3f;
		} else {
			StarX = transform.position.x + 3f;
		}

		StarDir = PlayerX - StarX;

		if (StarDir < 0) {
			//print ("facing right");
			StarSpeed = -StarSpeed;
		} else {
			StarSpeed = Mathf.Abs (StarSpeed);
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
