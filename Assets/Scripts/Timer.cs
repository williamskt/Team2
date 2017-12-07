using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public GameObject player;
	public Text timer;
	private PlayerScript2 playerScript;

	// Use this for initialization
	void Start () {
		playerScript = player.GetComponent<PlayerScript2> ();
		timer.text = playerScript.timeLeft.ToString ();
	}

	// Update is called once per frame
	void Update () {
		timer.text = playerScript.timeLeft.ToString ("#.00");
	}
}
