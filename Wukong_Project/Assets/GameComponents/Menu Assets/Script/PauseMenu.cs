using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public MenuInput pause;
    GameObject Selected;
    public GameObject PauseMenuUI;
    public GameObject Controlsmenu;
    public static bool GamePaused;
    private bool controlsmenu;

    private void Awake()
    {
        GamePaused = false;
        Controlsmenu.SetActive(false);
        PauseMenuUI.SetActive(false);
        controlsmenu = false;
        pause = new MenuInput();
        pause.PauseInput.Pause.performed += ctx => PauseGame();
        pause.PauseInput.select.performed += ctx => Resume();
        pause.PauseInput.Resume.performed += ctx => back();
    }
    void OnEnable()
    {
        pause.PauseInput.Enable();
    }
    void OnDisable()
    {
        pause.PauseInput.Disable();
    }
    void PauseGame()
    {
        Debug.Log("WW");
        if (GamePaused == false)
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
          
            GamePaused = true;
            Debug.Log("w");
        }
        else
        {
          PauseMenuUI.SetActive(false);
          Time.timeScale = 1f;
          GamePaused = false;
          Debug.Log("l");

        }
       
    }
    void Resume()
    {
        Selected = EventSystem.current.currentSelectedGameObject;
        if (Selected.name == "Resume Button")
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GamePaused = false;

        }
        else if (Selected.name == "Menu Button")
        {
            SceneManager.LoadScene("Menu");

        }
        else if (Selected.name == "Controls Button")
        {
            Controlsmenu.SetActive(true);
            controlsmenu = true;
            PauseMenuUI.SetActive(false);
            Time.timeScale = 0f;

        }
        
        
    }
    void back()
    {
        if(controlsmenu == true)
        {
            Controlsmenu.SetActive(false);
            controlsmenu = false;
            PauseMenuUI.SetActive(true);
            GamePaused = true;
            Time.timeScale = 0f;
        }
        else
        {
           PauseMenuUI.SetActive(false);
           GamePaused = false;
            Time.timeScale = 1f;
        }
    }
   


}
