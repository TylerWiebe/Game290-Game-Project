using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script must be attached to the attackRange game object and must also be a child of the "enemy" game object.
public class Script_Melee_Enemy_Attack : MonoBehaviour
{
    //allows enemy to attack if set to "true"
    private bool enemyInRange = false;

    //when a collision with player occurs, trigger attacks
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            enemyInRange = true;

            //when enemy is in range attack
            while (enemyInRange == true)
            {
                Attack();
                //attack every 1.5 seconds
                yield return new WaitForSeconds(1.5f);
            }
        }
    }




    //trigger when player leaves enemy range (stop attacking)
    private void OnTriggerExit2D(Collider2D other)
    {
        enemyInRange = false;
    }




    //temporary attack function
    private void Attack()
    {
        Debug.Log("Melee Attack Triggered");
    }
}
