using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterScript2 : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxProgress(float progress)
    {
        slider.maxValue = progress;
        slider.value = progress;

        fill.color = gradient.Evaluate(1f);

    }

    public void SetProgress(float progress)
    {
        slider.value = progress;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
