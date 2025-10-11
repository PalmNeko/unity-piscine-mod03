using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Health))]
[SelectionBase]
public class EnemyController : MonoBehaviour
{
	public Transform target;
	public float speed = 1;
	public Health health;
	public List<Health> targetHealths;

	private Rigidbody2D rb;
	private Attacker attacker;

	public void Initialize(Transform target = null, List<Health>targetHealths = null, float speed = 1.0f)
	{
		this.target = target;
		this.speed = speed;
		this.targetHealths = targetHealths;
	}

	void OnEnable()
	{
		health = GetComponent<Health>();
		if (health != null)
			health.onZeroHP.AddListener(Defeat);
	}

	void OnDisable()
	{
		if (health != null)
			health.onZeroHP.RemoveListener(Defeat);
	}
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		attacker = GetComponent<Attacker>();
		health = GetComponent<Health>();
	}

	void FixedUpdate() {
		MoveToClose(target);
	}

	void MoveToClose(Transform target)
	{
		if (target == null)
			return;
		float step = speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards(transform.position, target.position, step);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (TryGetHealth(other.gameObject, out var health))
        {
			attacker.Attack(health);
			Destroy(this.gameObject);
        }
	}

	public bool TryGetHealth(GameObject obj, out Health health)
	{
		health = null;
		if (targetHealths == null)
			return false;
		health = obj.GetComponent<Health>();
		if (health == null)
			return false;
		return targetHealths.Contains(health);
	}

	public void Defeat(Health health)
    {
		Destroy(gameObject);
    }
}
