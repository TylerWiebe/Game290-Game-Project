using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EnemyAlert : MonoBehaviour
{
    //Attach this script to the enemyVisionRange game object.
    //Note - the vision range game object must be a child of the "Enemy" game object

    //holds enemy alerted icon
    [SerializeField]
    private GameObject myPrefab = null;

    private Vector3 position;

    //on entering enemy alert range cause trigger event
    private void OnTriggerEnter2D (Collider2D other)
    {
        //check if the collision was with the player and player hasn't been seen by enemy before
        if (((other.tag == "player") || (other.tag == "Player")) && (gameObject.GetComponentInParent<Script_EnemyAI>().playerNotSeen == true))
        {
            //create alert indicator above enemy's head
            position = transform.position;
            position.y += 3;
            GameObject clone = Instantiate(myPrefab, position, Quaternion.identity);

            //destroy clone after 1 second
            Destroy(clone, 1.0f);

            //change the value of the enemy's playerNotSeen value (swap to aggressive mode)
            gameObject.GetComponentInParent<Script_EnemyAI>().playerNotSeen = false;
            gameObject.GetComponentInParent<Script_EnemyAI>().reachedBounds = false;
        }
    }
}
