using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[SelectionBase]
public class BulletController : MonoBehaviour
{
    public EnemyController target;
    public TurretSpec turretSpec;

	public void Initialize(EnemyController target, TurretSpec turretSpec)
	{
		this.target = target;
        this.turretSpec = turretSpec;
	}

	void FixedUpdate() {
        if (IsLostTarget() || target == null)
        {
            Destroy(gameObject);
            return;
        }
		MoveToClose(target.transform);
	}

    void MoveToClose(Transform target)
    {
        if (target == null)
            return;
        float step = turretSpec.bulletSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Attacker attacker = new Attacker();
        attacker.power = turretSpec.damage;
        if (other.gameObject == target.gameObject)
        {
            attacker.Attack(target.health);
            Destroy(this.gameObject);
        }
    }
    
    bool IsLostTarget()
    {
        return target == null;
    }
}
