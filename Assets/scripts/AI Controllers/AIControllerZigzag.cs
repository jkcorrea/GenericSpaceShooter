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

public class AIControllerZigzag : AIController {

	public float amplitude = 3f;
	public float frequency = 5f;

	override
	protected void Start() {
		base.Start ();
		ship.rigidbody.AddForce(Vector3.right*frequency*Mathf.Sqrt(amplitude*amplitude), ForceMode.VelocityChange);
	}

	override
	protected void Update() {
		base.Update ();
		ship.Accelerate(getAcceleration());
//		ship.weapon.Face (target, accuracy);
		Fire ();
	}

	Vector3 getAcceleration() {
		return -transform.position.x*Vector3.right*frequency*frequency;
	}

	override protected void SetModifiers(int level) {
		frequency = 1 + 2*Math.Min (level, 5);
	}

}

