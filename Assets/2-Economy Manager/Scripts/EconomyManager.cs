using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GlobalVars;


public class EconomyManager : MonoBehaviour {


	static int Fuel = 300;
	static int Iron = 400;
	static int Powder = 350;
	static int Wood = 500;
	static int Gold = 30;


	static Bill CurrentBill;
	static int NeededGoldToPayMissingCurrencies;


	#region Paying

	/// <summary>
	/// Pass the action in case have money
	/// </summary>
	/// <param name="DoAction">What to do if have enough money?</param>
	/// <param name="Bill">How much is the price?.</param>
	public static void PayFor(Bill theBill, Action DoAction){

		if (DoesHaveEnoughResources(theBill)) {

			PayTheBill (theBill);

			EventsClass.CallUpdateCostColors ();

			DoAction ();

		} else {

			Bill MissingCurrenciesBill = GetMissingCurrenciesBill (theBill);

			if (Gold < MissingCurrenciesBill.Gold) {
				UIMessageManager.ShowMessage ("You don't have enought Gold to pay for this action", Color.red);
				return;
			}

			DoAction += PayMissingCurrenciesAndNeededCoins;

			DoAction += EventsClass.CallUpdateCostColors ;

			MissingResourcesPanel.Show (MissingCurrenciesBill, DoAction);

		}

	}

	/// <summary>
	/// Pass the action in case have money or not
	/// </summary>
	/// <param name="Bill">How much is the price?.</param>
	/// <param name="DoAction">What to do if have enough money?</param>
	/// <param name="DropAction">What to do if doesn't enough money?</param>
	public static void PayFor(Bill theBill, Action DoAction, Action DropAcion){

		if (DoesHaveEnoughResources(theBill)) {

			PayTheBill (theBill);

			EventsClass.CallUpdateCostColors ();

			DoAction ();

		} else {

			Bill MissingCurrenciesBill = GetMissingCurrenciesBill (theBill);

			if (Gold < MissingCurrenciesBill.Gold) {
				UIMessageManager.ShowMessage ("You don't have enought Gold to pay for this action", Color.red);
				return;
			}

			DoAction += PayMissingCurrenciesAndNeededCoins;

			DropAcion += ResetVars;

			MissingResourcesPanel.Show ( MissingCurrenciesBill, DoAction, DropAcion);

		}

	}


	#endregion


	static bool DoesHaveEnoughResources(Bill theBill){
	
		if (theBill.Fuel > Fuel)
			return false;

		if (theBill.Iron > Iron)
			return false;

		if (theBill.Powder > Powder)
			return false;
		
		if (theBill.Wood > Wood)
			return false;
		
		return true;
	}


	static void PayTheBill (Bill theBill){
	
		Fuel 		-= 		theBill.Fuel;
		Iron 		-= 		theBill.Iron;
		Powder 		-= 		theBill.Powder;
		Wood		-= 		theBill.Wood;

	}


	static Bill GetMissingCurrenciesBill(Bill bill){

		// store the Bill to use it later in PayMissingCurrenciesAndNeededCoins() if payed with gold in MissingResourcesPanel.cs
		CurrentBill = bill;

		int fuel 	= 	GetMissingValue (bill.Fuel, CurrencyType.Fuel);
		int iron 	= 	GetMissingValue (bill.Iron, CurrencyType.Iron);
		int powder 	= 	GetMissingValue (bill.Powder, CurrencyType.Powder);
		int wood 	= 	GetMissingValue (bill.Wood, CurrencyType.Wood);

		int neededGold = 0;

		if (fuel != 0)
			neededGold 	+= fuel 	/ 	DataStorage.GetCurrencyValueToCoin (CurrencyType.Fuel);
		
		if (iron!= 0)
			neededGold 	+= iron 	/ 	DataStorage.GetCurrencyValueToCoin (CurrencyType.Iron);
		
		if (powder != 0)
			neededGold 	+= powder 	/ 	DataStorage.GetCurrencyValueToCoin (CurrencyType.Powder);
		
		if (wood != 0)
			neededGold 	+= wood 	/ 	DataStorage.GetCurrencyValueToCoin (CurrencyType.Wood);

		// store the needed gold to pay for the missing currencies to use it later in PayMissingCurrenciesAndNeededCoins()
		NeededGoldToPayMissingCurrencies = neededGold;

		Bill MissingCurrenciesBill = new Bill (fuel, iron, powder, wood, neededGold);

		return MissingCurrenciesBill;

	}


	static int GetMissingValue(int cost, CurrencyType type){

		int currency = GetCurrency (type);

		if (cost > currency) 
			return cost - currency;

		return 0;
	}


	// called in MissingResourcesPanel if payed with gold
	static void PayMissingCurrenciesAndNeededCoins(){

		// if the bill is bigger than the currency amout then it got paid with gold so make it 0
		// else just extract 

		if (CurrentBill.Fuel > Fuel) Fuel = 0; 				else Fuel -= CurrentBill.Fuel;

		if (CurrentBill.Iron > Iron) Iron = 0; 				else Iron -= CurrentBill.Iron;

		if (CurrentBill.Powder > Powder) Powder = 0; 		else Powder -= CurrentBill.Powder;

		if (CurrentBill.Wood > Wood) Wood = 0; 				else Wood -= CurrentBill.Wood;

		Gold -= NeededGoldToPayMissingCurrencies;

		ResetVars ();
	}

	static void ResetVars(){
		CurrentBill = null;
		NeededGoldToPayMissingCurrencies = 0;
	}


	// called in CurrenciesPanel.cs
	public static int GetCurrency(CurrencyType type){

		switch (type) {

			case CurrencyType.Fuel: 	return Fuel; 	break;
			case CurrencyType.Iron: 	return Iron; 	break;
			case CurrencyType.Powder: 	return Powder; 	break;
			case CurrencyType.Wood: 	return Wood; 	break;
			case CurrencyType.Gold: 	return Gold; 	break;
				
			default:
				Debug.LogError ("the type of currency you're trying to get don't exist, it's: " + type.ToString());
				return 0;
				break;

		}

	}


}