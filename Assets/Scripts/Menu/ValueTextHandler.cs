using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueTextHandler : MonoBehaviour
{
    Text MyText;
    public Slider MySlider;
    public float InitialValue = 100;
    float Offset;
    
	void Awake ()
    {
        MyText = GetComponent<Text>();
        MyText.text = InitialValue.ToString();
        Offset = MySlider.minValue;
	}

    public void UpdateText(float Value)
    {
        float NewValue = ((Value - Offset) / (MySlider.maxValue-Offset));
        int PrintValue = (int)(NewValue * InitialValue);
        MyText.text = PrintValue.ToString();
    }
		
}
