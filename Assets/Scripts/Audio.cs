using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public AudioMixer music;
    public AudioMixer sFX;
    Slider musicSlider;
    Slider sfxSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSlider = transform.Find("Options/Music/MusicSlider").GetComponent<Slider>();
        sfxSlider = transform.Find("Options/Sound/SoundSlider").GetComponent<Slider>();
        LoadVolume();
    }

    void LoadVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        sFX.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        music.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
