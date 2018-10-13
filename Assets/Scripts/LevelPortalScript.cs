using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortalScript : MonoBehaviour
{

    public event Action OnChangeLevel;

    public SaveData masterSaveData;
    public TeleportData masterTeleportData;
    public int teleportIndex;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { LoadOtherLevel(); }
    }

    private void LoadOtherLevel()
    {
        masterSaveData.UpdateSave(masterTeleportData.GetOtherScene(), teleportIndex);
        if (OnChangeLevel != null) { OnChangeLevel(); }
        SceneManager.LoadScene((int)masterTeleportData.GetOtherScene());
    }

}
