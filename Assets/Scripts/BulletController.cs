using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float BulletSpeed;
	public int enemyHealth = 0;
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



		GetComponent<Rigidbody2D>().velocity = new Vector2 (BulletSpeed, GetComponent<Rigidbody2D>().velocity.y);
	}

	void OnCollisionEnter2D(Collider2D other){

		if (other.gameObject.CompareTag ("enemy")) {
			//Code not quite working, only takes -1 off enemy health once, then never again
			//enemyHealth = enemyHealth - 1;
			print (enemyHealth);
			print ("hit");

			if (enemyHealth < 1) {
				Destroy (other.gameObject);
			}
		}

		Destroy (gameObject); //Destroys bullet when bullet hits another game object
	}
}
