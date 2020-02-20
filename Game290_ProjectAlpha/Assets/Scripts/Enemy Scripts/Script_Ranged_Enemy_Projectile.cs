using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_Ranged_Enemy_Projectile : MonoBehaviour
{
    private long start_time;


    void Start()
    {
        start_time = DateTime.Now.Ticks / 10000;
    }

    void Update()
    {
        if ((DateTime.Now.Ticks / 10000) - start_time >= 1500)
        {
            Destroy(this.gameObject);
        }
    }


    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            Debug.Log("Player hit with ranged attack!");
            yield break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }

    public void move_projectile()
    {

    }
}
