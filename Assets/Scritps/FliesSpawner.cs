using System.Collections;
using UnityEngine;

public class FliesSpawner : MonoBehaviour
{
    public static float spawnCooldown = 2f;

    [SerializeField] private GameObject flyPrefab;
    [SerializeField] private float DEBUGcooldownOverride;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnFlies());   
    }

    // Update is called once per frame
    void Update()
    {
        spawnCooldown = DEBUGcooldownOverride;

    }

    IEnumerator SpawnFlies()
    {
        while (true)
        {
            Instantiate(flyPrefab, transform);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
