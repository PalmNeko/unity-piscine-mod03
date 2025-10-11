using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
public class EnemySpawnerController : MonoBehaviour
{
	public EnemyController enemy;
	public Transform target;
	public float spawnDelay = 1;
	public List<Health> targetHealths;

	private DateTime nextSpawn;
	private bool canSpawn = true;
	
	void Start()
	{
		nextSpawn = GetNextSpawnDateTime();
		canSpawn = true;
	}
	
	void Update()
	{
		if (DateTime.Now > nextSpawn)
		{
			SpawnEnemy(enemy);
			nextSpawn = GetNextSpawnDateTime();
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
		newEnemy.Initialize(target, targetHealths, newEnemy.speed);
	}

	private DateTime GetNextSpawnDateTime()
	{
		return DateTime.Now.AddSeconds(spawnDelay);
	}
}
