using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour {


	public static RaycastHit GetRaycastHit(){

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray , out hit, 1000)){

			return hit;

		}

		Debug.LogError ("The raycastingn from the camera to mouse didn't hit anything!!!");
		return new RaycastHit();

	}


	public static GameObject GetObject(){

		return GetRaycastHit ().transform.gameObject;

	}

	public static Vector3 GetPosition(){

		return GetRaycastHit ().point;

	}

}