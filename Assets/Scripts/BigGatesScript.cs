using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGatesScript : MonoBehaviour
{

    public SaveData masterSaveData;
    public Transform leftGate;
    public Transform rightGate;
    
    public void Start()
    {
        if (masterSaveData.lastTrigger) { OpenGates(); }
        else CloseGates();
    }

    private void OpenGates()
    {
        SetYRotation(leftGate, 0);
        SetYRotation(rightGate, 0);
    }

    private void CloseGates()
    {
        SetYRotation(leftGate, 270);
        SetYRotation(rightGate, 90);
    }

    private void SetYRotation(Transform gate, float rot)
    {
        gate.eulerAngles = new Vector3(gate.rotation.x, rot, gate.rotation.z);
    }
    
}
