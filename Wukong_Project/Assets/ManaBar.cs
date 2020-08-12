using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider Manaslider;
    public Gradient Managradient;
    public Image Manafill;
    
    public void SetMaxMana(int Mana)
    {
        Manaslider.maxValue = Mana;
        Manaslider.value = Mana;

        Manafill.color = Managradient.Evaluate(1f);
    }
    public void SetMana(int Mana)
    {
        Manaslider.value = Mana;

        Manafill.color = Managradient.Evaluate(Manaslider.normalizedValue);

    }
}
