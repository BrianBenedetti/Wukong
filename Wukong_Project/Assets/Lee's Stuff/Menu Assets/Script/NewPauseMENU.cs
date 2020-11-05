using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewPauseMENU : MonoBehaviour
{
    //add scripts for the animation controller
    public GameObject resumebutton;
    public GameObject controlsbutton;
    public GameObject Menubutton;
    public GameObject Controlpanel;
    public GameObject ParticleSelect;

    public MenuInput Pauseinput;

    public GameObject PausePointer;
    public GameObject PauseControlsPointer;
    public GameObject ControlsMenu;
    public GameObject PauseMenuUI;
    public GameObject ControllerControls;
    public GameObject KMcontrols;

    public GameObject button1;
    public GameObject button2;

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
        Time.timeScale = 1f;
        controlsmenu = false;
        GamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

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

            if (SelectedButton == 1 && GamePaused == true)

            {
               // FindObjectOfType<AudioManager>().Play("click");
                // PausePointer = GameObject.FindGameObjectWithTag("Pointer");
                //GameObject ParticleS = GameObject.Instantiate(ParticleSelect, PausePointer.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                // Destroy(ParticleS, 0.1f);
                PauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GamePaused = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            else if (SelectedButton == 2 && GamePaused == true)

            {
               // FindObjectOfType<AudioManager>().Play("click");
                //  PausePointer = GameObject.FindGameObjectWithTag("Pointer");
                // GameObject ParticleS = GameObject.Instantiate(ParticleSelect, PausePointer.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                //  Destroy(ParticleS, 0.1f);
                ControlsMenu.SetActive(true);
                controlsmenu = true;
                PausePointer.SetActive(false);
                PauseControlsPointer.SetActive(true);
                PauseMenuUI.SetActive(false);
                Time.timeScale = 0f;

            }

            else if (SelectedButton == 3 && GamePaused == true)

            {
               // FindObjectOfType<AudioManager>().Play("click");
                //PausePointer = GameObject.FindGameObjectWithTag("Pointer");
                // GameObject ParticleS = GameObject.Instantiate(ParticleSelect, PausePointer.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                // Destroy(ParticleS, 0.1f);
                SceneManager.LoadScene("Menu");
            }
        }
        else if (controlsmenu == true)
        {
            if (selectB == 1)
            {
                //FindObjectOfType<AudioManager>().Play("click");
                KMcontrols.SetActive(false);
                ControllerControls.SetActive(true);
                PauseMenuUI.SetActive(false);
                PausePointer.SetActive(false);
                PauseControlsPointer.SetActive(false);
                ControlsMenu.SetActive(false);
             

            }
            else if (selectB == 2)
            {
               // FindObjectOfType<AudioManager>().Play("click");
                KMcontrols.SetActive(true);
                ControllerControls.SetActive(false);
                PauseMenuUI.SetActive(false);
                PausePointer.SetActive(false);
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
            Time.timeScale = 0f;
            

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
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                PausePointer.SetActive(true);
                PauseMenuUI.SetActive(true);
                GamePaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                PauseMenuUI.SetActive(false);
                PausePointer.SetActive(false);
                Time.timeScale = 1f;
                GamePaused = false;
            }


        }
        
    }

    private void Update()
    {
        // Checks if the pointer needs to move down or up, in this case the poiter moves up one button

 if (controlsmenu == false && GamePaused == true)

        {
            if (move >= 0.5)
            {
                if (SelectedButton > 1)
                {
                   // FindObjectOfType<AudioManager>().Play("select");
                    SelectedButton -= 1;
                    move = 0;
                }
            }
            else if (move <= -0.5)
            {
                if (SelectedButton < NumberOfButtons)
                {
                    //FindObjectOfType<AudioManager>().Play("select");
                    SelectedButton += 1;
                    move = 0;
                }

            }
            MoveThePointer();
            if (mousePos.y >= 710 && mousePos.y <= 840 && mousePos.x > 780 && mousePos.x < 1130  && GamePaused == true)
            {
                SelectedButton = 1;
                if (Pauseinput.PlayerInput.MouseSelect.triggered)
                {
                    
                    StartGame();
                }

            }
            else if (mousePos.y >= 560 && mousePos.y <= 690 && mousePos.x > 780 && mousePos.x < 1130 && GamePaused == true)
            {
                SelectedButton = 2;
                if (Pauseinput.PlayerInput.MouseSelect.triggered)
                {
                   
                    StartGame();
                }
            }
            else if (mousePos.y >= 420 && mousePos.y <= 540 && mousePos.x > 780 && mousePos.x < 1130 && GamePaused == true)
            {
                SelectedButton = 3;
                if (Pauseinput.PlayerInput.MouseSelect.triggered)
                {

                    StartGame();
                }
            }
        
        }

        else if (controlsmenu == true && GamePaused == true)

        {
            if (move >= 0.5f)
            {
                if (selectB > 1)
                {
                   // FindObjectOfType<AudioManager>().Play("select");
                    selectB -= 1;
                    move = 0;
                }
            }
            else if (move <= -0.5f)
            {
                if (selectB < nButtons)
                {
                   // FindObjectOfType<AudioManager>().Play("select");
                    selectB += 1;
                    move = 0;
                }

            }
            MoveThePoint();
            if (mousePos.y >= 645 && mousePos.y <= 902 && mousePos.x > 650 && mousePos.x < 1250)
            {
                selectB = 1;
                if (Pauseinput.PlayerInput.MouseSelect.triggered)
                {
                    StartGame();
                    controlsmenu = false;
                }
            }
            else if (mousePos.y >= 356 && mousePos.y <= 640 && mousePos.x > 650 && mousePos.x < 1250)
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
            resumebutton.SetActive(false);
            controlsbutton.SetActive(true);
            Menubutton.SetActive(true);
            PausePointer.transform.position = PauseButtonPos1.position;
        }
        else if (SelectedButton == 2)
        {
            resumebutton.SetActive(true);
            controlsbutton.SetActive(false);
            Menubutton.SetActive(true);
            PausePointer.transform.position = PauseButtonPos2.position;
        }
        else if (SelectedButton == 3)
        {
            controlsbutton.SetActive(true);
            Menubutton.SetActive(false);
            resumebutton.SetActive(true);
            PausePointer.transform.position = PauseButtonPos3.position;
        }
     
    }
    private void MoveThePoint()
    {
        if (selectB == 1)
        {
            button1.SetActive(false);
            button2.SetActive(true);
            PauseControlsPointer.transform.position = ButtonPos1.position;
        }
        else if (selectB == 2)
        {
            button1.SetActive(true);
            button2.SetActive(false);
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
