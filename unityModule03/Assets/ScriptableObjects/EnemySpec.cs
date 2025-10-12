using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpec", menuName = "Scriptable Objects/EnemySpec")]
public class EnemySpec : ScriptableObject
{
    public Health health;
    public float damage = 1.0f;
    public float speed = 1.0f;
    public Sprite icon;
}
