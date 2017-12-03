using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	Animator enemy_animator;
	private float x_pos;
	private float y_pos;
	private float z_pos;

	// Use this for initialization
	void Start () {

		enemy_animator = GetComponent<Animator> ();

		//Saving the inital x, y, z scales
		//If the character needs to be resized, this allows the code to adjust automatically
		//instead of recoding the values later on
		x_pos = transform.localScale.x;
		y_pos = transform.localScale.y;
		z_pos = transform.localScale.z;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Bullet_1")) {
			print ("hitEnemy");
			enemy_animator.SetBool ("Death", true);
		}
	}
}
