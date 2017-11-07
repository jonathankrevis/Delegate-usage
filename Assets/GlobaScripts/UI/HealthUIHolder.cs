using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalVars;

public class HealthUIHolder : MonoBehaviour {

	// you need this to reference this transform in the static void
	static Transform thisTrans;

	void Awake(){
		thisTrans = transform;
	}


	public static void SetNewObjectHealth(IHealth IH){
	

		GameObject healthUI = MasterPool.Get (PrefabTypes.HealthUI);
		// the HealthUI is a UI elements so it needs to be a child of this panel
		healthUI.transform.SetParent (thisTrans);

		if (Checker.ObjectDoesNotHave (healthUI, typeof(HealthUI))) return;

		healthUI.GetComponent<HealthUI> ().SetMonster (IH);

	}


}
