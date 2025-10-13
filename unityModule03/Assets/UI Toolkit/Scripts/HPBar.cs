using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HPBar : MonoBehaviour
{

    public Color fullColor = Color.green;
    public Color emptyColor = Color.red;

    private VisualElement hpValue;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        hpValue = uiDocument.rootVisualElement.Q<VisualElement>("HPValue");
    }

    public void SetHP(float hp, float maxHP)
    {
        if (hpValue != null)
        {
            float hpRate = hp / maxHP;
            Length length = new Length(hpRate * 100f, LengthUnit.Percent);
            hpValue.style.width = new StyleLength(length);
            hpValue.style.backgroundColor = Color.Lerp(emptyColor, fullColor, hpRate);
        }
    }
}
