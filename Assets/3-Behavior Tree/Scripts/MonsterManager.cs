using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class MonsterManager : MonoBehaviour {


	static List<Transform> SafePositions = new List<Transform> ();


	List<GameObject> AllMonsters = new List<GameObject> ();

	bool ShouldSpawn = false;

	GameObject ThePlayer;

	// define thie enum here because this is the only class that will use it
	enum SpawningType { None, AllPunch, AllWizard, Random };


	[Header("What monsters Should be spawned:")]
	[SerializeField] SpawningType spawningType;

	[Header("Number of monster in the map at one time")]
	[SerializeField] int MonstersInMap;

	[Space]

	[Header("Spawning Points")]
	[SerializeField] List<Transform> SpawnPositions;


	void Awake(){
		// for simplicity make the safe point the same as the spawning ones
		SafePositions = new List<Transform> (SpawnPositions);
	}

	void OnEnable(){
		EventsClass.OnPlayerSpawn += StartSpawning;
		EventsClass.OnPlayerDeath += StopSpawning;
		EventsClass.OnSceneLeave += Clear;
	}
	void OnDisable(){
		Clear ();
	}

	void Clear(){
		EventsClass.OnPlayerSpawn -= StartSpawning;
		EventsClass.OnPlayerDeath -= StopSpawning;
		EventsClass.OnSceneLeave -= Clear;
	}

	void StartSpawning(GameObject player){
		ThePlayer = player;
		ShouldSpawn = true;
	}

	void StopSpawning(){
		ShouldSpawn = false;
	}


	void FixedUpdate () {

		if (!ShouldSpawn)
			return;
		
		while (NumberOfActiveMonsters() < MonstersInMap) {
		
			SpawnMonster ();
		
		}

	}


	int NumberOfActiveMonsters(){

		int x = 0;

		foreach (GameObject monster in AllMonsters) {
		
			if (monster.activeSelf)
				x++;

		}

		return x;

	}


	void SpawnMonster(){

		GameObject monster = GetMonster ();

		monster.transform.position = GetRandomSpawnPoint ();

		monster.GetComponent<MonsterBrain> ().SetTarget (ThePlayer);

		AllMonsters.Add (monster);

	}

	GameObject GetMonster(){

		switch (spawningType) {

		case SpawningType.AllPunch:
			return MasterPool.Get (PrefabTypes.Punchy);
			break;

		case SpawningType.AllWizard:
			return MasterPool.Get (PrefabTypes.Wizard);
			break;

		case SpawningType.Random:
			return MasterPool.Get ( GetRandomMonsterType() );
			break;

		default:
			Debug.LogError ("You passed the wrong SpawningType in MonsterManager it's : " + spawningType.ToString ());
			return null;
		}


	}

	PrefabTypes GetRandomMonsterType(){

		// make an array of monsters you want to choose randomly from (just add more PrefabTypes to the array to extend it)
		PrefabTypes[] monstersArray = { PrefabTypes.Punchy, PrefabTypes.Wizard };

		// float between 0 -> 1
		float rand = Random.value;

		// expamples: if monstersArray.Length = 3 then 0.33			if monstersArray.Length = 5 then 0.2
		float precentage = (float) 1 / monstersArray.Length;

		float probability = 0;

		for (int x = monstersArray.Length; x > 0; x--) {
			
			if (rand < probability)
				return monstersArray [x];
			else
				probability += precentage;

		}

		Debug.LogError ("something wrong happend while getting random Monster in MonsterManager.GetRandomMonsterType()");
		return PrefabTypes.None;

	}


	Vector3 GetRandomSpawnPoint(){
	
		int ran = Random.Range (0, SpawnPositions.Count);
	
		return SpawnPositions [ran].position;

	}


	// called in MonstersActions;
	public static Vector3 RandomSafePoint(){

		int ran = Random.Range (0, SafePositions.Count);

		return SafePositions[ran].position;

	}

}