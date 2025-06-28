using UnityEngine;

public class Upg_AffectSpawnRate : UpgradeEffect
{
    [SerializeField] private float spawnRateAffection = 10f;
    public override void Apply(UpgradeContext context)
    {
        FliesSpawner.spawnCooldown *= (1-(spawnRateAffection/100));
    }
}
