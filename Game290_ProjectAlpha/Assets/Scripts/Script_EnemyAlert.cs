using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EnemyAlert : MonoBehaviour
{
    //Attach this script to the enemy vision range game object.
    //Note - the vision range game object must be a child of the "Enemy" game object

    //on entering enemy alert range cause trigger event
    private void OnTriggerEnter2D (Collider2D other)
    {
        //check if the collision was with the player
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            //change the value of the enemy's playerNotSeen value (swap to aggressive mode)
            gameObject.GetComponentInParent<Script_EnemyAI>().playerNotSeen = false;
        }
    }
}
