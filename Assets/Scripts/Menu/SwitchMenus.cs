using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMenus : MonoBehaviour
{
    public GameObject MenuToClose;
    public GameObject MenuToOpen;

    public void OpenMenu()
    {
        MenuToOpen.SetActive(true);
        MenuToClose.SetActive(false);
    }
}
