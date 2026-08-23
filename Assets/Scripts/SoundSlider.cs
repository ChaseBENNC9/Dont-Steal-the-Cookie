using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private string sliderName;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnVolumeChanged()
    {
        float val = slider.value;
        valueText.text = $"{val}%";
        audioSource.volume = val/100f;
    }
}
