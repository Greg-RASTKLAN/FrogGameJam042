using UnityEngine;

public class TongueStretch : MonoBehaviour
{
    public Transform baseTransform;  // The fixed base of the tongue (mouth)
    public Transform targetTransform; // The cursor or target transform the tongue points to

    private SpriteRenderer sr;
    private Vector3 initialScale;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        initialScale = sr.size;
    }

    void Update()
    {
        if (baseTransform == null || targetTransform == null) return;

        // Calculate distance from base to cursor in world space
        float distance = Vector3.Distance(baseTransform.position, targetTransform.position);

        // Calculate direction from base to cursor
        Vector3 direction = (targetTransform.position - baseTransform.position).normalized;

        // Position the tongue base where it should be (optional)
        transform.position = baseTransform.position;

        // Rotate the tongue to face the cursor
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Adjust height (scale.y) based on distance, keep x scale constant
        Vector3 newScale = initialScale;
        newScale.y = distance;
        sr.size = newScale;
    }
}
