using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{

    public event Action OnStartWon;
    public event Action OnEndWon;

    public RotateSphereScript sphere;
    public GameWonScript gameWonController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { Triggered(); }
    }

    private void Triggered()
    {
        sphere.Triggered();
        Invoke("GameWon5Seconds", 5);
        if (OnStartWon != null) { OnStartWon(); }
    }

    private void GameWon5Seconds()
    {
        gameWonController.GameWon();
        if (OnEndWon != null) { OnEndWon(); }
    }

}
