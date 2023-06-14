using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;

    private void Awake()
    {
        if (inst == null) inst = this;
        else Destroy(gameObject);
    }

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgMusicSource;

    [SerializeField] private float fadeTime;

    public bool BackgroundOn { get; private set; } = true;

    private void OnEnable()
    {
        bgMusicSource.Play();
    }

    public static void PlaySound(AudioClip sound)
    {
        inst.sfxSource.PlayOneShot(sound);
    }

    public static void StartBackground() { if (!inst.bgMusicSource.isPlaying) { inst.bgMusicSource.Play(); } } 

    public static void StopBackground() { if (inst.bgMusicSource.isPlaying) { inst.StartCoroutine(inst.AudioFadeOut()); } } 

    private IEnumerator AudioFadeOut()
    {
        float startVolume = bgMusicSource.volume;

        while (bgMusicSource.volume > 0)
        {
            bgMusicSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        bgMusicSource.Stop();
        bgMusicSource.volume = startVolume;
    }
}
