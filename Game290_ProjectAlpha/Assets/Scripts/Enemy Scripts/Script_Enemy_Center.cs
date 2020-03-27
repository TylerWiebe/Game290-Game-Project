using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Enemy_Center : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //return the transform of the gameobject
    public Transform getPosition()
    {
        return this.gameObject.transform;
    }

    //if runs into a wall during passive behaviour, just pick a new direction to yas in
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RangedEnemy") || other.gameObject.CompareTag("MeleeEnemy"))
        {
            if (other.gameObject.GetComponent<Script_EnemyAI>().reachedBounds = true)
            {
                other.gameObject.GetComponent<Script_EnemyAI>().reachedBounds = false;
                other.gameObject.GetComponent<Script_EnemyAI>().playerNotSeen = true;
            }
        }
        yield break;
    }
}
