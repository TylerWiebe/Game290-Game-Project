using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Enemy_Hit_Wall : MonoBehaviour
{
    Script_EnemyAI parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject.transform.parent.GetComponent<Script_EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if runs into a wall during passive behaviour, just pick a new direction to yas in
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") && parent.playerNotSeen)
        {
            switch (parent.direction)
            {
                //walking right into a wall, go left
                case 0:
                    parent.walkLeft();
                    parent.direction = 1;
                    break;
                //walking left into a wall, go right
                case 1:
                    parent.walkRight();
                    parent.direction = 0;
                    break;
                //walking up into a wall, go down
                case 2:
                    parent.walkDown();
                    parent.direction = 3;
                    break;
                //walking down into a wall, go up
                case 3:
                    parent.walkUp();
                    parent.direction = 2;
                    break;
            }
        }
        else if (other.gameObject.CompareTag("ZoneBoundary"))
        {
            parent.reachedBounds = true;
            parent.playerNotSeen = true;
        }
        yield break;
    }
}
