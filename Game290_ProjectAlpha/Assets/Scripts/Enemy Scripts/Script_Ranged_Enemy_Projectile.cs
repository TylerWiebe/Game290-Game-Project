﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_Ranged_Enemy_Projectile : MonoBehaviour
{
    private long start_time;
    private int damage;

    void Start()
    {
        start_time = DateTime.Now.Ticks / 10000;
    }

    void Update()
    {
        if ((DateTime.Now.Ticks / 10000) - start_time >= 2500)
        {
            Destroy(this.gameObject);
        }
    }


    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            GameObject player = GameObject.Find("AlienHead");
            player.GetComponent<Alien_Object>().Deal_Damage_To_Alien(damage);
            Destroy(this.gameObject);
            yield break;
        }
        else if (other.tag == "Wall")
        {
            Destroy(this.gameObject);
            yield break;
        }
    }

    public void set_damage(int damage)
    {
        this.damage = damage;
    }
}
