﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript2 : MonoBehaviour {

	private Animator my_animator;
	private Rigidbody2D rb;
	public Transform ShootPoint;	//Point for character to shoot from
	public GameObject Bullet_1; //Object the character will shoot

	public float jumpheight = 2f;
	public float speed = 10f;

	private float x_pos;		//x position of player
	private float y_pos;		//y position of player
	private float z_pos;		//z position of player
	public float ShootRate = 0.05F;		//Rate of shooting if shoot button held down
	private float NextShoot = 0.0F;		//Empty variable to hold time of next shot

	private int health = 8;		//Player health

	private bool isGrounded = true;		//Variable to tell if player is standing/running on ground
	private bool Gun = false;	//Variable to tell if the player has picked up gun





	// Use this for initialization
	void Start () {
		my_animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		//Saving the inital x, y, z scales
		//If the character needs to be resized, this allows the code to adjust automatically
		//instead of recoding the values later on
		x_pos = transform.localScale.x;
		y_pos = transform.localScale.y;
		z_pos = transform.localScale.z;
	}
	

	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		transform.position += new Vector3 (moveHorizontal, 0f, 0f) * Time.deltaTime * speed;


		if (Gun) { //If the player has collected the gun

			if (Input.GetButton ("Jump") && isGrounded) {	//Jumping
				my_animator.SetBool ("GunJump", true);
				my_animator.SetBool ("GunRun", false);
				my_animator.SetBool ("GunIdle", false);
				my_animator.SetBool ("GunIdleShoot", false);

				//rb.AddForce (new Vector2 (0f, 500f));
				rb.velocity = new Vector2 (0f, jumpheight);
				isGrounded = false;
			} 
			else if (moveHorizontal > 0) {		//Moving right (while jumping or running)
				transform.localScale = new Vector3 (x_pos, y_pos, z_pos);

				if (isGrounded) {				//Moving right while running
					my_animator.SetBool ("GunRun", true);
					my_animator.SetBool ("GunIdle", false);
					my_animator.SetBool ("GunIdleShoot", false);

					//Firing Gun
					Invoke ("Shoot", 0);	//Calling Shoot function
				}
			} 
			else if (moveHorizontal < 0) {		//Moving left (while jumping or running)
				transform.localScale = new Vector3 (-x_pos, y_pos, z_pos);

				if (isGrounded) {				//Moving left while running
					my_animator.SetBool ("GunRun", true);
					my_animator.SetBool ("GunIdle", false);
					my_animator.SetBool ("GunIdleShoot", false);

					//Firing Gun
					Invoke ("Shoot", 0);	//Calling Shoot function
				}
			} 
			else if (isGrounded) {				//Idle, if player is on ground
				//Firing Gun
				if (Input.GetButton ("Fire1")) {
					my_animator.SetBool ("GunRun", false);
					my_animator.SetBool ("GunJump", false);
					my_animator.SetBool ("GunIdle", false);
					my_animator.SetBool ("GunIdleShoot", true);

					//Firing Gun
					Invoke ("Shoot", 0);	//Calling Shoot function

				} else {
					my_animator.SetBool ("GunIdleShoot", false);
					my_animator.SetBool ("GunIdle", true);
					my_animator.SetBool ("GunRun", false);
				}
			}

		}
		else {

			if (Input.GetButton ("Jump") && isGrounded) {	//Jumping
				my_animator.SetBool ("isJumpingB", true);
				my_animator.SetBool ("isWalkingB", false);
				my_animator.SetBool ("isIdleB", false);

				//rb.AddForce (new Vector2 (0f, 500f));
				rb.velocity = new Vector2 (0f, jumpheight);
				isGrounded = false;
			}
			//Testing Double Jump, needs some work
			//else if(Input.GetButton("Jump") && (isGrounded == false)){
			//rb.velocity = new Vector2 (0f, jumpheight);
			//}

			else if (moveHorizontal > 0) {		//Moving right (while jumping or running)
				transform.localScale = new Vector3 (x_pos, y_pos, z_pos);

				if (isGrounded) {				//Moving right while running
					my_animator.SetBool ("isWalkingB", true);
					my_animator.SetBool ("isIdleB", false);
				}
			} else if (moveHorizontal < 0) {		//Moving left (while jumping or running)
				transform.localScale = new Vector3 (-x_pos, y_pos, z_pos);

				if (isGrounded) {				//Moving left while running
					my_animator.SetBool ("isWalkingB", true);
					my_animator.SetBool ("isIdleB", false);
				}
			} else if (isGrounded) {				//Idle, if player is on ground
				my_animator.SetBool ("isIdleB", true);
				my_animator.SetBool ("isWalkingB", false);
			}
		}



		// Letting Player go to main menu when pressing escape key
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}

	}

	//Checking to see if player is touching the ground
	//Making sure the jumping boolean is set to false if player is on the ground
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("floor")) {
			isGrounded = true;
			if (Gun) {
				my_animator.SetBool ("GunJump", false);
			} else {
				my_animator.SetBool ("isJumpingB", false);
			}
		}
		//Removes health if player comes in contact with enemy
		else if (other.gameObject.CompareTag ("enemy")) {
			health--;
			print ("Ouch. Health: " + health);
			if (health == 0) {
				SceneManager.LoadScene (2);
			}

		} //Checking to see if player picked up Gun 
		else if (other.gameObject.CompareTag ("Gun")) {
			Gun = true;
			other.gameObject.SetActive (false);
			my_animator.SetBool ("isJumpingB", false);
			my_animator.SetBool ("isWalkingB", false);
			my_animator.SetBool ("isIdleB", false);

			if (isGrounded) {
				my_animator.SetBool ("GunRun", true);
			} else {
				my_animator.SetBool ("GunJump", true);
			}

		} //Checking to see if player landed in Lava
		//If yes, then player gets sent back to start menu
		else if (other.gameObject.CompareTag ("Lava")) {
			SceneManager.LoadScene (0);
		}
	}

	public int getHealth(){
		return health;
	}

	//When called, shoots gun at a certain rate if the fire button is pressed
	void Shoot (){
		if (Input.GetButton ("Fire1") && Time.time > NextShoot) {
			NextShoot = Time.time + ShootRate;
			Instantiate (Bullet_1, ShootPoint.position, ShootPoint.rotation);
		}
	}

}
