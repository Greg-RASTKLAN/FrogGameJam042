using UnityEngine;

public abstract class UpgradeEffect : MonoBehaviour
{
    public abstract void Apply(UpgradeContext context);
}
