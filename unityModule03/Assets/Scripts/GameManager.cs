using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

[SelectionBase]
public class GameManager : MonoBehaviour
{
    static private GameManager instance;
    public BaseController baseController;
    public float initialEnergy = 0f;
    public float gainEnergy = 1f;
    public float energyCooldown = 1f;
    public UIManager ui;
    public SpawnSpec[] waves;
    public EnemySpawnerController spawner;
    public float score;

    public float energy;
    private float nextGainEnergyTime;
    private int waveIndex = 0;
    private Dictionary<int, float> scores = new Dictionary<int, float>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.instance = this;
    }

    void Start()
    {
        GameManager.instance = this;
        Play();
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
        spawner.StopSpawn();
        // エネミーの削除
        EnemyController[] enemyControllers = GameObject.FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
        foreach (EnemyController enemyController in enemyControllers)
            Destroy(enemyController.gameObject);
        ShowScore();
    }

    public void WaveClear()
    {
        ShowScore();
    }

    void FixedUpdate()
    {
        if (baseController != null)
        {
            ui.SetHP(baseController.HP.HP, baseController.HP.maxHP);
            ui.SetEnergy(energy);
        }
        if (Time.time > nextGainEnergyTime)
        {
            energy += gainEnergy;
            nextGainEnergyTime = NextGainEnergyTime();
        }
        if (IsWaveClear())
        {
            ShowScore();
        }
    }
    void ShowScore()
    {
        Time.timeScale = 0.0f;
        CalcScore();
        SceneManager.LoadScene("Score", LoadSceneMode.Additive);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            ui.pauseMenu.Enable();
        }
        UpdateGUI();
    }

    private bool IsWaveClear()
    {
        if (Mathf.Approximately(Time.timeScale, 0.0f))
            return false;
        if (spawner.IsEndSpawn() && IsClearAllEnemies())
            return true;
        return false;
    }

    private bool IsClearAllEnemies()
    {
        EnemyController[] enemyControllers = GameObject.FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
        return enemyControllers.Length == 0;
    }
    
    private void UpdateGUI()
    {
    }

    private float NextGainEnergyTime()
    {
        return Time.time + energyCooldown;
    }

    public bool UseEnergy(float energy)
    {
        if (this.energy < energy)
            return false;
        this.energy -= energy;
        return true;
    }

    public float CalcScore()
    {
        score = 0.0f;

        scores[waveIndex] = energy + baseController.HP.HP;
        score = scores.Sum(x => x.Value);
        return score;
    }
    
    public void NextWave()
    {
        waveIndex += 1;
        Play();
        return;
    }

    public void Replay()
    {
        Play();
    }

    public void Play()
    {
        Time.timeScale = 1.0f;
        energy = initialEnergy;
        baseController.HP.ResetHP();
        if (waveIndex >= waves.Length)
            ToEnding();
        else
            spawner.NextWave(waves[waveIndex]);
    }

    private void ToEnding()
    {
        SceneManager.LoadScene("Ending");
    }
    
    static public GameManager GetInstance()
    {
        return GameManager.instance;
    }
}
