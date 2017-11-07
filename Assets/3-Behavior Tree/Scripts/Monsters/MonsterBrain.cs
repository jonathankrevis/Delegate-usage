using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

using GlobalVars;
using MonstersStates;


/// <summary>
/// 
/// MonsterBrain is just the monster data holder
/// 
/// THIS CLASS SHOULD NOT PROCESS IT'S DATA UNDER ANY CIRCUMSTANCE 
/// 
/// the MonsterBrain asks the DecisionsTree what State the monster should be in passed in his type
/// 
/// In case of State change get the needed State data from StatesManager passed on StateType
/// 
/// </summary>

public class MonsterBrain : MonoBehaviour {

	// this monster component
	BoxCollider coll;
	public NavMeshAgent navAgent;
	public Animator anim;
	public MonsterHealth monsterHealth;


	// player component (get set from MonsterManager)
	PlayerBrain playerBrain;
	public PlayerHealth playerHealth;
	public GameObject playerObj;


	// the modle object (first child)
	GameObject ModelObj;


	[SerializeField]
	public StateType monsterState = StateType.None;
	State CurrentState = new State ("NEW");


	public bool IsTargetDead = false;

	public Vector3 theSafePoint;


	[Header("This Monster Is:")]
	public MonstersType monsterType = MonstersType.None;

	// 4 for punchy,		15 for wizard
	[SerializeField] float DistanceFromPlayerToAttack;

	[Space]

	[Header("Attacking vars")]
	public int monsterDamage;

	public Transform IceBallLauncingTra;


	float timer;
	[SerializeField] float timeBetweenAttacks;



	#region Mono calls

	void Awake(){

		// just to make sure that all component are there so they the DecisionsTrees won't have to check if they're null
		if (Checker.ObjectDoesNotHave (gameObject, typeof(NavMeshAgent))) return;
		if (Checker.ObjectDoesNotHave (gameObject, typeof(MonsterHealth))) return;
		if (Checker.ObjectDoesNotHave (gameObject, typeof(BoxCollider))) return;

		// check if first child has an Animator
		ModelObj = transform.GetChild (0).gameObject;
		if (Checker.ObjectDoesNotHave (ModelObj, typeof(Animator))) return;


		coll = GetComponent<BoxCollider> ();
		navAgent = GetComponent<NavMeshAgent> ();
		monsterHealth = GetComponent<MonsterHealth> ();
		anim = ModelObj.GetComponent<Animator> ();


		// make the agent stop when the monster is in range to attack (so the monster won't puch the player)
		navAgent.stoppingDistance = DistanceFromPlayerToAttack;
	}

	void OnEnable(){

		EventsClass.OnPlayerSpawn += SetBackToMovement;

		EventsClass.OnPlayerDeath += PlayerIsDead;

		ResetStates ();

	}
	void OnDisable(){

		EventsClass.OnPlayerSpawn -= SetBackToMovement;

		EventsClass.OnPlayerDeath -= PlayerIsDead;

	}

	void FixedUpdate(){
		
		DecisionsTrees.UpdateBehaviorFor (this);

		CurrentState.CallUpdateStateActions (this);

	}
		
	#endregion


	#region Events calles

	void PlayerIsDead(){

		IsTargetDead = true;

	}


	// the monsters how will listen to OnPlayerSpawn event are the one in the map already ( the monster how killed the player in previous round )
	void SetBackToMovement(GameObject player){

		IsTargetDead = false;

		SetTarget (player);

		ResetStates ();

	}

	#endregion


	#region Decisions Trees calles

	public void SetMonsterState(StateType type){
		
		State NewState = StatesManager.GetPunchState (type);

		monsterState = type;


		CurrentState.CallExitStateActions (this);

		CurrentState = NewState;

		CurrentState.CallEnterStateActions (this);

	}
		
	public bool IsPlayerInRange(){
		return Checker.IsCloseEnough (transform.position, playerBrain.transform.position, DistanceFromPlayerToAttack);
	}

	public bool IsFarEnoughFromPlayer(){
		return Checker.IsCloseEnough (transform.position, playerBrain.transform.position, 50);
	}

	public bool IsAtSafePoint(){
		return Checker.IsCloseEnough (transform.position, theSafePoint, 5);
	}

	#endregion


	#region Monsters Actions Calls

	public bool IsTimeForNextAttack(){

		if (timer < Time.time) {

			timer = Time.time + timeBetweenAttacks;

			return true;
		}

		return false;
	}

	public void DisappearAfter(){
		StartCoroutine (disappear ());
	}
	IEnumerator disappear(){

		yield return new WaitForSeconds (2);

		gameObject.SetActive (false);

	}

	#endregion

	// called in MonsterSpawner.cs
	public void SetTarget(GameObject Player){
		playerObj = Player;
		playerBrain = playerObj.GetComponent<PlayerBrain> ();
		playerHealth = playerObj.GetComponent<PlayerHealth> ();
	}



	void ResetStates(){

		monsterState = StateType.None;

		CurrentState = new State ("NEW");

	}


}