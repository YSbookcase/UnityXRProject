using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonHandler : MonoBehaviour
{
    public void OnRestartButton()
    {
        Time.timeScale = 1f;



        // �������� �̱��� �ʱ�ȭ
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetState(); // ���� �ʱ�ȭ �Լ�
           
        }
       

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainTitle"); // ���� �޴� �� �̸�
    }
}
