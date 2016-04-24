using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	private NavMeshAgent player;


	void PlayIdle(){
		Debug.Log ("Idle Animation Playing");
	}

	void PlayMovement(){
		Debug.Log ("Running Animation Playing");
	}

	void RunAnimations()
	{
		if ( player.remainingDistance < 1.0 ) {
			PlayIdle ();
		} else {
			PlayMovement ();
		}
		//Debug.Log(player.remainingDistance);
	}

	void ProcessMovement()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(screenRay, out hit))
			{
				player.SetDestination(hit.point);
			}
		}
	}

	void Start()
	{
		player = GetComponent<NavMeshAgent> ();
	}



	void Update()
	{
		ProcessMovement ();
		RunAnimations ();
	}
}
