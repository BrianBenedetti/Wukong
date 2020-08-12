using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int MaxRage = 100;
    public int maxMana = 0;
    public int currentHealth;
    public healthBar HealthBar;
    public RageBar ragebar;
    public MenuInput DMG;
    public ManaBar mana;
    public int currentMana;
    public int currentRage;

    // Start is called before the first frame update

    private void Awake()
    {
        DMG = new MenuInput();
    }
    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
        currentMana = maxMana;
        mana.SetMaxMana(maxMana);
        currentRage = MaxRage;
        ragebar.SetMaxRage(MaxRage);

    }
    private void OnEnable()
    {
        DMG.PlayerDMG.Enable();
    }
    private void OnDisable()
    {
        DMG.PlayerDMG.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(DMG.PlayerDMG.Action.triggered){

            TakeDamage(2);

          }

        else if (DMG.PlayerDMG.Action2.triggered)
        {

            ManaDecrease(5);

        }
        else if (DMG.PlayerDMG.Action2.triggered)
        {

            IncreaseRage(5);

        }

    }
     void TakeDamage(int damage)
     {
     currentHealth -= damage;
     HealthBar.SetHealth(currentHealth);
     }
    void ManaDecrease(int ManaD)
    {
        currentMana -= ManaD;
        mana.SetMana(currentMana);
    }
   void IncreaseRage(int RageInc)
    {
        currentRage += RageInc;
        ragebar.SetRage(currentRage);
    }



}
