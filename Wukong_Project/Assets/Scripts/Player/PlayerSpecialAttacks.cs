using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttacks : MonoBehaviour
{
    PlayerInputActions inputActions;

    PlayerElementalForms elementalFormScript;

    PlayerCombat combatScript;

    PlayerMovement movementScript;

    ObjectPooler objectPooler;

    readonly string fireSpecialAttack = "Fire Special Attack";
    readonly string waterSpecialAttack = "Water Special Attack";
    readonly string airSpecialAttack = "Air Special Attack";

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        elementalFormScript = GetComponent<PlayerElementalForms>();
        combatScript = GetComponent<PlayerCombat>();
        movementScript = GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActions.PlayerControls.SpecialAttack.triggered && movementScript.isGrounded && combatScript.currentMana == combatScript.maxMana)
        {
            switch (elementalFormScript.currentElement)
            {
                case PlayerElementalForms.ElementalForms.fire:
                    //Debug.Log("Used Fire attack");
                    objectPooler.SpawnFromPool(fireSpecialAttack, transform.position, Quaternion.identity);
                    combatScript.currentMana = 0;
                    combatScript.manaBar.SetValue(combatScript.currentMana);
                    break;

                case PlayerElementalForms.ElementalForms.water:
                    Debug.Log("Used Water attack");
                    //objectPooler.SpawnFromPool(waterSpecialAttack, transform.position, Quaternion.identity);
                    //combatScript.currentMana = 0;
                    //combatScript.manaBar.SetValue(combatScript.currentMana);
                    break;

                case PlayerElementalForms.ElementalForms.air:
                    Debug.Log("Used Air attack");
                    //objectPooler.SpawnFromPool(airSpecialAttack, transform.position, Quaternion.identity);
                    //combatScript.currentMana = 0;
                    //combatScript.manaBar.SetValue(combatScript.currentMana);
                    break;

                default:
                    break;
            }
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
