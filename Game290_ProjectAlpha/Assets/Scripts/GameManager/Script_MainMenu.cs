using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_MainMenu : MonoBehaviour
{
    //mixer reference
    [SerializeField]
    private AudioMixer mixer = null;

    //holds available resolutions as array
    private Resolution[] resolutions;

    [SerializeField]
    private Dropdown resolutionDropdown = null;

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
        //Load next Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Exits the game
    public void ExitGame()
    {
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

    //sets the volume slider value
    public void SetVolume(float sliderValue)
    {
        //logarithm for smooth volume change
        mixer.SetFloat("volume", Mathf.Log10(sliderValue) * 20);
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
}
