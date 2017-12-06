using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public int startingHealth = 8;
	public int currentHealth;
	public Image healthImage;

	private Animator healthUI;
	private PlayerScript2 player;
	private bool damaged;
	private bool dead;
	private double time;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerScript2> ();
		currentHealth = startingHealth;
		healthUI = healthImage.GetComponent<Animator> ();
		healthUI.speed = 0;
		time = 0;
	}

	void Update () {

	}
	
	public void TakeDamage(){
		damaged = true;
		currentHealth--;
		if (currentHealth <= 0) {
			player.GameOver ();
		} 
		else {
			time += 0.0833333333;
			//healthUI.PlayInFixedTime (time);
		}
		damaged = false;
	}
}
