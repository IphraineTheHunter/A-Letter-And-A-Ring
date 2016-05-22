using UnityEngine;
using System.Collections;


public class HumanAnimationController : MonoBehaviour {

	private NavMeshAgent player;
	private Animator anim;

	void PlayIdle(){
		anim.SetBool ("IsMoving", false);
	}

	void PlayRun(){
		anim.SetBool ("IsRunning", true);
		player.acceleration = 50.0f;
	}

	void PlayWalk(){
		anim.SetBool ("IsRunning", false);
		player.acceleration = 8.0f;
	}
		
	void PlayMovement() {
		if (player.remainingDistance > 8.0) {
			anim.SetBool ("IsRunning", true);
		} else {
			anim.SetBool ("IsRunning", false);
		}
	}


	void RunAnimations()
	{
		if (player.remainingDistance < 1.0) {
			PlayIdle ();
		} else {
			anim.SetBool("IsMoving", true);
			PlayMovement ();
		}
	}

	void Start () {
		player = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate () {
		RunAnimations ();
	}
}
