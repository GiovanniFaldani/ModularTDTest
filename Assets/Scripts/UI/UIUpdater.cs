using TMPro;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    public static UIUpdater Instance;

    [SerializeField] private TextMeshProUGUI moneyPanel;
    [SerializeField] private TextMeshProUGUI baseHPPanel;
    [SerializeField] public GameObject nextWaveButton;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        UpdateMoney(); 
        UpdateBaseHP();
    }

    private void UpdateMoney()
    {
        moneyPanel.text = "Money: " + GameManager.Instance.playerMoney;
    }

    private void UpdateBaseHP()
    {
        baseHPPanel.text = "Base HP: " + GameManager.Instance.baseHP;
    }

    public void SpawnWave()
    {
        EnemySpawner.Instance.SpawnWave();
        // Hide spawnwave UI until next wave
        nextWaveButton.SetActive(false);
    }

    public void BuildPreviewForModule(int index)
    {
        ModuleTypes moduleType = (ModuleTypes)index;
        if (GameManager.Instance.playerMoney >= Turret_Builder.Instance.costs[moduleType])
        {
            GameManager.Instance.playerMoney -= Turret_Builder.Instance.costs[moduleType];
            Turret_Builder.Instance.SpawnPreview(moduleType);
        }
        else
        {
            MessagePrinter.Instance.PrintMessage("Not enough money!", 5);
        }
    }
}
