using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    [Header("Card Content")]
    [SerializeField] private Sprite sprite;
    [SerializeField] private string title;
    [SerializeField] private string description;
    public int cost;

    [Header("UI References")]
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;


    public void OnValidate()
    {
        if (cardImage != null)
            cardImage.sprite = sprite;

        if (titleText != null)
            titleText.text = title;

        if (descriptionText != null)
            descriptionText.text = description;

        if (costText != null)
            costText.text = ("Cost: " + cost + " flies");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
