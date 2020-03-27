using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Controller : MonoBehaviour
{

    Alien_Object myScript;
    //sfx of the morph
    //morph volume some value between 0-1
    public AudioClip morphSFX;
    public float sfxVolume;
    AudioSource audioSource;

    //alien Death sounds
    public AudioClip dieB;
    public AudioClip dieA;
    public AudioClip dieS;

    //alien attack sounds
    public AudioClip attackB;
    public AudioClip attackA;
    public AudioClip attackS;

    //alien hurt sounds
    public AudioClip hurtB;
    public AudioClip hurtA;
    public AudioClip hurtS;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myScript = gameObject.GetComponentInChildren<Alien_Object>();
    }

    public void BruiserDied()
    {
        if (myScript.get_current_health() <= 0)
        {
            audioSource.PlayOneShot(dieB, sfxVolume);
        }
        else
        {
            audioSource.PlayOneShot(morphSFX, sfxVolume);
        }
    }
    public void AssassinDied()
    {
        if (myScript.get_current_health() <= 0)
        {
            audioSource.PlayOneShot(dieA, sfxVolume);
        }
        else
        {
            audioSource.PlayOneShot(morphSFX, sfxVolume);
        }
    }
    public void SniperDied()
    {
        Debug.Log("calledSniperDied");
        if (myScript.get_current_health() <= 0)
        {
            audioSource.PlayOneShot(dieS, sfxVolume);
        }
        else
        {
            audioSource.PlayOneShot(morphSFX, sfxVolume);
        }
    }

    public void BruiserAttack()
    {
        audioSource.PlayOneShot(attackB, sfxVolume);
    }
    public void AssassinAttack()
    {
        audioSource.PlayOneShot(attackA, sfxVolume);
    }
    public void SniperAttack()
    {
        audioSource.PlayOneShot(attackS, sfxVolume);
    }

    public void BruiserHurt()
    {
        audioSource.PlayOneShot(hurtB, sfxVolume);
    }
    public void AssassinHurt()
    {
        audioSource.PlayOneShot(hurtA, sfxVolume);

    }
    public void SniperHurt()
    {
        audioSource.PlayOneShot(hurtS, sfxVolume);

    }

}
