using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public Vector3 ExplosionPos;

	public float ExplosionRange, ExplosionFource;

	public int Damage;

	public LayerMask EffectedLayers;


	public Explosion (Vector3 pos, int damage, float range, LayerMask layer){
		ExplosionPos = pos;
		Damage = damage;
		ExplosionRange = range;
		EffectedLayers = layer;
	}


}