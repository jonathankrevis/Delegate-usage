using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class MovingCube : MonoBehaviour {


	[SerializeField] float ZMovementAmount;

	[SerializeField] GameObject transparentCube;


	public void StartMovingProcess(){

		Action DoAction = new Action (MoveRight);
		DoAction += HideTransparentCube;

		Action DropAction = new Action (HideTransparentCube);

		EconomyManager.PayFor (DataStorage.GetBillFor (CostType.Movement), DoAction, DropAction);

	}

	void MoveRight(){
		
		Vector3 MovementPos = transform.position;
		MovementPos.z += ZMovementAmount;
		transform.position = MovementPos;

	}

	#region Button Event Trigger Calls

	public void ShowTransparentCube(){

		transparentCube.SetActive (true);

	}

	public void HideTransparentCube(){

		transparentCube.SetActive (false);

	}

	#endregion


}