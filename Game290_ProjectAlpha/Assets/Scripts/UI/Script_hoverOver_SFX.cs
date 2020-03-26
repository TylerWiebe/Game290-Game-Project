using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Script_hoverOver_SFX : MonoBehaviour
{
    public AudioClip AudioClip;
    private AudioSource audioSource;

    
    public void playHover()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.PlayOneShot(AudioClip, 1);
    }

}
