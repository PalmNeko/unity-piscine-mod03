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

	private SpawnSpec spec;
	private float nextSpawn;
	private bool canSpawn = true;
	private int spawnCount = 0;
	
	void FixedUpdate()
	{
		if (spec == null)
			return;
		if (canSpawn && Time.time > nextSpawn)
		{
			SpawnEnemy(enemy);
			nextSpawn = GetNextSpawnTime();
		}
	}

	public void NextWave(SpawnSpec spec)
    {
		canSpawn = true;
		this.spec = spec;
		spawnCount = 0;
		nextSpawn = GetNextSpawnTime();
		Debug.Log(nextSpawn);
    }

	public void StopSpawn()
	{
		canSpawn = false;
	}

	public void SpawnEnemy(EnemyController enemy)
	{
		if (canSpawn == false)
			return;
		EnemyController newEnemy = Instantiate(enemy, transform);
		newEnemy.Initialize(target);
		spawnCount += 1;
		if (IsEndSpawn())
		{
			StopSpawn();
		}
	}
	
	public bool IsEndSpawn()
	{
		if (spec == null)
			return false;
		Debug.Log($"{spawnCount} {spec.spawnCount}");
		return spawnCount >= spec.spawnCount;
    }

	private float GetNextSpawnTime()
	{
		if (spec == null)
			return Single.PositiveInfinity;
		return Time.time + spec.spawnDelay;
	}
}
