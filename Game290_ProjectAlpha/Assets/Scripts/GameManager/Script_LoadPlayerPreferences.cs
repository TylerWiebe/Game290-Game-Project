using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Script_LoadPlayerPreferences : MonoBehaviour
{
    //mixer reference
    [SerializeField]
    private AudioMixer mixer = null;

    [SerializeField]
    private Slider slider = null;

    // Load preferences on Scene start
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SliderValue");
    }
}
