//AUTHOR: Dast
//
//Convention: preface variables with db if it's only used for debugging


//not needed, but keeping as a reference in case I run into a situation where
//I need to specify what I'm using
//using Assets.Scripts.Combat;

using UnityEngine;
using System.Collections.Generic;
using Assets;
using System.Linq;
using System.Text;

//TODO: Move this to it's own file once it's confirmed working
//dropped in this file out of laziness
public class Injury {
		public int severity = 0;
		public bool debuff = false;

		public void applyDamage(int damage){
			severity += damage;
		}

		public bool isBroken(){
			return (severity > 1);
		}

		public bool debuffApplied(){
			return debuff;
		}

		public void applyDebuff(){
			Debug.Log("Y'all broke this homies somethin or another.");
			debuff = true;
		}

		//instantiates the object with 1/4th the character's health
		public Injury(int health){
			severity = health / 4;
		}
}

public class Character : MonoBehaviour {

	//TODO: Change things around so the context and AI doesn't need to be
	//instantiated here, but just in the derived NPC classes, so the Player
	//doesn't get these allocated as well
	public BasicAI _AI;
	public CombatContext _context;
	public NavMeshAgent _self;

	//TODO: rewrite this to use enums, now that I kinda get how they work
	//1 for forward, 2 for back, 3 for left, 4 for right, 5 for idle
	private int _position = 0;
	public int aggro = 0;

	//Base Stats
	protected int finesse = 10;
	protected int might = 10;
	protected int wit = 10;
	public int perceptiveness = 10;
	protected int toughness = 10;
	protected int willpower = 10;
	protected int focusLimit = 10;
	public int health = 100;
	protected int currentHealth = 0;
	protected bool crit = false;
	public bool isCrippled = false;
	public bool incapacitated = false;

  protected enum BodyParts {Eyes, Ears, Brain, RightArm, RightHand, LeftArm, LeftHand, Torso, Legs};
	public List<Injury> _bodyParts = new List <Injury>();

	public int _level = 1; //Public so it can be changed per scene as needed
						  					// We may instead grab this value from a quest's difficulty level instead
	public int _focus = 0;
	public int db_lastacc = 0;
	public int db_lasteva = 0;
	public int db_lastfoc = 0;

	///////////////////////////////////////////////
	//              PUBLIC METHODS               //
	///////////////////////////////////////////////
	public NavMeshAgent getCharacterNavMesh(){
		return _self;
	}

	public int getPosition(){
		return _position;
	}

	public void setPosition(int i){
		_position = i;
	}

	public int getFin(){
		return finesse;
	}

	public int getPer(){
		return perceptiveness;
	}

	public void DealDamage(int damage){
		currentHealth = currentHealth - damage;
		if (currentHealth <= 0) {
			Death ();
		}
	}

	public int getLevel(){
		return _level;
	}

	public void applyCripple(int limb){
		//If limb is already broken
		if (_bodyParts[limb].debuffApplied()){
			return;
		}

		switch (limb){
			case (int) BodyParts.Eyes:
				incapacitated = true;
				break;
			case (int) BodyParts.Brain:
				wit = 5;
				break;
			case (int) BodyParts.RightArm :
			case (int) BodyParts.RightHand :
			case (int) BodyParts.LeftArm :
			case (int) BodyParts.LeftHand :
			case (int) BodyParts.Torso :
			case (int) BodyParts.Legs :
				perceptiveness -= _level/10;
				break;
		}

		//mark limb as broken
		_bodyParts[limb].applyDebuff();

	}

	//picks an injury to apply to the character
	public void RollInjury(int damage){
		int i = Random.Range(0, 9);
		DamageLimb(i, damage);
	}

	public void DamageLimb(int limb, int damage){
		Debug.Log(limb);
		_bodyParts[limb].applyDamage(damage); //TODO
		if (_bodyParts[limb].isBroken()){
			applyCripple(limb);
			isCrippled = true;
		}
	}

	public bool isIncapacitated(){
		return incapacitated;
	}

