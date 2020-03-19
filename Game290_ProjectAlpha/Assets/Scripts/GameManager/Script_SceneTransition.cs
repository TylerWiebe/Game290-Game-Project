using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Script_SceneTransition : MonoBehaviour
{
    //animator reference
    [SerializeField]
    private Animator animator = null;

    //mixer reference
    [SerializeField]
    private AudioMixer mixer = null;

    [SerializeField]
    private Slider slider = null;


    void Start()
    {
        if (PlayerPrefs.GetFloat("SliderValue") == 1)
        {
            mixer.SetFloat("volume", -1);
        }

        //reset volume
        StartCoroutine(VolumeFadeIn());

        //update saved slider position
        slider.value = PlayerPrefs.GetFloat("SliderValue");
    }



    /*
     * Play transition but dont change scenes
     */

    //sets a trigger to play the fade out animation & volume but dont go to next scene
    public void TransitionCall (int time)
    {
        StartCoroutine(Transition(time));
    }

    IEnumerator Transition(int time)
    {
        //VolumeFade mixer volume
        StartCoroutine(VolumeFadeOut());

        animator.SetTrigger("FadeOut");

        //wait for animation
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
        yield return new WaitForSeconds(3);

        //VolumeFade mixer volume
        StartCoroutine(VolumeFadeOut());

        animator.SetTrigger("FadeOut");

        //wait for animation(1 second)
        yield return new WaitForSeconds(time);

        //Load next Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
     * Volume fading
     */

    //slowly decrease volume of mixer
    IEnumerator VolumeFadeOut()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume");

        float vol = savedVolume;

        while (vol > -80)
        {
            vol += -5;

            //set volume
            mixer.SetFloat("volume", vol);
            yield return new WaitForSeconds(0.2f);
        }
    }

    //slowly increase volume of mixer
    IEnumerator VolumeFadeIn()
    {
        //set volume
        mixer.SetFloat("volume", -80);

        float vol = -80f;

        float savedVolume = PlayerPrefs.GetFloat("MasterVolume");

        float temp = 0;
        while (vol < savedVolume)
        {
            vol += 2 + temp;
            temp += 0.25f;
            mixer.SetFloat("volume", vol);
            yield return new WaitForSeconds(0.1f);
        }
    }
}

