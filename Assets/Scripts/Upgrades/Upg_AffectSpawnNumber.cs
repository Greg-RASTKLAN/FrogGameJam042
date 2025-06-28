using UnityEngine;

public class Upg_AffectSpawnNumber : UpgradeEffect
{
    [SerializeField] private int spawnNumberAffection;
    public override void Apply(UpgradeContext context)
    {
        FliesSpawner.fliesToSpawn += spawnNumberAffection;
    }
}
