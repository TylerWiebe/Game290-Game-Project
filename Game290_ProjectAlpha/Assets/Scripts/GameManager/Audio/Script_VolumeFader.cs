using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Script_VolumeFader : MonoBehaviour
{
    //mixer reference
    [SerializeField]
    private AudioMixer mixer = null;


    //sets the volume slider value
    public void SetVolume(float sliderValue)
    {
        float value = Mathf.Log10(sliderValue) * 20;

        //set mixer volume - logarithm for smooth volume change
        mixer.SetFloat("volume", value);

        //save volume to player preferences
        PlayerPrefs.SetFloat("MasterVolume", value);

        //save slider's positional value to player preferences
        PlayerPrefs.SetFloat("SliderValue", sliderValue);
    }
}
