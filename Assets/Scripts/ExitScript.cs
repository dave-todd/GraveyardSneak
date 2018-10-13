using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{

	private void Update ()
    {
        if (Input.GetKey("escape")) { ExitGame(); }
	}

    public void ExitGame()
    {
        Application.Quit();
    }

}
