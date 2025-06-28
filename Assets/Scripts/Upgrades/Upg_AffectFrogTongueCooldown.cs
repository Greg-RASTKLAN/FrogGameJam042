using UnityEngine;

public class Upg_AffectFrogTongueCooldown : UpgradeEffect
{
    [SerializeField] private float tongueCooldown;
    public override void Apply(UpgradeContext context)
    {
        GameManager.Instance.DecreaseTongueCooldown(tongueCooldown);
    }
}
