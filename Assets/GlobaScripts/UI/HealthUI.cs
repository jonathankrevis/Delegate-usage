using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	Slider HealthSlider;


	IHealth ObjectHealth;

	void Awake(){
		
		// make sure that the slider is the first and only child
		HealthSlider = transform.GetChild (0).GetComponent<Slider> ();

	}

	// called in HealthUIHolder
	public void SetMonster(IHealth IH){
		
		ObjectHealth = IH;

		HealthSlider.maxValue = ObjectHealth.GetObjectMaxHealth();
		HealthSlider.value = ObjectHealth.GetObjectCurrentHealth();

	}


	void LateUpdate () {
		
		// if the object got deactivated (monster died)
		if ( ! ObjectHealth.TheObject().activeSelf) {
		
			ObjectHealth = null;

			gameObject.SetActive (false);

		}

		if (ObjectHealth == null)
			return;

		// update position
		transform.position = Camera.main.WorldToScreenPoint (ObjectHealth.TheObject().transform.position);
		// update value
		HealthSlider.value = ObjectHealth.GetObjectCurrentHealth();

	}
}
