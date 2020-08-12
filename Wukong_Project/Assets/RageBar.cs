using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RageBar : MonoBehaviour
{
    public Slider Rageslider;
    public Gradient Ragegradient;
    public Image Ragefill;

    public void SetMaxRage(int Rage)
    {
        Rageslider.maxValue = Rage;
        Rageslider.value = Rage;

        Ragefill.color = Ragegradient.Evaluate(1f);
    }
    public void SetRage(int Rage)
    {
        Rageslider.value = Rage;

        Ragefill.color = Ragegradient.Evaluate(Rageslider.normalizedValue);

    }
}
