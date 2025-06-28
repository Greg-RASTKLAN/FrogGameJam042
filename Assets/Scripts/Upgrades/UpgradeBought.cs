using UnityEngine;

public class UpgradeBought : MonoBehaviour
{

    [SerializeField] private UpgradeContext context;

    private UpgradeEffect[] effects;
    private int upgradeCost;

    private void Awake()
    {
        effects = GetComponentsInChildren<UpgradeEffect>();
        upgradeCost = GetComponentInParent<UpgradeCard>().cost;
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
}
