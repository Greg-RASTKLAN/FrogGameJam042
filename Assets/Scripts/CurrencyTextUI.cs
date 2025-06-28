using TMPro;
using UnityEngine;

public class CurrencyTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;

    private void OnEnable()
    {
        SubscribeToGameManager();
    }

    private void Start()
    {
        // Backup subscription in case OnEnable was too early
        SubscribeToGameManager();
    }

    private void SubscribeToGameManager()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCurrencyChanged -= UpdateText; // Prevent double subscription
            GameManager.Instance.OnCurrencyChanged += UpdateText;
            UpdateText(GameManager.Instance.currency); // Set initial value
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCurrencyChanged -= UpdateText;
        }
    }

    private void UpdateText(int newCurrency)
    {
        if (currencyText != null)
        {
            currencyText.text = $"${newCurrency}";
        }
        else
        {
            Debug.LogWarning("CurrencyTextUI: currencyText is not assigned!");
        }
    }
}
