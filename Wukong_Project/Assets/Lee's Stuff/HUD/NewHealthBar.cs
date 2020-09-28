using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewHealthBar : MonoBehaviour
{
    public Image HealthBar;
    public Image damagedBarImage;

    public PlayerCombat healthSystem;

    private void Start()
    {
        damagedBarImage.fillAmount = HealthBar.fillAmount;
    }

    private void Update()
    {
        damagedBarImage.fillAmount = healthSystem.currentHealth / healthSystem.maxHealth;
    }
}
