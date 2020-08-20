using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//using DG.Tweening;

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

    //animated gameobjects
    public GameObject playbutton;
    public GameObject controlsbutton;
    public GameObject creditsbutton;
    public GameObject quitbutton;

    
    //check if the menu is active- to use an if statement
    private bool ControlsM;
    private bool MenuActive;

    // ints for with button is selected and how many buttons there are
    private int SelectedButton = 1;
    private int selectB = 1;
    private int NumberOfButtons = 4;
    private int nButtons = 2;

    //used to calculate if there has been an input and move the select cursor accordingly 
    float move;
    Vector2 mousePos;

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
        MenuActive = true;

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
        menuinput.PlayerInput.Cursor.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
        //Cursor.visible = false;
        
    }

    void StartGame()
    {
        if (ControlsM == false && MenuActive == true)
        {
            if (SelectedButton == 1)
            {
                StartCoroutine(Delay());
                SelectedButton = 0;
                MenuActive = false;
            }
            else if (SelectedButton == 2)
            {
                ControlsM = true;
                ControlsMenu.SetActive(true);
                Point.SetActive(false);
                Menu.SetActive(false);
                Pointer.SetActive(true);
                SelectedButton = 0;
                MenuActive = false;
            }
            else if (SelectedButton == 3)
            {
                ControlsM = false;
                ControlsMenu.SetActive(false);
                CreditsMenu.SetActive(true);
                Point.SetActive(false);
                Menu.SetActive(false);
                Pointer.SetActive(false);
                SelectedButton = 0;
                MenuActive = false;
            }
            else if (SelectedButton == 4)
            {
                Application.Quit();
                SelectedButton = 0;
            }
        }
        else if (ControlsM == true && MenuActive == false)
        {

            if (selectB == 1)
            {
                ControllerControls.SetActive(true);
                Pointer.SetActive(false);
                ControlsMenu.SetActive(false);
                KMcontrols.SetActive(false);
                MenuActive = false;
                ControlsM = false;


            }
            else if (selectB == 2)
            {
                KMcontrols.SetActive(true);
                ControllerControls.SetActive(false);
                Pointer.SetActive(false);
                ControlsMenu.SetActive(false);
                MenuActive = false;
                ControlsM = false;
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
            MenuActive = true;
        }
        else if (ControlsM == false)
        {
            CreditsMenu.SetActive(false);
            Point.SetActive(true);
            Menu.SetActive(true);
            ControllerControls.SetActive(false);
            KMcontrols.SetActive(false);
            MenuActive = true;
        }
    }

    private void Update()
    {



        if (ControlsM == false && MenuActive == true)
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
            if (mousePos.y >= 530 && mousePos.y < 650 && mousePos.x > 950 && mousePos.x < 1270)
            {
                SelectedButton = 1;
                if(SelectedButton == 1 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    StartCoroutine(Delay());
                    SelectedButton = 0;
                    MenuActive = false;
                }
            }
            else if (mousePos.y <= 520 && mousePos.y >= 410 && mousePos.x > 950 && mousePos.x < 1270)
            {
                SelectedButton = 2;
                if (SelectedButton == 2 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    ControlsM = true;
                    ControlsMenu.SetActive(true);
                    Point.SetActive(false);
                    Menu.SetActive(false);
                    Pointer.SetActive(true);
                    SelectedButton = 0;
                    MenuActive = false;
                }
            }
            else if (mousePos.y <= 390f && mousePos.y >= 288f && mousePos.x > 950 && mousePos.x < 1270)
            {
                SelectedButton = 3;
                if (SelectedButton == 3 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    ControlsM = false;
                    ControlsMenu.SetActive(false);
                    CreditsMenu.SetActive(true);
                    Point.SetActive(false);
                    Menu.SetActive(false);
                    Pointer.SetActive(false);
                    MenuActive = false;
                    SelectedButton = 0;
                }
            }
            else if (mousePos.y <= 265f && mousePos.y > 172 && mousePos.x > 950 && mousePos.x < 1270)
            {
                SelectedButton = 4;
                if (SelectedButton == 4 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    Application.Quit();
                    SelectedButton = 0;
                    MenuActive = false;
                }
            }

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
            if (mousePos.y >= 236 && mousePos.y <= 320)
            {
                selectB = 1;
                if(selectB == 1 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    ControllerControls.SetActive(true);
                    Pointer.SetActive(false);
                    ControlsMenu.SetActive(false);
                    KMcontrols.SetActive(false);
                    MenuActive = false;
                    ControlsM = false;
                }
            }
            else if (mousePos.y <= 230)
            {
                selectB = 2;
                if (selectB == 2 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    KMcontrols.SetActive(true);
                    ControllerControls.SetActive(false);
                    Pointer.SetActive(false);
                    ControlsMenu.SetActive(false);
                    MenuActive = false;
                    ControlsM = false;
                }
            }
        }
    }
  
    private void MoveThePointer()
    {
        if (SelectedButton == 1 )
        {
           // playbutton.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
          //  controlsbutton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
            Point.transform.position = ButtonPosition1.position;
        }
        else if (SelectedButton == 2 )
        {
          //  playbutton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
           // controlsbutton.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
           // creditsbutton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
            Point.transform.position = ButtonPosition2.position;
        }
        else if (SelectedButton == 3 )
        {
           // controlsbutton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
          //  creditsbutton.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
           // quitbutton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
            Point.transform.position = ButtonPosition3.position;
        }
        else if (SelectedButton == 4 )
        {
         //   creditsbutton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
         //   quitbutton.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
            Point.transform.position = ButtonPosition4.position;
        }
    }
    private void MoveThePoint()
    {
        if (selectB == 1 && ControlsM == true)
        {
            Pointer.transform.position = ButtonPos1.position;
        }
        else if (selectB == 2 && ControlsM == true)
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

    //button back inputs
    //back button for credits
    public void BackCredits()
    {
        
        Point.SetActive(true);
        Menu.SetActive(true);
        CreditsMenu.SetActive(false);
       
    }
    //Back from key board and mouse panel
    public void kMBack()
    {
        MenuActive = true;
        ControlsMenu.SetActive(false);
        ControlsM = false;
        Point.SetActive(true);
        Menu.SetActive(true);
        Pointer.SetActive(false);
        KMcontrols.SetActive(false);
        ControllerControls.SetActive(false);
    }
    public void ConBack()
    {
        MenuActive = true;
        ControlsMenu.SetActive(false);
        ControlsM = false;
        Point.SetActive(true);
        Menu.SetActive(true);
        Pointer.SetActive(false);
        KMcontrols.SetActive(false);
        ControllerControls.SetActive(false);
    }
    public void ControlsBack()
    {
        MenuActive = true;
        ControlsMenu.SetActive(false);
        ControlsM = false;
        Point.SetActive(true);
        Menu.SetActive(true);
        ControllerControls.SetActive(false);
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Gameplay");

    }

   
}
