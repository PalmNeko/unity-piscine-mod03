using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[UxmlElement]
public partial class TurretCardElement : VisualElement
{
    private TurretSpec _spec;
    private bool _ui_init = false;
    private VisualElement turretImage;
    private VisualElement overlay;
    private Label price;
    private Label cooldown;
    private Label damage;

    public bool isEnabled;

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
        if (_uxml == null || _ui_init == true)
            return;
        _ui_init = true;
        _uxml.CloneTree(this);

        price = this.Q<Label>("price");
        cooldown = this.Q<Label>("cooldown");
        damage = this.Q<Label>("damage");
        turretImage = this.Q("turret-image");
        overlay = this.Q("Overlay");
        Update();
    }

    public void Update()
    {
        if (_spec == null)
            return;
        if (price != null)
            price.text = _spec.cost.ToString();
        if (cooldown != null)
            cooldown.text = _spec.cooldown.ToString();
        if (damage != null)
            damage.text = _spec.damage.ToString();
        if (turretImage != null)
            turretImage.style.backgroundImage = new StyleBackground(_spec.icon);
        GameManager gameManager = GameManager.GetInstance();
        if (gameManager == null)
            return;
        if (_spec.cost > gameManager.energy)
            Disable();
        else
            Enable();
    }

    public void Disable()
    {
        isEnabled = false;
        if (overlay == null)
            return;
        Color color = Color.gray;
        color.a = 0.5f;
        overlay.style.backgroundColor = new StyleColor(color);
    }
    
    public void Enable()
    {
        isEnabled = true;
        if (overlay == null)
            return;
        Color color = Color.white;
        color.a = 0f;
        overlay.style.backgroundColor = new StyleColor(color);
    }
}
