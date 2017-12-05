using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : PlayerScript2 {

	public Text timer;

	// Use this for initialization
	void Start () {
		timer.text = timeLeft.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		timer.text = getTime().ToString ();
	}
}
