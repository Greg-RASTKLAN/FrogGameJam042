using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBought : MonoBehaviour
{
    [SerializeField] private UpgradeContext context;

    private UpgradeEffect[] effects;
    public int upgradeCost;
    private Button buttonRef;

    [Header("Progression Curve")]
    [SerializeField] private bool isIncremental = true;
    [SerializeField] private int numberOfSteps = 5;
    [SerializeField] private AnimationCurve increaseCurve;
    [SerializeField] private int basePrice = 5;
    [SerializeField] private int finalPrice = 300;

    private float stepIncrement;    // How much normalized value increases per step
    private int currentStep = 0;    // Step counter (int)

    private void Awake()
    {
        effects = GetComponentsInChildren<UpgradeEffect>();
        buttonRef = GetComponent<Button>();

        stepIncrement = 1f / numberOfSteps;
        RecalculateUpgradeCost();
    }

    private void Start()
    {
        SubscribeToGameManager();
    }

    private void SubscribeToGameManager()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnCurrencyChanged -= UpdateButton;
            GameManager.Instance.OnCurrencyChanged += UpdateButton;
            UpdateButton(GameManager.Instance.currency);
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

        if (isIncremental)
        {
            currentStep = Mathf.Clamp(currentStep + 1, 0, numberOfSteps);
            RecalculateUpgradeCost();

            // Optional: hide if maxed
            if (currentStep >= numberOfSteps)
                Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject); // One-time upgrade
        }
    }

    private void RecalculateUpgradeCost()
    {
        float t = (float)currentStep / numberOfSteps;
        float curveValue = increaseCurve.Evaluate(t);
        float interpolatedPrice = Mathf.Lerp(basePrice, finalPrice, curveValue);
        upgradeCost = Mathf.RoundToInt(interpolatedPrice);

        GetComponentInParent<UpgradeCard>().UpdateCostText(upgradeCost);
    }

    public void UpdateButton(int currency)
    {
        if (buttonRef != null)
        {
            buttonRef.interactable = (currency >= upgradeCost);
        }
    }
}
