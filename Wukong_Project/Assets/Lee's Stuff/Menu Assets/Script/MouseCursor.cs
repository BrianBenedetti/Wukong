using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseCursor : MonoBehaviour
{
    public MenuInput cursorpos;

    Vector2 MousePos;

    private void Awake()
    {
        cursorpos = new MenuInput();
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
