using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HPBar : MonoBehaviour
{

    public Color fullColor = Color.green;
    public Color emptyColor = Color.red;

    private float maxHP = 100f;
    private VisualElement hpValue;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        hpValue = uiDocument.rootVisualElement.Q<VisualElement>("HPValue");
    }

    public void Initialize(float maxHP, float currentHP = -1f)
    {
        this.maxHP = maxHP;
        if (currentHP >= 0)
        {
            SetHP(currentHP);
        }
    }

    public void SetHP(float hp)
    {
        maxHP = hp;
        if (hpValue != null)
        {
            float hpRate = hp / maxHP;
            Length length = new Length(hpRate * 100f, LengthUnit.Percent);
            hpValue.style.width = new StyleLength(length);
            hpValue.style.backgroundColor = Color.Lerp(emptyColor, fullColor, hpRate);
        }
    }
}
