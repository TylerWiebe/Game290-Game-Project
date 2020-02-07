using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PauseMenu : MonoBehaviour
{
    //reference to pause menu object
    [SerializeField]
    private GameObject pauseMenu = null;

    public static bool gameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
        //trigger on escape key press
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            //in case game is not paused
            if (gameIsPaused == false)
            {
                gameIsPaused = true;

                //freeze time
                Time.timeScale = 0f;

                pauseMenu.SetActive(true);
            }

            //in case game is paused
            else
            {
                gameIsPaused = false;

                //Unfreeze time
                Time.timeScale = 1f;

                pauseMenu.SetActive(false);
            }

        }
    }

    public void ResumeButton ()
    {
        pauseMenu.SetActive(false);

        gameIsPaused = false;

        //Unfreeze time
        Time.timeScale = 1f;
    }
}

