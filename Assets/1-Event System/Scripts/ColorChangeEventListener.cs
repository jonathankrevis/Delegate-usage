using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChangeEventListener : MonoBehaviour {
	
	MeshRenderer mesh;

	void Awake(){
		mesh = GetComponent<MeshRenderer> ();
	}

	void OnEnable(){

		ResetColor ();

		EventsClass.ColorArray += ChangeToColorArray;
		EventsClass.OnSceneLeave += ClearSubscription;

	}
	void OnDisable(){
		ClearSubscription ();
	}

	void ClearSubscription(){
		EventsClass.ColorArray -= ChangeToColorArray;
		EventsClass.OnSceneLeave -= ClearSubscription;
	}


	void ResetColor(){
		SetMeshColorTo (Color.white);
	}


	void ChangeToColorArray(Color[] x){
		StartCoroutine(ChangeColorEverySecond(x));
	}
	IEnumerator ChangeColorEverySecond(Color[] col){

		for (int x = 0; x < col.Length; x++) {

			SetMeshColorTo ( col[x] );

			yield return new WaitForSeconds (1f);

		}

	}

	void SetMeshColorTo(Color x){
		mesh.material.color = x;
	}

}
