//AUTHOR: Dast

﻿using UnityEngine;
using System.Collections.Generic;
using Assets;
using System.Linq;
using System.Text;

public class BasicAI : MonoBehaviour {
	private static CombatContext context;
	private GameObject playerObject;
	private Character target;
	private Vector3 destination;
	public bool forward, back, left, right = false;
	private int tickCount = 0;

	public List<Character> db_npcs;
	public int db_attackDelay = 800;

	//////////////////////////////////////////////////////////////////////////////
	////////////////////// Pathfinding and Positioning ///////////////////////////

	void assignEnemyPosition(Character i){
		if ( i.getPosition() > 0 && i.getPosition() != 5 )
			return;

		int pos = Random.Range(1,5);

		//Assigns a position randomly, ensuring no two characters stand in the
		//same spot, while also restricting the number present to 4 at a time
		while (true) {
			if (pos == 1 && !forward){
				forward = true;
				break;
			} else if (pos == 2 && !back) {
				back = true;
				break;
			} else if (pos == 3 && !left) {
				left = true;
				break;
			} else if (pos == 4 && !right) {
				right = true;
				break;
			} else if (forward && back && left && right) {
				pos = 5;
				break;
			} else {
				pos = Random.Range(1,5);
			}
		}

		i.setPosition(pos);
	}

	void assignEnemyDestinations(Character i){
		destination = target.transform.position;
		if ( i.getPosition() == 1 ){
			destination = target.transform.position - Vector3.forward;
		}		else if ( i.getPosition() == 2) {
			destination = target.transform.position - Vector3.back;
		} else if (i.getPosition() == 3) {
			destination = target.transform.position - Vector3.left;
		} else  if (i.getPosition() == 4){
			destination = target.transform.position - Vector3.right;
		} else { //position = 5
			destination = i.getCharacterNavMesh().transform.position;
		}
	}

	void moveActor(Character enemy){
		enemy.getCharacterNavMesh().SetDestination(destination);
	return;
	}


	public void reassignNPC(int pos) {
		if (pos == 1){
			forward = false;
		} else if (pos == 2) {
			back = false;
		} else if (pos == 3) {
			left = false;
		} else if (pos == 4) {
			right = false;
		} else {
			return;
		}


		foreach (Character i in db_npcs) {
			if (i.getPosition() == 5)
				i.setPosition(0);

				break;
		}
	}
	//////////////////////////////////////////////////////////////////////////////


	//////////////////////////////////////////////////////////////////////////////
	////////////////////////// COMBAT MANAGEMENT /////////////////////////////////


	//////////////////////////////////////////////////////////////////////////////


	// Use this for initialization
	void Start () {
		context = FindObjectOfType (typeof(CombatContext)) as CombatContext;
		playerObject = GameObject.Find("Player");
		db_npcs = context._all;
		target = playerObject.GetComponent<Character> ();
	}

	// Update is called once per frame
	//void Update () {
	public void Tick(){
			//Handles each Character one at a time
			//outline:
			//Set their position
			//check if combat ready
			//make attack, and don't move while attacking
			//move towards the actor if not attacking
			foreach (Character i in db_npcs) {
				if (i.aggro == -1){
					if ( !i.isIncapacitated() ){
						if (i != null){
							if (tickCount > 40){
								if ( Vector3.Distance(i.transform.position, target.transform.position) < 10.0 ) {
									if ( Vector3.Distance(i.transform.position, target.transform.position) < 3.0 && tickCount == db_attackDelay) {
											Debug.Log("Attack!");
											i.Attack(target);
											tickCount = 0;
										} else {
										assignEnemyPosition(i);
										assignEnemyDestinations (i);
										moveActor(i);
										}
								}
							}
							if (tickCount != db_attackDelay) {
								tickCount++;
							}
						}
					}
				}
			}
		}
	}
