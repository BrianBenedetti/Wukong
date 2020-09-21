using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementalForms : MonoBehaviour
{
    enum ElementalForms
    {
        normal,
        fire,
        water,
        air
    }

    [HideInInspector] public PlayerInputActions inputActions;

    [SerializeField] ElementalForms myElement;
    public DamageTypes myDamageType;
    public DamageResistances myResistances;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    
    private void Start()
    {
        myDamageType = DamageTypes.normal;
        myElement = ElementalForms.normal;
    }

    // Update is called once per frame
    void Update()
    {
        //checks for input to change element
        if (inputActions.PlayerControls.NormalForm.triggered)
        {
            myElement = ElementalForms.normal;
            //change hair shader
            //change weapon VFX
        }
        else if (inputActions.PlayerControls.FireForm.triggered)
        {
            myElement = ElementalForms.fire;
            //change hair shader
            //play VFX
            //change weapon VFX
        }
        else if (inputActions.PlayerControls.WaterForm.triggered)
        {
            myElement = ElementalForms.water;
            //change hair shader
            //play VFX
            //change weapon VFX
        }
        else if (inputActions.PlayerControls.AirForm.triggered)
        {
            myElement = ElementalForms.air;
            //change hair shader
            //play VFX
            //change weapon VFX
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
