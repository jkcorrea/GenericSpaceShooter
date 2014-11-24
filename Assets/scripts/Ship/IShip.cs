using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IShip : MonoBehaviour {
	
	public Rect bounds;
	public float initialHealth;
	public IWeapon weapon;
	public Transform explosionPrelab;
	public float score;
	
	int currentWeaponIndex;
	Vector3 initialVelocity;
	Vector3 initialAcceleration;
	Vector3 acceleration;
	float health;
	bool isDead;

	List<DeathListener> deathListeners;

	void Awake() {
		deathListeners = new List<DeathListener>();
	}

	public virtual void Start() {
//		Debug.Log ("Ship Start");
//		Debug.Log (initialVelocity);
		rigidbody.AddForce(initialVelocity, ForceMode.VelocityChange);
		acceleration = initialAcceleration;
		health = initialHealth;
		isDead = health > 0f;
//		weapon = gameObject.AddComponent ("BulletWeapon") as BulletWeapon;
		weapon = gameObject.AddComponent ("LaserWeapon") as LaserWeapon;
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

	public void RegisterDeathListener(DeathListener listener) {
		deathListeners.Add(listener);
	}

	void Die() {
		isDead = true;
		Instantiate (explosionPrelab, transform.position, transform.rotation);
		notifyDeathListeners();
		Debug.Log ("Died...");
		Destroy (gameObject);
	}

	void notifyDeathListeners() {
		foreach (DeathListener listener in deathListeners) {
			listener.NotifyDeath(this);
		}
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
