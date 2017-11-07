using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GlobalVars;


public class MissingResourcesPanel : MonoBehaviour {


	static GameObject MessagePanel;

	static Text FuelText, IronText, PowderText, WoodText, GoldText;

	// these two used to store the actions to activate later in Pay() or Drop()
	static Action DoAction;
	static Action DropAction;


	void Awake(){
		
		MessagePanel = transform.GetChild (0).gameObject;

		Text[] textArray = MessagePanel.transform.GetComponentsInChildren<Text> ();

		// 0 is the Pay for text
		FuelText 	= 	textArray [1];
		IronText 	= 	textArray [2];
		PowderText 	= 	textArray [3];
		WoodText 	= 	textArray [4];
		GoldText 	= 	textArray [5];

	}


	#region Called in EconomyManager

	public static void Show(Bill MissingCurrenciesBill, Action doAction){

		DoAction = doAction;

		ShowCurrenciesPanel (MissingCurrenciesBill);

	}

	public static void Show(Bill MissingCurrenciesBill, Action doAction, Action dropAction){

		DoAction = doAction;

		DropAction = dropAction;

		ShowCurrenciesPanel (MissingCurrenciesBill);

	}

	static void ShowCurrenciesPanel(Bill bill){

		MessagePanel.SetActive (true);

		FuelText.text 		= 		bill.Fuel.ToString ();
		IronText.text 		= 		bill.Iron.ToString ();
		PowderText.text 	= 		bill.Powder.ToString ();
		WoodText.text 		= 		bill.Wood.ToString ();

		GoldText.text 		=		bill.Gold.ToString();

	}

	#endregion

	#region Called in MissingResourcesPanel Buttons

	public void Pay(){

		DoAction ();

		EventsClass.CallUpdateCostColors ();

		CleanVars ();

	}

	public void Drop(){

		if(DropAction != null)		// in case an event didn't send a DropAction
			DropAction ();

		CleanVars ();

	}

	#endregion


	void CleanVars(){

		MessagePanel.SetActive (false);

		DoAction 	= null;
		DropAction 	= null;

		FuelText.text 	= string.Empty;
		IronText.text	= string.Empty;
		PowderText.text = string.Empty;
		WoodText.text	= string.Empty;

	}


}