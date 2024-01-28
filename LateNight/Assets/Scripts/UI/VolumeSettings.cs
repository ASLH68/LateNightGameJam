/*****************************************************************************
    AIAdev Master Audio Mixer: 
    Tutorial Video: https://youtube.com/shorts/_m6nTQOMFl0?feature=share

    Author: Caden Sheahan
    Creation Date: 3/8/23, Modified on 1/28/24
    Description: The settings for a slider UI object for master volume control.
******************************************************************************/
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] AudioMixerGroup sfxMixer;
    [SerializeField] AudioMixerGroup musicMixer;

    private bool _hasVolumeBeenSet = false;

    // Start is called before the first frame update
    void Start()
    {
        SetSFXVolume(PlayerPrefs.GetFloat("SavedSFXVolume", 100));
        SetMusicVolume(PlayerPrefs.GetFloat("SavedMusicVolume", 100));
        _hasVolumeBeenSet = true;
    }

    #region SFXVolume
    public void SetSFXVolume(float _value)
    {
        if (_value < 0)
        {
            _value = .001f;
        }

        RefreshSFXSlider(_value);
        PlayerPrefs.SetFloat("SavedSFXVolume", _value);
        sfxMixer.audioMixer.SetFloat("SFXVolume", Mathf.Log10(_value / 100) * 20f);
    }

    public void SetSFXVolumeFromSlider()
    {
        SetSFXVolume(sfxVolumeSlider.value);
    }

    public void RefreshSFXSlider(float _value)
    {
        sfxVolumeSlider.value = _value;
    }
    #endregion

    #region MusicVolume
    public void SetMusicVolume(float _value)
    {
        if (_value < 0)
        {
            _value = .001f;
        }

        RefreshMusicSlider(_value);
        PlayerPrefs.SetFloat("SavedMusicVolume", _value);
        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(_value / 100) * 20f);
    }

    public void SetMusicVolumeFromSlider()
    {
        SetMusicVolume(musicVolumeSlider.value);
    }

    public void RefreshMusicSlider(float _value)
    {
        musicVolumeSlider.value = _value;
    }
    #endregion
}
