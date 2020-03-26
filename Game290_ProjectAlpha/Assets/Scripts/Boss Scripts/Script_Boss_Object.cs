using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Boss_Object : MonoBehaviour
{
    private GameObject gate;
    public float maxHitPoints = 1000;
    private float hitPoints;
    private AudioSource bossSting;
    private AudioSource bossMusicIdle;
    private AudioSource bossMusicCombat;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maxHitPoints;
        gate = GameObject.Find("Gate");

        bossSting = GameObject.Find("BossSting").GetComponent<AudioSource>();
        bossMusicIdle = GameObject.Find("IdleMusic").GetComponent<AudioSource>();
        bossMusicCombat = GameObject.Find("CombatMusic").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //damage boss
    public void damageBoss(float damage)
    {
        Debug.Log("current Boss HP: " + hitPoints.ToString());
        hitPoints -= damage;
        if (hitPoints <= 0)
            destroyBoss();
    }

    //called by destruction/dying animation upon completion of animation
    public void destroyBoss()
    {
        //remove gate
        gate.SetActive(false);

        //spawn stat orb
        this.GetComponent<Script_SpawnStatOrb>().SpawnStatOrb(100, this.transform.position);

        //fade out music
        InvokeRepeating("FadeOutMusic", 0, 0.25f);
    }

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

    public float getHitPoints()
    {
        return hitPoints;
    }
    public float getMaxHitPoints()
    {
        return maxHitPoints;
    }
}
