﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

//This script must be attached to the attackRange game object and must also be a child of the "enemy" game object.
public class Script_Ranged_Enemy_Attack : MonoBehaviour
{
    //allows enemy to attack if set to "true"
    public float speed;
    private GameObject target;
    private bool playerSeen = false;
    public float stoppingDistance = 7f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {

    }

    //when a collision with player occurs, trigger attacks
    //IEnumerator OnTriggerEnter2D(Collider2D other)
    IEnumerator OnTriggerStay2D(Collider2D other)
    {
        if (((other.tag == "player") || (other.tag == "Player")) && playerSeen)
        {
            //stop the enemy from moving
            this.transform.parent.gameObject.GetComponent<Script_EnemyAI>().canMove = false;
            //wait time for attack
            yield return new WaitForSeconds(0.25f);
            //call animation
            this.transform.GetComponentInParent<Animator>().SetBool("isAttacking", true);
        }
    }

    //attack function called by animation
    public void Attack()
    {
        GameObject projectile = Resources.Load("boss_projectile") as GameObject;
        GameObject projectile_instance = Instantiate(projectile, new Vector3(this.gameObject.transform.parent.gameObject.transform.position.x, this.gameObject.transform.parent.gameObject.transform.position.y, 0), Quaternion.identity);

        //shoots the way that the enemy is facing when an attack is triggered
        //get direction of shooty boi and convert to radians
        float theta = ((this.gameObject.transform.parent.GetComponent<Script_EnemyAI>().angle + 90)) * Mathf.Deg2Rad;
        float v1 = (float)(10 * Math.Cos(theta)); //find x velocity
        float v2 = (float)(10 * Math.Sin(theta)); //find y velocity
        Vector3 vector = new Vector3(v1, v2, 0f); //create a vector of x and y velocities

        //add a velocity to the projectile instance's rigidbody
        projectile_instance.GetComponent<Rigidbody2D>().velocity = vector * speed;
        //rotate projectile to face the direction it is being shootedededed
        projectile_instance.transform.rotation = Quaternion.Euler(0, 0, theta * Mathf.Rad2Deg + 90);
        //set the damage of the projectile   
        projectile_instance.GetComponent<Script_Ranged_Enemy_Projectile>().set_damage(this.gameObject.GetComponentInParent<Script_Ranged_Enemy_Object>().get_attack_damage());

        //allow enemy to move
        this.transform.parent.gameObject.GetComponent<Script_EnemyAI>().canMove = true;
        //stop attacking, if the play does not leave the attack range, another attakc will be launched right away
        this.transform.GetComponentInParent<Animator>().SetBool("isAttacking", false);
    }

    public void playerHasBeenSeen()
    {
        playerSeen = true;
    }
}
