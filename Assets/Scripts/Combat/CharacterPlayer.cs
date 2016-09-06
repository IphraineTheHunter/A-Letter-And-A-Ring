//AUTHOR: Dast

﻿using UnityEngine;
using System.Collections;

public class CharacterPlayer : Character {

	//This will be overloaded by a Player object
	public override int GetFocus(){
		int focus = _focus; //Random.Range(1, 10);
		if (isCrippled){
			focus = 2*focus/3;
		}

		db_lastfoc = focus;
		return focus;
	}

	public void setFocus(int focus){
		_focus = focus;
	}

	public override void Start() {
		_self =  GetComponent<NavMeshAgent> ();
		currentHealth = health;

		for (int i = 0; i < 9; i++) {
			_bodyParts.Add( new Injury(health) );
		}
	}

	public override void GenerateStats(int level){
		level = _level;
		finesse = (2 * finesse + Random.Range (1, 100)) * level / 100 + 5;
		might = (2 * might + Random.Range (1, 100)) * level / 100 + 5;
		wit = (2 * wit + Random.Range (1, 100)) * level / 100 + 5;
		perceptiveness = (2 * perceptiveness + Random.Range (1, 100)) * level / 100 + 5;
		toughness = (2 * toughness + Random.Range (1, 100)) * level / 100 + 5;
		willpower = (2 * willpower + Random.Range (1, 100)) * level / 100 + 5;
		focusLimit = (2 * focusLimit + Random.Range (1, 100)) * level / 100 + 5;
		health = (2 * health + Random.Range (1, 100)) * level / 100 + 5;
		currentHealth = health;
		//printStats();
	}

	override public void Death() {
		//TODO: Death Animation code here
		//_AI.reassignNPC(_position);
		//_context.removeCharacter(this);
		//Destroy(this.gameObject);
	}

}
