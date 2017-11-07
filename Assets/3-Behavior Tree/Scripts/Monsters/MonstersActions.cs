using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;


namespace MonstersActions{


	/// <summary>
	/// 
	/// A holder for all action for all monsters
	/// 
	/// this action get added to States delegates in StatesManager.cs
	/// 
	/// </summary>


	public class AllMovements : MonoBehaviour {

		public static void MoveToTarget(MonsterBrain mb){

			if(mb.navAgent .isStopped = true)
				mb.navAgent.isStopped = false;
			
			mb.navAgent.SetDestination ( mb.playerObj.transform.position );

		}

		public static void SetSafePoint(MonsterBrain mb){
			
			Vector3 safePosition = MonsterManager.RandomSafePoint ();

			mb.theSafePoint = safePosition;

		}

		public static void MoveToSafePoint(MonsterBrain mb){

			if(mb.navAgent.isStopped = true)
				mb.navAgent.isStopped = false;
			
			mb.navAgent.SetDestination (mb.theSafePoint);

		}

		public static void FacePlayer(MonsterBrain mb){

			Vector3 targetPos = mb.playerObj.transform.position;

			Vector3 lookPos = targetPos - mb.transform.position;
			lookPos.y = 0;

			mb.transform.rotation = Quaternion.LookRotation (lookPos);

		}


		public static void StopMoving(MonsterBrain mb){
		
			mb.navAgent.isStopped = true;

		}

	}


	public class AllAttacks : MonoBehaviour {

		public static void MeleeAttack(MonsterBrain mb){
			
			if (mb.IsTimeForNextAttack ())
				mb.playerHealth.DamagePlayer ( mb.monsterDamage );
			
		}

		public static void WizardRangeAttack(MonsterBrain mb){

			if (mb.IsTimeForNextAttack ()) {
			
				GameObject iceBall = MasterPool.Get (PrefabTypes.IceBall);

				Vector3 ballPos = mb.IceBallLauncingTra.position;
				ballPos.y = 2;
				iceBall.transform.position = ballPos;

				iceBall.transform.rotation = mb.transform.rotation;

			}

		}

	}


	public class AllAnimations : MonoBehaviour {

		public static void StartMovingAnimation(MonsterBrain mb){
			mb.anim.SetTrigger ("Move");
		}

		public static void StartAttackingAnimation(MonsterBrain mb){
			mb.anim.SetTrigger ("Attack");
		}

		public static void StartVictoryAnimation(MonsterBrain mb){
			mb.anim.SetTrigger ("Victory");
		}

		public static void StartDeathAnimation(MonsterBrain mb){
			mb.anim.SetTrigger ("Die");
		}

	}


	public class AllEffects : MonoBehaviour {

		public static void SwitchStatusEffect(MonsterBrain mb){

			GameObject effect = MasterPool.Get (PrefabTypes.SwitchStateEffect);

			effect.transform.position = mb.transform.position;	

		}

		public static void HealtingEffect(MonsterBrain mb){
			mb.monsterHealth.Heal ();
		}

		public static void Desappear(MonsterBrain mb){
			mb.DisappearAfter ();
		}

	}



}