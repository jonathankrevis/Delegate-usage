using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalVars;

public class PlayerHealth : MonoBehaviour {

	PlayerBrain playerBrain;

	[SerializeField]
	int MaxHealth = 100;
	[SerializeField]
	int CurrentHealth = 0;

	void Awake(){
		playerBrain = GetComponent <PlayerBrain> ();
	}

	void OnEnable(){
		
		ResetHealth ();

	}
		

	void ResetHealth(){
		CurrentHealth = MaxHealth;
	}


	public void DamagePlayer(int amount){
	
		CurrentHealth -= amount;

		if (CurrentHealth <= 0) {
			killPlayer ();
		}
	
	}


	public int GetPlayerMaxHealth(){
		return MaxHealth;
	}
	public int GetPlayerCurrentHealth(){
		return CurrentHealth;
	}


	void killPlayer(){

		playerBrain.KillPlayer ();

	}
		
}