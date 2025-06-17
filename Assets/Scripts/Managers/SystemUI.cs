using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DesignPattern;

public class SystemUI : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private Transform mainSlotParent;
    [SerializeField] private GameObject unitCardPrefab;

    private bool isMenuOpen = false;

    private void Awake()
    {

        GameManager.Instance.RegisterUI(this); 



        if (menuPanel == null)
        {
            GameObject found = GameObject.FindWithTag("Menu");
            if (found != null)
                menuPanel = found;
        }
    }

    private void OnEnable()
    {
        Debug.Log("[SystemUI] OnEnable ȣ���");
        GameManager.Instance.RegisterUI(this);

        SceneManager.sceneLoaded += OnSceneLoaded;

        // ���� �̹� �ε�� ��� ���� ����
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainGame")
        {
            AssignDependencies();
            InitializeUI();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);

     
     
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[SystemUI] OnSceneLoaded ȣ��� - ��: {scene.name}");

        if (scene.name == "MainGame")
        {
            AssignDependencies();
            InitializeUI();
        }
    }

    private void AssignDependencies()
    {
       
       

       
       

        if (volumeSlider == null)
            volumeSlider = FindObjectOfType<Slider>();

        if (mainSlotParent == null)
            mainSlotParent = GameObject.Find("UnitCardPool")?.transform;

        if (menuPanel == null)
            menuPanel = GameObject.FindWithTag("Menu");

        if (gameOverPanel == null)
        {
            GameObject go = GameObject.FindWithTag("GameOver");
            if (go != null) gameOverPanel = go;
        }

        if (victoryPanel == null)
        {
            GameObject v = GameObject.Find("VictoryUI");
            if (v != null) victoryPanel = v;
        }

        //  �ʱ� ��Ȱ��ȭ ó��
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (victoryPanel) victoryPanel.SetActive(false);

        Debug.Log($"[SystemUI] GameOverPanel �Ҵ� ����: {(gameOverPanel == null ? "null" : gameOverPanel.name)}");
    }

    private void InitializeUI()
    {
        if (volumeSlider != null && GameManager.Instance?.Audio != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            float initialVolume = GameManager.Instance.Audio.GetBgmVolume();
            volumeSlider.value = initialVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        CloseMenu();
        //Time.timeScale = 0f;


    }

    
    
    

    
    
    
    
    

    
    
    

    public void StartGame()
    {
        //ResumeGame();
       
    }

    public void ExitGame()
    {
        ResumeGame();
        GameManager.Instance.ExitGame();
    }

    public void LoadScene(string sceneName)
    {
        ResumeGame();
        GameManager.Instance.LoadScene(sceneName);
    }

    public void RestartScene()
    {
        ResumeGame();
     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleMenu()
    {
        if (isMenuOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    public void OpenMenu()
    {
        if (menuPanel == null) return;
        isMenuOpen = true;
        menuPanel.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        if (menuPanel == null) return;
        isMenuOpen = false;
        menuPanel.SetActive(false);
        //Time.timeScale = 1f;
    }

    private void ResumeGame() => Time.timeScale = 1f;

    private void OnVolumeChanged(float value)
    {
        GameManager.Instance.Audio.SetBgmVolume(value);
    }

    private void CleanSstMainCardSlots()
    {
        foreach (Transform slot in mainSlotParent)
        {
            if (slot.childCount == 0) continue;

            GameObject oldCard = slot.GetChild(0).gameObject;
          
       

            Destroy(oldCard);

            GameObject newCard = Instantiate(unitCardPrefab, slot);
            newCard.transform.localPosition = Vector3.zero;
            newCard.transform.localRotation = Quaternion.identity;
            newCard.transform.localScale = Vector3.one;

          
          
        }
    }

    
    
    
    

    public void ShowGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverPanel�� UIManager�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

    public void ShowVictoryUI()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("[SystemUI] VictoryPanel�� ������� �ʾҽ��ϴ�.");
        }
    }
}
