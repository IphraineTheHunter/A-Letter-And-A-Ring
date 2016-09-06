//AUTHOR: Dast

﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	//TODO: Change this so when it's instantiated, it just grabs the player
	//from here, instead of setting the player by hand in the engine
	void Start () {
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame after all items are processed
	//TODO: Why did I set this as LateUpdate?
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
