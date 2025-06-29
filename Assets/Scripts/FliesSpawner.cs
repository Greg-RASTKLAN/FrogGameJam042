using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class FlyEntry
{
    public Fly prefab;                              // Reference to the prefab
    [Range(0, 100)] public float spawnPercent = 25; // Initial value, editable in Inspector

    [HideInInspector] public float runtimeSpawnPercent; // Used at runtime
}

public class FliesSpawner : MonoBehaviour
{
    public static FliesSpawner Instance;

    public static float spawnCooldown = 3f;
    public static int fliesToSpawn = 1;

    public BoxCollider2D spawnArea;

    [Header("MaxFliesOnScreen")]
    [SerializeField] private int maxFlies = 200;
    private int currentFlies = 0;


    [SerializeField] private List<FlyEntry> flyEntries;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize runtime values from inspector
        foreach (var entry in flyEntries)
        {
            entry.runtimeSpawnPercent = entry.spawnPercent;
        }

        StartCoroutine(SpawnFlies());
    }

    
    IEnumerator SpawnFlies()
    {
        while (true)
        {
            for (int i = 0; i < fliesToSpawn; i++)
            {
                if (currentFlies >= maxFlies)
                    break;
                GameObject flyToSpawn = GetRandomFlyByPercent();
                Instantiate(flyToSpawn, GetRandomPoint(), transform.rotation, transform);
                currentFlies++;
            }
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    private GameObject GetRandomFlyByPercent()
    {
        float total = flyEntries.Sum(e => e.runtimeSpawnPercent);
        if (total <= 0f)
        {
            Debug.LogWarning("Total spawn percent is zero.");
            return flyEntries[0].prefab.gameObject;
        }

        float roll = Random.Range(0f, total);
        float cumulative = 0f;

        foreach (var entry in flyEntries)
        {
            cumulative += entry.runtimeSpawnPercent;
            if (roll <= cumulative)
            {
                return entry.prefab.gameObject;
            }
        }

        return flyEntries[0].prefab.gameObject;
    }


    //Call this function after upgrades
    public void NormalizeSpawnChance()
    {
        float total = flyEntries.Sum(e => e.runtimeSpawnPercent);
        if (total <= 0f) return;

        foreach (var entry in flyEntries)
        {
            entry.runtimeSpawnPercent = (entry.runtimeSpawnPercent / total) * 100f;
        }
    }

    public Vector2 GetRandomPoint()
    {
        Vector2 center = spawnArea.bounds.center;
        Vector2 extents = spawnArea.bounds.extents;

        float x = Random.Range(center.x - extents.x, center.x + extents.x);
        float y = Random.Range(center.y - extents.y, center.y + extents.y);

        return new Vector2(x, y);
    }

    public void IncreaseFlySpawnChance(Fly targetFly, float increaseAmount)
    {
        var entry = flyEntries.FirstOrDefault(e => e.prefab == targetFly);
        if (entry == null || increaseAmount <= 0f) return;

        float maxIncrease = 100f - entry.runtimeSpawnPercent;
        increaseAmount = Mathf.Min(increaseAmount, maxIncrease);

        float totalOther = flyEntries.Where(e => e.prefab != targetFly).Sum(e => e.runtimeSpawnPercent);
        if (totalOther <= 0f)
        {
            entry.runtimeSpawnPercent = 100f;
            foreach (var e in flyEntries)
            {
                if (e != entry) e.runtimeSpawnPercent = 0f;
            }
            return;
        }

        foreach (var e in flyEntries)
        {
            if (e == entry) continue;
            float share = e.runtimeSpawnPercent / totalOther;
            e.runtimeSpawnPercent -= increaseAmount * share;
            e.runtimeSpawnPercent = Mathf.Max(0f, e.runtimeSpawnPercent);
        }

        entry.runtimeSpawnPercent += increaseAmount;
        NormalizeSpawnChance(); // optional cleanup
    }

    public void FlyDestroyed()
    {
        currentFlies = Mathf.Max(0, currentFlies - 1);
    }
}
