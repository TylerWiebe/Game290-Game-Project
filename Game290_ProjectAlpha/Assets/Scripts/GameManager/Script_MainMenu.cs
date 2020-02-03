using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_MainMenu : MonoBehaviour
{

    /*
     * Main Menu
     */

    //plays game by changing scenes
    public void PlayGame()
    {
        //Load next Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Exits the game
    public void ExitGame()
    {
        Application.Quit();
    }
}
