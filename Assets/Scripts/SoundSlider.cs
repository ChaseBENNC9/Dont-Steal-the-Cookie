using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class SoundSlider : MonoBehaviour
{
    public enum SoundTypes
    {
        MUSIC,
        EFFECTS,
        MASTER

    }
    public SoundTypes soundType;
    [SerializeField] private Slider slider;
    [SerializeField] private string sliderName;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private AudioSource audioSource;

    public void OnVolumeChanged(float value)
    {
        value = (float)Math.Round((value),2);
        valueText.text = $"{value * 100}%";
        audioSource.volume = value;
        SetSoundType(soundType,value);
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        
    }
    public void LoadSettings()
    {
        audioSource.volume = GetSoundType(soundType);
        valueText.text = $"{GetSoundType(soundType)*100f}%";
        slider.value = GetSoundType(soundType);
        audioSource.mute = GameSettings.Mute;
    }

    public float GetSoundType(SoundTypes sound)
    {
        switch (sound)
        {
            case SoundTypes.MUSIC:
                return GameSettings.MusicVolume;
            case SoundTypes.EFFECTS:
                return GameSettings.SoundEffectsVolume;
            case SoundTypes.MASTER:
                return GameSettings.MasterVolume;
            default:
                return 0;
        }
    }
    private void SetSoundType(SoundTypes sound, float value = 1f)
    {
        switch(sound)
        {
            case SoundTypes.MUSIC:
                GameSettings.MusicVolume = value;
                break;
            case SoundTypes.EFFECTS:
                GameSettings.SoundEffectsVolume = value;
                break;
            case SoundTypes.MASTER:
             GameSettings.MasterVolume = value;
             break;
        }
    }
}