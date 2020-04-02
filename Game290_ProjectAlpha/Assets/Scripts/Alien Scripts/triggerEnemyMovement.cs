using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //follow player
        Vector3 follow = GameObject.Find("AlienBody").transform.position;
        follow.x -= 21;
        transform.position = follow;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RangedEnemy")
            collision.gameObject.GetComponent<Script_EnemyAI>().collided = true;
        else if (collision.gameObject.tag == "MeleeEnemy")
            collision.gameObject.GetComponent<Script_EnemyAI>().collided = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RangedEnemy")
            collision.gameObject.GetComponent<Script_EnemyAI>().collided = false;
        else if (collision.gameObject.tag == "MeleeEnemy")
            collision.gameObject.GetComponent<Script_EnemyAI>().collided = false;
    }
}
