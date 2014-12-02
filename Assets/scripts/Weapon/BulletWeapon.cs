//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public class BulletWeapon : IWeapon {

	public Transform projectile;
	public float cooldown;
	public Transform shootPosition;
	public float speed;

	float cooldownRemaining;
	bool canFire;

	void Start() {
		cooldownRemaining = 0f;
		canFire = true;
	}

	void Update() {
		cooldownRemaining = Mathf.Max (0f, cooldownRemaining - Time.deltaTime);
		if (cooldownRemaining == 0f) {
			canFire = true;
		}
	}

	override
	public bool Fire() {
		if (canFire) {
			canFire = false;
			cooldownRemaining = cooldown;
			Debug.Log (projectile);
			Debug.Log (shootPosition);
			Transform bullet = GameObject.Instantiate(projectile, shootPosition.position, shootPosition.rotation) as Transform;
			bullet.rigidbody.velocity = shootPosition.forward * speed;
			//audio.Play ();
			return true;
		}
		return false;
	}

}

