using UnityEngine;
using System.Collections;

public class BasicWeapon: MonoBehaviour{
	
	public float speed;
	
	public BasicProjectile projectile;

	Quaternion shootingRotation;

	Vector3 shootingPosition;

//	public BasicWeapon (int theSpeed, int damage, BasicProjectile.projectileType type){
//		speed = theSpeed;
//		projectile = new BasicProjectile(damage,type);
//	}

	public void initProjectile(int damage, BasicProjectile.projectileType type){
		projectile = gameObject.AddComponent("BasicProjectile") as BasicProjectile;
		projectile.damage = damage;
		projectile.pType = type;
	}
	

	public void switchWeaponType(BasicProjectile.projectileType type)
	{
		if (type == BasicProjectile.projectileType.bullet) {

		} else {

		}
	}
	
	public void shootProjectiles(Vector3 position, Quaternion rotation, Rigidbody projectile)
	{
		GameObject clone; 
		clone = Instantiate (projectile, position, rotation) as GameObject;
			
		if(clone != null){
			//clone.rigidbody.AddForce( * speed);
			clone.rigidbody.velocity = transform.TransformDirection (Vector3.forward * speed);
		}

		Destroy(clone, 3f);
	}
		
}
