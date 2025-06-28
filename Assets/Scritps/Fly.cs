using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float moveRange = 2f;
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private float lifetime = 3f; //Duration of the fly
    //[SerializeField] private float noiseScale = 1f;
    [SerializeField] private float noiseSpeed = 1f;

    [Range(0,100)] public float spawnPercent = 25f; 

    private float xSeed;
    private float ySeed;

    void Start()
    {
        // Random seeds for variation per fly
        xSeed = Random.Range(0f, 100f);
        ySeed = Random.Range(0f, 100f);
    }

    void Update()
    {
        MoveWithPerlin();
    }

    public virtual void MoveWithPerlin()
    {
        float t = Time.time * noiseSpeed;

        float x = Mathf.PerlinNoise(xSeed + t, ySeed) * 2f - 1f;
        float y = Mathf.PerlinNoise(xSeed, ySeed + t) * 2f - 1f;

        Vector3 direction = new Vector3(x, y, 0f).normalized;

        transform.position += direction * speed * moveRange * Time.deltaTime;
    }
}
