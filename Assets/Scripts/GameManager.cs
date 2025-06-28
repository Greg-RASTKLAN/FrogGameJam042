using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currency;
    public float tongueRadius = 0.5f;

    public event Action<int> OnCurrencyChanged;
    public event Action<float> OnTongueRadiusChanged;

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


    public void DEBUGAddCurrency()
    {
        AddCurrency(500);
    }
}
