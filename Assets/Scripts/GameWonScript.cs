using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWonScript : MonoBehaviour
{

    public GameObject gameWonScreen;

    private bool gameWon;

    public void Update()
    {
        if (gameWon && Input.GetKeyDown(KeyCode.Return)) { SceneManager.LoadScene(0); }
    }

    public void GameWon()
    {
        gameWonScreen.SetActive(true);
        gameWon = true;
    }

}
