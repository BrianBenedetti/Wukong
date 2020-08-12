using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPauseMENU : MonoBehaviour
{
    public MenuInput Pauseinput;
    public GameObject PausePointer;
    private int SelectedButton = 1;
    public GameObject ControlsMenu;
    public GameObject PauseMenuUI;

    [SerializeField]
    private int NumberOfButtons = 4;
    public Transform PauseButtonPos1;
    public Transform PauseButtonPos2;
    public Transform PauseButtonPos3;
    float move;
    [SerializeField]
    public static bool GamePaused;
    private bool controlsmenu;

    void Awake()
    {
        GamePaused = false;
        PausePointer.SetActive(true);
        ControlsMenu.SetActive(false);
        Pauseinput = new MenuInput();
        Pauseinput.PlayerInput.Select.performed += ctx => StartGame();
        Pauseinput.PlayerInput.BackB.performed += ctx => Back();
        Pauseinput.PlayerInput.Move.performed += ctx => move = ctx.ReadValue<float>();
        Pauseinput.PlayerInput.Pause.performed += ctx => PauseGame();
     

    }

    void OnEnable()
    {
        Pauseinput.PlayerInput.Enable();
       
    }
    private void OnDisable()
    {
        Pauseinput.PlayerInput.Disable();
       
    }

    void StartGame()
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
            Debug.Log("controls");

        }
        else if (SelectedButton == 3)
        {
            SceneManager.LoadScene("Menu");
        }
   
    }
    void Back()
    {
            
            PauseMenuUI.SetActive(false);
            GamePaused = false;
            Time.timeScale = 1f;
        
    }
    void PauseGame()
    {
        if (controlsmenu == false)
        {
            if (GamePaused == false)
            {
                PauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GamePaused = true;
            }
            else
            {
                PauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GamePaused = false;
            }

        }

    }

    private void Update()
    {

        Debug.Log(move);
        // Checks if the pointer needs to move down or up, in this case the poiter moves up one button

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
}
