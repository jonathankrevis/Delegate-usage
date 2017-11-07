using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;


/// <summary>
/// 
/// this class is a holder of acions of one state
/// the State get created and filled in StateManager.cs
/// 
/// </summary>


public class State : MonoBehaviour {

	// just for debuing
	string StateName;

	public State(string name){
		StateName = name;
	}


	// just for debuging
	public string TheStateName(){
		return StateName;
	}


	// events that takes (MonsterBrain) as a parameter
	// enter and exit are called in MonsterBrain.SetMonsterState()
	// update is called every FixedUpdate in MonsterBrain
	public event MonsterBrainEvent EnterStateActions, UpdateStateActions, ExitStateActions;


	public void CallEnterStateActions(MonsterBrain mb){
		if (EnterStateActions != null)
			EnterStateActions (mb);
		else
			Debug.LogWarning (StateName + " ENTER event is empty");
	}

	public void CallUpdateStateActions(MonsterBrain mb){
		if (UpdateStateActions != null)
			UpdateStateActions (mb);
		else
			Debug.LogWarning (StateName + " UPDATE event is empty");
	}

	public void CallExitStateActions(MonsterBrain mb){
		if(ExitStateActions != null)
			ExitStateActions (mb);
		else
			Debug.LogWarning (StateName + " EXIT event is empty");
	}


}