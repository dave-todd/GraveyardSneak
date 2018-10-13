using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResolutionOptions
{
    public int x = 0;
    public int y = 0;
}

public class SwitchResolution : MonoBehaviour
{

    public Dropdown resolutionOptions;
    public List<ResolutionOptions> resList = new List<ResolutionOptions>();
    public int defaultResolutionIndex;
    
	void Start ()
    {
        SelectResolution(defaultResolutionIndex);
	}

    public void SelectResolution(int index)
    {
        Screen.SetResolution(resList[index].x, resList[index].y, Screen.fullScreen);
    }

}
