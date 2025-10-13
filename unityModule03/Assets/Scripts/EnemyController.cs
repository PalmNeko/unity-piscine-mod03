using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[SelectionBase]
public class EnemyController : MonoBehaviour
{
	public EnemySpec spec;
	public Health health;
	public BaseController target;

	private Rigidbody2D rb;
	private Attacker attacker;

	public void Initialize(BaseController target)
	{
		this.target = target;
	}

	void OnEnable()
	{
		health ??= new Health();
		if (health != null)
		{
			health.onZeroHP.AddListener(Defeat);
			health.maxHP = spec.maxHP;
			health.HP = spec.maxHP;
        }
	}

	void OnDisable()
	{
		if (health != null)
			health.onZeroHP.RemoveListener(Defeat);
	}
	
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		attacker = new Attacker();
		attacker.power = spec.damage;
		health ??= new Health();
	}

	void FixedUpdate() {
		MoveToClose(target.transform);
	}

	void MoveToClose(Transform target)
	{
		if (target == null || spec == null)
			return;
		float step = spec.speed * Time.deltaTime;
		transform.position = Vector2.MoveTowards(transform.position, target.position, step);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (TryGetHealth(other.gameObject, out var theBase))
        {
			attacker.Attack(theBase.HP);
			Destroy(this.gameObject);
        }
	}

	public bool TryGetHealth(GameObject obj, out BaseController theBase)
	{
		theBase = null;
		if (target == null)
			return false;
		theBase = obj.GetComponent<BaseController>();
		if (theBase == null)
			return false;
		return target == theBase;
	}

	public void Defeat(Health health)
    {
		Destroy(gameObject);
    }
}
