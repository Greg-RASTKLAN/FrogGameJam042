using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AudioSource>().volume = 0.20f; // Volume entre 0 (muet) et 1 (volume max)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
