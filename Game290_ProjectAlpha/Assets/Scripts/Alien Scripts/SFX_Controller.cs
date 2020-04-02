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

    private bool notDead = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myScript = gameObject.GetComponentInChildren<Alien_Object>();
    }

    public void BruiserDied()
    {
        if (notDead)
        {

            if (myScript.get_current_health() <= 0)
            {
                audioSource.PlayOneShot(dieB, sfxVolume);
                notDead = false;
            }
            else
            {
                audioSource.PlayOneShot(morphSFX, sfxVolume);
            }
        }
    }
    public void AssassinDied()
    {
        if (notDead)
        {
            if (myScript.get_current_health() <= 0)
            {
                audioSource.PlayOneShot(dieA, sfxVolume);
                notDead = false;
            }
            else
            {
                audioSource.PlayOneShot(morphSFX, sfxVolume);
            }

        }
    }
    public void SniperDied()
    {
        if (notDead)
        {
            Debug.Log("calledSniperDied");
            if (myScript.get_current_health() <= 0)
            {
                audioSource.PlayOneShot(dieS, sfxVolume);
                notDead = false;
            }
            else
            {
                audioSource.PlayOneShot(morphSFX, sfxVolume);
            }
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
