using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class SizeChangeEventListener : MonoBehaviour {


	void OnEnable(){

		ResetSize ();

		EventsClass.Xchange += UpdataX;
		EventsClass.Zchange += UpdateZ;
		EventsClass.OnSceneLeave += ClearSubscription;

	}
	void OnDisable(){
		ClearSubscription ();
	}

	void ClearSubscription(){
		EventsClass.Xchange -= UpdataX;
		EventsClass.Zchange -= UpdateZ;
		EventsClass.OnSceneLeave -= ClearSubscription;
	}


	void ResetSize(){
		transform.localScale = Vector3.one;
	}


	void UpdataX(float x){
		UpdateSize (x, transform.localScale.z);
	}
	void UpdateZ(float z){
		UpdateSize (transform.localScale.x, z);
	}
	void UpdateSize(float x, float z){
		transform.localScale = new Vector3 (x, transform.localScale.y, z);
	}


}