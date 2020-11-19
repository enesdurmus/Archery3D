using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealt(int healt)
    {
        slider.maxValue = healt;
        slider.value = healt;
        fill.color = gradient.Evaluate(1);
    }

    public void SetHealt(int healt)
    {
        slider.value = healt;
        fill.color = gradient.Evaluate((float)((float)healt/100f));
    }
}
