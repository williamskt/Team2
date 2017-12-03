using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float BulletSpeed;
	private GameObject player;


	// Use this for initialization
	void Start () {

		player = GameObject.Find ("PlayerBasic");

		//Problem: when player faces opposite direction, all bullets reverse, including bullets already fired
		if (player.transform.localScale.x < 0) {
			BulletSpeed = -BulletSpeed;
		} else {
			Mathf.Abs (BulletSpeed);
		}

	}
	
	// Update is called once per frame
	void Update () {
		//Adding velocity to bullet
		GetComponent<Rigidbody2D>().velocity = new Vector2 (BulletSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}

	void OnTriggerEnter2D(Collider2D other){
		print ("Destroyed");
		Destroy (gameObject); //Destroys bullet when bullet hits another game object
	}
}
