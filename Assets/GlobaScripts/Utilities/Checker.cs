using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Checker : MonoBehaviour {


	public static bool ObjectDoesNotHave(GameObject theObject, Type type){

		var com = theObject.GetComponent( type );

		if (com == null) {
			Debug.LogError ("The Object Named: " + theObject.name + " don't have: " + type.ToString() + " on it");
			return true;
		}

		//print ("The Object Named: " + theObject.name + " has : " + type.ToString() + " on it");
		return false;

	}


	public static bool IsCloseEnough(Vector3 posOne, Vector3 posTwo, float range){
	
		if( Vector3.Distance(posOne,posTwo) < range) {
			return true;
		}else{
			return false;
		}
	
	}


	public static bool IsLayerInLayerMask(LayerMask mask, int layer){
		return mask == (mask | (1 << layer));
	}


}
