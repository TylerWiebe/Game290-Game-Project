using System.Collections;
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
    public float stoppingDistance = 7f;

    //gameobject's audio player
    AudioSource audioSource;

    //slime death sound
    public AudioClip rangedGuardAttackSFX;

    //SFX volume
    public float sfxVolume;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        //set audioSource to the gameobject's "audio controller"
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    //When the player enters the ranged enemy's attack radius
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (((other.tag == "player") || (other.tag == "Player")) && !this.gameObject.GetComponentInParent<Script_EnemyAI>().playerNotSeen)
        {
            InvokeRepeating("AttackSetUp", 1f, 1.5f);
        }
    }

    //When the player leaves the ranged enemy's attack radius
    public void OnTriggerExit2D(Collider2D other)
    {
        if (((other.tag == "player") || (other.tag == "Player")) && !this.gameObject.GetComponentInParent<Script_EnemyAI>().playerNotSeen)
        {
            CancelInvoke("AttackSetUp");
        }
    }


    public void AttackSetUp()
    {
        //stop the enemy from moving
        this.transform.parent.gameObject.GetComponent<Script_EnemyAI>().canMove = false;
        //call animation
        this.transform.GetComponentInParent<Animator>().SetBool("isAttacking", true);
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
        playRangedGuardAttackSFX();
    }

    //play slime attacking sound
    public void playRangedGuardAttackSFX()
    {
        audioSource.PlayOneShot(rangedGuardAttackSFX, sfxVolume);
    }
}
