using UnityEngine;

public class Upg_IncreaseGoldenFlyChance : UpgradeEffect
{
    [SerializeField] private Fly flyToIncrease;
    [SerializeField] private float percentToIncrease;

    public override void Apply(UpgradeContext context)
    {
        FliesSpawner.Instance.IncreaseFlySpawnChance(flyToIncrease, percentToIncrease);
    }
}
