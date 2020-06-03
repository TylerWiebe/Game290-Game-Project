using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Melee_Death : MonoBehaviour
{
    public AudioClip death_SFX;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

        audioSource.PlayOneShot(death_SFX, 0.7f);
    }
}
