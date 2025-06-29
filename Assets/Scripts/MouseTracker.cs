using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    void Start()
    {
        // Subscribe to tongue radius updates
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTongueRadiusChanged -= UpdateTonguePreviewScale; // Prevent double-subscription
            GameManager.Instance.OnTongueRadiusChanged += UpdateTonguePreviewScale;

            // Set initial scale
            UpdateTonguePreviewScale(GameManager.Instance.tongueRadius);
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        transform.rotation *= Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    void UpdateTonguePreviewScale(float newRadius)
    {
        float diameter = newRadius + 0.2f;
        transform.localScale = new Vector3(diameter, diameter, 1f);
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTongueRadiusChanged -= UpdateTonguePreviewScale;
        }
    }
}
