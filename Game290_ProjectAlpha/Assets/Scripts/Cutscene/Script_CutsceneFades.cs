using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Controls The black fade out and in effect
 * in the starting cutscene
 */

public class Script_CutsceneFades : MonoBehaviour
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
        yield return new WaitForSeconds(4f);

        //fade in
        while (temp < 1f)
        {
            yield return new WaitForSeconds(0.01f);
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, temp);
            temp += 0.01f;
        }

        yield return new WaitForSeconds(0.5f);

        //fade out
        while (temp > 0)
        {
            yield return new WaitForSeconds(0.01f);
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, temp);
            temp -= 0.01f;
        }

        yield return new WaitForSeconds(1f);

        //fade in
        while (temp < 1f)
        {
            yield return new WaitForSeconds(0.01f);
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, temp);
            temp += 0.01f;
        }

        yield return new WaitForSeconds(1f);

        //fade out
        while (temp > 0)
        {
            yield return new WaitForSeconds(0.01f);
            blackScreen.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, temp);
            temp -= 0.01f;
        }

    }
}
