using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;


public class VideoController : MonoBehaviour
{
    public float waitTime = 45f;
    public TextMeshProUGUI skipDisplay;

    [HideInInspector] public PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.Cutscene.Skip.started += ctx => StartCoroutine(EnableText(true)); 
        inputActions.Cutscene.Skip.performed += ctx => SkipCinematic();
        inputActions.Cutscene.Skip.canceled += ctx => StartCoroutine(EnableText(false));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForVideo());
    }

    private IEnumerator EnableText(bool state)
    {
        var gamepad = Gamepad.current;

        if(state == true)
        {
            if (gamepad != null)
            {
                skipDisplay.gameObject.SetActive(true);
                skipDisplay.text = "Hold X to Skip";
            }
            else
            {
                skipDisplay.gameObject.SetActive(true);
                skipDisplay.text = "Hold Space to Skip";
            }
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            skipDisplay.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForVideo(){
        yield return new WaitForSeconds(waitTime);
        SkipCinematic();
    }

    private void SkipCinematic()
    {
        StopCoroutine(WaitForVideo());
        SceneManager.LoadScene(1);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}
