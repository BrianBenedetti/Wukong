using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public MenuInput DMG;
    
    

    // Start is called before the first frame update

    private void Awake()
    {
        DMG = new MenuInput();
    }
    void Start()
    {
     
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

            NewHealthBar.healthSystem.Damage(10);
            Debug.Log("taking Dmg");
        }

        else if (DMG.PlayerDMG.Action2.triggered)
        {
            NewHealthBar.healthSystem.Heal(10);


        }
        else if (DMG.PlayerDMG.Action3.triggered)
        {
            RageBar.Rage += 10;
         

        }

    }

}
