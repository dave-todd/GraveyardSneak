using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignBrightnessPanel : MonoBehaviour {

    public SettingsData MasterSettings;

    private void Awake()
    {
        MasterSettings.BrightnessPanel = GetComponent<Image>();
    }

}
