using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Script_SceneTransition : MonoBehaviour
{
    //animator reference
    [SerializeField]
    private Animator animator = null;

    //mixer reference
    [SerializeField]
    private AudioMixer mixer = null;


    void Start()
    {
        //reset volume
        StartCoroutine(VolumeFadeIn());
    }



    /*
     * Play transition but dont change scenes
     */

    //sets a trigger to play the fade out animation
    public void TransitionCall (int time)
    {
        StartCoroutine(Transition(time));
    }

    IEnumerator Transition(int time)
    {
        animator.SetTrigger("FadeOut");

        //wait for animation(1 second)
        yield return new WaitForSeconds(time);
    }


    /*
     * Play transition and go to next scene
     */

    public void NextSceneCall(int time)
    {
        StartCoroutine(NextScene(time));
    }

    IEnumerator NextScene(int time)
    {
        //VolumeFade mixer volume
        StartCoroutine(VolumeFadeOut());

        animator.SetTrigger("FadeOut");

        //wait for animation(1 second)
        yield return new WaitForSeconds(time);

        if (SceneManager.GetActiveScene().buildIndex <= 2)
        {
            //Load next Scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        else
        {
            //Load next Scene
            SceneManager.LoadScene("MainMenu");
        }
    }




        /*
         * Called to fade volume of mixer
         */

        //slowly decrease volume of mixer
        IEnumerator VolumeFadeOut()
    {
        int vol = 0;
        for (int i = 0; i < 8; i++)
        {
            vol += -5;

            //set volume
            mixer.SetFloat("volume", vol);
            yield return new WaitForSeconds(0.20f);
        }

        //set volume to 0
        mixer.SetFloat("volume", -80);
    }

    //slowly increase volume of mixer
    IEnumerator VolumeFadeIn()
    {
        int vol = -80;
        for (int i = 0; i < 5; i++)
        {
            vol += 12;

            //set volume
            mixer.SetFloat("volume", vol);
            yield return new WaitForSeconds(0.10f);
        }

        //slower fade up in level (to make smoother)
        for (int i = 0; i < 9; i++)
        {
            vol += 2;

            //set volume
            mixer.SetFloat("volume", vol);
            yield return new WaitForSeconds(0.15f);
        }
    }
}

