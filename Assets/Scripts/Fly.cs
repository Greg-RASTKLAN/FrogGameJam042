using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float moveRange = 2f;
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float noiseSpeed = 1f;

    //[Range(0, 100)] public float spawnPercent = 25f;
    //public int baseValue = 1;

    private float xSeed;
    private float ySeed;
    private Vector3 spawnPosition; // Store the original spawn position
    private bool isInitialized = false;

    private float age = 0f;

    void Start()
    {
        // Store the spawn position
        spawnPosition = transform.position;

        // Random seeds for variation per fly
        xSeed = Random.Range(0f, 100f);
        ySeed = Random.Range(0f, 100f);

        isInitialized = true;
    }

    void Update()
    {
        if (isInitialized)
        {
            age += Time.deltaTime;
            MoveWithPerlin();
        }
    }


    public virtual void OnCaught()
    {
        GameManager.Instance.AddCurrency(GameManager.baseFlyValue);
        FliesSpawner.Instance.FlyDestroyed();
        Destroy(gameObject);
    }



    public virtual void MoveWithPerlin()
    {
        float t = age * noiseSpeed; // instead of Time.time

        // Get Perlin noise values (-1 to 1 range)
        float x = Mathf.PerlinNoise(xSeed + t, ySeed + t) * 2f - 1f;
        float y = Mathf.PerlinNoise(xSeed - t, ySeed - t) * 2f - 1f;

        // Calculate the offset from spawn position
        Vector3 offset = new Vector3(x, y, 0f) * moveRange;

        // Set position relative to spawn position instead of adding to current position
        transform.position = spawnPosition + offset;
    }

    protected virtual void OnDestroy()
    {
        if (FliesSpawner.Instance != null)
        {
            FliesSpawner.Instance.FlyDestroyed();
        }
    }

}
