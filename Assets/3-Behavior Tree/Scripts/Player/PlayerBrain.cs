using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalVars;


public class PlayerBrain : MonoBehaviour {
	
	Collider coll;
	PlayerHealth playerHealth;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;
	PlayerAnimation playerAnimation;


	void Awake(){

		if (Checker.ObjectDoesNotHave (gameObject, typeof(BoxCollider))) 	 return;
		if (Checker.ObjectDoesNotHave (gameObject, typeof(PlayerHealth))) 	 return;
		if (Checker.ObjectDoesNotHave (gameObject, typeof(PlayerShooting)))  return;
		if (Checker.ObjectDoesNotHave (gameObject, typeof(PlayerMovement)))  return;
		if (Checker.ObjectDoesNotHave (gameObject, typeof(PlayerAnimation))) return;


		coll = GetComponent<BoxCollider> ();
		playerHealth = GetComponent<PlayerHealth> ();
		playerMovement = GetComponent<PlayerMovement> ();
		playerShooting = GetComponent<PlayerShooting> ();
		playerAnimation = GetComponent<PlayerAnimation> ();

	}

	void Start(){
		EventsClass.CallOnPlayerSpawn (gameObject);
	}


	// called from PlayerHealth when health is zero
	public void KillPlayer(){
		
		playerAnimation.DeathAnimation ();

		// disable all component so they won't process input anymore 
		playerAnimation.enabled = coll.enabled = playerMovement.enabled = playerShooting.enabled = playerHealth.enabled = false;

		EventsClass.CallOnPlayerDeath ();

		// no need for the PlayerBrain anymore, just keep the body
		this.enabled = false;

	}



}