using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerElementalForms : MonoBehaviour
{
    public enum ElementalForms
    {
        normal,
        fire,
        water,
        air
    }

    public Material[] elementalHairMaterials;
    public Material[] elementalBodyMaterials;

    //public GameObject[] weaponElementalVFX;
    public GameObject[] transitionElementalVFX;

    public SkinnedMeshRenderer myHairRenderer;
    public SkinnedMeshRenderer myBodyRenderer;

    [HideInInspector] public PlayerInputActions inputActions;

    public ElementalForms currentElement;
    public DamageTypes currentDamageType;
    public DamageResistances currentResistances;
    public DamageResistances[] allResistances;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    
    private void Start()
    {
        currentElement = ElementalForms.normal;
        currentResistances = allResistances[3];
        currentDamageType = DamageTypes.normal;
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            if (gamepad.leftShoulder.isPressed && inputActions.PlayerControls.Jump.triggered && currentElement != ElementalForms.normal)
            {
                //set new element
                currentElement = ElementalForms.normal;
                //set new resistances
                currentResistances = allResistances[3];
                //set new damage type
                currentDamageType = DamageTypes.normal;
                //change hair and body materials
                SetSkinnedMaterial(myHairRenderer, 0, elementalHairMaterials[3]);
                SetSkinnedMaterial(myBodyRenderer, 0, elementalBodyMaterials[3]);
                //play transition VFX
                //change weapon VFX
            }
            else if(gamepad.leftShoulder.isPressed && inputActions.PlayerControls.PrimaryAttack.triggered && currentElement != ElementalForms.fire)
            {
                //set new element
                currentElement = ElementalForms.fire;
                //set new resistances
                currentResistances = allResistances[0];
                //set new damage type
                currentDamageType = DamageTypes.fire;
                //change hair and body materials
                SetSkinnedMaterial(myHairRenderer, 0, elementalHairMaterials[0]);
                SetSkinnedMaterial(myBodyRenderer, 0, elementalBodyMaterials[0]);
                //play transition VFX
                //change weapon VFX
            }
            else if (gamepad.leftShoulder.isPressed && inputActions.PlayerControls.SecondaryAttack.triggered && currentElement != ElementalForms.water)
            {
                //set new element
                currentElement = ElementalForms.water;
                //set new resistances
                currentResistances = allResistances[1];
                //set new damage type
                currentDamageType = DamageTypes.water;
                //change hair and body materials
                SetSkinnedMaterial(myHairRenderer, 0, elementalHairMaterials[1]);
                SetSkinnedMaterial(myBodyRenderer, 0, elementalBodyMaterials[1]);
                //play transition VFX
                //change weapon VFX
            }
            else if (gamepad.leftShoulder.isPressed && inputActions.PlayerControls.Interact.triggered && currentElement != ElementalForms.air)
            {
                //set new element
                currentElement = ElementalForms.air;
                //set new resistances
                currentResistances = allResistances[2];
                //set new damage type
                currentDamageType = DamageTypes.air;
                //change hair and body materials
                SetSkinnedMaterial(myHairRenderer, 0, elementalHairMaterials[2]);
                SetSkinnedMaterial(myBodyRenderer, 0, elementalBodyMaterials[2]);
                //play VFX
                //change weapon VFX
            }
        }
        else
        {
            //checks for input to change element
            if (inputActions.PlayerControls.NormalForm.triggered && currentElement != ElementalForms.normal)
            {
                //set new element
                currentElement = ElementalForms.normal;
                //set new resistances
                currentResistances = allResistances[3];
                //set new damage type
                currentDamageType = DamageTypes.normal;
                //change hair and body materials
                SetSkinnedMaterial(myHairRenderer, 0, elementalHairMaterials[3]);
                SetSkinnedMaterial(myBodyRenderer, 0, elementalBodyMaterials[3]);
                //play transition VFX
                //change weapon VFX
            }
            else if (inputActions.PlayerControls.FireForm.triggered && currentElement != ElementalForms.fire)
            {
                //set new element
                currentElement = ElementalForms.fire;
                //set new resistances
                currentResistances = allResistances[0];
                //set new damage type
                currentDamageType = DamageTypes.fire;
                //change hair and body materials
                SetSkinnedMaterial(myHairRenderer, 0, elementalHairMaterials[0]);
                SetSkinnedMaterial(myBodyRenderer, 0, elementalBodyMaterials[0]);
                //play transition VFX
                //change weapon VFX
            }
            else if (inputActions.PlayerControls.WaterForm.triggered && currentElement != ElementalForms.water)
            {
                //set new element
                currentElement = ElementalForms.water;
                //set new resistances
                currentResistances = allResistances[1];
                //set new damage type
                currentDamageType = DamageTypes.water;
                //change hair and body materials
                SetSkinnedMaterial(myHairRenderer, 0, elementalHairMaterials[1]);
                SetSkinnedMaterial(myBodyRenderer, 0, elementalBodyMaterials[1]);
                //play transition VFX
                //change weapon VFX
            }
            else if (inputActions.PlayerControls.AirForm.triggered && currentElement != ElementalForms.air)
            {
                //set new element
                currentElement = ElementalForms.air;
                //set new resistances
                currentResistances = allResistances[2];
                //set new damage type
                currentDamageType = DamageTypes.air;
                //change hair and body materials
                SetSkinnedMaterial(myHairRenderer, 0, elementalHairMaterials[2]);
                SetSkinnedMaterial(myBodyRenderer, 0, elementalBodyMaterials[2]);
                //play VFX
                //change weapon VFX
            }
        }
    }

    void SetSkinnedMaterial(SkinnedMeshRenderer renderer, int Mat_Nr, Material Mat)
    {
        Material[] mats = renderer.materials;

        mats[Mat_Nr] = Mat;

        renderer.materials = mats;
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
