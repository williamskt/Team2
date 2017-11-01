﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float speed = 10f;
	Animator my_animator;
	Rigidbody2D rb;
	public float jumpheight = 2f;
	bool isGrounded = true;

	// Use this for initialization
	void Start () {
		my_animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		transform.position += new Vector3 (moveHorizontal, 0f, 0f) * Time.deltaTime * speed;
		if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
			my_animator.SetTrigger ("isJumping");
			rb.velocity = new Vector2 (0f, jumpheight);
			isGrounded = false;
		}
		else if (moveHorizontal > 0) {
			my_animator.SetTrigger ("isWalking");
			//transform.localScale = new Vector3 (1f, 1f, 1f);
		}
		else if (moveHorizontal < 0) {
			my_animator.SetTrigger ("isWalking");
			//transform.localScale = new Vector3 (-1f, 1f, 1f);
		} 
		else {
			my_animator.SetTrigger ("isIdle");
		}
	}

	void OnCollisionEnter(Collision2D other){
		if (other.collider.CompareTag ("floor")) {
			isGrounded = true;
		}
	}
}
