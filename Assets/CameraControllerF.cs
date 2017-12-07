using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerF : MonoBehaviour {

	public GameObject player;

	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (player.transform.position.x + 6, player.transform.position.y, -5);
	}
}
