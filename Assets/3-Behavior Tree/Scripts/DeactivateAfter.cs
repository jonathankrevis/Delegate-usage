using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfter : MonoBehaviour {


	float timer;

	[SerializeField] float lifeTime;

	void OnEnable(){

		timer = Time.time + lifeTime;

	}


	void FixedUpdate () {
		
		if(IsTimeToDeactivate())
			gameObject.SetActive (false);

	}

	bool IsTimeToDeactivate(){

		if (timer < Time.time) {

			return true;

		}

		return false;

	}

}