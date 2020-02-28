using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script must be attached to the attackRange game object and must also be a child of the "enemy" game object.
public class Script_Melee_Enemy_Attack : Script_Melee_Enemy_Object
{
    private int attack_damage;
    public float stoppingDistance = 1.5f;


    //when a collision with player occurs, trigger attacks
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            InvokeRepeating("Attack", 2f, 1.5f);
        }
        yield break;
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
        GameObject player = GameObject.Find("AlienHead");
        player.GetComponent<Alien_Object>().Deal_Damage_To_Alien(attack_damage);
    }

    public void set_attack_damage(int attack_damage)
    {
        this.attack_damage = attack_damage;
    }
}
