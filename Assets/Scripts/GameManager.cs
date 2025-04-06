using UnityEngine;

// Singleton that handles game over and player money and purchases

public class GameManager : MonoBehaviour
{
    private int playerMoney;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int money)
    {
        this.playerMoney += money;
    }

    public void GameOver()
    {

    }
}
