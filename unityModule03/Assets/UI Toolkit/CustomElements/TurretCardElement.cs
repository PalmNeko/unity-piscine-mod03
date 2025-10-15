using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[UxmlElement]
public partial class TurretCardElement : VisualElement
{
    private TurretSpec _spec;
    private VisualElement turretImage;
    private Label price;
    private Label cooldown;
    private Label damage;

    [UxmlAttribute]
    public TurretSpec spec
    {
        get => _spec;
        set
        {
            _spec = value;
            Refresh();
        }
    }

    private VisualTreeAsset _uxml;
    [UxmlAttribute]
    [SerializeField]
    [Tooltip("TurretCard.uxmlが設定されることを想定しています。")]
    private VisualTreeAsset uxml
    {
        get => _uxml;
        set
        {
            _uxml = value;
            Refresh();
        }
    }

    public TurretCardElement() { }

    public TurretCardElement(TurretSpec spec)
    {
        Init(spec);
    }

    public void Init(TurretSpec spec)
    {
        this.spec = spec;
        Refresh();
    }

    public void Refresh()
    {
        if (_uxml == null)
            return;

        Clear();
        _uxml.CloneTree(this);

        price = this.Q<Label>("price");
        cooldown = this.Q<Label>("cooldown");
        damage = this.Q<Label>("damage");
        turretImage = this.Q("turret-image");

        if (spec != null)
        {
            if (price != null)
                price.text = spec.cost.ToString();
            if (cooldown != null)
                cooldown.text = spec.cooldown.ToString();
            if (damage != null)
                damage.text = spec.damage.ToString();
            if (turretImage != null)
                turretImage.style.backgroundImage = new StyleBackground(spec.icon);
        }
        
    }
}
