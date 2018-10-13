using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{

    public event Action OnStartOpen;
    public event Action OnEndOpen;
    public event Action OnStartClose;
    public event Action OnEndClose;

    public SaveData masterSaveData;
    public AudioSource gatesOpenAudio;
    public AudioSource gatesMoveAudio;
    public AudioSource gatesCloseAudio;
    public Transform leftGate;
    public Transform rightGate;
    public float openSpeed = 20;

    private bool open = false;
    private bool moving = false;
    

    public void Start()
    {
        if (masterSaveData.currentTeleportIndex < 0) { OpenGates(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") { CloseGates(); }
    }

    private void Update()
    {

        if (moving)
        {

            if (open) // Keep Closing
            {
                leftGate.Rotate(Vector3.up, openSpeed * Time.deltaTime * -1);
                rightGate.Rotate(Vector3.up, openSpeed * Time.deltaTime * 1);
                moving = (leftGate.eulerAngles.y > 180);
                if (!moving)
                { // Finished Closing
                    open = false;
                    gatesCloseAudio.Play();
                    if (OnEndClose != null) { OnEndClose(); }
                }
            }
            else // Keep Opening
            {
                leftGate.Rotate(Vector3.up, openSpeed * Time.deltaTime * 1);
                rightGate.Rotate(Vector3.up, openSpeed * Time.deltaTime * -1);
                moving = (leftGate.eulerAngles.y < 240);
                if (!moving)
                { // Finished Opening
                    open = true;
                    gatesOpenAudio.Play();
                    if (OnEndOpen != null) { OnEndOpen(); }
                }
            }
            
        }

    }

    public void OpenGates()
    {
        if (!open && !moving)
        {
            moving = true;
            gatesMoveAudio.Play();
            if (OnStartOpen != null) { OnStartOpen(); }
        }
    }

    public void CloseGates()
    {
        if (open && !moving)
        {
            moving = true;
            gatesMoveAudio.Play();
            masterSaveData.UpdateSave(Scenes.Graveyard, 0);
            if (OnStartClose != null) { OnStartClose(); }
        }
    }

}
