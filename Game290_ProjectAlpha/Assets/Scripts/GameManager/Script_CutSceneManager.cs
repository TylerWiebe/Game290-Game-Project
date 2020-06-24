using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_CutSceneManager : MonoBehaviour
{
    //holds scene transition manager object
    [SerializeField]
    private GameObject sceneTransitionManager = null;

    [SerializeField]
    private AudioSource winAudio;

    private static bool IsLevel1 = false;

    private bool hasCollided = false;

    //Music script to be used to quiet the music when moving to next room
    private Script_SwapMusic myScript_Music;

    /*
    * Bring player end cutscene
    */

    //Called by signal
    public void EndCutScene()
    {
        //play fade out animation
        sceneTransitionManager.GetComponent<Script_SceneTransition>().NextSceneCall(4);
    }

    //Called by signal
    public void ReturnToMenuCall()
    {
        //Save the game statistics
        AchieveManager AM = new AchieveManager();
        AM.SaveData();
        //Head back to menu
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {

        //Save the game statistics
        AchieveManager AM = new AchieveManager();
        AM.SaveData();

        //play fade out animation
        sceneTransitionManager.GetComponent<Script_SceneTransition>().TransitionCall(4);

        //Set the level to level one, such that the alien will reset from main menu.
        IsLevel1 = true;

        // ensure game is not paused (for transition play)
        Script_PauseMenu.gameIsPaused = false;
        Time.timeScale = 1f;

        //wait for animation(1 second)
        yield return new WaitForSeconds(4);

        SceneManager.LoadScene("MainMenu");
    }

    /*
     * Bring player to game scene
     */

    //Called by signal
    public void ReviveCall()
    {
        //Save the game statistics
        AchieveManager AM = new AchieveManager();
        AM.SaveData();
        //Start Revive call
        StartCoroutine(Revive());
    }

    IEnumerator Revive()
    {
        //play fade out animation
        sceneTransitionManager.GetComponent<Script_SceneTransition>().TransitionCall(4);

        //Set a Boolean such that the Alien will reset itself
        IsLevel1 = true;

        //wait for animation(1 second)
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Level_1");
    }


    //on entering enemy alert range cause trigger event
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if the collision was with the player
        if (((other.tag == "player") || (other.tag == "Player")) & (hasCollided == false))
        {
            myScript_Music = GameObject.Find("Music").GetComponent<Script_SwapMusic>();
            myScript_Music.fadeOutMusic();

            GameObject.Find("AlienHead").GetComponent<Alien_Object>().AlienCanMove = false;
            GameObject.Find("AlienHead").GetComponent<Alien_Object>().AlienTouchedStairs1 = true;

            hasCollided = true;

            winAudio.Play();

            //play fade out animation & final cutscene
            sceneTransitionManager.GetComponent<Script_SceneTransition>().NextSceneCall(4);
        }
    }

    public bool getIsLevel1()
    {
        return IsLevel1;
    }

    public void setIsLevel1False()
    {
        IsLevel1 = false;
    }
}
