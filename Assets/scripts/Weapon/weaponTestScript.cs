using UnityEngine;
using System.Collections;

public class weaponTestScript : MonoBehaviour {
		
	    public Rigidbody projectile1;
	    public Rigidbody projectile2;

	    public BasicWeapon weapon;
		
		void Start(){
		    weapon = gameObject.AddComponent("BasicWeapon") as BasicWeapon;
            weapon.speed = 20f;
		    weapon.initProjectile (20, BasicProjectile.projectileType.bullet);
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetButton ("Fire1")) {
			if (weapon.projectile.pType == BasicProjectile.projectileType.bullet) {
				   weapon.shootProjectiles(transform.position, transform.rotation,projectile1);
			   } else {
				   weapon.shootProjectiles(transform.position, transform.rotation,projectile2);
			   }
			}
		}
}
