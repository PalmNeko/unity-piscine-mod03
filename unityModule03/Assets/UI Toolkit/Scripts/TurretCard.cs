using UnityEngine;

[CreateAssetMenu(fileName = "TurretCard", menuName = "Scriptable Objects/TurretCard")]
public class TurretCard : ScriptableObject
{
    public float cooldown = 1f;
	public float damage = 1f;
	public float cost = 1f;
	public float range = 1f;
	public Sprite icon;
	public GameObject turretPrefab;
}
