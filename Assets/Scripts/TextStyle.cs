using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMP_TextHoverPress : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private TextMeshProUGUI tmpText;
    private Color originalColor;

    [Header("Colors")]
    public Color hoverColor = Color.yellow;
    public Color pressedColor = Color.red;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalColor = tmpText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tmpText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tmpText.color = originalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tmpText.color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Retour à hover si toujours sur le bouton
        if (RectTransformUtility.RectangleContainsScreenPoint(
            (RectTransform)transform, Input.mousePosition, eventData.enterEventCamera))
        {
            tmpText.color = hoverColor;
        }
        else
        {
            tmpText.color = originalColor;
        }
    }
}