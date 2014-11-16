using UnityEngine;
using System.Collections;

public class BasicProjectile: MonoBehaviour{

	public enum projectileType{
		laser,
		bullet
	}

	public projectileType pType;

	public float damage;

    GameObject enemy;

	void Start() {
		enemy = GameObject.FindGameObjectWithTag("enemy");
	}

	void OnCollisionEnter (Collision collision) {

		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.blue);
		}

		// Apply damage to target object
		enemy.collider.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);//"applyDamage" is the method enemy to call when projectile hit the enemy

	}

	void destoryProjectile(){
		if (gameObject.tag.Equals ("bullet")) {
			Destroy (gameObject, 4f);
			Debug.Log (gameObject.name);
		}
	}

	void Update(){
		destoryProjectile ();
	}


}
