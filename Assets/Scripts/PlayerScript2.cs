using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript2 : MonoBehaviour {

	public float speed = 10f;
	Animator my_animator;
	Rigidbody2D rb;
	public float jumpheight = 2f;
	bool isGrounded = true;
	private float x_pos;
	private float y_pos;
	private float z_pos;
	private int health = 8;
	private bool Gun = false;

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


		if (Gun) {

			if (Input.GetButton ("Jump") && isGrounded) {	//Jumping
				my_animator.SetBool ("GunJump", true);
				my_animator.SetBool ("GunRun", false);
				my_animator.SetBool ("GunIdle", false);

				//rb.AddForce (new Vector2 (0f, 500f));
				rb.velocity = new Vector2 (0f, jumpheight);
				isGrounded = false;
			}

			else if (moveHorizontal > 0) {		//Moving right (while jumping or running)
				transform.localScale = new Vector3 (x_pos, y_pos, z_pos);

				if (isGrounded) {				//Moving right while running
					my_animator.SetBool ("GunRun", true);
					my_animator.SetBool ("GunIdle", false);
				}
			} else if (moveHorizontal < 0) {		//Moving left (while jumping or running)
				transform.localScale = new Vector3 (-x_pos, y_pos, z_pos);

				if (isGrounded) {				//Moving left while running
					my_animator.SetBool ("GunRun", true);
					my_animator.SetBool ("GunIdle", false);
				}
			} else if (isGrounded) {				//Idle, if player is on ground
				my_animator.SetBool ("GunIdle", true);
				my_animator.SetBool ("GunRun", false);
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
			my_animator.SetBool ("isIdleB", true);
		}
	}

}
