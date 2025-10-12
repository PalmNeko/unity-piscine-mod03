using UnityEngine;

public class Attacker
{
    public float power = 1.0f;

    public void Attack(Health hp)
    {
        hp.TakeDamage(this);
    }
}
