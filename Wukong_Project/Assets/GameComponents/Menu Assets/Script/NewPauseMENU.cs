using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPauseMENU : MonoBehaviour
{
    //add scripts for the animation controller

    public MenuInput Pauseinput;

    public GameObject PausePointer;
    public GameObject PauseControlsPointer;
    public GameObject ControlsMenu;
    public GameObject PauseMenuUI;
    public GameObject ControllerControls;
    public GameObject KMcontrols;

    [SerializeField]
    private int NumberOfButtons = 4;
    public Transform PauseButtonPos1;
    public Transform PauseButtonPos2;
    public Transform PauseButtonPos3;
    public Transform ButtonPos1;
    public Transform ButtonPos2;

    float move;
    Vector2 mousePos;

    private int SelectedButton = 1;
    private int nButtons = 2;
    private int selectB = 1;

    [SerializeField]
    public static bool GamePaused;
    private bool controlsmenu;

    void Awake()
    {
        ControllerControls.SetActive(false);
        KMcontrols.SetActive(false);
        PausePointer.SetActive(true);
        ControlsMenu.SetActive(false);
        PauseControlsPointer.SetActive(false);

        controlsmenu = false;
        GamePaused = false;
       
        Pauseinput = new MenuInput();
        Pauseinput.PlayerInput.Select.performed += ctx => StartGame();
        Pauseinput.PlayerInput.BackB.performed += ctx => Back();
        Pauseinput.PlayerInput.Move.performed += ctx => move = ctx.ReadValue<float>();
        Pauseinput.PlayerInput.Pause.performed += ctx => PauseGame();
        Pauseinput.PlayerInput.Cursor.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
    }

    void StartGame()
    {
        if (controlsmenu == false)
        {
            if (SelectedButton == 1)
            {
                PauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GamePaused = false;
            }
            else if (SelectedButton == 2)
            {
                ControlsMenu.SetActive(true);
                controlsmenu = true;
                PausePointer.SetActive(false);
                PauseControlsPointer.SetActive(true);
                PauseMenuUI.SetActive(false);
              

            }
            else if (SelectedButton == 3)
            {
                SceneManager.LoadScene("Menu");
            }
        }
        else if (controlsmenu == true)
        {
            if (selectB == 1)
            {
                ControllerControls.SetActive(true);
                PauseControlsPointer.SetActive(false);
                ControlsMenu.SetActive(false);
                

            }
            else if (selectB == 2)
            {
                KMcontrols.SetActive(true);
                PauseControlsPointer.SetActive(false);
                ControlsMenu.SetActive(false);
                
            }
        }
   
    }
    public void Back()
    {
        if (controlsmenu == true)
        {
            KMcontrols.SetActive(false);
            ControllerControls.SetActive(false);
            PauseMenuUI.SetActive(true);
            ControlsMenu.SetActive(false);
            PausePointer.SetActive(true);
            PauseControlsPointer.SetActive(false);
            controlsmenu = false;
           
        }
        else if (controlsmenu == false)
        {
            PauseMenuUI.SetActive(false);
            ControllerControls.SetActive(false);
            KMcontrols.SetActive(false);
            ControlsMenu.SetActive(false);
            PauseControlsPointer.SetActive(false);
            GamePaused = false;
            Time.timeScale = 1f;


        }
    }
    void PauseGame()
    {
        if (controlsmenu == false)
        {
            if (GamePaused == false)
            {
                PausePointer.SetActive(true);
                PauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GamePaused = true;
            }
            else
            {
                PauseMenuUI.SetActive(false);
                PausePointer.SetActive(false);
                Time.timeScale = 1f;
                GamePaused = false;
            }


        }
        
    }

    private void Update()
    {

        Debug.Log(move);
        // Checks if the pointer needs to move down or up, in this case the poiter moves up one button
        if (controlsmenu == false)
        {
            if (move >= 0.5)
            {
                if (SelectedButton > 1)
                {
                    SelectedButton -= 1;
                    move = 0;
                }
            }
            else if (move <= -0.5)
            {
                if (SelectedButton < NumberOfButtons)
                {
                    SelectedButton += 1;
                    move = 0;
                }

            }
            MoveThePointer();
            if (mousePos.y >= 270)
            {
                SelectedButton = 1;
                if (Pauseinput.PlayerInput.MouseSelect.triggered)
                {
                    StartGame();
                }

            }
            else if (mousePos.y <= 260 && mousePos.y >= 210)
            {
                SelectedButton = 2;
                if (Pauseinput.PlayerInput.MouseSelect.triggered)
                {
                    StartGame();
                }
            }
            else if (mousePos.y <= 200f)
            {
                SelectedButton = 3;
                if (Pauseinput.PlayerInput.MouseSelect.triggered)
                {
                    StartGame();
                }
            }
        
        }
        else if (controlsmenu == true)
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
            if (mousePos.y >= 236 && mousePos.y <= 320)
            {
                selectB = 1;
                if (Pauseinput.PlayerInput.MouseSelect.triggered)
                {
                    StartGame();
                    controlsmenu = false;
                }
            }
            else if (mousePos.y <= 230)
            {
                selectB = 2;
                if (Pauseinput.PlayerInput.MouseSelect.triggered)
                {
                    StartGame();
                    controlsmenu = false;
                }
            }
        }
    }

    private void MoveThePointer()
    {
        // Moves the pointer
        if (SelectedButton == 1)
        {
            PausePointer.transform.position = PauseButtonPos1.position;
        }
        else if (SelectedButton == 2)
        {
            PausePointer.transform.position = PauseButtonPos2.position;
        }
        else if (SelectedButton == 3)
        {
            PausePointer.transform.position = PauseButtonPos3.position;
        }
     
    }
    private void MoveThePoint()
    {
        if (selectB == 1)
        {
            PauseControlsPointer.transform.position = ButtonPos1.position;
        }
        else if (selectB == 2)
        {
            PauseControlsPointer.transform.position = ButtonPos2.position;
        }

    }
    void OnEnable()
    {
        Pauseinput.PlayerInput.Enable();

    }
    private void OnDisable()
    {
        Pauseinput.PlayerInput.Disable();

    }
    public void kMBack()
    {

        KMcontrols.SetActive(false);
        ControllerControls.SetActive(false);
        PauseMenuUI.SetActive(true);
        PausePointer.SetActive(true);
        PauseControlsPointer.SetActive(false);
        controlsmenu = false;
        ControlsMenu.SetActive(false);
    }
    public void ConBack()
    {
        KMcontrols.SetActive(false);
        ControllerControls.SetActive(false);
        PauseMenuUI.SetActive(true);
        PausePointer.SetActive(true);
        PauseControlsPointer.SetActive(false);
        controlsmenu = false;
        ControlsMenu.SetActive(false);
    }
    public void ControlsBack()
    {
        KMcontrols.SetActive(false);
        ControllerControls.SetActive(false);
        PauseMenuUI.SetActive(true);
        PausePointer.SetActive(true);
        PauseControlsPointer.SetActive(false);
        controlsmenu = false;
        ControlsMenu.SetActive(false);
    }
    public void BackPause()
    {
        KMcontrols.SetActive(false);
        ControllerControls.SetActive(false);
        PauseMenuUI.SetActive(false);
        PausePointer.SetActive(false);
        PauseControlsPointer.SetActive(false);
        controlsmenu = false;
        ControlsMenu.SetActive(false);

    }
}