	public virtual int GetFocus(){
		int focus = Random.Range(1, 10);
		if (isCrippled){
			focus = 2*focus/3;
		}
		db_lastfoc = focus;
		return focus;
	}

	public void Attack(Character target){
		int focus = GetFocus ();
		int accuracy = GenerateAccuracy(focus);
		if (CheckHit(accuracy, target) ) {
			Debug.Log("Hit the target");
			int damage = GenerateDamage(focus);
			if (crit){
				crit = false;
				damage = damage * 2;
			}
			//sends 1/4 of the rolled damage to a limb
			target.RollInjury(damage / 4);
			target.DealDamage(damage);
		} else {
			Debug.Log("Sorry, you missed! ");
		}
	}

	///////////////////////////////////////////////
	//              PRIVATE METHODS              //
	///////////////////////////////////////////////

	/* Given a hit chance and a target
	 We roll evasion and calculate the threshold for a critical strike,
	 handle critical roll events, debuff the roll if
	 the character is crippled, and return if the attack hit or not*/
	bool CheckHit(int hitChance, Character target){
		int eva = Random.Range( 0, target.getLevel()/2 ) + target.getFin()+target.getPer();
		int critThreshold = target.getLevel()/2 + target.getFin()+target.getPer();
		if (hitChance > critThreshold){
			Debug.Log("Crit!");
			crit = true;
		}

		db_lasteva = eva; //To see in the Character object's inspector view

		if (isCrippled){
			eva = eva / 2;
		}
		return hitChance > eva;
	}

	int GenerateAccuracy(int focus){
		int accuracy = 10;
		for (int i = 1; i < focus; i++) {
			accuracy += Random.Range ( 1, getLevel()/2 ) + perceptiveness;
		}
		db_lastacc = accuracy;
		return accuracy;
	}

	int GenerateDamage(int focus){
		int damage = 20;
		for (int i = 1; i < focus; i++){
			damage += Random.Range (1, might/2);
		}
		return damage;
	}

	//TODO: Player doesn't get destroyed, but I'm leaving it as is for now
	//until I make a game over screen
	public virtual void Death() {
		//TODO: Death Animation code here
		_AI.reassignNPC(_position);
		_context.removeCharacter(this);
		Destroy(this.gameObject);
	}

	//For debugging purposes
	void db_printStats(){
		Debug.Log(finesse);
		Debug.Log(might);
		Debug.Log(wit);
		Debug.Log(perceptiveness);
		Debug.Log(toughness);
		Debug.Log(willpower);
		Debug.Log(focusLimit);
		Debug.Log(health);
	}

	//Pokemon formulas for generating stats, these are generated per Character,
	//TODO: including the player at the moment.
	//Also handles instantiating the limbs and setting their health
	public virtual void GenerateStats(int inLevel){
		int level = 3*inLevel/4;
		finesse = (2 * finesse + Random.Range (1, 100)) * level / 100 + 5;
		might = (2 * might + Random.Range (1, 100)) * level / 100 + 5;
		wit = (2 * wit + Random.Range (1, 100)) * level / 100 + 5;
		perceptiveness = (2 * perceptiveness + Random.Range (1, 100)) * level / 100 + 5;
		toughness = (2 * toughness + Random.Range (1, 100)) * level / 100 + 5;
		willpower = (2 * willpower + Random.Range (1, 100)) * level / 100 + 5;
		focusLimit = (2 * focusLimit + Random.Range (1, 100)) * level / 100 + 5;
		health = (2 * health + Random.Range (1, 100)) * level / 100 + 5;
		currentHealth = health;
		_level = level;


		for (int i = 0; i < 9; i++) {
			_bodyParts.Add( new Injury(health) );
		}
		//printStats();
	}

	public virtual void Start() {
		_self =  GetComponent<NavMeshAgent> ();
		_AI = (BasicAI) FindObjectOfType( typeof(BasicAI) );
		_context = FindObjectOfType (typeof(CombatContext)) as CombatContext;
		currentHealth = health;
	}
}
