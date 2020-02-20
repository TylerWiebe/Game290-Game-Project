using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

//This script must be attached to the attackRange game object and must also be a child of the "enemy" game object.
public class Script_Ranged_Enemy_Attack : MonoBehaviour
{
    //allows enemy to attack if set to "true"
    private bool enemyInRange = false;
    public float speed;
    private GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {

    }


    //when a collision with player occurs, trigger attacks
    //IEnumerator OnTriggerEnter2D(Collider2D other)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            enemyInRange = true;
            InvokeRepeating("Attack", 0f, 1.5f);
        }
    }

    //trigger when player leaves enemy range (stop attacking)
    private void OnTriggerExit2D(Collider2D other)
    {
        //enemyInRange = false;
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            CancelInvoke();
        }
    }

    //temporary attack function
    private void Attack()
    {
        GameObject projectile = Resources.Load("Ranged_Enemy_Projectile") as GameObject;
        GameObject projectile_instance = Instantiate(projectile, new Vector3(this.gameObject.transform.parent.gameObject.transform.position.x, this.gameObject.transform.parent.gameObject.transform.position.y, 0), Quaternion.identity);

        //shoots the way that the enemy is facing when an attack is triggered
        //get direction of shooty boi and convert to radians
        float theta = ((this.gameObject.transform.parent.GetComponent<Script_EnemyAI>().angle + 90)) * Mathf.Deg2Rad;
        float v1 = (float)(10 * Math.Cos(theta)); //find x velocity
        float v2 = (float)(10 * Math.Sin(theta)); //find y velocity
        Vector3 vector = new Vector3(v1, v2, 0f); //create a vector of x and y velocities
        //Debug.Log(vector);
        //add a velocity to the projectile instance's rigidbody
        projectile_instance.GetComponent<Rigidbody2D>().velocity = vector * speed;
        projectile_instance.transform.rotation = Quaternion.Euler(0, 0, theta * Mathf.Rad2Deg - 90);

    }
}
