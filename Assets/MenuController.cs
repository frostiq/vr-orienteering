﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void StartTask()
    {
        Application.LoadLevel(1);
        
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
