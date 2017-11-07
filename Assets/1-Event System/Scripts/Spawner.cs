using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;


public class Spawner : MonoBehaviour {


	[SerializeField] PrefabTypes objectType;

	[SerializeField] float XLimitPos, ZLimitPos;


	// called in Canvas -> Spawn Button
	public void RandomSpawn(){

		GameObject obj = MasterPool.Get (objectType);

		obj.transform.position = GetRandomSpawningPos ();

	}

	Vector3 GetRandomSpawningPos(){

		Vector3 pos;

		pos.x = Random.Range (-XLimitPos, XLimitPos);
		pos.y = 5;
		pos.z = Random.Range (0, ZLimitPos);

		return pos;

	}

}