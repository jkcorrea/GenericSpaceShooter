using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour, DeathListener 
{
	public GameObject baseEnemy;
    public GameObject GUIObject;
	public float period;
	public float speed;

	float timer;

	public void beginSpawning() {
		timer = period;
		spawnEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0f && Random.value >= 0.67f) {
			spawnEnemy();
			timer = period;
		}
	}

	void spawnEnemy() {
		AIController[] enemyTypes = GetComponentsInChildren<AIController>();
		int index = Mathf.FloorToInt(Random.value*enemyTypes.Length);
		GameObject enemy = GameObject.Instantiate(baseEnemy, transform.position, transform.rotation) as GameObject;
		IShip enemyShip = enemy.GetComponent<IShip>();
		registerDeathListeners (enemyShip);
		AIController enemyType = enemyTypes[index];
		enemy.AddComponent(enemyType.GetType());
		enemy.rigidbody.AddForce(transform.forward*speed, ForceMode.VelocityChange);
	}

	void registerDeathListeners(IShip enemyShip) {
		enemyShip.RegisterDeathListener(this);
		enemyShip.RegisterDeathListener(GUIObject.GetComponent<Scoreboard>());
	}

	public void NotifyDeath(IShip deadShip) {
		timer = 0f;
	}

}
