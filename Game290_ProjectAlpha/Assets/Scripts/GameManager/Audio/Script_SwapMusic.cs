using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SwapMusic : MonoBehaviour
{
    //boolean conditions
    public bool isPlayingCombatMusic = false;
    private int alertedEnemies = 0;

    private GameObject music;

    //reference to Audio Source components
    private AudioSource idleMusic;
    private AudioSource introCombatMusic;
    private AudioSource combatMusic;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("Music");

        idleMusic = music.transform.Find("IdleMusic").gameObject.GetComponent<AudioSource>();
        introCombatMusic = music.transform.Find("IntroCombatMusic").gameObject.GetComponent<AudioSource>();
        combatMusic = music.transform.Find("CombatMusic").gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        //if no alerted enemies swap to idle music
    }
    



    public void PlayCombatMusic()
    {
        {
            StartCoroutine(SwapToCombatMusic());
        }
    }

    public void PlayIdleMusic()
    {
        {
            StartCoroutine(SwapToIdleMusic());
        }
    }






   IEnumerator SwapToIdleMusic()
    {
        InvokeRepeating("FadeOutCombatMusic", 0, 0.25f);
        InvokeRepeating("FadeInIdleMusic", 0, 0.25f);
        yield return new WaitForSeconds(0);
    }

    IEnumerator SwapToCombatMusic()
    {
        InvokeRepeating("FadeOutIdleMusic", 0, 0.25f);

        introCombatMusic.Play(0);
        InvokeRepeating("FadeInIntroCombatMusic", 0, 0.25f);

        yield return new WaitForSeconds(7);

        combatMusic.Play(0);
        InvokeRepeating("FadeInCombatMusic", 0, 0.25f);
    }





    void FadeOutIdleMusic()
    {
        if (idleMusic.volume > 0)
        {
            idleMusic.volume -= 0.1f;
        }
    }

    void FadeInIdleMusic()
    {
        if (idleMusic.volume < 1)
        {
            idleMusic.volume += 0.1f;
        }
    }

    void FadeInIntroCombatMusic()
    {
        if (introCombatMusic.volume < 1)
        {
            introCombatMusic.volume += 0.1f;
        }
    }

    void FadeInCombatMusic()
    {
        if (combatMusic.volume < 1)
        {
            combatMusic.volume += 0.1f;
        }
    }

    void FadeOutCombatMusic()
    {
        if (combatMusic.volume > 0)
        {
            combatMusic.volume -= 0.1f;
        }
    }
}
