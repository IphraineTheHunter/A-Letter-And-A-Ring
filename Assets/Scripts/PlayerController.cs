using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	private NavMeshAgent player;
	public NavMeshAgent destination;

	void SetDestination()
	{
		if (Input.GetMouseButtonDown (0)) {
			Ray screenRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast (screenRay, out hit)) {
				if (Vector3.Distance (hit.point, player.transform.position) > 2.0) {
					destination.transform.position = hit.point;
				}				
			}
		}
	}

	void ProcessMovement()
	{
		player.SetDestination (destination.transform.position);
	}
		
	void Start()
	{
		player = GetComponent<NavMeshAgent> ();
		destination.transform.position = player.transform.position;

	}



	void Update()
	{
		SetDestination ();
		ProcessMovement ();
	}
}
