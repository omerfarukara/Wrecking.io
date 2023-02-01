using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] int maxMultipleSound = 5;

    private SoundData _soundData;

    readonly Dictionary<string, float> volume = new Dictionary<string, float>();
    readonly Dictionary<string, AudioClip> clip = new Dictionary<string, AudioClip>();

    AudioSource[] audioSources;

    private void Awake()
    {
        _soundData = Resources.Load("SoundData") as SoundData;
        Singleton(true);
        Initiate();
    }

    void Initiate()
    {
        foreach (SoundClip soundClip in _soundData.SoundClips)
        {
            volume.Add(soundClip.Name, soundClip.Volume);
            clip.Add(soundClip.Name, soundClip.Clip);
        }
        
        for (int i = 0; i < maxMultipleSound; i++)
        {
            GameObject newAudioSource =  new GameObject();
            newAudioSource.AddComponent<AudioSource>();
            newAudioSource.name = $"AudioSource {i}";
            newAudioSource.transform.parent = transform;
        }
        
        audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void Play(string soundName)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip[soundName];
                audioSource.volume = volume[soundName];
                audioSource.Play();
                break;
            }
        }
    }

    public void PlayOnIncrease(string soundName, float coefficient)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip[soundName];
                audioSource.volume = volume[soundName];
                StartCoroutine(IncreaseVolume(audioSource, coefficient));
                break;
            }
        }
    }

    public void PlayOnDecrease(string soundName, float coefficient)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip[soundName];
                audioSource.volume = volume[soundName];
                StartCoroutine(DecreaseVolume(audioSource, coefficient));
                break;
            }
        }
    }

    IEnumerator IncreaseVolume(AudioSource audioSource, float coefficient)
    {
        float clipLenght = audioSource.clip.length;
        float currentTime = clipLenght;

        audioSource.Play();

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime * coefficient;
            audioSource.volume -= currentTime / clipLenght;
            yield return null;
        }

        audioSource.volume = 0;
    }

    IEnumerator DecreaseVolume(AudioSource audioSource, float coefficient)
    {
        float clipLenght = audioSource.clip.length;
        float currentTime = clipLenght;

        audioSource.Play();

        while (currentTime < 1)
        {
            currentTime += Time.deltaTime * coefficient;
            audioSource.volume += currentTime / clipLenght;
            yield return null;
        }

        audioSource.volume = 1;
    }
}
