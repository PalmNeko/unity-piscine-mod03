using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(CircleCollider2D))]
[SelectionBase]
public class TurretController : MonoBehaviour
{
    public BulletController bullet;
    public float coolDown = 1.0f;
    public float shotRange = 0.5f;
    public string targetTag;
    public CircleCollider2D detectCircleRange;

    private Attacker attacker;
    private List<Health> shotRangeEnemies;
    private DateTime nextShotDateTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attacker = GetComponent<Attacker>();
        detectCircleRange = GetComponent<CircleCollider2D>();
        nextShotDateTime = GetNextShotDateTime();
        shotRangeEnemies = new List<Health>();
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
            Health health = other.gameObject.GetComponent<Health>();
            if (health != null)
                shotRangeEnemies.Add(health);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (shotRangeEnemies == null)
            return ;
        if (other.gameObject.tag == targetTag)
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (health != null)
                shotRangeEnemies.RemoveAll((Health pivot) => pivot == health);
        }
    }

    Health GetClosestEnemy()
    {
        Health closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        if (shotRangeEnemies == null)
            return null;
        foreach (Health shotRangeEnemy in shotRangeEnemies)
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
        return DateTime.Now.AddSeconds(coolDown);
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
        Health closestEnemyHealth = GetClosestEnemy();
        if (closestEnemyHealth == null)
            return;
        BulletController newBullet = Instantiate(bullet, transform);
        newBullet.Initialize(closestEnemyHealth, newBullet.speed, attacker);
    }
}
