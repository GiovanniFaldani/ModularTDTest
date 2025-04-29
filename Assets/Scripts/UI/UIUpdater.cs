using TMPro;
using UnityEngine;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyPanel;
    [SerializeField] private GameObject nextWaveButton;

    void Start()
    {
        
    }

    void Update()
    {
        UpdateMoney(); 
    }

    private void UpdateMoney()
    {
        moneyPanel.text = "Money: " + GameManager.Instance.playerMoney;
    }
}
