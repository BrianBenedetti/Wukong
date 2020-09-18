using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Image manaBar;
    float MaxMana = 100f;
    public static float Mana;

    // Start is called before the first frame update
    void Start()
    {
        manaBar = GetComponent<Image>();
        Mana = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.fillAmount = Mana / MaxMana;
    }
}
