using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistObject : MonoBehaviour
{

    public SettingsData masterSettings;
    public static GameObject brightnessObject;

	void Awake ()
    {
        if (brightnessObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            brightnessObject = gameObject;
        }

        DontDestroyOnLoad(gameObject);

        if (masterSettings.BrightnessPanel == null)
        {
            masterSettings.BrightnessPanel = transform.GetChild(0).GetComponent<Image>();
        }
	}
	
}
