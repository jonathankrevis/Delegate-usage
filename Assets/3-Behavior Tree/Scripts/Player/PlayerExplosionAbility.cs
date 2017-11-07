using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class PlayerExplosionAbility : MonoBehaviour {


	[SerializeField] float ExplosionRange, ExplosionFource;

	[SerializeField] int Damage;

	[SerializeField] LayerMask EffectedLayers;

	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Q)) {
		
			SendExplostion ();

		}

	}

	void SendExplostion(){

		Explosion exp = new Explosion (transform.position, Damage, ExplosionRange, EffectedLayers);

		EventsClass.CallExplosion (exp);

		ExplostionEffect ();

	}

	void ExplostionEffect(){

		GameObject effect = MasterPool.Get (PrefabTypes.ExplosionEffect);

		effect.transform.position = transform.position;

		SFXManager.PlaySFXFor (SFXtype.Explosion);

	}

}