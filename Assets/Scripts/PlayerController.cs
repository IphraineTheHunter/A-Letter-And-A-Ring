//AUTHOR: Dast

﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private CharacterPlayer player;
	private Character target;
	public bool isAttacking = false;
	public Slider focusSlider;
	public NavMeshAgent destination;

	void GrabInputs()
	{
		//When the right mouse button is clicked, and we aren't clicking right next
		//to the player, we set our destination
		if (Input.GetMouseButtonDown (1)) {
			Ray screenRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast (screenRay, out hit)) {
				if (Vector3.Distance (hit.point, player.transform.position) > 2.0) {
					target = null;
					destination.transform.position = hit.point;
				}
			}
		}

		//When the left mouse button is clicked, and we hit a Character
		//We set our target and handle attacking operations
		if (Input.GetMouseButtonDown (0)) {
			Ray screenRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast (screenRay, out hit)) {
				if (hit.collider.gameObject.tag == "Character"){
					target = hit.collider.gameObject.GetComponent(typeof(Character) ) as Character;
					//Check how far away from the player the target is, and move closer
					//as needed
					isAttacking = true;
				}
			}
		}

		//when we have an active target, and it is too far away, we set our destination
		// towards it
		//otherwise, we check if we have already issued an attack.
		//if we haven't, then we attack the target
		if (target != null){
			if (Vector3.Distance(target.transform.position, player.transform.position) > 3.0){
				destination.transform.position = target.transform.position;
			} else if (isAttacking) {
				launchAttack();
			}
		}
	}

	void launchAttack(){
			//TODO: add attacking animations here
			player.Attack(target);
			isAttacking = false;
	}


	//We move the player towards the destination
	private void ProcessMovement()
	{
		player.getCharacterNavMesh().SetDestination (destination.transform.position);
	}

	private void GrabFocus(){
		player.setFocus( (int) focusSlider.value );
	}

	void Start()
	{
		player = GetComponent<CharacterPlayer> ();
	}

	//TODO: Figure out why the slider isn't working anymore
	//This takes input from the slider, and sets the player's focus to it
	public void applyFocus(float focus){
		player.setFocus((int) focus);
	}

	public void Tick()
	{
		GrabFocus();
		GrabInputs ();
		ProcessMovement ();
	}
}
