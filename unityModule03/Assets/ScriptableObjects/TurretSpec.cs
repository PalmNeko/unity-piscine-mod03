using UnityEngine;

[CreateAssetMenu(fileName = "TurretSpec", menuName = "Scriptable Objects/TurretSpec")]
public class TurretSpec : ScriptableObject
{
    public float cooldown = 1f;
	public float damage = 1f;
	public float cost = 1f;
	public float range = 1f;
	public Sprite icon;
}
