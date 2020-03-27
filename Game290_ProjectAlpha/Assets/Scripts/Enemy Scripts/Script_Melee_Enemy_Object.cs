﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Melee_Enemy_Object : MonoBehaviour
{
    public float hit_points;

    //stat orb spawn chance
    private int spawnChance = 15;
    private Script_SwapMusic swapMusicScript;

    //gameobject's audio player
    AudioSource audioSource;

    //slime death sound
    public AudioClip slimeDeathSFX;

    //SFX volume
    public float sfxVolume;

    void Start()
    {
        swapMusicScript = GameObject.Find("Music").GetComponent<Script_SwapMusic>();

        //set audioSource to the gameobject's "audio controller"
        audioSource = GetComponent<AudioSource>();
    }

    public void set_hit_points(float hit_points)
    {
        this.hit_points = hit_points;
    }

    public float get_hit_points()
    {
        return hit_points;
    }

    public void attacked(float damage)
    {

        Debug.Log("GotHitMelee");
        hit_points -= damage;

        playSlimeAttackSFX();

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
    public void playSlimeAttackSFX()
    {
        audioSource.PlayOneShot(slimeDeathSFX, sfxVolume);
    }

    public void destroy()
    {
        gameObject.GetComponent<Script_SpawnStatOrb>().SpawnStatOrb(spawnChance, gameObject.transform.position);
        swapMusicScript.alertedEnemiesCount -= 1;
        Destroy(gameObject);
    }
}
