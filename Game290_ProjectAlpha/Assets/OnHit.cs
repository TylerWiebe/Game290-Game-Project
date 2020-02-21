﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{

    public float ttd = 2f;
    private void FixedUpdate()
    {
        if (ttd <= 0)
        {
            Destroy(gameObject); 
        }
        ttd -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MeleeEnemy")
        {
            collision.gameObject.GetComponent<Script_Melee_Enemy_Object>().attacked(GameObject.Find("AlienHead").GetComponent<Alien_Object>().getDamage() * 2);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "RangedEnemy")
        {
            collision.gameObject.GetComponent<Script_Ranged_Enemy_Object>().attacked(GameObject.Find("AlienHead").GetComponent<Alien_Object>().getDamage() * 2);
            Destroy(gameObject);
        }
    }
}