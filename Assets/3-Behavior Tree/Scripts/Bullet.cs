using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] int damage, speed;

	[SerializeField] LayerMask EffectedLayers;

	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.forward * speed * Time.deltaTime);

	}

		 
	void OnTriggerEnter(Collider obj){

		int ObjLayer = obj.gameObject.layer;

		if(Checker.IsLayerInLayerMask(EffectedLayers, ObjLayer)){			// should the built effect this layer?
				
			if (ObjLayer == 9) {						// if bullet hit enemy obj
				
				if( Checker.ObjectDoesNotHave(obj.gameObject, typeof(MonsterHealth)) ) return;

				obj.gameObject.GetComponent<MonsterHealth> ().Damage (damage);

			} else if (ObjLayer == 10) {				// if bullet hit Player obj
			
				if( Checker.ObjectDoesNotHave(obj.gameObject, typeof(PlayerHealth)) ) return;

				obj.gameObject.GetComponent<PlayerHealth> ().DamagePlayer (damage);

			}


		}

		gameObject.SetActive (false);

	}

}