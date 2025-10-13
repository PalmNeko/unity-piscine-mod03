using UnityEngine;

[SelectionBase]
public class GameManager : MonoBehaviour
{
    public BaseController baseController;
    public UIManager ui;

    void OnEnable()
    {
        baseController.onGameOver.AddListener(GameOver);
    }

    void OnDisable()
    {
        baseController.onGameOver.RemoveListener(GameOver);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        // スポーンのストップ
        EnemySpawnerController[] spawnControllers = GameObject.FindObjectsByType<EnemySpawnerController>(FindObjectsSortMode.None);
        foreach (EnemySpawnerController spawnController in spawnControllers)
            spawnController.StopSpawn();
        // エネミーの削除
        EnemyController[] enemyControllers = GameObject.FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
        foreach (EnemyController enemyController in enemyControllers)
            Destroy(enemyController.gameObject);
    }

    void Update()
    {
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        if (baseController != null)
        {
            ui.SetHP(baseController.HP.HP, baseController.HP.maxHP);
        }
    }
}
