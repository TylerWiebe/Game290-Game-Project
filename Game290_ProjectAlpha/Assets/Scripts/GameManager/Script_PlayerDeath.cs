using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_PlayerDeath : MonoBehaviour
{
    //holds scene transition manager object
    [SerializeField]
    private GameObject sceneTransitionManager = null;

    //Called on player Death by Alien_Object
    public void DeathScreen()
    {
        //reference SceneTransitionManager, find Script_SceneTransition and call function "NextScene"
        sceneTransitionManager.GetComponent<Script_SceneTransition>().NextSceneCall(3);
    }
}
