using UnityEngine;
using System.Collections;

public abstract class IShip : MonoBehaviour {
	
	public Rect bounds;
	public float initialHealth;
	public IWeapon weapon;
	public Transform explosionPrelab;
	
	int currentWeaponIndex;
	Vector3 initialVelocity;
	Vector3 initialAcceleration;
	Vector3 acceleration;
	float health;
	bool isDead;

	void Start() {
		Debug.Log ("Ship Start");
		Debug.Log (initialVelocity);
		rigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
		acceleration = initialAcceleration;
		health = initialHealth;
		isDead = health > 0f;
	}

	void FixedUpdate() {
		rigidbody.AddForce(acceleration,ForceMode.Acceleration);
//		Die ();
//		Debug.Log ("Velocity: " + rigidbody.velocity + "; " + rigidbody.velocity.magnitude);
//		Debug.Log ("Acceleration: " + acceleration + "; " + acceleration.magnitude);
	}


	public void SetInitialVelocity(Vector3 initialVelocity) {this.initialVelocity = initialVelocity;}
	public void SetInitialAcceleration(Vector3 initialAcceleration) {this.initialAcceleration = initialAcceleration;}
	public void Accelerate(Vector3 targetAcceleration) {acceleration = targetAcceleration;}

	public bool Fire() {
		return weapon.Fire();
	}

	public void ApplyDamage(float damage) {
		health -= damage;
		if (health <= 0f) {
			Die();
		}
	}

	void Die() {
		isDead = true;

		Instantiate (explosionPrelab, transform.position, transform.rotation);
		Debug.Log ("Died...");
		Destroy (gameObject);
	}

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
