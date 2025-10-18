using UnityEngine;

public class DraggingTurret : MonoBehaviour
{
    public TurretSpec spec;
    public SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
            UpdateDrag();
        else
            Destroy(gameObject);
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
}
