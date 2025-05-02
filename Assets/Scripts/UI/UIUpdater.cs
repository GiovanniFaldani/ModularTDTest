using System.Reflection;
using TMPro;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    public static UIUpdater Instance;

    [SerializeField] private TextMeshProUGUI moneyPanel;
    [SerializeField] private GameObject nextWaveButton;

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
    }

    private void UpdateMoney()
    {
        moneyPanel.text = "Money: " + GameManager.Instance.playerMoney;
    }

    public void SpawnWave()
    {
       //EnemySpawner.Instance.SpawnWave() ...
    }

    public void BuildPreviewForModule(int index)
    {
        // TODO check for money
        ModuleTypes moduleType = (ModuleTypes)index;
        if (GameManager.Instance.playerMoney >= Turret_Builder.Instance.costs[moduleType])
        {
            GameManager.Instance.playerMoney -= Turret_Builder.Instance.costs[moduleType];
            Turret_Builder.Instance.SpawnPreview(moduleType);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
