using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_CutSceneManager : MonoBehaviour
{
    //holds scene transition manager object
    [SerializeField]
    private GameObject sceneTransitionManager = null;

    //Called by signal
    public void EndCutScene()
    {
        //play fade out animation
        sceneTransitionManager.GetComponent<Script_SceneTransition>().NextSceneCall(4);
    }

    //Called by signal
    public void ReturnToMenuCall()
    {
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        //play fade out animation
        sceneTransitionManager.GetComponent<Script_SceneTransition>().TransitionCall(4);

        //wait for animation(1 second)
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("MainMenu");
    }


    //on entering enemy alert range cause trigger event
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if the collision was with the player
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            //play fade out animation & final cutscene
            sceneTransitionManager.GetComponent<Script_SceneTransition>().NextSceneCall(4);
        }
    }
}
