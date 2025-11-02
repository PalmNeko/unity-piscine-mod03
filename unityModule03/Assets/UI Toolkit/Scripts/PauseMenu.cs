using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class PauseMenu : MonoBehaviour
{
    public ConfirmMenu confirmMenu;

    private Button quitButton;
    private Button resumeButton;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        resumeButton = uiDocument.rootVisualElement.Q<Button>("OK");
        quitButton = uiDocument.rootVisualElement.Q<Button>("Cancel");
        resumeButton.RegisterCallback<ClickEvent>(OnResume);
        quitButton.RegisterCallback<ClickEvent>(OnQuit);
    }

    private void OnDisable()
    {
        resumeButton.UnregisterCallback<ClickEvent>(OnResume);
        quitButton.UnregisterCallback<ClickEvent>(OnQuit);
    }

    public void Enable()
    {
        Time.timeScale = 0.0f;
        gameObject.SetActive(true);
    }

    private void OnResume(ClickEvent evt)
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
    
    private void OnQuit(ClickEvent evt)
    {
        Debug.Log("Figguer");
        confirmMenu.Enable();
    }
}
