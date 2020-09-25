using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RageBar : MonoBehaviour
{
    public Image rageBar;
    float MaxRage = 100f;
    public static float Rage;

    // Start is called before the first frame update
    void Start()
    {
        rageBar = GetComponent<Image>();
        Rage = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        rageBar.fillAmount = Rage / MaxRage;
    }
}
