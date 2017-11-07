using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;


public class SceneChanger : MonoBehaviour {
	
	public  void LoadScene(string SceneName){

		// clear before leaving
		EventsClass.CallOnSceneLeave ();

		Application.LoadLevel (SceneName);

	}

}