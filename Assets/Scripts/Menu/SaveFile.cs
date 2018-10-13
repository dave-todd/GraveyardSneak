using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveFile
{

    public int currentScene;
    public int currentTeleportIndex;
    public bool hasGun;
    public bool gunAvailable;
    public bool ironBarsDown;
    public bool stairsRaised;
    public bool lastTrigger;

    public SaveFile() // constructor
    {
        currentScene = 0;
        currentTeleportIndex = -1;
        hasGun = false;
        gunAvailable = false;
        ironBarsDown = false;
        stairsRaised = false;
        lastTrigger = false;
    }

    public SaveFile(SaveData newSaveData) // constructor
    {
        currentScene = (int)newSaveData.currentScene;
        currentTeleportIndex = newSaveData.currentTeleportIndex;
        hasGun = newSaveData.hasGun;
        gunAvailable = newSaveData.gunAvailable;
        ironBarsDown = newSaveData.ironBarsDown;
        stairsRaised = newSaveData.stairsRaised;
        lastTrigger = newSaveData.lastTrigger;
    }

    public Scenes GetCurrentScene()
    {
        switch (currentScene)
        {
            case 1: return Scenes.Graveyard;
            case 2: return Scenes.Dungeon;
            default: return Scenes.Menu;
        }
    }

    public string GetSceneName()
    {
        switch (currentScene)
        {
            case 1: return "Graveyard";
            case 2: return "Dungeon";
            default: return "Menu";
        }
    }
    
}
