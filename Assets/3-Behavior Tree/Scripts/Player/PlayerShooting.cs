using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalVars;

public class PlayerShooting : MonoBehaviour {

	[SerializeField] GameObject GunTimPoint;


	float timer = 0;

	[SerializeField] float timeBetweenBullets;

	void Update () {
		
		if (Input.GetMouseButton (0)) {

			if (timer < Time.time) {

				timer = Time.time + timeBetweenBullets;

				Shoot ();

			}
		
		} 
	}



	void Shoot(){
		
		GameObject bullet = MasterPool.Get (PrefabTypes.Bullet);

		bullet.transform.position = GunTimPoint.transform.position;

		bullet.transform.rotation = transform.rotation;

		SFXManager.PlaySFXFor (SFXtype.ShotBullet);

	}


}