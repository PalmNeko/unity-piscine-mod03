using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class EnergyUI : MonoBehaviour
{
    private Label energyElement;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        energyElement = uiDocument.rootVisualElement.Q<Label>("Energy");
    }

    public void SetEnergy(float energy)
    {
        if (energyElement != null)
        {
            energyElement.text = energy.ToString();
        }
    }

}
