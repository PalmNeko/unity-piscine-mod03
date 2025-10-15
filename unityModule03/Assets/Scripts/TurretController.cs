using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
[SelectionBase]
public class TurretController : MonoBehaviour
{
    public BulletController bullet;
    public TurretSpec spec;
    public string targetTag;
    public CircleCollider2D detectCircleRange;

    private Attacker attacker;
    private List<EnemyController> shotRangeEnemies;
    private DateTime nextShotDateTime;

    void OnValidate()
    {
        AssignObject();
    }

    private void AssignObject()
    {
        if (spec == null)
            return;
        attacker = new Attacker
        {
            power = spec.damage
        };

        detectCircleRange = GetComponent<CircleCollider2D>();
        detectCircleRange.radius = spec.range;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AssignObject();
        nextShotDateTime = GetNextShotDateTime();
        shotRangeEnemies = new List<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanShot())
        {
            Shot();
            nextShotDateTime = GetNextShotDateTime();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (shotRangeEnemies == null)
            return ;
        if (other.gameObject.tag == targetTag)
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
                shotRangeEnemies.Add(enemy);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (shotRangeEnemies == null)
            return ;
        if (other.gameObject.tag == targetTag)
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
                shotRangeEnemies.RemoveAll((EnemyController pivot) => pivot == enemy);
        }
    }

    EnemyController GetClosestEnemy()
    {
        EnemyController closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        if (shotRangeEnemies == null)
            return null;
        foreach (EnemyController shotRangeEnemy in shotRangeEnemies)
        {
            float distance;
            distance = Vector3.Distance(transform.position, shotRangeEnemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = shotRangeEnemy;
            }
        }
        return closestEnemy;
    }

    DateTime GetNextShotDateTime()
    {
        return DateTime.Now.AddSeconds(spec.cooldown);
    }

    bool CanShot()
    {
        // Debug.Log($"{shotRangeEnemies} {shotRangeEnemies.Count} {DateTime.Now > nextShotDateTime}");
        return shotRangeEnemies != null
            && shotRangeEnemies.Count > 0
            && DateTime.Now > nextShotDateTime;
    }
    
    void Shot()
    {
        EnemyController closestEnemy = GetClosestEnemy();
        if (closestEnemy == null)
            return;
        BulletController newBullet = Instantiate(bullet, transform);
        newBullet.Initialize(closestEnemy, spec);
    }
}
