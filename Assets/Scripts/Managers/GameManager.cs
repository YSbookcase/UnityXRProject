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

    // 씬 전환
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 게임 종료
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 종료
#else
        Application.Quit(); // 빌드 환경에서 종료
#endif
    }


    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    private bool isVictory = false;
    public bool IsVictory => isVictory;

    public void GameOver()
    {

        Debug.Log($"[GameManager] GameOver 호출됨 {isGameOver}");

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
        Debug.Log("[GameManager] Victory 호출됨");

        isVictory = true;

        UI?.ShowVictoryUI(); // 승리 UI 표시 등
    }



    public void ResetState()
    {
        isGameOver = false;
       
    }

}
