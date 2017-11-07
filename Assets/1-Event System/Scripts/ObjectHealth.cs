using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class ObjectHealth : MonoBehaviour , IHealth {

	int CurrentHealth = 0;
	int MaxHealth = 100;

	IHealth IH;

	void Awake(){
		IH = GetComponent<IHealth> ();
	}

	void OnEnable(){

		ResetHealth ();

		HealthUIHolder.SetNewObjectHealth (IH);

	}


	void ResetHealth(){
		CurrentHealth = MaxHealth;
	}



	#region IHealth 

	public GameObject TheObject(){
		return gameObject;
	}

	public void Damage(int amount){

		CurrentHealth -= amount;

		if (CurrentHealth <= 0) {
			CurrentHealth = 0;
			ExplosionEffect ();
			gameObject.SetActive (false);
		}

	}

	public int GetObjectMaxHealth(){
		return MaxHealth;
	}

	public int GetObjectCurrentHealth(){
		return CurrentHealth;
	}

	#endregion

	void ExplosionEffect(){

		GameObject explosion = MasterPool.Get (PrefabTypes.ExplosionEffect);

		explosion.transform.position = transform.position;

	}

}
