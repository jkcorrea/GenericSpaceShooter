using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public GameObject baseEnemy;
	public float period;
	public float speed;

	float timer;

	// Use this for initialization
	void Start () {
		timer = period;
		spawnEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0f) {
			spawnEnemy();
			timer = period;
		}
	}

	void spawnEnemy() {
		AIController[] enemyTypes = GetComponentsInChildren<AIController>();
		int index = Mathf.FloorToInt(Random.value*enemyTypes.Length);
		GameObject enemy = GameObject.Instantiate(baseEnemy, transform.position, transform.rotation) as GameObject;
		AIController enemyType = enemyTypes[index];
		Debug.Log(enemy);
		enemy.AddComponent(enemyType.GetType());
		enemy.rigidbody.AddForce(transform.forward*speed, ForceMode.VelocityChange);
	}

}
