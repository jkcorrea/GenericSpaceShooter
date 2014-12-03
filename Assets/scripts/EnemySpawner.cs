using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour, DeathListener 
{
	public GameObject baseEnemy;
    public GameObject GUIObject;
	public float period;
	public float speed;
	public GameObject boss;
	public int ScoreForBoss;

	float timer;
	bool spawnedBoss;

	// Use this for initialization
	void Start () {
		timer = period;
		spawnEnemy();
//		spawnBoss ();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0f && Random.value >= 0.67f) {
			spawnEnemy();
			timer = period;
		}
		if (!spawnedBoss && GameObject.FindObjectOfType<Scoreboard>().GetScore () >= ScoreForBoss) {
			spawnBoss();
			spawnedBoss = true;
		}
	}

	void spawnBoss() {
		GameObject enemy = GameObject.Instantiate(boss, transform.position, transform.rotation) as GameObject;
		IShip bossShip = enemy.GetComponent<IShip>();
		registerDeathListeners(bossShip);
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
