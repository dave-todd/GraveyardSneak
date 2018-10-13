using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTrigger : MonoBehaviour
{

    public event Action OnShrineTriggered;

    public SaveData masterSaveData;
    public GameObject shrine;
    public float dropSpeed = 1;

    private bool dropShrine = false;
    private float startY;

    public void Start()
    {
        shrine.SetActive(!masterSaveData.lastTrigger);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { Triggererd(); }
    }

    private void Update()
    {
        if (dropShrine)
        {
            shrine.transform.localPosition -= shrine.transform.forward * dropSpeed * Time.deltaTime;
            dropShrine = (shrine.transform.localPosition.y > startY - 10);
        }
    }

    private void Triggererd()
    {
        startY = shrine.transform.localPosition.y;
        dropShrine = true;
        masterSaveData.lastTrigger = true;
        if (OnShrineTriggered != null) { OnShrineTriggered(); }
    }

}
