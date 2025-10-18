using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UIDocument))]
public class TurretCardBehaiviour : MonoBehaviour
{
    private List<TurretCardElement> turretCardList;
    public DraggingTurret draggingTurret;

    void OnEnable()
    {
        UIDocument ui = GetComponent<UIDocument>();
        turretCardList = ui.rootVisualElement.Query<TurretCardElement>().ToList();
        if (turretCardList == null)
            return;
        foreach (TurretCardElement turretCard in turretCardList)
            turretCard.RegisterCallback<MouseDownEvent, TurretCardElement>(OnDragStart, turretCard);
    }

    void OnDisable()
    {
        foreach (TurretCardElement turretCard in turretCardList)
            turretCard.UnregisterCallback<MouseDownEvent, TurretCardElement>(OnDragStart);
    }
    
    void OnDragStart(MouseDownEvent evt, TurretCardElement elm)
    {
        if (evt.propagationPhase != PropagationPhase.BubbleUp)
            return;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        DraggingTurret container = Instantiate(draggingTurret, target, transform.rotation);
        container.Init(elm.spec);
    }
}
