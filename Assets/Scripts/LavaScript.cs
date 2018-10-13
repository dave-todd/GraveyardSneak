using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{

    public event Action OnBurnPlayer;

    public PlayerControl player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { BurnPlayer(); }
    }

    private void BurnPlayer()
    {
        GetComponent<AudioSource>().Play();
        player.Kill();
        if (OnBurnPlayer != null) { OnBurnPlayer(); }
    }

}
