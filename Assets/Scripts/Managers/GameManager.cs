using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public AudioManager Audio { get; private set; }
    public PlayerManager Player { get; private set; }

    public TileManager Tile { get; private set; }

    //public UnitPlacer Placer { get; private set;  }

    public SystemUI UI { get; private set; }

    //public WaveManager Wave { get; private set; }

    private void Awake() => Init();

    private void Init()
    {
        base.SingletonInit();
        Audio = GetComponentInChildren<AudioManager>();
        Player = GetComponentInChildren<PlayerManager>();
        Tile = GetComponentInChildren<TileManager>();
        //Placer = GetComponentInChildren<UnitPlacer>();
        UI = FindObjectOfType<SystemUI>();
        //Wave = GetComponentInChildren<WaveManager>();


        if (Audio == null) Debug.LogError("AudioManager is missing");
        if (Player == null) Debug.LogError("PlayerManager is missing");
        if (Tile == null) Debug.LogError("TileManager is missing");
        //if (Placer == null) Debug.LogError("UnitPlacer is missing");
        if (UI == null) Debug.LogError("SystemUI is missing from the scene.");
        //if (Wave == null) Debug.LogError("WaveManage is missing");
    }

    public void RegisterUI(SystemUI ui)
    {
        UI = ui;
    }

    // �� ��ȯ
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ���� ����
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ����
#else
        Application.Quit(); // ���� ȯ�濡�� ����
#endif
    }


    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    private bool isVictory = false;
    public bool IsVictory => isVictory;

    public void GameOver()
    {

        Debug.Log($"[GameManager] GameOver ȣ��� {isGameOver}");

        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("Game Over!");
        //Wave?.StopWaves();
        Time.timeScale = 0f;

        Audio.StopBgm();
        UI.ShowGameOverUI();  
    }

    public void Victory()
    {
        Debug.Log("[GameManager] Victory ȣ���");

        isVictory = true;

        UI?.ShowVictoryUI(); // �¸� UI ǥ�� ��
    }



    public void ResetState()
    {
        isGameOver = false;
       
    }

}
