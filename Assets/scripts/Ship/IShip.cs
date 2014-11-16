using UnityEngine;
using System.Collections;

public abstract class IShip : MonoBehaviour {
	
	public Rect Bounds;
	
	int currentWeaponIndex;
	Vector3 initialPosition;
	Vector3 initialVelocity;
	Vector3 initialAcceleration;
	Vector3 velocity;
	Vector3 acceleration;

	void Start() {
		transform.position = initialPosition;
		velocity = initialVelocity;
		acceleration = initialAcceleration;
	}

	void Update() {
		velocity += acceleration*Time.deltaTime;
		transform.position += new Vector3(velocity.x, velocity.y, 0f)*Time.deltaTime;
		acceleration = Vector3.zero;
	}


	public void SetInitialPosition(Vector3 initialPosition) {this.initialPosition = initialPosition;}
	public void SetInitialVelocity(Vector3 initialVelocity) {this.initialVelocity = initialVelocity;}
	public void SetInitialAcceleration(Vector3 initialAcceleration) {this.initialAcceleration = initialAcceleration;}
	public Vector3 GetVelocity(){return velocity;}
	public void Accelerate(Vector3 targetAcceleration) {acceleration = targetAcceleration;}
	public bool Fire() {return false;}
	//	public Weapon CycleWeapon();

	//	override
	//	public Weapon CycleWeapon() {
	//		currentWeaponIndex++;
	//		if (currentWeaponIndex == weapons.Count) {
	//			currentWeaponIndex = 0;
	//		}
	//		currentWeapon = weapons[currentWeaponIndex];
	//		return currentWeapon;
	//	}

	
	//	IList<Weapon> weapons;

	//	Weapon currentWeapon;
	


}
