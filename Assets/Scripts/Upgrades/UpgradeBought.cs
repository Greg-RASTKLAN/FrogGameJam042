using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeBought : MonoBehaviour
{

    [SerializeField] private UpgradeContext context;

    private UpgradeEffect[] effects;
    private int upgradeCost;
    private Button buttonRef;


    private void Awake()
    {
        effects = GetComponentsInChildren<UpgradeEffect>();
        upgradeCost = GetComponentInParent<UpgradeCard>().cost;
        buttonRef = gameObject.GetComponent<Button>();
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
            GameManager.Instance.OnCurrencyChanged -= UpdateButton; // Prevent double subscription
            GameManager.Instance.OnCurrencyChanged += UpdateButton;
            UpdateButton(GameManager.Instance.currency); // Set initial value
        }
    }

    public void BuyWithSavedContext()
    {
        Buy(context);
    }
    public void Buy(UpgradeContext context)
    {
        GameManager.Instance.SpendCurrency(upgradeCost);
        foreach (UpgradeEffect effect in effects)
        {
            effect.Apply(context);
        }
        Destroy(gameObject);
    }

    public void UpdateButton(int currency)
    {
        if (buttonRef != null) {
            if (currency < upgradeCost)
            {
                buttonRef.interactable = false;
            }
            else buttonRef.interactable = true;
        }

    }
}
