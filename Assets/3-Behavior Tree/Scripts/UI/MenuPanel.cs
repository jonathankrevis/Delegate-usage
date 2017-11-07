using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalVars;

public class MenuPanel : MonoBehaviour {


	Animator anim;

	void Awake () {

		if (Checker.ObjectDoesNotHave (gameObject, typeof(Animator))) return;

		anim = GetComponent<Animator> ();

	}


	void OnEnable(){
		EventsClass.OnPlayerSpawn += DeactivatePanel;
		EventsClass.OnPlayerDeath += ActivatePanel;
		EventsClass.OnSceneLeave += Clear;
	}
	void OnDisable(){
		Clear ();
	}

	void Clear(){
		EventsClass.OnPlayerSpawn -= DeactivatePanel;
		EventsClass.OnPlayerDeath -= ActivatePanel;
		EventsClass.OnSceneLeave -= Clear;
	}


	void DeactivatePanel(GameObject player){
		anim.SetTrigger ("In");
	}

	void ActivatePanel(){
		anim.SetTrigger ("Out");
	}



	// called on Play UI Button
	public void SpawnPlayer(){

		GameObject player = MasterPool.Get (PrefabTypes.Player);

		player.transform.position = Vector3.zero;

	}

}