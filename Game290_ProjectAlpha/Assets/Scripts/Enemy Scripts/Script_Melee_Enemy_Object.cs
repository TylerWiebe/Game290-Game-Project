﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Melee_Enemy_Object : MonoBehaviour
{
    public float hit_points = 100;

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

    public void destroy()
    {
        Destroy(gameObject);
    }
}
