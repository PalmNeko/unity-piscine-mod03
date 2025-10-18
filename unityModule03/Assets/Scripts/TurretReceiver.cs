using UnityEngine;

public class TurretReceiver : MonoBehaviour
{
    bool isSet;

    void Start()
    {
        isSet = false;
    }

    public void SetTurret(DraggingTurret draggingTurret)
    {
        if (isSet)
            return;
        isSet = true;
        if (draggingTurret == null)
            return;
        if (draggingTurret.spec == null || draggingTurret.spec.prefab == null)
            return;
        Instantiate(draggingTurret.spec.prefab, transform);
    }
}
