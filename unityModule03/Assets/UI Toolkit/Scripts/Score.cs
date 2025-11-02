using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Score : MonoBehaviour
{
    private Label rankLabel;
    private Label scoreLabel;
    private Button nextButton;


    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        nextButton = uiDocument.rootVisualElement.Q<Button>("NextButton");
        rankLabel = uiDocument.rootVisualElement.Q<Label>("Rank");
        scoreLabel = uiDocument.rootVisualElement.Q<Label>("Score");

        if (IsClear())
            nextButton.text = "next";
        else
            nextButton.text = "replay";
        nextButton.RegisterCallback<ClickEvent>(OnNext);

        var gameManager = GameManager.GetInstance();
        if (gameManager == null)
            return;
        rankLabel.text = CalcRank(gameManager.score);
        scoreLabel.text = gameManager.score.ToString();
    }

    private void OnDisable()
    {
        nextButton.UnregisterCallback<ClickEvent>(OnNext);
    }

    private void OnNext(ClickEvent evt)
    {
        var gameManager = GameManager.GetInstance();
        if (gameManager == null)
            return;
        if (IsClear())
            gameManager.NextWave();
        else
            gameManager.Replay();
        SceneManager.UnloadSceneAsync("Score");
        return;
    }

    private bool IsClear()
    {
        var gameManager = GameManager.GetInstance();
        if (gameManager == null)
            return false;
        if (gameManager.baseController.HP.HP <= 0)
            return false;
        return true;
    }

    private string CalcRank(float score)
    {
        if (score < 10)
        {
            return "C";
        }
        else if (score < 50)
        {
            return "B";
        }
        else
        {
            return "A";
        }
    }
}
