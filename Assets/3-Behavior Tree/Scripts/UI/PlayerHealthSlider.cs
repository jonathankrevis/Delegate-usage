using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GlobalVars;

public class PlayerHealthSlider : MonoBehaviour {
	
	Slider slider;

	PlayerHealth playerHealth;

	void Awake() {

		if (Checker.ObjectDoesNotHave (gameObject, typeof(Slider))) return;

		slider = GetComponent<Slider>();

	}

	void OnEnable(){
		EventsClass.OnPlayerSpawn += SetPlayerHealth;
		EventsClass.OnPlayerDeath += ReleasePlayer;
		EventsClass.OnSceneLeave += Clear;
	}
	void OnDisable(){
		Clear ();
	}

	void Clear(){
		EventsClass.OnPlayerSpawn -= SetPlayerHealth;
		EventsClass.OnPlayerDeath -= ReleasePlayer;
		EventsClass.OnSceneLeave -= Clear;
	}


	void SetPlayerHealth(GameObject player){

		if (Checker.ObjectDoesNotHave (player, typeof(PlayerHealth))) return;

		playerHealth = player.GetComponent<PlayerHealth> ();

		slider.maxValue = playerHealth.GetPlayerMaxHealth ();

	}

	void ReleasePlayer(){
		playerHealth = null;
	}


	void FixedUpdate () {

		if (playerHealth != null) {
			slider.value = playerHealth.GetPlayerCurrentHealth ();
		}

	}



}