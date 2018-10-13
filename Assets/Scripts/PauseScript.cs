using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    public event Action OnStartPause;
    public event Action OnEndPause;

    public GameObject pauseScreen;

    private bool paused;

    public void Update()
    {
        if (paused && Input.GetKeyDown(KeyCode.Space)) { ResumeGame(); }
        else if (paused && Input.GetKeyDown(KeyCode.Return)) { LoadMainMenu(); }
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        paused = true;
        Time.timeScale = 0f;
        if (OnStartPause != null) { OnStartPause(); }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        paused = false;
        pauseScreen.SetActive(false);
        if (OnEndPause != null) { OnEndPause(); }
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(0);
    }
    
}
