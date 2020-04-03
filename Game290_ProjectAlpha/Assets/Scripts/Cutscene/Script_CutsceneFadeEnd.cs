using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CutsceneFadeEnd : MonoBehaviour
{
    //holds scene transition manager object
    [SerializeField]
    private GameObject blackScreen = null;

    private float temp = 0;

    void Start()
    {
        StartCoroutine("TransitionFade");
    }

    IEnumerator TransitionFade()
    {
        //wait
        yield return new WaitForSeconds(12f);

        //fade in
        while (temp < 1f)
        {
            yield return new WaitForSeconds(0.01f);
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, temp);
            temp += 0.01f;
        }
    }
}
