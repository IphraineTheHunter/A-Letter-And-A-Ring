using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	/*void Update()
	{
		
	}*/

	//public float speed;
	//private Vector3 targetPosition;

	public Transform destination;
	private NavMeshAgent player;
//	private Rigidbody player;

	void ProcessMovement()
	{
		/* float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		if (moveHorizontal == 0 && moveVertical == 0) {
			player.AddForce (movement);
		} else {
			player.AddForce (movement * speed);
		}*/

		if (Input.GetMouseButtonDown(0))
		{
			Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(screenRay, out hit))
			{
				player.SetDestination(hit.point);
			}
		}

		//player.transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

	}

	void Start()
	{
		player = GetComponent<NavMeshAgent> ();
	}



	void FixedUpdate()
	{
		ProcessMovement ();
	}
}
