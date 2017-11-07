using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GlobalVars;

public class CostPanel : MonoBehaviour {

	[SerializeField] CostType type;

	[Space]

	[SerializeField] Text fuel, iron, powder, wood;

	Dictionary<CurrencyType , int> currencyCost = new Dictionary<CurrencyType, int>();


	void Awake(){

		Bill bill = DataStorage.GetBillFor (type);

		currencyCost.Add (CurrencyType.Fuel, 	bill.Fuel);
		currencyCost.Add (CurrencyType.Iron, 	bill.Iron);
		currencyCost.Add (CurrencyType.Powder, 	bill.Powder);
		currencyCost.Add (CurrencyType.Wood, 	bill.Wood);

		// update the text
		foreach(CurrencyType type in currencyCost.Keys)
			currencyText (type).text = currencyCost [type].ToString();
		

		UpdateColors();

	}

	#region Events Calls

	void OnEnable(){
		EventsClass.UpdateCostColors += UpdateColors;
	}
	void OnDisable(){
		EventsClass.UpdateCostColors -= UpdateColors;
	}

	void UpdateColors(){

		SetCurrencyColor (CurrencyType.Fuel);
		SetCurrencyColor (CurrencyType.Iron);
		SetCurrencyColor (CurrencyType.Powder);
		SetCurrencyColor (CurrencyType.Wood);

	}

	#endregion


	void SetCurrencyColor(CurrencyType type){
	
		Text curencyText = currencyText (type);

		if (currencyCost [type] > EconomyManager.GetCurrency (type))
			curencyText.color = Color.red;
		else
			curencyText.color = Color.black;

	}

	Text currencyText(CurrencyType type){

		switch (type) {

			case CurrencyType.Fuel: 	return fuel; 	break;
			case CurrencyType.Iron: 	return iron; 	break;
			case CurrencyType.Powder: 	return powder; 	break;
			case CurrencyType.Wood: 	return wood; 	break;

			default:
				Debug.LogError("There isn't any text for: " + type.ToString());
				return null;

		}

	}

}