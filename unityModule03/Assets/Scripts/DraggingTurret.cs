using UnityEngine;

public class DraggingTurret : MonoBehaviour
{
    public TurretSpec spec;
    public SpriteRenderer spriteRenderer;

    private TurretReceiver targetReceiver;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
            UpdateDrag();
        else
        {
            PutTurret();
            Destroy(gameObject);
        }
    }

    public void Init(TurretSpec spec)
    {
        this.spec = spec;

        if (spec == null || spec.icon == null)
            return;

        if (spriteRenderer == null)
            return;
        spriteRenderer.sprite = spec.icon;
        UpdateDrag();
    }

    void UpdateDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        target.z += 1;
        transform.position = target;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (targetReceiver != null)
            return;
        TurretReceiver turretReceiver = other.GetComponent<TurretReceiver>();
        targetReceiver = turretReceiver;
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (targetReceiver == null)
            return;
        TurretReceiver turretReceiver = other.GetComponent<TurretReceiver>();
        if (turretReceiver == targetReceiver)
            targetReceiver = null;
    }
    
    void PutTurret()
    {
        if (targetReceiver == null)
            return;
        targetReceiver.SetTurret(this);
        if (spec != null)
            GameManager.GetInstance()?.UseEnergy(spec.cost);
    }
}
