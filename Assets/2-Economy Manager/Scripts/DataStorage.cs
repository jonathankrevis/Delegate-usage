using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class DataStorage : MonoBehaviour {

	/// <summary>
	/// Note: there should don't be any cost under CurrencyValueToCoin or you'll get a 0 gold in MissingResourcesPanel
	/// </summary>

	static Dictionary<CurrencyType, int> CurrencyValueToCoin = new Dictionary<CurrencyType, int>(){

		{CurrencyType.Fuel, 	20},
		{CurrencyType.Iron, 	25},
		{CurrencyType.Powder, 	35},
		{CurrencyType.Wood, 	40}

	};


	static Dictionary<CostType, Bill> PrefabBill = new Dictionary<CostType, Bill> () {

		{ CostType.SpownUnit, new Bill(50,20,10,10) },
		{ CostType.Movement, new Bill(25,40,100,150) }

	};


	public static int GetCurrencyValueToCoin(CurrencyType type){
		if (CurrencyValueToCoin.ContainsKey (type))
			return CurrencyValueToCoin [type];

		Debug.LogError ("the Currency Type of: " + type + " don't exict in DataStorage.cs");
		return 0;
	}


	public static Bill GetBillFor(CostType type){
		
		if (PrefabBill.ContainsKey (type))
			return PrefabBill [type];
		
		Debug.LogError ("the PrefabType: " + type + " don't have a bill in DataStorage.cs");
		return null;
	}

}