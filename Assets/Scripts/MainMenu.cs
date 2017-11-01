using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Defining variables for different menu buttons
	public bool isStart;		// Variable for Start button
	public bool isSettings;		// Variable for Settings button
	public bool isExit;			// Variable for Exit button

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp () {
		if (isStart) {
			SceneManager.LoadScene(1);
			//Application.LoadLevel (1);

		} else if (isSettings) {

		} else if (isExit) {
			Application.Quit();
		}
	}
}
