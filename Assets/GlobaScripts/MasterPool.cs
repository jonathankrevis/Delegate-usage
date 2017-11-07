using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using GlobalVars;

 
public class MasterPool : MonoBehaviour {


	readonly string PrefabsPath = "Prefabs";


	static Transform MasterPoolTransform;


	#region Data based on PrefabType


	static Dictionary<PrefabTypes, GameObject> PrefabsReference = new Dictionary<PrefabTypes, GameObject> ();


	static Dictionary<PrefabTypes, List<GameObject>> PrefabsPools = new Dictionary<PrefabTypes, List<GameObject>> ();


	static Dictionary<PrefabTypes, Transform> PrefabsParents = new Dictionary<PrefabTypes, Transform> ();


	#endregion



	void Awake () {
		
		MasterPoolTransform = transform;

		SetPrefabsData ();

		StartCoroutine (UpdateNames ());

	}

	void SetPrefabsData(){


		GameObject[] ResroucesPrefabs = Resources.LoadAll<GameObject> (PrefabsPath);


		PrefabTypes[] AllPrefabsTypes = System.Enum.GetValues (typeof(PrefabTypes)) as PrefabTypes[];


		foreach (PrefabTypes PrefabTypeName in AllPrefabsTypes) {

			foreach (GameObject prefabObj in ResroucesPrefabs) {

				if (string.Equals (PrefabTypeName.ToString(), prefabObj.name)) {

					PrefabsReference.Add (PrefabTypeName, prefabObj);

					PrefabsPools.Add (PrefabTypeName, new List<GameObject> () );

				}

			}

		}


	}


	#region Clear statics

	// All statics needs to be cleared before leaving the Scene

	void OnEnable(){
		EventsClass.OnSceneLeave += ClearStatics;
	}
	void OnDisable(){
		EventsClass.OnSceneLeave -= ClearStatics;
	}

	void ClearStatics(){
		PrefabsReference.Clear ();
		PrefabsPools.Clear ();
		PrefabsParents.Clear ();
		MasterPoolTransform = null;
	}

	#endregion


	#region Updateing Names

	// update name 4 time a second
	IEnumerator UpdateNames(){

		while (true) {

			yield return new WaitForSeconds (0.25f);

			foreach (PrefabTypes type in PrefabsParents.Keys) {
				UpdateNameAnalysis (type);
			}

		}

	}


	void UpdateNameAnalysis(PrefabTypes type){

		// A : actived	D : Disactived
		int A = 0;
		int D = 0;

		foreach (GameObject child in PrefabsPools[type]) {

			if (child.gameObject.activeSelf)
				A++;
			else
				D++;

		}

		PrefabsParents[type].name = type.ToString() + " (A:" + A + " ,D:" + D + ")";

	}

	#endregion


	public static GameObject Get(PrefabTypes type){

		if ( ! PrefabsPools.ContainsKey (type)) {
			Debug.LogError ("the PrefabType: (" + type.ToString () + ") don't have prefab in MasterPool");
			return null;
		}

		// return unative prefab of this type
		foreach (GameObject obj in PrefabsPools[type]) {

			if ( ! obj.activeSelf) {

				obj.SetActive (true);

				return obj;

			}

		}


		// or create a new one
		GameObject NewObj = Instantiate (PrefabsReference[type]);

		PrefabsPools[type].Add (NewObj);

		SetToParent (NewObj, type);

		return NewObj;

	}

	// Add it to the right Parett or create one 
	static void SetToParent(GameObject NewObj, PrefabTypes type){
		
		if ( ! PrefabsParents.ContainsKey (type)) {
			
			GameObject PrefabParent = new GameObject (type.ToString ());

			PrefabParent.transform.SetParent (MasterPoolTransform);

			PrefabsParents.Add (type, PrefabParent.transform);

		}

		NewObj.transform.SetParent (PrefabsParents [type]);

	}


}