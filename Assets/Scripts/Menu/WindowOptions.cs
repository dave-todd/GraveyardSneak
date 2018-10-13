using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOptions : MonoBehaviour
{
    public void ChangeWindows(bool fullScreen)
    {
        if (fullScreen) { Screen.fullScreen = true; }
        else { Screen.fullScreen = false; }
    }
}
