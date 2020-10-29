using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class Launcher_UI : MonoBehaviour
{
    public string Directory;
    public string URL;

    public void Play_Game()
    {
        Process.Start(Directory);
    }
    public void Quit_Laucher()
    {
        Application.Quit();
    }
}
