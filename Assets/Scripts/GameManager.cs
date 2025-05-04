using UnityEngine;

// Singleton that handles game over and player money and purchases
public class GameManager : MonoBehaviour
{
    [SerializeField] private int startMoney;

    public int playerMoney;
    public int baseHP;

    private static GameManager instance;
    public static GameManager Instance {  get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        playerMoney = startMoney;
    }

    public void AddMoney(int money)
    {
        this.playerMoney += money;
    }


    public void WinGame()
    {
        MenuManager.Instance.ShowWinScreen();
    }
    public void GameOver()
    {
        MenuManager.Instance.ShowGameOverScreen();
    }
}
