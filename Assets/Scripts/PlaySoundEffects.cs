using UnityEngine;
using System;
using System.Collections.Generic;
[RequireComponent(typeof(AudioSource))]
public class PlaySoundEffects : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string sound)
    {
        AudioClip clip = Resources.Load<AudioClip>($"SFX/{sound}");
        audioSource.clip = clip;
        audioSource.Play();
    }
}