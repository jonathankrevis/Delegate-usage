using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIMessage : MonoBehaviour {
	

	[SerializeField] float messageLifeTime;
	[SerializeField] float YSpeed;
	[SerializeField] float AlphaSpeed;


	float timeSinceEnable;

	Text messageText;

	Color showColor;


	#region MonoBehaviourCalles

	void Awake () {
		messageText = GetComponent<Text> ();
		showColor = messageText.color;
	}

	void OnEnable(){

		messageText.color = showColor;

		timeSinceEnable = Time.time + messageLifeTime;

	}

	void FixedUpdate () {

		MoveUp ();

		Fadeout ();

		if (IsOutOfTime()) {
			gameObject.SetActive (false);	
		}

	}

	#endregion


	#region FixedUpdate calles

	void MoveUp(){
		Vector3 tempPos = transform.position;
		tempPos.y += YSpeed;
		transform.position = tempPos;
	}

	void Fadeout(){
		Color tempColor = messageText.color;
		tempColor.a -= AlphaSpeed;
		messageText.color = tempColor;
	}

	bool IsOutOfTime(){

		if (timeSinceEnable < Time.time)
			return true;

		return false;

	}

	#endregion


	// called in UIMessageManager
	public void SetMessage(string message, Color col){
		messageText.text = message;
		messageText.color = col;
	}

}
