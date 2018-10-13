using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBarsControl : MonoBehaviour
{

    public SaveData masterSaveData;

    private const float UpHeight = -5.6f;
    private const float DownHeight = -20f;

    public void Start()
    {
        gameObject.SetActive(!masterSaveData.ironBarsDown);
    }

}
