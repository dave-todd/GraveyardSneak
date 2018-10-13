using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningScript : MonoBehaviour
{

    public float flashSpeed = 1;
    public float maxIntensity = 0.3f;

    private Image redScreen;
    private float percentChanged = 0;
    private float timeAtLastChange = 0;
    private bool brightening = true;
    private bool active = false;
    
    public void Start()
    {
        redScreen = GetComponent<Image>();
    }

    void Update()
    {

        if (active) // if the warning screen is active
        {

            float secondsSinceChange = (Time.time - timeAtLastChange); // how long since we last changed direction

            if (secondsSinceChange > flashSpeed) // reverse direction
            {
                brightening = !brightening;
                timeAtLastChange = Time.time;
                secondsSinceChange = 0;
            }

            percentChanged = Mathf.Clamp(secondsSinceChange / flashSpeed,0,1);
            if (!brightening) { percentChanged = 1 - percentChanged; } // reverse to decrease alpha 
            SetScreenColour();

        }

    }

    public void Activate ()
    {
        brightening = true;
        timeAtLastChange = Time.time;
        active = true;
}

    public void Deactivate()
    {
        active = false;
        percentChanged = 0;
        SetScreenColour();
    }

    private void SetScreenColour()
    {
        redScreen.color = new Color(redScreen.color.r, redScreen.color.g, redScreen.color.b, maxIntensity * percentChanged);
    }

}
