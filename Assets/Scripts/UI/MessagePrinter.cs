using TMPro;
using UnityEngine;

public class MessagePrinter : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;

    public static MessagePrinter Instance { get; private set; }
    private float _timer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PrintMessage(string message, float duration)
    {
        messagePanel.SetActive(true);
        messagePanel.GetComponentInChildren<TextMeshProUGUI>().text = message;
        _timer = duration;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = -1;
            messagePanel.SetActive(false);
        }
    }
}
