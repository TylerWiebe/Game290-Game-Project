using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls Enemy behaviour for both passive and alerted phases.  
//Enemy walks back and forth.  If player appears in sight, walk towards and attack player.
//NOTE - Player game object must be tagged "Player"

public class Script_EnemyAI : MonoBehaviour
{
    //Speed of walking after seeing player
    [SerializeField]
    private int aggressiveSpeed = 7;

    //Speed of walking before seeing player
    [SerializeField]
    private int passiveSpeed = 5;

    //Whether player has been spotted or not
    public bool playerNotSeen = true;

    //holds the game object enemy chases
    private Transform target;

    //direction enemy starts walking
    [SerializeField]
    private string walkingDirection = "Right";

    [SerializeField]
    private float roamDistance = 5f;

    void Start ()
    {
        //assign the object with tag "Player" to be the enemy's target 
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerNotSeen == true)
        {
            StartCoroutine(PassiveBehaviour());
        }

        else
        {
            StartCoroutine(AggressiveBehaviour());
        }
    }




    //Passive enemy behaviour (When enemy has not yet spotted player)
    IEnumerator PassiveBehaviour()
    {
        //Walk right for specified seconds
        while ((walkingDirection == "right") || (walkingDirection == "Right"))
        {
            transform.Translate(Vector2.right * passiveSpeed * Time.deltaTime);

            //wait
            yield return new WaitForSeconds(roamDistance);

            //change direction
            walkingDirection = "left";
            yield break;
        }

        //walk
        while ((walkingDirection == "left") || (walkingDirection == "Left"))
        {
            transform.Translate(Vector2.left * passiveSpeed * Time.deltaTime);

            //wait
            yield return new WaitForSeconds(roamDistance);

            //change direction
            walkingDirection = "right";
            yield break;
        }

        //pause and turn right
        yield return new WaitForSeconds(1);
    }

    //Agressive enemy behaviour triggered after enemy notices player
    IEnumerator AggressiveBehaviour()
    {
        //pause and play alerted animation
        yield return new WaitForSeconds(1);

        //walk towards target position at specified speed
        transform.position = Vector2.MoveTowards(transform.position, target.position, aggressiveSpeed * Time.deltaTime);
    }
}
