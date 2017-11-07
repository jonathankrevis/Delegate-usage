using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPanel : MonoBehaviour {

	GameObject panel;

	void Awake(){
		panel = transform.GetChild (0).gameObject;
	}

	void Update () {
	
		if (Input.GetKeyDown (KeyCode.P)) {
			Show ();
		}
		if (Input.GetKeyUp(KeyCode.P)) {
			Hide ();
		}

	}

	void Show(){

		panel.SetActive (true);

		Time.timeScale = 0;

	}

	void Hide(){
		
		panel.SetActive (false);

		Time.timeScale = 1;

	}

}