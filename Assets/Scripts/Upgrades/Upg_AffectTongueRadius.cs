using UnityEngine;

public class Upg_AffectTongueRadius : UpgradeEffect
{
    [SerializeField] private float tongueRadiusModifier;
    public override void Apply(UpgradeContext context)
    {
        GameManager.Instance.AdjustTongueRadius(tongueRadiusModifier);
    }
}
