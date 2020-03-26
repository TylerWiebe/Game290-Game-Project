using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Controller : MonoBehaviour
{
    //sfx of the morph
    //morph volume some value between 0-1
    public AudioClip morphSFX;
    public float morphVolume;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playMorph()
    {
        audioSource.PlayOneShot(morphSFX, morphVolume);
    }
}
