﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ranged_Enemy_Object : MonoBehaviour
{
    public int attack_damage = 50;
    public float hit_points = 50;

    //stat orb spawn chance
    private int spawnChance = 20;
    private Script_SwapMusic swapMusicScript;


    private GameObject enemy_body;
    private GameObject enemy_attack_cone;


    //gameobject's audio player
    AudioSource audioSource;

    //guard hurt sound
    public AudioClip rangedGuardDamagedSFX;

    //SFX volume
    public float sfxVolume;

    // Start is called before the first frame update
    void Start()
    {
        swapMusicScript = GameObject.Find("Music").GetComponent<Script_SwapMusic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int get_attack_damage()
    {
        return attack_damage;
    }

    public float get_hit_points()
    {
        return hit_points;
    }


    public void set_attack_damage(int attack_damage)
    {
        this.attack_damage = attack_damage;
    }

    public void set_hit_points(float hit_points)
    {
        this.hit_points = hit_points;
    }

    public void attacked(float damage)
    {
        //Debug.Log("GotHit");
        hit_points -= damage;
        if (hit_points <= 0)
        {
            //stop enemy from moving when dead
            this.transform.GetComponentInParent<Script_EnemyAI>().canMove = false;

            //set the enemy's velocity to zero when it dies
            this.transform.GetComponentInParent<Script_EnemyAI>().rigidBody.velocity = Vector3.zero;

            //stop enemy from rotating
            this.transform.GetComponentInParent<Script_EnemyAI>().canRotate = false;

            //stop enemy from attacking if it is mid attack
            this.transform.GetComponentInParent<Animator>().SetBool("isAttacking", false);

            //call the death animation
            this.transform.GetComponentInParent<Animator>().SetBool("isDead", true);
        }
    }

    //play slime attacking sound
    public void playRangedGuardDamagedSFX()
    {
        audioSource.PlayOneShot(rangedGuardDamagedSFX, sfxVolume);
    }

    public void destroy()
    {
        gameObject.GetComponent<Script_SpawnStatOrb>().SpawnStatOrb(spawnChance, gameObject.transform.position);
        swapMusicScript.alertedEnemiesCount -= 1;

        Destroy(gameObject);
    }
}
