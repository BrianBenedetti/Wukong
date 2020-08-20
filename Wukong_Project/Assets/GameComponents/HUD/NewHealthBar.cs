using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewHealthBar : MonoBehaviour
{
    private const float MaxdamagedHealthTimer = 0.3f;

    public Image HealthBar;
    public Image damagedBarImage;
    private float damagedHealthTimer;
  
    public static HealthSystem healthSystem;
   

    private void Start()
    {
        damagedBarImage.fillAmount = HealthBar.fillAmount;
        healthSystem = new HealthSystem(100);
        SetHealth(healthSystem.GetHealthNormalized());
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }
    private void Update()
    {
        damagedHealthTimer -= Time.deltaTime;
        if(damagedHealthTimer < 0)
        {
            if(HealthBar.fillAmount < damagedBarImage.fillAmount)
            {
                float shrinkSpeed = 1f;
                damagedBarImage.fillAmount -= shrinkSpeed * Time.deltaTime;
            }
        }
    
    }
    public void HealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        SetHealth(healthSystem.GetHealthNormalized());
        damagedBarImage.fillAmount = HealthBar.fillAmount;
    }

    public void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        damagedHealthTimer = MaxdamagedHealthTimer;
        SetHealth(healthSystem.GetHealthNormalized());
    }
   
    public void SetHealth(float healthNormalized)
    {
        HealthBar.fillAmount = healthNormalized;
    }
}
