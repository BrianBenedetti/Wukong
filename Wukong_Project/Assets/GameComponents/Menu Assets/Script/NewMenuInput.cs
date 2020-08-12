using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewMenuInput : MonoBehaviour
{
    public MenuInput menuinput;
    public GameObject Point;
    private int SelectedButton = 1;
    public GameObject ControlsMenu;
    public GameObject CreditsMenu;

    [SerializeField]
    private int NumberOfButtons = 4;
    public Transform ButtonPosition1;
    public Transform ButtonPosition2;
    public Transform ButtonPosition3;
    public Transform ButtonPosition4;
    float move;
 
    void Awake()
    {
        Point.SetActive(true);
        ControlsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        menuinput = new MenuInput();
        menuinput.PlayerInput.Select.performed += ctx => StartGame();
        menuinput.PlayerInput.BackB.performed += ctx => Back();
        menuinput.PlayerInput.Move.performed += ctx => move = ctx.ReadValue<float>();
        //menuinput.PlayerInput.Move.canceled += ctx => move = 0;

    }

  void OnEnable()
    {
        menuinput.PlayerInput.Enable();
    }
    private void OnDisable()
    {
        menuinput.PlayerInput.Disable();
    }

    void StartGame()
    {
        if(SelectedButton == 1)
        {
            SceneManager.LoadScene("Gameplay");
        }
        else if(SelectedButton == 2)
        {
            ControlsMenu.SetActive(true);
            Debug.Log("controls");

        }
        else if(SelectedButton == 3)
        {
            Debug.Log("credits");
            CreditsMenu.SetActive(true);
            Point.SetActive(false);

        }
        else if(SelectedButton == 4)
        {
            Application.Quit();
        }
    }
    void Back()
    {
        ControlsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        Point.SetActive(true);
    }

    private void Update()
    {
       
        Debug.Log(move);
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
            Point.transform.position = ButtonPosition1.position;
        }
        else if (SelectedButton == 2)
        {
            Point.transform.position = ButtonPosition2.position;
        }
        else if (SelectedButton == 3)
        {
            Point.transform.position = ButtonPosition3.position;
        }
        else if (SelectedButton == 4)
        {
            Point.transform.position = ButtonPosition4.position;
        }
    }
}
