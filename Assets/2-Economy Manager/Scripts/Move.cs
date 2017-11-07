using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	[SerializeField] float Speed;

	void Update () {

		transform.Translate (-Vector3.forward * Speed * Time.deltaTime);

	}


}