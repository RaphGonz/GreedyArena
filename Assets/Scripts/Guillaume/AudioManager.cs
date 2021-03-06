using System;
using System.Collections;
using System.Collections.Generic;
using Guillaume;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    private void Awake()
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
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //TODO : Entrer ici le nom du thème qu'on veut jouer au lancement de la scène. 
        //Play();
        Debug.Log("Music playing");
        Play("Theme");
    }

    /**
     * Appeler FindObjectOfType<AudioManager>().Play(SoundEffet) dans la méthode qui lance le son, et kaboum.
     * Sinon, on peut identifier directement l'AudioManager pour ne pas avoir à faire appel "FindObjectOfType" qui peut être lourd.  
     */
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound "+name+" could not be found in array 'sounds'");
            return;
        }
        s.source.Play();
    }
}
