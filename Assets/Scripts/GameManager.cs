using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currency;
    public float tongueRadius = 0.5f;
    public float tongueCooldown = 1f;

    public static int baseFlyValue = 1;
    public static int goldenFlyMultiplier = 10;


    public event Action<int> OnCurrencyChanged;
    public event Action<float> OnTongueRadiusChanged;
    public event Action<float> OnTongueCooldownDecreased;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        OnCurrencyChanged?.Invoke(currency);
    }
    public void SpendCurrency(int amount)
    {
        currency -= amount;
        OnCurrencyChanged?.Invoke(currency);
    }

    public void AdjustTongueRadius(float amount)
    {
        tongueRadius *= (1 + (amount/100));
        OnTongueRadiusChanged?.Invoke(tongueRadius);
    }

    public void DecreaseTongueCooldown (float amount)
    {
        tongueCooldown *= (1 - (amount / 100));
        OnTongueCooldownDecreased?.Invoke(tongueCooldown);
    }

    public void IncreaseBaseFlyValue(int amount)
    {
        baseFlyValue += amount;
    }
    public void DEBUGAddCurrency()
    {
        AddCurrency(500);
    }
}
