using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GlobalVars;
using MonstersActions;

namespace MonstersStates{

	/// <summary>
	/// 
	/// this class is the holder of all Monsters States info
	/// 
	/// Create State and fill it's delegates with actions so monsters can called it runtime
	/// 
	/// </summary>

	public class StatesManager {


		static State MovingState 		= 		new State ("Moving");

		static State AttackingState 	= 		new State ("Attacking");

		static State RunningState 		= 		new State ("RunningFromPlayer");

		static State HealingState 		= 		new State ("Healing");

		static State RangeAttack 		= 		new State ("RangeAttack");

		static State VictoyState 		=		new State ("Victory");

		static State DeathState 		=		new State ("Death");


		static Dictionary<StateType , State> MonstersStates = new Dictionary<StateType , State> ();


		// called in MonsterBrain when changing states
		public static State GetPunchState(StateType type){

			if (MonstersStates.Count == 0)
				SetData ();

			if(MonstersStates.ContainsKey(type))
				return MonstersStates [type];

			Debug.LogError ("The state:" + type.ToString() + " doesn't exict in the MonstersStates Dictionary");
			return null;
		}



		static void SetData () {

			#region Fill states

			MovingState.EnterStateActions 		+= 		AllAnimations.StartMovingAnimation;

			MovingState.UpdateStateActions 		+= 		AllMovements.MoveToTarget;

			MovingState.ExitStateActions 		+= 		AllEffects.SwitchStatusEffect;



			RangeAttack.EnterStateActions 		+= 		AllAnimations.StartAttackingAnimation;

			RangeAttack.UpdateStateActions 		+= 		AllAttacks.WizardRangeAttack;

			RangeAttack.UpdateStateActions		+=		AllMovements.FacePlayer;

			RangeAttack.ExitStateActions 		+= 		AllEffects.SwitchStatusEffect;



			AttackingState.EnterStateActions 	+= 		AllAnimations.StartAttackingAnimation;

			AttackingState.UpdateStateActions 	+= 		AllAttacks.MeleeAttack;

			AttackingState.ExitStateActions 	+= 		AllEffects.SwitchStatusEffect;



			RunningState.EnterStateActions		+=		AllAnimations.StartMovingAnimation;
			RunningState.EnterStateActions		+=		AllMovements.SetSafePoint;

			RunningState.UpdateStateActions		+=		AllMovements.MoveToSafePoint;

			RunningState.ExitStateActions		+=		AllEffects.SwitchStatusEffect;



			HealingState.EnterStateActions		+=		AllAnimations.StartVictoryAnimation;		// use victory animation just for testing

			HealingState.UpdateStateActions		+=		AllEffects.HealtingEffect;



			VictoyState.EnterStateActions		+=		AllAnimations.StartVictoryAnimation;
			VictoyState.EnterStateActions		+=		AllMovements.StopMoving;
			VictoyState.EnterStateActions 		+=		AllEffects.SwitchStatusEffect;


			DeathState.EnterStateActions 		+=		AllMovements.StopMoving;
			DeathState.EnterStateActions		+=		AllAnimations.StartDeathAnimation;
			DeathState.EnterStateActions 		+=		AllEffects.SwitchStatusEffect;
			DeathState.EnterStateActions 		+=		AllEffects.Desappear;


			#endregion

			#region Fill Dictonary

			MonstersStates.Add(StateType.Moving, 			MovingState);
			MonstersStates.Add(StateType.Attacking, 		AttackingState);
			MonstersStates.Add(StateType.RangeAttack, 		RangeAttack);
			MonstersStates.Add(StateType.Running, 			RunningState);
			MonstersStates.Add(StateType.Healing, 			HealingState);

			MonstersStates.Add(StateType.Victory, 			VictoyState);
			MonstersStates.Add(StateType.Death, 			DeathState);

			#endregion

		}



	}

}