using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class TileMenuRuntimeUI : MonoBehaviour
{
    public string loadSceneName = "SampleScene";

    private Button playButton;
    private Button quitButton;
    
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        playButton = uiDocument.rootVisualElement.Q<Button>("Play");
        quitButton = uiDocument.rootVisualElement.Q<Button>("Quit");

        playButton.RegisterCallback<ClickEvent>(PlayStart);
        quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }

    private void OnDisable()
    {
        playButton.UnregisterCallback<ClickEvent>(PlayStart);
        quitButton.UnregisterCallback<ClickEvent>(QuitGame);
    }
    
    private void QuitGame(ClickEvent evt)
    {
        GameController.QuitGame();
    }

    private void PlayStart(ClickEvent evt)
    {
        SceneManager.LoadScene(loadSceneName);
    }
}
