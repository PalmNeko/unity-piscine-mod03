using UnityEngine;

[CreateAssetMenu(fileName = "SpawnSpec", menuName = "Scriptable Objects/SpawnSpec")]
public class SpawnSpec : ScriptableObject
{
    public float spawnDelay;
    public int spawnCount;
}
