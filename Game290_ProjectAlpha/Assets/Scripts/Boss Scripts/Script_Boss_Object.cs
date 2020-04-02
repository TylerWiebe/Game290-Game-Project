using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Boss_Object : MonoBehaviour
{
    private GameObject gate;
    public float maxHitPoints = 2000;
    private float hitPoints;
    private AudioSource bossSting;
    private AudioSource bossMusicIdle;
    private AudioSource bossMusicCombat;

    //gameobject's audio player
    AudioSource audioSource;

    //boss damaged sound
    public AudioClip bossDamagedSFX;

    //boss deateh sound
    public AudioClip bossDeathSFX;

    //SFX volume
    public float sfxVolume;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maxHitPoints;
        gate = GameObject.Find("Gate");

        //music stuff
        bossSting = GameObject.Find("BossSting").GetComponent<AudioSource>();
        bossMusicIdle = GameObject.Find("IdleMusic").GetComponent<AudioSource>();
        bossMusicCombat = GameObject.Find("CombatMusic").GetComponent<AudioSource>();

        //set audioSource to the gameobject's "audio controller"
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //fade in boss
        StartCoroutine("Fade");
    }

    //fade in boss
    IEnumerator Fade()
    {
        float alpha = 0;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 8f)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1f, t));
            transform.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }

    //damage boss
    public void damageBoss(float damage)
    {
        Debug.Log(transform.GetComponent<Renderer>().material.color.a);
        if (transform.GetComponent<Renderer>().material.color.a >= 0.99)
        {
            hitPoints -= damage;
            playBossDamagedSFX();
            if (hitPoints <= 0)
                destroyBoss();
        }
    }

    //called by destruction/dying animation upon completion of animation
    public void destroyBoss()
    {
        //remove gate
        gate.SetActive(false);

        //spawn stat orb
        this.GetComponent<Script_SpawnStatOrb>().SpawnStatOrb(100, this.transform.position);

        //play boss mucked SFX
        playBossDeathSFX();

        //fade out music
        InvokeRepeating("FadeOutMusic", 0, 0.25f);
    }

    //music stuff
    void FadeOutMusic()
    {
        if (bossMusicIdle.volume > 0)
        {
            bossMusicIdle.volume -= 0.1f;
        }

        if (bossMusicCombat.volume > 0)
        {
            bossMusicCombat.volume -= 0.1f;
        }

        else if (bossMusicIdle.volume == 0 & bossMusicCombat.volume == 0)
        {
            bossSting.Play();
            Destroy(gameObject);
            CancelInvoke();
        }
    }

    //return the boss' hit points
    public float getHitPoints()
    {
        return hitPoints;
    }

    //return the maximum hit points for the boss
    public float getMaxHitPoints()
    {
        return maxHitPoints;
    }

    //play boss damaged SFX
    public void playBossDamagedSFX()
    {
        audioSource.PlayOneShot(bossDamagedSFX, sfxVolume);
    }

    //play boss death SFX
    public void playBossDeathSFX()
    {
        audioSource.PlayOneShot(bossDeathSFX, sfxVolume);
    }
}
