using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundScript : MonoBehaviour
{

    public SaveData masterSaveData;
    public TeleportData masterTeleportData;
    public PlayerControl player;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable ()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (masterSaveData.currentTeleportIndex > -1)
        {
            player.transform.position = masterTeleportData.GetLocation(masterSaveData.currentScene, masterSaveData.currentTeleportIndex);
            GetComponent<AudioSource>().Play();
        }
        else
        {
            masterSaveData.UpdateSave(Scenes.Graveyard, -1);
        }
    }

}
