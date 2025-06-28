using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FliesSpawner : MonoBehaviour
{
    public static float spawnCooldown = 3f;
    public static int fliesToSpawn = 1;


    [SerializeField] private List<Fly> fliesList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnFlies());   
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnFlies()
    {
        while (true)
        {
            for (int i = 0; i < fliesToSpawn; i++)
            {
                GameObject flyToSpawn = GetRandomFlyByPercent();
                Instantiate(flyToSpawn, transform);
            }
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    private GameObject GetRandomFlyByPercent()
    {
        float total = 0f;
        foreach (var fly in fliesList)
        {
            total += fly.spawnPercent;
        }

        if (total <= 0f)
        {
            Debug.LogWarning("Total spawn percent is zero! Check the list of fly prefabs.");
            return fliesList[0].gameObject;
        }

        float roll = Random.Range(0f, total);
        float cumulative = 0f;

        foreach (var fly in fliesList)
        {
            cumulative += fly.spawnPercent;
            if (roll <= cumulative)
            {
                return fly.gameObject;
            }
        }

        return fliesList[0].gameObject; //Fallback

    }

    //Call this function after upgrades
    public void NormalizeSpawnChance()
    {
        float total = fliesList.Sum(f => f.spawnPercent);
        if (total <= 0f) return;

        foreach (var fly in fliesList)
        {
            fly.spawnPercent = (fly.spawnPercent / total) * 100f;
        }
    }

}
