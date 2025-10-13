using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Health
{
    public float HP = 5.0f;
    public float maxHP = 5.0f;

    [Header("Events")]
    [ReadOnly]
    public UnityEvent<Health> onZeroHP;
    [ReadOnly]
    public UnityEvent<Health, Attacker> onTakeDamage;

    public void TakeDamage(Attacker attacker)
    {
        if (HP == 0)
            return;
        HP -= attacker.power;
        onTakeDamage.Invoke(this, attacker);
        if (HP <= 0)
        {
            HP = 0.0f;
            if (onZeroHP != null)
                onZeroHP.Invoke(this);
        }
    }
}
