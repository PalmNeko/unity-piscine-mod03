using UnityEngine;
using System;

[SelectionBase]
public class GameManager : MonoBehaviour
{
    public BaseController baseController;
    public float initialEnergy = 0f;
    public float gainEnergy = 1f;
    public float energyCooldown = 1f;
    public UIManager ui;

    private float energy;
    private DateTime nextGainEnergyTime;

    void Start()
    {
        energy = initialEnergy;
        nextGainEnergyTime = NextGainEnergyTime();
    }

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
        if (DateTime.Now > nextGainEnergyTime)
        {
            energy += gainEnergy;
            nextGainEnergyTime = NextGainEnergyTime();
        }
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        if (baseController != null)
        {
            ui.SetHP(baseController.HP.HP, baseController.HP.maxHP);
            ui.SetEnergy(energy);
        }
    }

    private DateTime NextGainEnergyTime()
    {
        return DateTime.Now.AddSeconds(energyCooldown);
    }

}
