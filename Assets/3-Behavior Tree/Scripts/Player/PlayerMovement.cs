using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	Rigidbody rb;

	// get set from the Axis
	Vector3 MovingDirection;

	// save last MovingDirection that is not 0 to keep the player facing the same direction if not moving
	Vector3 LastMovingMD;


	[SerializeField] float Speed;


	#region Mono calls

	void Awake() {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {

		PrecessInput ();

	}

	void FixedUpdate(){

		rb.velocity = MovingDirection * Speed;


		Vector3 target = TargetPos ();

		if(target != Vector3.zero)
			transform.rotation = Quaternion.LookRotation (target);

	}

	#endregion


	void PrecessInput(){

		MovingDirection = new Vector3 ( Input.GetAxis ("Vertical"), 
										0, 
										-Input.GetAxis ("Horizontal") );
		

		if (MovingDirection != Vector3.zero)
			LastMovingMD = MovingDirection;
		
	}


	Vector3 TargetPos(){

		if (Input.GetMouseButton (0)) 
			return MousePosOnGround();

		return LastMovingMD;

	}


	Vector3 MousePosOnGround(){
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);;
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, float.PositiveInfinity)){
			
			//Debug.DrawLine(Camera.main.transform.position, hit.point, Color.yellow);
			Vector3 targetPos = hit.point - transform.position;
			targetPos.y = transform.position.y;
			return targetPos;

		}
		
		//Debug.LogError ("mouse is not on anything");		// in this case you should make map limit (walls, mountains....)
		return LastMovingMD;

	}


}