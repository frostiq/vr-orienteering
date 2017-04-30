using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public void StartTask()
    {
        //Application.LoadLevel(1);
        SceneManager.LoadScene(3);

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
        Debug.Log("Back");
        SceneManager.LoadScene(0);
    }
    public void LoadFirstTask()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadSecondTask()
    {
        SceneManager.LoadScene(4);
    }
    

}
