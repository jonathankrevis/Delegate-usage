using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using GlobalVars;

/// <summary>
/// 
/// this class is the Dicision maker for all monsters
/// 
/// to create new monster behaviour create new void for it, the monster should ask for it every FixedUpdate passed on his type
/// 
/// </summary>

public class DecisionsTrees : MonoBehaviour {


	public static void UpdateBehaviorFor(MonsterBrain mb){
	
		switch (mb.monsterType) {

			case MonstersType.Punch: 	DecisionsTrees.UpdatePunchyBehavior (mb); 	break;

			case MonstersType.Wizard: 	DecisionsTrees.UpdateWizardBehavior (mb);	break;


			default:
				Debug.LogError ("The DecisionsTrees.cs is not set to update behaviour for: " + mb.monsterType.ToString()); 
				break;
				

		}


	}


	static void UpdatePunchyBehavior(MonsterBrain MB) {
	
		// Death & Victory can be changed to regaredless of the current state

		if (MB.monsterHealth.IsDead()) {
		
			if (MB.monsterState != StateType.Death)
				MB.SetMonsterState (StateType.Death);

			return;
		}

		if (MB.IsTargetDead) {

			if (MB.monsterState != StateType.Victory) 
				MB.SetMonsterState (StateType.Victory);

			return;
		}


		switch (MB.monsterState) {

			// if this is the first call for this MonsterBrain
			case StateType.None:
				MB.SetMonsterState (StateType.Moving);
				break;


			case StateType.Moving:
				
				if (MB.IsPlayerInRange ()) {
					MB.SetMonsterState (StateType.Attacking);
					return;
				}

					
				if (MB.monsterHealth.IsHealthTooLow()) {
					MB.SetMonsterState (StateType.Running);
					return;
				}

				break;


			case StateType.Attacking:

				if (!MB.IsPlayerInRange ()) {
					MB.SetMonsterState (StateType.Moving);
					return;
				}


				if (MB.monsterHealth.IsHealthTooLow()) {
					MB.SetMonsterState (StateType.Running);
					return;
				}

				break;


			case StateType.Running:

				if (MB.IsAtSafePoint ()) 
					MB.SetMonsterState (StateType.Healing);

				break;

			case StateType.Healing:

				if (MB.monsterHealth.IsAtMaxHealth ())
					MB.SetMonsterState (StateType.Moving);
		
				break;


			default:
				Debug.LogError ("the monster Punchy DecisionsTree is not prepared for: " + MB.monsterState.ToString ());
				return;


		}

	}


	static void UpdateWizardBehavior(MonsterBrain MB) {
		
		// Death & Victory can be changed to regaredless of the current state

		if (MB.monsterHealth.IsDead()) {

			if (MB.monsterState != StateType.Death)
				MB.SetMonsterState (StateType.Death);

			return;
		}

		if (MB.IsTargetDead) {

			if (MB.monsterState != StateType.Victory) 
				MB.SetMonsterState (StateType.Victory);

			return;
		}


		switch (MB.monsterState) {

			// if this is the first call for this MonsterBrain
			case StateType.None:
				MB.SetMonsterState (StateType.Moving);
				break;

			case StateType.Moving:

				if (MB.IsPlayerInRange ()) 
					MB.SetMonsterState (StateType.RangeAttack);
				
				break;


			case StateType.RangeAttack:

				if (!MB.IsPlayerInRange ()) {
					MB.SetMonsterState (StateType.Moving);
					return;
				}

				break;



			default:
				Debug.LogError ("the monster Wizard is in the wrong state, it's : " + MB.monsterState.ToString ());
				return;


		}

	}


}