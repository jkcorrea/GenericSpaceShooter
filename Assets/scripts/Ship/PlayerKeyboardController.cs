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

public class PlayerKeyboardController : MonoBehaviour {

	IShip ship;

	void Start() {
		ship = GetComponent<IShip>();
	}

	void FixedUpdate() {
		Vector3 accel = Vector3.zero;
		accel.x = Input.GetAxis ("Horizontal");
		accel.y = Input.GetAxis ("Vertical");
		ship.Accelerate(accel);
		if (Input.GetButton ("Fire1")) {
			ship.Fire ();
		}

	}

	

}

