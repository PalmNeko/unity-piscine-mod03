using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
[SelectionBase]
public class BaseController : MonoBehaviour
{

    [SerializeField]
    private Health HP;

    [Header("Events")]
    [ReadOnly]
    public UnityEvent onGameOver;

    void OnEnable()
    {
        HP.onZeroHP.AddListener(InvokeGameOver);
        HP.onTakeDamage.AddListener(OnTakeDamage);
    }

    void OnDisable()
    {
        HP.onZeroHP.RemoveListener(InvokeGameOver);
        HP.onTakeDamage.RemoveListener(OnTakeDamage);
    }
    
    /// コールバック群
    public void InvokeGameOver(Health health)
    {
        if (onGameOver != null)
            onGameOver.Invoke();
    }

    public void OnTakeDamage(Health health, Attacker attacker)
    {
        Debug.Log($"{health.HP}");
    }
}
