using UnityEngine;

public interface IHealth {

	GameObject TheObject ();

	void Damage(int damage);

	int GetObjectMaxHealth();

	int GetObjectCurrentHealth();

}