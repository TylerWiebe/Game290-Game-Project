using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    //Stings
    //Sting_Lose is to be played on death
    public GameObject sting_lose;

    private bool notDead = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myScript = gameObject.GetComponentInChildren<Alien_Object>();
    }

    public void BruiserMorph()
    {
        if (notDead)
        {
            audioSource.PlayOneShot(morphSFX, sfxVolume);
        }
    }
    public void AssassinMorph()
    {
        if (notDead)
        {
            audioSource.PlayOneShot(morphSFX, sfxVolume);
        }
    }
    public void SniperMorph()
    {
        if (notDead)
        {
            audioSource.PlayOneShot(morphSFX, sfxVolume);
        }
       
    }

    public void BruiserDied()
    {
        audioSource.PlayOneShot(dieB, sfxVolume);
        Instantiate(sting_lose);
        notDead = false;
    }
    public void AssassinDied()
    {
        audioSource.PlayOneShot(dieA, sfxVolume);
        Instantiate(sting_lose);
        notDead = false;
    }
    public void SniperDied()
    {
        audioSource.PlayOneShot(dieS, sfxVolume);
        Instantiate(sting_lose);
        notDead = false;
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
        if (notDead)
        {
            audioSource.PlayOneShot(hurtB, sfxVolume);

        }
    }
    public void AssassinHurt()
    {
        if (notDead)
        {
            audioSource.PlayOneShot(hurtA, sfxVolume);

        }

    }
    public void SniperHurt()
    {
        if (notDead)
        {
            audioSource.PlayOneShot(hurtS, sfxVolume);

        }

    }

}
