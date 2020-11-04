using System.Collections;
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

    //animated gameobjects
    public GameObject playbutton;
    public GameObject controlsbutton;
    public GameObject creditsbutton;
    public GameObject quitbutton;
    public GameObject Controlpanel;
    public GameObject ParticleSelect;
    public AudioSource audioS;

    public GameObject button1;
    public GameObject button2;
    //check if the menu is active- to use an if statement
    private bool ControlsM;
    
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
        
                         
    }

    void StartGame()
    {
        if (ControlsM == false) 
        {
            if (SelectedButton == 1)
            {
                FindObjectOfType<AudioManager>().Play("click");
                GameObject clone = Instantiate(ParticleSelect, Point.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                Destroy(clone, 1f);
                SceneManager.LoadScene(1);
                //StartCoroutine(Delay());                            
            }
            else if (SelectedButton == 2)
            {
                FindObjectOfType<AudioManager>().Play("click");
                GameObject clone = Instantiate(ParticleSelect, Point.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                Destroy(clone, 1f);
                ControlsM = true;
                ControlsMenu.SetActive(true);
                Point.SetActive(false);
                Menu.SetActive(false);
                Pointer.SetActive(true);               
                SelectedButton = 1;
            }
            else if (SelectedButton == 3)
            {
                FindObjectOfType<AudioManager>().Play("click");
                GameObject clone = Instantiate(ParticleSelect, Point.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                Destroy(clone, 1f);
                ControlsM = false;
                ControlsMenu.SetActive(false);
                CreditsMenu.SetActive(true);
                Point.SetActive(false);
                Menu.SetActive(false);
                Pointer.SetActive(false);               
            }
            else if (SelectedButton == 4)
            {
                FindObjectOfType<AudioManager>().Play("click");
                GameObject clone = Instantiate(ParticleSelect, Point.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                Destroy(clone, 1f);
                Application.Quit();                
            }
        }
        else if (ControlsM == true )
        {
            if (selectB == 1)
            {
                FindObjectOfType<AudioManager>().Play("click");
                ControllerControls.SetActive(true);
                Pointer.SetActive(false);
                ControlsMenu.SetActive(false);
                KMcontrols.SetActive(false);               
                ControlsM = false;
            }
            else if (selectB == 2)
            {
                FindObjectOfType<AudioManager>().Play("click");
                KMcontrols.SetActive(true);
                ControllerControls.SetActive(false);
                Pointer.SetActive(false);
                ControlsMenu.SetActive(false);               
                ControlsM = false;
            }
        }
    }
   void Back()
    {
        if (ControlsM == true)
        {
            FindObjectOfType<AudioManager>().Play("click");
            SelectedButton = 1;
            ControlsMenu.SetActive(false);
            ControlsM = false;
            Point.SetActive(true);
            Menu.SetActive(true);
            Pointer.SetActive(false);
            ControllerControls.SetActive(false);
            KMcontrols.SetActive(false);        
        }
        else if (ControlsM == false)
        {
            FindObjectOfType<AudioManager>().Play("click");
            SelectedButton = 1;
            CreditsMenu.SetActive(false);
            Point.SetActive(true);
            Menu.SetActive(true);
            ControllerControls.SetActive(false);
            KMcontrols.SetActive(false);                   
        }
    }

    private void Update()
    {
   
        if (ControlsM == false)
        {
            if (move >= 0.5f)
            {
                if (SelectedButton > 1)
                {
                   FindObjectOfType<AudioManager>().Play("select");
                    SelectedButton -= 1;
                    move = 0;
                }
            }
            else if (move <= -0.5f)
            {
                if (SelectedButton < NumberOfButtons)
                {
                    FindObjectOfType<AudioManager>().Play("select");
                    SelectedButton += 1;
                    move = 0;
                }
            }
            MoveThePointer();
            if (mousePos.y >= 620 && mousePos.y < 760 && mousePos.x > 950 && mousePos.x < 1500)
            {                
                SelectedButton = 1;
                if(SelectedButton == 1 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    FindObjectOfType<AudioManager>().Play("click");
                    GameObject clone = GameObject.Instantiate(ParticleSelect, Point.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                    Destroy(clone, 0.05f);
                    SceneManager.LoadScene(1);                    
                }
            }
            else if (mousePos.y <= 610 && mousePos.y >= 470 && mousePos.x > 950 && mousePos.x < 1500)
            {
                SelectedButton = 2;
                if (SelectedButton == 2 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    FindObjectOfType<AudioManager>().Play("click");
                    GameObject clone = Instantiate(ParticleSelect, Point.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                    ControlsM = true;
                    ControlsMenu.SetActive(true);
                    Point.SetActive(false);
                    Menu.SetActive(false);
                    Pointer.SetActive(true);
                    Destroy(clone, 0.1f);
                }
            }
            else if (mousePos.y <= 460f && mousePos.y >= 340f && mousePos.x > 950 && mousePos.x < 1500)
            {        
                SelectedButton = 3;
                if (SelectedButton == 3 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    FindObjectOfType<AudioManager>().Play("click");
                    GameObject clone = Instantiate(ParticleSelect, Point.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
      
                    ControlsM = false;
                    ControlsMenu.SetActive(false);
                    CreditsMenu.SetActive(true);
                    Point.SetActive(false);
                    Menu.SetActive(false);
                    Pointer.SetActive(false);
                    Destroy(clone, 0.1f);
                }
            }
            else if (mousePos.y <= 330f && mousePos.y > 170 && mousePos.x > 950 && mousePos.x < 1500)
            {                              
                SelectedButton = 4;
                if (SelectedButton == 4 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    FindObjectOfType<AudioManager>().Play("click");
                    GameObject clone = Instantiate(ParticleSelect, Point.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                    Destroy(clone, 0.1f);
                    Application.Quit();
                    
                }
            }
        }
        else if (ControlsM == true)
        {           
            if (move >= 0.5f)
            {
                if (selectB > 1)
                {
                    FindObjectOfType<AudioManager>().Play("select");
                    selectB -= 1;
                    move = 0;                    
                }
            }
            else if (move <= -0.5f)
            {
                if (selectB < nButtons)
                {
                    FindObjectOfType<AudioManager>().Play("select");
                    selectB += 1;
                    move = 0;
                }
            }
            MoveThePoint();
            if (mousePos.y >= 550 && mousePos.y <= 840 && mousePos.x > 650 && mousePos.x < 1250)
            {
                selectB = 1;
                if(selectB == 1 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    FindObjectOfType<AudioManager>().Play("click");
                    ControllerControls.SetActive(true);
                    Pointer.SetActive(false);
                    ControlsMenu.SetActive(false);
                    KMcontrols.SetActive(false);
                    ControlsM = false;                   
                }
            }
            else if (mousePos.y >= 250 && mousePos.y <= 540 && mousePos.x > 650 && mousePos.x < 1250)
            {
                selectB = 2;
                if (selectB == 2 && menuinput.PlayerInput.MouseSelect.triggered)
                {
                    FindObjectOfType<AudioManager>().Play("click");
                    KMcontrols.SetActive(true);
                    ControllerControls.SetActive(false);
                    Pointer.SetActive(false);
                    ControlsMenu.SetActive(false);
                    ControlsM = false;                   

                }
            }
        }
    }
  
    private void MoveThePointer()
    {
        if (SelectedButton == 1 )
        {
            quitbutton.SetActive(true);
            playbutton.SetActive(false);
            controlsbutton.SetActive(true);
            creditsbutton.SetActive(true);
            Point.transform.position = ButtonPosition1.position;
        }
        else if (SelectedButton == 2 )
        {
            quitbutton.SetActive(true);
            playbutton.SetActive(true);
            controlsbutton.SetActive(false);
            creditsbutton.SetActive(true);
            Point.transform.position = ButtonPosition2.position;
        }
        else if (SelectedButton == 3 )
        {
         
            controlsbutton.SetActive(true);
            creditsbutton.SetActive(false);
            quitbutton.SetActive(true);
            playbutton.SetActive(true);
            Point.transform.position = ButtonPosition3.position;
        }
        else if (SelectedButton == 4 )
        {
           
            playbutton.SetActive(true);
            creditsbutton.SetActive(true);
            quitbutton.SetActive(false);
            playbutton.SetActive(true);
            Point.transform.position = ButtonPosition4.position;
        }
    }
    private void MoveThePoint()
    {
        if (selectB == 1 && ControlsM == true)
        {
            button1.SetActive(false);
            button2.SetActive(true);
            Pointer.transform.position = ButtonPos1.position;
        }
        else if (selectB == 2 && ControlsM == true)
        {
            button1.SetActive(true);
            button2.SetActive(false);
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
        FindObjectOfType<AudioManager>().Play("click");
        Point.SetActive(true);
        Menu.SetActive(true);
        CreditsMenu.SetActive(false);
        SelectedButton = 1;
    }
    //Back from key board and mouse panel
    public void kMBack()
    {
        FindObjectOfType<AudioManager>().Play("click");
        ControlsMenu.SetActive(false);
        ControlsM = false;
        Point.SetActive(true);
        Menu.SetActive(true);
        Pointer.SetActive(false);
        KMcontrols.SetActive(false);
        ControllerControls.SetActive(false);
        SelectedButton = 1;
    }
    public void ConBack()
    {
        FindObjectOfType<AudioManager>().Play("click");
        ControlsMenu.SetActive(false);
        ControlsM = false;
        Point.SetActive(true);
        Menu.SetActive(true);
        Pointer.SetActive(false);
        KMcontrols.SetActive(false);
        ControllerControls.SetActive(false);
        SelectedButton = 1;
    }
    public void ControlsBack()
    {
        FindObjectOfType<AudioManager>().Play("click");
        ControlsMenu.SetActive(false);
        ControlsM = false;
        Point.SetActive(true);
        Menu.SetActive(true);
        ControllerControls.SetActive(false);
        SelectedButton = 1;
    }     
}
