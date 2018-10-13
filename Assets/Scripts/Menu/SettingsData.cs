using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Audio;

[CreateAssetMenu()]

public class SettingsData : ScriptableObject
{

    public float MaxAudio = 0;
    public float MinAudio = 80;
    public AudioMixer MasterMixer;
    public Image BrightnessPanel;
    [Range(0, 1)]
    public float Brightness = 1;
    [Range(0, 1)]
    public float MasterVolume = 1;
    [Range(0, 1)]
    public float Sensitivity = 1;

    public void UpdateBrightness(float Value)
    {
        Brightness = 1 - Value;
        BrightnessPanel.color = new Color(BrightnessPanel.color.r, BrightnessPanel.color.g, BrightnessPanel.color.b, Brightness);
    }

    public void UpdateMasterVolume(float Value)
    {
        MasterVolume = 1 - Value;
        float Min = 0 - (MinAudio * MasterVolume);
        MasterMixer.SetFloat("MasterVolume", Min);
    }

    public void UpdateSensitivity(float value)
    {
        Sensitivity = value * 2;
    }

}