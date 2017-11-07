using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

[RequireComponent(typeof(Rigidbody))]
public class JumpEventListener : MonoBehaviour {

	Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
	}

	void OnEnable(){

		ResetVelocity ();

		EventsClass.Jump += Jump;
		EventsClass.OnSceneLeave += ClearSubscription;
		EventsClass.OnSceneLeave += ClearSubscription;

	}
	void OnDisable(){
		ClearSubscription ();
	}

	void ClearSubscription(){
		EventsClass.Jump -= Jump;
		EventsClass.OnSceneLeave -= ClearSubscription;
		EventsClass.OnSceneLeave -= ClearSubscription;
	}


	void ResetVelocity(){
		rb.velocity = Vector3.zero;
	}


	void Jump(float y){
		
		rb.AddForce (new Vector3 (0, y, 0));

	}

}
