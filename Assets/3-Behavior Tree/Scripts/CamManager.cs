using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalVars;

public class CamManager : MonoBehaviour {


	Vector3 StartGamePos;

	GameObject player;
	Vector3 Offset;


	float CamOrthographicSizeGoal;

	float ZoomedOutCamSize = 25;
	float ZoomedInCamSize = 12;


	float ZoomingSpeed;

	float ZoomingInSpeed = 3;
	float ZoomingOutSpeed = 1;


	void Awake(){
		CamOrthographicSizeGoal = ZoomedOutCamSize;
		StartGamePos = transform.position;
	}

	void OnEnable(){
		EventsClass.OnPlayerSpawn += ZoomIn;
		EventsClass.OnPlayerSpawn += SetTarget;

		EventsClass.OnPlayerDeath += ZoomOut;
		EventsClass.OnPlayerDeath += ReleasePlayer;
		EventsClass.OnPlayerDeath += ResetToStartPos;

		EventsClass.OnSceneLeave += Clear;
	}
	void OnDisable(){
		Clear ();
	}

	void Clear(){
		EventsClass.OnPlayerSpawn -= ZoomIn;
		EventsClass.OnPlayerSpawn -= SetTarget;

		EventsClass.OnPlayerDeath -= ZoomOut;
		EventsClass.OnPlayerDeath -= ReleasePlayer;
		EventsClass.OnPlayerDeath -= ResetToStartPos;

		EventsClass.OnSceneLeave -= Clear;
	}

	void SetTarget(GameObject pl){
		
		player = pl;

		Offset = transform.position - player.transform.position;

	}
	void ReleasePlayer(){
		player = null;
		Offset = Vector3.zero;
	}

	void ZoomIn(GameObject player){
		CamOrthographicSizeGoal = ZoomedInCamSize;
		ZoomingSpeed = ZoomingInSpeed;
	}
	void ZoomOut(){
		CamOrthographicSizeGoal = ZoomedOutCamSize;
		ZoomingSpeed = ZoomingOutSpeed;
	}
	void ResetToStartPos(){
		StartCoroutine (BakeToStartingPos ());
	}

	// get to the start position after 1.5 to give the Zooming out animation time to finish
	IEnumerator BakeToStartingPos(){
	
		yield return new WaitForSeconds (1.5f);

		transform.position = StartGamePos;

	}

	void FixedUpdate () {


		if (player != null) {
			transform.position = player.transform.position + Offset;
		}

		Camera.main.orthographicSize = Mathf.Lerp ( Camera.main.orthographicSize,
													CamOrthographicSizeGoal,
													ZoomingSpeed * Time.deltaTime);

	}

}