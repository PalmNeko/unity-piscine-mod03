using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ConfirmMenu : MonoBehaviour
{
    public string redirectSceneName;

    private Button yesButton;
    private Button noButton;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        yesButton = uiDocument.rootVisualElement.Q<Button>("OK");
        noButton = uiDocument.rootVisualElement.Q<Button>("Cancel");
        yesButton.RegisterCallback<ClickEvent>(OnYes);
        noButton.RegisterCallback<ClickEvent>(OnNo);
    }

    private void OnDisable()
    {
        yesButton.UnregisterCallback<ClickEvent>(OnYes);
        noButton.UnregisterCallback<ClickEvent>(OnNo);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    private void OnYes(ClickEvent evt)
    {
        SceneManager.LoadScene(redirectSceneName);
    }
    
    private void OnNo(ClickEvent evt)
    {
        gameObject.SetActive(false);
    }
}
