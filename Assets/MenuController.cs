using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public void StartTask()
    {
        //Application.LoadLevel(1);
        SceneManager.LoadScene(1);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void About()
    {
        SceneManager.LoadScene(2);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
