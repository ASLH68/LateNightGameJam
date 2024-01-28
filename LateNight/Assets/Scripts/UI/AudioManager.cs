/*****************************************************************************
    Brackeys Audio Manager
    Tutorial video: https://youtu.be/6OT43pvUyfY

    Author: Caden Sheahan
    Creation Date: 3/8/23, Modified on 9/19/23
    Description: Creates an array of all sound effects defined by "Sound" script
    and adds an audio source to each of them in the AudioManager game object.
    
    Use this line to play any sound anywhere, if you have the name set in 
    the inspector:
    
    FindObjectOfType<AudioManager>().Play("[INSERT_NAME_FROM_INSPECTOR]");
 *****************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    //public float musicVolume;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in Sounds)
        {   
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.rolloffMode = AudioRolloffMode.Linear;
        }
    }

    #region Sound Controls
    public void Play(string name, float volume = -1)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning(name + ": audio not found");
            return;
        }
        s.source.Play();
        if (volume >= 0)
        {
            s.source.volume = 0;
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning(name + ": audio not found");
            return;
        }
        s.source.Stop();
    }

    public AudioSource Source(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning(name + ": audio not found");
            return null;
        }
        return s.source;
    }

    public float ClipLength(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning(name + ": audio not found");
            return 0;
        }
        return s.source.clip.length;
    }

    ///// <summary>
    ///// Specifically for music, this function disables the volume of a clip
    ///// </summary>
    ///// <param name="name"></param>
    //public void Mute(string name)
    //{
    //    Sound s = Array.Find(Sounds, sound => sound.name == name);
    //    if (s == null)
    //    {
    //        Debug.LogWarning(name + ": audio not found");
    //        return;
    //    }
    //    s.source.volume = 0.0f;
    //}

    ///// <summary>
    ///// Specifically for music, this function enables the volume of a clip
    ///// </summary>
    ///// <param name="name"></param>
    //public void Unmute(string name)
    //{
    //    Sound s = Array.Find(Sounds, sound => sound.name == name);
    //    if (s == null)
    //    {
    //        Debug.LogWarning(name + ": audio not found");
    //        return;
    //    }
    //    s.source.volume = 
    //}

    #endregion
}
