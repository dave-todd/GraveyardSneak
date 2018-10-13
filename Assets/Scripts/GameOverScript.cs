using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    public GameObject gameOverScreen;

    private bool gameOver;

    public void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Space)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
        else if (gameOver && Input.GetKeyDown(KeyCode.Return)) { SceneManager.LoadScene(0); }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameOver = true;
    }

}
