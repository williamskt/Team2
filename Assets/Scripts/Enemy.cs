using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
	Animator enemy_animator;
	public GameObject Player;
	public GameObject NinjaStar;//Object the character will attack with
	public Transform AttackPoint;
	private float x_scaleE;		//x scale of enemy
	private float y_scaleE;		//y scale of enemy
	private float z_scaleE;		//z scale of enemy
	private float x_posE;		//x position of enemy
	private float y_posE;		//y position of enemy
	private float z_posE;		//z position of enemy
	private float x_posP;		//x position of player
	private float y_posP;		//y position of player
	private float z_posP;		//z position of player

	public int EnemyHealth = 12;	//Health points for enemy
	public float AttackDist = 12F;	//Distance when enemy starts attacking
	private float Dist;				//Variable to tell how far player is from enemy
	private Vector3 target;
	public float AttackRate = 0.1F;
	private float NextAttack = 0.0F;
	private bool Death = false;
	protected bool throwFlip = false;


	// Use this for initialization
	void Start () {

		enemy_animator = GetComponent<Animator> ();

		//Saving the inital x, y, z scales
		//If the enemy needs to be resized, this allows the code to adjust automatically
		//instead of recoding the values later on
		x_scaleE = transform.localScale.x;
		y_scaleE = transform.localScale.y;
		z_scaleE = transform.localScale.z;
	}
	
	// Update is called once per frame
	void Update () {

		if (Death) {
			//No calculations should occur if enemy is dead
		} else {
			//Updating position of player
			x_posP = Player.transform.position.x;
			y_posP = Player.transform.position.y;
			z_posP = Player.transform.position.z;

			//Updating position of enemy
			x_posE = transform.position.x;
			y_posE = transform.position.y;
			z_posE = transform.position.z;

			if (x_posP > x_posE) {
				transform.localScale = new Vector3 (-x_scaleE, y_scaleE, z_scaleE);
				throwFlip = true;
			} else if (x_posP < x_posE) {
				transform.localScale = new Vector3 (x_scaleE, y_scaleE, z_scaleE);
				throwFlip = false;
			}
			//Both instances below (Abs & Distance) work, but both output two numbers, don't know why
			//Dist = Mathf.Abs (x_posE - x_posP); 
			Dist = Vector3.Distance(Player.transform.position, transform.position);
			//print (Dist);

			if (Dist <= AttackDist) {
				enemy_animator.SetBool ("AttackToIdle", false);
				enemy_animator.SetBool ("IdleToAttack", true);

				if (Time.time > NextAttack) {
					NextAttack = Time.time + AttackRate;
					Instantiate (NinjaStar, AttackPoint.position, AttackPoint.rotation);
				}
			} else {
				enemy_animator.SetBool ("IdleToAttack", false);
				enemy_animator.SetBool ("AttackToIdle", true);
			}
		}

		
	}

	IEnumerator OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Bullet_1")) {
			//print ("hitEnemy");

			if (EnemyHealth >= 1) {
				EnemyHealth -= 1;
				//print (EnemyHealth);
			}
			else if (EnemyHealth < 1) {	//Enemy dies if all health is removed
				Death = true;
				enemy_animator.SetBool("IdleToAttack", false);
				enemy_animator.SetBool ("AttackToIdle", false);
				enemy_animator.SetBool ("Death", true);
				yield return new WaitForSeconds (1);
				Destroy (gameObject);
			}
		}
	}

	void Attack(){

	}

	public float getScale(){
		return transform.lossyScale.x;
	}
}
