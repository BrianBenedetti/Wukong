using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewMenuInput : MonoBehaviour
{
    public MenuInput menuinput;

    //all games objects being used
    public GameObject Point;
    public GameObject Pointer;
    public GameObject Menu;
    public GameObject ControlsMenu;
    public GameObject CreditsMenu;
    public GameObject ControllerControls;
    public GameObject KMcontrols;

    //check if the menu is active- to use an if statement
    private bool ControlsM;

    // ints for with button is selected and how many buttons there are
    private int SelectedButton = 1;
    private int selectB = 1;
    private int NumberOfButtons = 4;
    private int nButtons = 2;

    //used to calculate if there has been an input and move the select cursor accordingly 
    float move;

    //the positions which the select cursor moves to
    public Transform ButtonPosition1;
    public Transform ButtonPosition2;
    public Transform ButtonPosition3;
    public Transform ButtonPosition4;
    public Transform ButtonPos1;
    public Transform ButtonPos2;
    
   
 
    void Awake()
    {
        //the controls menu will be disabled at the start
        ControlsM = false;

        //setting which game objects should be active or not active at the start
        Pointer.SetActive(false);
        Menu.SetActive(true);
        Point.SetActive(true);
        ControlsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        ControllerControls.SetActive(false);
        KMcontrols.SetActive(false);

        //inputs
        menuinput = new MenuInput();
        menuinput.PlayerInput.Select.performed += ctx => StartGame();
        menuinput.PlayerInput.BackB.performed += ctx => Back();
        menuinput.PlayerInput.Move.performed += ctx => move = ctx.ReadValue<float>();
    }

    void StartGame()
    {
        if (ControlsM == false)
        {
            if (SelectedButton == 1)
            {
                SceneManager.LoadScene("Gameplay");
            }
            else if (SelectedButton == 2)
            {
                ControlsM = true;
                ControlsMenu.SetActive(true);
                Point.SetActive(false);
                Menu.SetActive(false);
                Pointer.SetActive(true);
            }
            else if (SelectedButton == 3)
            {

                CreditsMenu.SetActive(true);
                Point.SetActive(false);
                Menu.SetActive(false);

            }
            else if (SelectedButton == 4)
            {
                Application.Quit();
            }
        }
        else if (ControlsM == true)
        {

            if (selectB == 1)
            {
                ControllerControls.SetActive(true);
                Pointer.SetActive(false);
                ControlsMenu.SetActive(false);


            }
            else if (selectB == 2)
            {
                KMcontrols.SetActive(true);
                Pointer.SetActive(false);
                ControlsMenu.SetActive(false);
            }
        }
    }
    void Back()
    {
        if (ControlsM == true)
        {
            ControlsMenu.SetActive(false);
            ControlsM = false;
            Point.SetActive(true);
            Menu.SetActive(true);
            Pointer.SetActive(false);
            ControllerControls.SetActive(false);
            KMcontrols.SetActive(false);
        }
        else
        {
            CreditsMenu.SetActive(false);
            Point.SetActive(true);
            Menu.SetActive(true);
            
        }


    }

    private void Update()
    {
       
       
        Debug.Log(move);
        if (ControlsM == false)
        {
            if (move >= 0.5f)
            {
                if (SelectedButton > 1)
                {
                    SelectedButton -= 1;
                    move = 0;
                }
            }
            else if (move <= -0.5f)
            {
                if (SelectedButton < NumberOfButtons)
                {
                    SelectedButton += 1;
                    move = 0;
                }

            }
            MoveThePointer();
        }
        else if (ControlsM == true)
        {
            if (move >= 0.5f)
            {
                if (selectB > 1)
                {
                    selectB -= 1;
                    move = 0;
                }
            }
            else if (move <= -0.5f)
            {
                if (selectB < nButtons)
                {
                    selectB += 1;
                    move = 0;
                }

            }
            MoveThePoint();
        }
    }
 
    private void MoveThePointer()
    {
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
    private void MoveThePoint()
    {
        if (selectB == 1)
        {
            Pointer.transform.position = ButtonPos1.position;
        }
        else if (selectB == 2)
        {
            Pointer.transform.position = ButtonPos2.position;
        }

    }
    private void OnEnable()
    {
        menuinput.PlayerInput.Enable();
    }
    private void OnDisable()
    {
        menuinput.PlayerInput.Disable();
    }
}
