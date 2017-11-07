using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bill : MonoBehaviour {
	

	public int Gold = 0;

	public int Fuel = 0;
	public int Iron = 0;
	public int Powder = 0;
	public int Wood = 0;


	public Bill (int fuel, int iron, int powder, int wood){
		Fuel 	= 	fuel;
		Iron 	= 	iron;
		Powder 	= 	powder;
		Wood 	= 	wood;
	}

	public Bill (int fuel, int iron, int powder, int wood, int gold){

		Fuel 	= 	fuel;
		Iron 	= 	iron;
		Powder 	= 	powder;
		Wood 	= 	wood;

		Gold 	= 	gold;

	}

}