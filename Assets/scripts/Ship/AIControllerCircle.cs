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
using System.Collections.Generic;
using UnityEngine;

public class AIControllerCircle : AIController {

	public float Radius = 3f;
	public float Velocity = 10f;

	IShip ship;
	Vector2 center;

	void Start() {
		Debug.Log ("Controller Start");
		center = new Vector2(transform.position.x, transform.position.y);
		transform.position = new Vector3(transform.position.x - Radius, transform.position.y, transform.position.z);
		ship = GetComponent<IShip>();
		ship.rigidbody.AddForce(new Vector3(0f, Velocity, 0f), ForceMode.VelocityChange);
		ship.SetInitialAcceleration(new Vector3(Velocity*Velocity/Radius, 0f, 0f));
	}

	void Update() {
		ship.Accelerate(getAcceleration());
//		ship.Fire ();
	}

	Vector3 getAcceleration() {
		float magnitude = Velocity*Velocity/Radius;
		Vector3 direction = new Vector3(center.x - transform.position.x, center.y - transform.position.y, 0f).normalized;
		Debug.Log (direction);
//		Debug.Log (magnitude*direction);
		return magnitude*direction;
	}

}
