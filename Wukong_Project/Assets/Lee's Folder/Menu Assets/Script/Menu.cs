using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    public MenuInput menuinput;
    //checks which button is currently selected
    GameObject currentSelected;
    public GameObject ControlsMenu;
    public GameObject CreditsMenu;

    private void Awake()
    {
       ControlsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        menuinput = new MenuInput();
        menuinput.PlayerInput.Select.performed += ctx => StartGame();
        menuinput.PlayerInput.BackB.performed += ctx => Back();
      
    }
    void StartGame()
    {
        //if the currently selected buttons naming convention is the same the action takes effect;
        currentSelected = EventSystem.current.currentSelectedGameObject;

        if (currentSelected.name == "Play Button")
        {
            SceneManager.LoadScene("Gameplay");
        }
        else if (currentSelected.name == "Controls Button")
        {
            ControlsMenu.SetActive(true);
            
        }
        else if (currentSelected.name == "Credits Button")
        {
            CreditsMenu.SetActive(true);
        }
        else if (currentSelected.name == "Quit Button")
        {
            Application.Quit();
        }

    }
    void Back()
    {
        ControlsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
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
