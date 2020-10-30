using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsSelect : MonoBehaviour
{
    public MenuInput ControlMenuInput;
    public GameObject Pointer;
    private int SelectedButton = 1;
    [SerializeField]
    private int NumberOfButtons = 2;
    public Transform ButtonPos1;
    public Transform ButtonPos2;
    public GameObject ControllerControls;
    public GameObject KMcontrols;
    float move;
    // Start is called before the first frame update
    void Awake()
    {
        Pointer.SetActive(true);
        ControllerControls.SetActive(false);
        KMcontrols.SetActive(false);
        ControlMenuInput = new MenuInput();
        ControlMenuInput.PlayerInput.Select.performed += ctx => StartGame();
        ControlMenuInput.PlayerInput.BackB.performed += ctx => Back();
        ControlMenuInput.PlayerInput.Move.performed += ctx => move = ctx.ReadValue<float>();
        //menuinput.PlayerInput.Move.canceled += ctx => move = 0;

    }
    void OnEnable()
    {
            ControlMenuInput.PlayerInput.Enable();
    }
    void OnDisable()
    {
            ControlMenuInput.PlayerInput.Disable();
    }

    void StartGame()
    {
        if(SelectedButton == 1)
        {
            ControllerControls.SetActive(true);
            Pointer.SetActive(false);
        }
        else if(SelectedButton == 2)
        {
            KMcontrols.SetActive(true);
            Pointer.SetActive(false);
        }
    }
    void Back()
    {
            ControllerControls.SetActive(false);
            KMcontrols.SetActive(false);
            Pointer.SetActive(true);
    }

    private void Update()
    {
       
        // Checks if the pointer needs to move down or up, in this case the poiter moves up one button
      
        if ( move >= 0.5)
        {
            if (SelectedButton > 1)
            {
                SelectedButton -= 1;
                move = 0;
            }
        }
        else if(move <= -0.5)
        {
            if (SelectedButton < NumberOfButtons)
            {
                SelectedButton += 1;
                move = 0;
            }
           
        }
        MoveThePointer();
    }

    private void MoveThePointer()
    {
        // Moves the pointer
        if (SelectedButton == 1)
        {
            Pointer.transform.position = ButtonPos1.position;
        }
        else if (SelectedButton == 2)
        {
            Pointer.transform.position = ButtonPos2.position;
        }

    }

}
 

