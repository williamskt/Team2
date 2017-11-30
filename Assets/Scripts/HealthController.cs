using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : PlayerScript2 {

	public GameObject player;
	public Sprite h8, h7, h6, h5, h4, h3, h2, h1;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerBasic");
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (getHealth () == 8) {
			sr.sprite = h8;
		}
		else if (getHealth () == 7) {
			sr.sprite = h7;
		}
		else if (getHealth () == 6) {
			sr.sprite = h6;
		}
		else if (getHealth () == 5) {
			sr.sprite = h5;
		}
		else if (getHealth () == 4) {
			sr.sprite = h4;
		}
		else if (getHealth () == 3) {
			sr.sprite = h3;
		}
		else if (getHealth () == 2) {
			sr.sprite = h2;
		}
		else if (getHealth () == 1) {
			sr.sprite = h1;
		}
	}
}
