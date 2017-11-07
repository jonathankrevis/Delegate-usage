using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	Rigidbody rb;

	[SerializeField] Animator anim;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){

		if (Input.GetMouseButtonDown (0)) {
			
			anim.SetBool ("IsShooting", true);

		} else if (Input.GetMouseButtonUp (0)) {

			anim.SetBool ("IsShooting", false);

		}

		if (rb.velocity != Vector3.zero) {
			
			anim.SetBool ("IsRunning", true);

		} else {
			
			anim.SetBool ("IsRunning", false);

		}

	}


	// called from PlayerBrain.cs
	public void DeathAnimation(){

		anim.SetTrigger ("Die");

	}


}
