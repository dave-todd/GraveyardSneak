using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinTrapTrigger : MonoBehaviour
{

    public event Action OnTrapActivate;

    public GameObject creature;
    public GameObject coffin;

    public void Start()
    {
        coffin.SetActive(true);
        creature.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            coffin.SetActive(false);
            creature.SetActive(true);
            if (OnTrapActivate != null) { OnTrapActivate(); }
        }
    }


}
