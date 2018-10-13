using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SerializedOptions
{
    private float brightness;
    public float Brightness { get { return brightness; } }
    private float masterVolume;
    public float MasterVolume { get { return masterVolume; } }
    private bool fullscreen;
    public bool Fullscreen { get { return fullscreen; } }
    private int resolution;
    public int Resolution { get { return resolution; } }
    private float sensitivity;
    public float Sensitivity { get { return sensitivity; } }

    public SerializedOptions()
    {
        brightness = 1;
        masterVolume = 1;
        fullscreen = true;
        sensitivity = 1;
}

    public SerializedOptions(float b, float v, bool f, int r, float s)
    {
        brightness = b;
        masterVolume = v;
        fullscreen = f;
        resolution = r;
        sensitivity = s;
    }

}

public class SettingsFileManager : MonoBehaviour
{
    public Slider Brightness;
    public Slider MasterVolume;
    public Toggle fullscreenToggle;
    public Toggle windowedToggle;
    public Dropdown resDropdown;
    public Slider Sensitivity;

    public void Start ()
    {
        SerializedOptions LoadedOptions = Load();
        Brightness.value = LoadedOptions.Brightness;
        MasterVolume.value = LoadedOptions.MasterVolume;
        fullscreenToggle.isOn = LoadedOptions.Fullscreen;
        windowedToggle.isOn = !LoadedOptions.Fullscreen;
        resDropdown.value = LoadedOptions.Resolution;
        Sensitivity.value = LoadedOptions.Sensitivity;
    }

    public void Save()
    {
        BinaryFormatter BF = new BinaryFormatter();
        FileStream file = File.Create(GetFileName());
        SerializedOptions CurrentOptions = new SerializedOptions(Brightness.value, MasterVolume.value, fullscreenToggle.isOn, resDropdown.value, Sensitivity.value);
        BF.Serialize(file, CurrentOptions);
        file.Close();
    }

    private SerializedOptions Load()
    {
        SerializedOptions LoadedOptions = new SerializedOptions();

        if (File.Exists(GetFileName()))
        {
            BinaryFormatter BF = new BinaryFormatter();
            FileStream file = File.Open(GetFileName(), FileMode.Open);
            LoadedOptions = BF.Deserialize(file) as SerializedOptions;
            file.Close();
        }

        return LoadedOptions;
    }

    private string GetFileName()
    {
        return Application.persistentDataPath + "/Options.dat";
    }

}
