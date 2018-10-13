using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveFileManager : MonoBehaviour
{

    public SaveData masterSaveData;
    public List<GameObject> slotList = new List<GameObject>();
	
    void Awake ()
    {
        SaveFile save;
        for (int i = 0; i < slotList.Count; i++)
        {
            save = masterSaveData.LoadSaveFile(i);
            slotList[i].GetComponent<SaveSlotControl>().Initialise(save, this, i);
        }
	}

    public void ClearSlot(int slot)
    {
        masterSaveData.ClearSave(slot);
    }

    public void LoadGame(int slot)
    {
        SaveFile loadedSave = masterSaveData.LoadSaveFile(slot);
        SceneManager.LoadScene((int)loadedSave.currentScene);
    }

    public void CreateNewSave(int slot)
    {
        masterSaveData.CreateSave(slot);
        SceneManager.LoadScene(1);
    }

}
