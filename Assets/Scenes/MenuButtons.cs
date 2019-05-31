using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public Button StartGameButton;

    public Button QuitButton;

    public string newGameName;


    public void newGame()
    {
        SceneManager.LoadScene(newGameName);
    }

    public void quitGame()
    {
        Application.Quit();
    }
    
}
