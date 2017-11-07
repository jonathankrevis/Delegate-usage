using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GlobalVars;

public class CurrenciesPanel : MonoBehaviour {


	[SerializeField] Text fuelText, ironText, powderText, woodText, goldText;

	void Start(){
		StartCoroutine (KeepUpdateingText ());
	}

	// Updateing text four time per second is better than using Update or FixedUpdata
	IEnumerator KeepUpdateingText(){

		while (true) {
		
			yield return new WaitForSeconds (0.25f);

			UpdateResourcesText ();

		}

	}

	void UpdateResourcesText(){
		fuelText.text 		= 		"Fuel: " 		+ 		EconomyManager.GetCurrency (CurrencyType.Fuel).ToString();
		ironText.text 		= 		"Iron: " 		+ 		EconomyManager.GetCurrency (CurrencyType.Iron).ToString();
		powderText.text 	= 		"Powder: " 		+ 		EconomyManager.GetCurrency (CurrencyType.Powder).ToString();
		woodText.text 		= 		"Wood: " 		+ 		EconomyManager.GetCurrency (CurrencyType.Wood).ToString();
		goldText.text 		= 		"Gold: " 		+ 		EconomyManager.GetCurrency (CurrencyType.Gold).ToString();
	}


}