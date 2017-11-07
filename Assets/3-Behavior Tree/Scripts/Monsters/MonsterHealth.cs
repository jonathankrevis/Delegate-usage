using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class MonsterHealth : MonoBehaviour , IHealth {

	Rigidbody rb;
	MonsterBrain monsterBrain;
	IHealth IH;

	[SerializeField] int CurrentHealth = 0;
	[SerializeField] int MaxHealth = 100;


	int LowHealthlimit = 25;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
		monsterBrain = GetComponent <MonsterBrain> ();
		IH = GetComponent<IHealth> ();
	}

	void OnEnable(){

		ResetHealth ();

		HealthUIHolder.SetNewObjectHealth (IH);

		EventsClass.OnExplosion += ListenToExplosion;

	}

	void OnDisable(){
		EventsClass.OnExplosion -= ListenToExplosion;
	}


	void ListenToExplosion(Explosion exp){

		if ( ! Checker.IsLayerInLayerMask (exp.EffectedLayers, gameObject.layer))
			return;

		if ( ! Checker.IsCloseEnough (transform.position, exp.ExplosionPos, exp.ExplosionRange))
			return;

		rb.AddExplosionForce (exp.ExplosionFource, exp.ExplosionPos, exp.ExplosionRange);

		Damage (exp.Damage);

	}


	void ResetHealth(){
		CurrentHealth = MaxHealth;
	}


	public void Heal(){
		
		if (IsTimeForNextHeal ()) {
			
			CurrentHealth += 10;

			GameObject effect = MasterPool.Get (PrefabTypes.HealEffect);
			effect.transform.position = transform.position;

		}


	}



	#region IHealth 

	// called in bullet
	public void Damage(int amount){

		CurrentHealth -= amount;

		if (CurrentHealth <= 0) {
			CurrentHealth = 0;
		}

		SFXManager.PlaySFXFor (SFXtype.MonsterHit);

	}

	public GameObject TheObject(){
		return gameObject;
	}

	public int GetObjectMaxHealth(){
		return MaxHealth;
	}

	public int GetObjectCurrentHealth(){
		return CurrentHealth;
	}

	#endregion


	public bool IsHealthTooLow(){

		if (CurrentHealth > LowHealthlimit)
			return false;
		else
			return true;

	}

	public bool IsAtMaxHealth(){
		if (CurrentHealth == MaxHealth)
			return true;
		else
			return false;
	}

	public bool IsDead(){
		if (CurrentHealth == 0)
			return true;
		else
			return false;
	}

	float timer;
	[SerializeField] float timeBetweenHeals;
	bool IsTimeForNextHeal(){

		if (timer < Time.time) {
		
			timer = Time.time + timeBetweenHeals;

			return true;
		}

		return false;
	}

//	void Update(){
//
//		if (Input.GetKeyDown (KeyCode.P)) {
//			DamagePlayer (10);
//		}
//
//	}

}