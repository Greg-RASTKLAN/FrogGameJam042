using UnityEngine;

public class FrogClickChange : MonoBehaviour
{

    public Sprite frog;
    public Sprite frogtong;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = frog;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            spriteRenderer.sprite = frogtong;
        }
        else 
        {
            spriteRenderer.sprite = frog;
        }
    }
}
