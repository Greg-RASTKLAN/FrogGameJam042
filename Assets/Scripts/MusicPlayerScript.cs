using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioMixer mixer;

    private float musicVolume = .2f;

    void Start()
    {
        //AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //AudioSource.volume = musicVolume;
    }

    public void UpdateVolume(float volume)
    {
        float normalized = volume / 0.2f; 
        float clamped = Mathf.Max(normalized, 0.0001f);
        mixer.SetFloat("MasterVolume", Mathf.Log10(clamped) * 20f);
    }
}
