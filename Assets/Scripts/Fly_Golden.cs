using UnityEngine;

public class Fly_Golden : Fly
{
    public override void OnCaught()
    {
        GameManager.Instance.AddCurrency(GameManager.baseFlyValue * 10);
        // Play sparkle VFX
        Destroy(gameObject);
    }
}
