using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIDocument))]
public class Ending : MonoBehaviour
{
    VisualElement container;

    private void OnEnable()
    {
        Invoke(nameof(SetPosition), 0.5f);
        var uiDocument = GetComponent<UIDocument>();

        container = uiDocument.rootVisualElement.Q<VisualElement>("Container");
        container.RegisterCallback<ClickEvent>(ToTitle);
    }

    private void OnDisable()
    {
        container.UnregisterCallback<ClickEvent>(ToTitle);
    }
    
    private void SetPosition()
    {
        var uiDocument = GetComponent<UIDocument>();

        var container = uiDocument.rootVisualElement.Q<VisualElement>("Container");
        container.style.top = new StyleLength(new Length(-200.0f, LengthUnit.Percent));
    }

    private void ToTitle(ClickEvent evt)
    {
        SceneManager.LoadScene("Menu");
    }
}
