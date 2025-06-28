using UnityEngine;
using UnityEngine.Rendering;

public class TongHit : MonoBehaviour
{

    private SpriteRenderer sr;
    private CircleCollider2D cc;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTongueRadiusChanged -= UpdateTongueRadius; // Prevent double subscription
            GameManager.Instance.OnTongueRadiusChanged += UpdateTongueRadius;
            UpdateTongueRadius(GameManager.Instance.tongueRadius); // Set initial value
        }
    }

    public void UpdateTongueRadius(float newTongueRadius)
    {
        Debug.Log(newTongueRadius);
        if (sr != null && sr.sprite != null && cc != null)
        {
            cc.radius = newTongueRadius;

            float spriteDiameter = sr.sprite.bounds.size.x; // Assuming it's a circle sprite
            float desiredDiameter = cc.radius * 2f;

            float scale = desiredDiameter / spriteDiameter;

            transform.localScale = new Vector3(scale, scale, 1f);
        }
    }


}