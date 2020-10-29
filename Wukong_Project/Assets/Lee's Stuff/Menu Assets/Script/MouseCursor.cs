using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseCursor : MonoBehaviour
{
    public MenuInput cursorpos;


//    public GameObject trailEffect;
    Vector2 MousePos;
  

    private void Awake()
    {
        cursorpos = new MenuInput();
       // cursorpos.PlayerInput.Cursor.performed += ctx => MousePos = ctx.ReadValue<Vector2>();
    }
    void Update()
    {
       
       
       
    }

    private void OnEnable()
    {
        cursorpos.PlayerInput.Enable();
    }
    private void OnDisable()
    {
       cursorpos.PlayerInput.Disable();
    }
}
