using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerBasic");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
