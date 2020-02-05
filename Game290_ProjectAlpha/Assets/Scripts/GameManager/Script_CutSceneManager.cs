using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_CutSceneManager : MonoBehaviour
{
    //holds scene transition manager object
    [SerializeField]
    private GameObject sceneTransitionManager = null;

    //Called when cutscene ends
    public void EndCutScene()
    {
        //play fade out animation
        sceneTransitionManager.GetComponent<Script_SceneTransition>().NextSceneCall(4);
    }
}
