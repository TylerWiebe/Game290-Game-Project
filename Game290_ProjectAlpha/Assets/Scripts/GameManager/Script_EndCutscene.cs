using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_EndCutscene : MonoBehaviour
{
    //Called when cutscene ends
    public void EndCutScene()
    {
        //Load next Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
