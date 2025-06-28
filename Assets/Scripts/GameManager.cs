using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currency;

    public event Action<int> OnCurrencyChanged;

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


    public void DEBUGAddCurrency()
    {
        AddCurrency(500);
    }
}
