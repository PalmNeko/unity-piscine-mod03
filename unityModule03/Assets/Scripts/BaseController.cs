using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class BaseController : MonoBehaviour
{

    [SerializeField]
    public Health HP;

    [Header("Events")]
    [ReadOnly]
    public UnityEvent onGameOver;

    void OnEnable()
    {
        HP ??= new Health();
        HP.onZeroHP.AddListener(InvokeGameOver);
        HP.onTakeDamage.AddListener(OnTakeDamage);
    }

    void OnDisable()
    {
        HP ??= new Health();
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
