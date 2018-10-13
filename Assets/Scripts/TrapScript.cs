using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{

    public event Action OnTrapActivate;
    
    public PlayerControl player;
    public Animation trapAnimation;
    
    public void Start()
    {
        trapAnimation.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { TrapActivate(); }
    }

    private void TrapActivate()
    {
        trapAnimation.gameObject.SetActive(true);
        trapAnimation.transform.LookAt(player.transform.position);
        GetComponent<AudioSource>().Play();
        player.Kill();
        if (OnTrapActivate != null) { OnTrapActivate(); }
    }

}
