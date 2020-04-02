using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_MainMenu : MonoBehaviour
{
    //holds available resolutions as array
    private Resolution[] resolutions;

    [SerializeField]
    private Dropdown resolutionDropdown = null;

    //holds scene transition manager object
    [SerializeField]
    private GameObject sceneTransitionManager = null;

    //holds scene transition manager object
    [SerializeField]
    private GameObject mainMenu = null;

    //holds scene transition manager object
    [SerializeField]
    private GameObject optionsMenu = null;

    //holds scene transition manager object
    [SerializeField]
    private GameObject tutorial = null;

    void Start ()
    {
        //on start store available resolutions in an array
        resolutions = Screen.resolutions;

        //clear default resolution options
        resolutionDropdown.ClearOptions();

        //create list
        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = 0;

        //convert array to list of strings
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            //compare resolution with screen resolution to identify correct resolution to start at
            if ((resolutions[i].width == Screen.currentResolution.width) && (resolutions[i].height == Screen.currentResolution.height))
            {
                currentResolutionIndex = i;
            }
        }

        //add resolution options to dropdown
        resolutionDropdown.AddOptions(resolutionOptions);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    /*
     * Main Menu
     */

    //plays game by changing scenes
    public void PlayGame()
    {
        //reference SceneTransitionManager, find Script_SceneTransition and call function "NextScene"
        sceneTransitionManager.GetComponent<Script_SceneTransition>().NextSceneCall(4);
    }

    //Call coroutine Exit
    public void ExitGame()
    {
        StartCoroutine(Exit());
    }

    //exits after transition and pause
    IEnumerator Exit()
    {
        sceneTransitionManager.GetComponent<Script_SceneTransition>().TransitionCall(2);

        //wait 1 second
        yield return new WaitForSeconds(2);

        //exit game
        Application.Quit();
    }

    /*
     * Options Menu
     */

    //set resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //setss game graphical quality
    public void SetQuality (int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    //setts game window to fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }




    public void OnOptionsClick()
    {
        StartCoroutine(Wait(0));
    }

    public void OnBackClick()
    {
        StartCoroutine(Wait(1));
    }

    //add pause for options button clicks
    IEnumerator Wait(int i)
    {
        if (i == 0)
        {
            yield return new WaitForSeconds(0.2f);
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }
        
        if (i == 1)
        {
            yield return new WaitForSeconds(0.2f);
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }

    /*
     * Tutorial Menu
     */

    public void OnTutorialClick()
    {
        StartCoroutine(TutorialWait(0));
    }

    public void OnTutorialBackClick()
    {
        StartCoroutine(TutorialWait(1));
    }

    //add pause for options button clicks
    IEnumerator TutorialWait(int i)
    {
        if (i == 0)
        {
            yield return new WaitForSeconds(0.2f);
            mainMenu.SetActive(false);
            tutorial.SetActive(true);
        }

        if (i == 1)
        {
            yield return new WaitForSeconds(0.2f);
            mainMenu.SetActive(true);
            tutorial.SetActive(false);
        }
    }
}
