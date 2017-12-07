using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour {

	public int startingHealth = 8;
	public int currentHealth;
	public Image healthImage;
	public Slider healthSlider;

	private Animator healthUI;
	private PlayerScript2 player;
	private bool damaged;
	private bool dead;
	private double time;
	private int SceneNum;

	// Use this for initialization
	void Start () {
		SceneNum = SceneManager.GetActiveScene ().buildIndex;
		print (SceneNum);
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
		print (currentHealth);
		if (currentHealth <= 0) {
			SceneManager.LoadScene (SceneNum);
			//player.GameOver ();
		} 
		else {
			healthSlider.value = currentHealth;
		}
		damaged = false;
	}
}
