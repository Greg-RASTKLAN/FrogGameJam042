using UnityEngine;
using UnityEngine.SceneManagement;

public class Upg_FinalUpgrade : UpgradeEffect
{
    public override void Apply(UpgradeContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
