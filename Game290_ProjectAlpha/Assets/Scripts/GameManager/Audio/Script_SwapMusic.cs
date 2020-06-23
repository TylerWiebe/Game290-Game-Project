using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SwapMusic : MonoBehaviour
{
    //boolean conditions
    public bool isPlayingCombatMusic = false;
    public int alertedEnemiesCount = 0;

    private GameObject music;

    //reference to Audio Source components
    private AudioSource idleMusic;
    private AudioSource combatMusic;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("Music");

        idleMusic = music.transform.Find("IdleMusic").gameObject.GetComponent<AudioSource>();
        combatMusic = music.transform.Find("CombatMusic").gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((alertedEnemiesCount == 0) & (isPlayingCombatMusic))
        {
            PlayIdleMusic();
        }
    }
    



    public void PlayCombatMusic()
    {
        {
            InvokeRepeating("FadeOutIdleMusic", 0, 0.25f);
            InvokeRepeating("FadeInCombatMusic", 0, 0.25f);
        }
    }

    public void PlayIdleMusic()
    {
        {
            isPlayingCombatMusic = false;
            InvokeRepeating("FadeOutCombatMusic", 0, 0.25f);
            InvokeRepeating("FadeInIdleMusic", 0, 0.25f);
        }
    }

    public void fadeOutMusic()
    {
        {
            InvokeRepeating("FadeOutCombatMusic", 0, 0.25f);
            InvokeRepeating("FadeOutIdleMusic", 0, 0.25f);
        }
    }


    //Idle Music Functions

    void FadeOutIdleMusic()
    {
        if (idleMusic.volume > 0)
        {
            idleMusic.volume -= 0.1f;
        }

        else if (idleMusic.volume == 0)
        {
            CancelInvoke();
        }
    }

    void FadeInIdleMusic()
    {
        if (idleMusic.volume < 1)
        {
            idleMusic.volume += 0.1f;
        }

        else if (idleMusic.volume == 1)
        {
            CancelInvoke();
        }
    }


    //combat Music Functions

    void FadeInCombatMusic()
    {
        if (combatMusic.volume < 1)
        {
            combatMusic.volume += 0.1f;
        }

        else if (combatMusic.volume == 1)
        {
            CancelInvoke();
        }
    }

    void FadeOutCombatMusic()
    {
        if (combatMusic.volume > 0)
        {
            combatMusic.volume -= 0.1f;
        }

        else if (idleMusic.volume == 0)
        {
            CancelInvoke();
        }
    }
}
