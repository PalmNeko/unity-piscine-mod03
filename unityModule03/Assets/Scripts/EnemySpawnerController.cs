using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
public class EnemySpawnerController : MonoBehaviour
{
	public EnemyController enemy;
	public BaseController target;
	public float spawnDelay = 1;

	private float nextSpawn;
	private bool canSpawn = true;
	
	void Start()
	{
		nextSpawn = GetNextSpawnTime();
		canSpawn = true;
	}
	
	void FixedUpdate()
	{
		if (Time.time > nextSpawn)
		{
			SpawnEnemy(enemy);
			nextSpawn = GetNextSpawnTime();
		}
	}

	public void StopSpawn()
	{
		canSpawn = false;
	}
	
	public void SpawnEnemy(EnemyController enemy)
	{
		if (canSpawn == false)
			return ;
		EnemyController newEnemy = Instantiate(enemy, transform);
		newEnemy.Initialize(target);
	}

	private float GetNextSpawnTime()
	{
		return Time.time + spawnDelay;
	}
}
