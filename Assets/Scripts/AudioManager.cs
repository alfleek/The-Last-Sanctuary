using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager: MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle musicMuteToggle;
    public Toggle sfxMuteToggle;

    private float savedMusicVolume = 1f;
    private float savedSFXVolume = 1f;

    void Start()
    {
        // Initialize sliders from PlayerPrefs
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSlider.value = musicVol;
        sfxSlider.value = sfxVol;

        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Initialize mute toggles
        musicMuteToggle.onValueChanged.AddListener(SetMusicMute);
        sfxMuteToggle.onValueChanged.AddListener(SetSFXMute);
    }

    public void SetMusicVolume(float volume)
    {
        if (musicMuteToggle.isOn) return; // don’t override mute
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxMuteToggle.isOn) return; // don’t override mute
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMusicMute(bool isMuted)
    {
        if (isMuted)
        {
            // save current value before muting
            savedMusicVolume = musicSlider.value;
            mainMixer.SetFloat("MusicVolume", -80f); // silence
        }
        else
        {
            SetMusicVolume(savedMusicVolume);
        }
    }

    public void SetSFXMute(bool isMuted)
    {
        if (isMuted)
        {
            savedSFXVolume = sfxSlider.value;
            mainMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            SetSFXVolume(savedSFXVolume);
        }
    }
}
