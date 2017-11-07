using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class Shooter : MonoBehaviour {

	int damage = 25;


	void Update () {

		if (Input.GetMouseButtonDown (0)) {
		
			GameObject obj = CameraRaycast.GetObject ();

			if (obj.CompareTag ("Listener")) {
				SendDamage (obj);
			}

		}

	}


	void SendDamage(GameObject obj){

		if (Checker.ObjectDoesNotHave (obj, typeof(IHealth))) return;

		obj.GetComponent<IHealth>().Damage (damage);

		SparksEffect (obj.transform.position);

	}

	void SparksEffect (Vector3 pos){

		GameObject effect = MasterPool.Get (PrefabTypes.SparksEffect);

		effect.transform.position = pos;

	}

}