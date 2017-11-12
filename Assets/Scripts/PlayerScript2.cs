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

	// Use this for initialization
	void Start () {
		my_animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	

	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		transform.position += new Vector3 (moveHorizontal, 0f, 0f) * Time.deltaTime * speed;


		if(Input.GetButton("Jump") && isGrounded){	//Jumping
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

		else if (moveHorizontal > 0){		//Moving right (while jumping or running)
			transform.localScale = new Vector3 (0.2f, 0.2f, 0f);

			if (isGrounded) {				//Moving right while running
				my_animator.SetBool ("isWalkingB", true);
				my_animator.SetBool ("isIdleB", false);
			}
		}
		else if (moveHorizontal < 0){		//Moving left (while jumping or running)
			transform.localScale = new Vector3 (-0.2f, 0.2f, 0f);

			if (isGrounded) {				//Moving left while running
				my_animator.SetBool ("isWalkingB", true);
				my_animator.SetBool ("isIdleB", false);
			}
		} 
		else if (isGrounded){				//Idle, if player is on ground
			my_animator.SetBool ("isIdleB", true);
			my_animator.SetBool ("isWalkingB", false);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}
	}

	//Checking to see if player is touching the ground
	//Making sure the jumping boolean is set to false, if player is on the ground
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("floor")) {
			print ("ground");
			isGrounded = true;
			my_animator.SetBool ("isJumpingB", false);
		}
	}
}
