using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;

    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowWinScreen()
    {
        UIUpdater.Instance.gameObject.SetActive(false);
        winScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ShowGameOverScreen()
    {
        UIUpdater.Instance.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ShowPauseScreen()
    {
        UIUpdater.Instance.gameObject.SetActive(false);
        pauseScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void UnPause()
    {
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
        UIUpdater.Instance.gameObject.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
