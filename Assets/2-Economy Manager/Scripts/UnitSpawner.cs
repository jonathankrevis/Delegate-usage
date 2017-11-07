using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class UnitSpawner : MonoBehaviour {

	[SerializeField] PrefabTypes UnitType;

	[SerializeField] Transform SpawnPoint;


	public void SpawningProcess(){

		Action SpawnAction = new Action (Spawn);

		Bill bill = DataStorage.GetBillFor (CostType.SpownUnit);

		EconomyManager.PayFor ( bill , SpawnAction);

	}

	void Spawn(){

		GameObject obj = MasterPool.Get (UnitType);

		obj.transform.position = SpawnPoint.position;

	}


}