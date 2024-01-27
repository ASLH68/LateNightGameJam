/******************************************************************************
//      File Name: AudioSettings.cs
//      Author: Caden Sheahan
//      Creation Date: January 27th, 2024
//
//      Description: Adapted from Scott Games Sounds video on FMOD Audio Settings
//      with that video linked here https://www.youtube.com/watch?v=yQgVKR6PMqo.
//      This script sets the volume of 2 different busses in FMOD to sliders in Unity.
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    //SFX Test

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
    }

    // Update is called once per frame
    void Update()
    {
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }
}
