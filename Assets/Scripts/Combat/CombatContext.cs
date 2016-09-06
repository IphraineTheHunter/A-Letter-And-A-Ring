//AUTHOR: Dast

﻿using UnityEngine;
using System.Collections.Generic;
using Assets;
using System.Linq;
using System.Text;

public class CombatContext : MonoBehaviour {

	public static CombatContext context = null;
	private BasicAI _AI;
	private PlayerController _player;
	public List<Character> _neutral = new List<Character>();
	public List<Character> _enemy = new List<Character>();
	public List<Character> _friendly = new List<Character>();
	public List<Character> _all = new List<Character>();
	public bool battleMode = false; //assume Wander mode
	public int level = 0;
	//public ActionMenu = new ActionMenu();

	public int getLevel(){
		return level;
	}

	public void removeCharacter(Character i){
		if ( i.aggro == -1 ){
			_enemy.Remove(i);
		} else if ( i.aggro == 0 ) {
			_neutral.Remove(i);
		} else {
			_friendly.Remove(i);
		}
	}
	// Use this for initialization
	void Start () {
		Character[] activeCharacters = FindObjectsOfType(typeof(Character)) as Character[];
		foreach (Character i in activeCharacters){
			_all.Add(i);
			if (i.tag == "Player") {
				i.GenerateStats(level);
			} else if (i.aggro == -1){
				_enemy.Add(i);
				i.GenerateStats(level);
			} else if (i.aggro == 0){
				_neutral.Add(i);
				i.GenerateStats(level);
			} else {
				_friendly.Add(i);
				i.GenerateStats(level);
			}
		}
		_AI = (BasicAI) FindObjectOfType( typeof(BasicAI) );
		_player = (PlayerController) FindObjectOfType( typeof(PlayerController) );
	}

	// Update is called once per frame
	void Update () {
		if (_enemy.Count > 0){
			battleMode = true;
		} else {
			battleMode = false;
		}
		Tick();
	}

	internal void Tick(){
		_AI.Tick();
		_player.Tick();
	}
}
