using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBarsTrigger : MonoBehaviour
{

    public SaveData masterSaveData;
    public GameObject shrine;
    public float dropSpeed = 1;

    private bool dropShrine = false;
    private float startY;

    public void Start()
    {
        shrine.SetActive(!masterSaveData.ironBarsDown);
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
            dropShrine = (shrine.transform.localPosition.y > startY - 100);
        }
    }

    private void Triggererd()
    {
        startY = shrine.transform.localPosition.y;
        dropShrine = true;
        masterSaveData.ironBarsDown = true;
    }

}
