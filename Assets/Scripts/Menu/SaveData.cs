using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class SaveData : ScriptableObject
{
    
    public Scenes currentScene;
    public int currentTeleportIndex;
    public bool hasGun;
    public bool gunAvailable;
    public bool ironBarsDown;
    public bool stairsRaised;
    public bool lastTrigger;

    private SaveFile currentSave;
    private int currentSlot;

    public void CreateSave(int newSlot)
    {
        currentSave = new SaveFile();
        currentSlot = newSlot;
        ApplyLoadedSave(currentSave);
        Save();
    }

    public void UpdateSave(Scenes newScene, int newTeleportIndex)
    {
        currentScene = newScene;
        currentTeleportIndex = newTeleportIndex;
        currentSave = new SaveFile(this);
        Save();
    }

    public SaveFile Load(int slot)
    {
        currentSlot = slot;
        SaveFile loadedSave = LoadSaveFile(currentSlot);
        currentSave = loadedSave;
        return loadedSave;
    }

    public void ClearSave(int slotToClear)
    {
        File.Delete(GetFileName(slotToClear));
    }

    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetFileName(currentSlot));
        bf.Serialize(file, currentSave);
        file.Close();
    }

    public SaveFile LoadSaveFile(int slot)
    {

        if (File.Exists(GetFileName(slot)))
        {
            SaveFile loadedSave = new SaveFile();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(GetFileName(slot), FileMode.Open);
            loadedSave = bf.Deserialize(file) as SaveFile;
            file.Close();
            ApplyLoadedSave(loadedSave);
            return loadedSave;
        }
        else
        {
            return null;
        }

    }

    private void ApplyLoadedSave(SaveFile loadedSave)
    {
        currentScene = loadedSave.GetCurrentScene();
        currentTeleportIndex = loadedSave.currentTeleportIndex;
        hasGun = loadedSave.hasGun;
        gunAvailable = loadedSave.gunAvailable;
        ironBarsDown = loadedSave.ironBarsDown;
        stairsRaised = loadedSave.stairsRaised;
        lastTrigger = loadedSave.lastTrigger;
    }

    private string GetFileName(int slot)
    {
        return Application.persistentDataPath + "/SaveFile_" + slot.ToString() + ".dat";
    }

}
