using UnityEngine;

public class Upg_IncreaseFlyValue : UpgradeEffect
{

    public override void Apply(UpgradeContext context)
    {
        GameManager.Instance.IncreaseBaseFlyValue(1);
    }
}
