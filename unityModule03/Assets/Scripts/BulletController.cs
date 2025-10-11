using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[SelectionBase]
public class BulletController : MonoBehaviour
{
    public Health target;
    public float speed = 1;

	private Rigidbody2D rb;
	private Attacker attacker;

	public void Initialize(Health target, float speed, Attacker attacker)
	{
		this.target = target;
        this.speed = speed;
        this.attacker = attacker;
	}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
	{
		rb = GetComponent<Rigidbody2D>();
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
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (attacker == null)
            return;
        if (other.gameObject == target.gameObject)
        {
            attacker.Attack(target);
            Destroy(this.gameObject);
        }
    }
    
    bool IsLostTarget()
    {
        return target == null;
    }
}
