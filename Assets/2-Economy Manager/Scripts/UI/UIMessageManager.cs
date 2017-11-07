using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalVars;

public class UIMessageManager : MonoBehaviour {

	// set new messages as children to this transform because all UI stuff need to be under Canvas
	static Transform thisTransform;

	static float timer;
	static float timeBetweenMessages = 0.5f;

	void Awake(){
		thisTransform = transform;

	}


	public static void ShowMessage(string message, Color col){
	
		if ( ! IsTimeForNextMessage ())
			return;


		GameObject mess = MasterPool.Get (PrefabTypes.UIMessage);

		mess.transform.SetParent (thisTransform);

		if (Checker.ObjectDoesNotHave (mess, typeof(UIMessage))) return;

		mess.GetComponent<UIMessage>().SetMessage(message, col);


		// set the RectTransform to the center of the canvase
		RectTransform messageTran = mess.GetComponent<RectTransform> ();

		messageTran.localPosition = Vector3.zero;
		messageTran.sizeDelta = Vector2.zero;

	}


	static bool IsTimeForNextMessage(){
	
		if (timer < Time.time) {
		
			timer = Time.time + timeBetweenMessages;

			return true;

		}

		return false;
	}

}