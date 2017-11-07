using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GlobalVars {


	#region public Enums


	public enum PrefabTypes { 
							// for debuging
							None,  
							//Objects
							Cube, Sphere, Player ,Bullet , Punchy, Wizard, IceBall, MovingUnit,
							// Effects
							SparksEffect, ExplosionEffect, SwitchStateEffect, HealEffect, WaveEffect,
							// UI
							UIMessage, HealthUI,  
							
							};


	public enum SFXtype { None, Coin, ShotBullet, Explosion, MonsterHit };

	public enum CurrencyType { None, Fuel, Iron, Powder, Wood, Gold };

	public enum MonstersType { None, Punch, Wizard };


	public enum CostType { None, SpownUnit, Movement };


	public enum StateType { None, Healing, Moving, Running, Attacking, RangeAttack, Victory, Death };


	#endregion


	#region Public Delegates

	public delegate void Action ();

	public delegate void MonsterBrainEvent (MonsterBrain mb);

	#endregion	


	public class EventsClass : MonoBehaviour {


		#region Event System Scene

		public delegate void NoParamEvent();
		public static event NoParamEvent OnSceneLeave;

		public delegate void EventColorArray(Color[] colArray);
		public static event EventColorArray ColorArray;

		public delegate void FloatChange(float newSize);
		public static event FloatChange Xchange, Zchange, Jump;


		public static void CallOnSceneLeave(){
			if (OnSceneLeave != null)
				OnSceneLeave ();
			else
				Debug.LogWarning ("the OnSceneLeave event is empty");
		}

		public static void CallColorArray(Color[] colArray){
			if (ColorArray != null)
				ColorArray (colArray);
			else 
				Debug.LogWarning ("The ColorArray Event is Empty!");
		}

		public static void CallXChange(float x){
			if (Xchange != null)
				Xchange (x);
			else
				Debug.LogWarning ("The Xchange event is empty!");
		}
			
		public static void CallZChange(float z){
			if (Zchange != null)
				Zchange (z);
			else
				Debug.LogWarning ("The Zchange event is empty!");
		}

		public static void CallJump(float Yforce){
			if (Jump != null)
				Jump (Yforce);
			else
				Debug.LogWarning ("The Jump Event is Empty!");
		}


		#endregion



		#region EconomyManager Scene

		public static event Action UpdateCostColors;

		public static void CallUpdateCostColors(){
			if (UpdateCostColors != null)
				UpdateCostColors();
			else
				Debug.LogWarning ("the UpdateCostColors event is empty");
		}

		#endregion



		#region TopDownShooter Scene

		public delegate void NoParameterEvent();
		public static event NoParameterEvent OnPlayerDeath;

		public delegate void GameObjectEvent(GameObject player);
		public static event GameObjectEvent OnPlayerSpawn;

		public delegate void ExplosionEvent (Explosion exp);
		public static event ExplosionEvent OnExplosion;


		public static void CallOnPlayerSpawn(GameObject player){
			if(OnPlayerSpawn != null)
				OnPlayerSpawn (player);
			else 
				Debug.LogWarning ("the OnPlayerSpawn event is empty");
		}

		public static void CallOnPlayerDeath(){
			if (OnPlayerDeath != null)
				OnPlayerDeath ();
			else
				Debug.LogWarning ("OnPlayerDeath is empty!");
		}

		public static void CallExplosion(Explosion e){
			if(OnExplosion != null)
				OnExplosion (e);
			else
				Debug.LogWarning ("the OnExplosion event is empty");
		}

		#endregion


	}


}