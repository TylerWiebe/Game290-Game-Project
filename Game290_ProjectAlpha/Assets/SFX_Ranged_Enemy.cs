using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Ranged_Enemy : MonoBehaviour
{

    public AudioClip death_SFX_1;
    public AudioClip death_SFX_2;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //play the SFX when the guard dies
        int random = UnityEngine.Random.Range(1, 3);
        if (random == 1)
            audioSource.PlayOneShot(death_SFX_1, 0.7f);
        else
            audioSource.PlayOneShot(death_SFX_2, 0.7f);

    }
}
