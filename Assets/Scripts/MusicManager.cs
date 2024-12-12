using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private const string PREFS_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance {  get; private set; }

    private AudioSource musicAudioSource;
    private float volume = 0.4f;

    private void Awake()
    {
        Instance = this;    
        musicAudioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PREFS_MUSIC_VOLUME, 0.3f);
        musicAudioSource.volume = volume;
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f) {
            volume = 0;
        }

        musicAudioSource.volume = volume;

        PlayerPrefs.SetFloat(PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }

}
