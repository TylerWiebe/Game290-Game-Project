using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls Enemy behaviour for both passive and alerted phases.  
//Enemy walks back and forth.  If player appears in sight, walk towards and attack player.
//NOTE - Player game object must be tagged "Player"

public class enemy_AI : MonoBehaviour
{
    //Speed of walking after seeing player
    [SerializeField]
    private int aggressiveSpeed = 7;

    //Randomization of AI passive walk direction
    //dont need this if you are not using the randomization of walking direction
    private int iter = 0;
    private System.Random rand = new System.Random();
    private int direction = 0;
    private int walkTime = 100;
    public int walkTime_LowerLimit = 100;
    public int walkTime_UpperLimit = 250;

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

    [SerializeField]
    private float stoppingDistance = 3f;

    void Start()
    {
        //assign the object with tag "Player" to be the enemy's target 
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //control behaviour type
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
        
        if (iter == walkTime)
        {
            iter = 0;
            direction = rand.Next(0, 4);
            walkTime = rand.Next(walkTime_LowerLimit, walkTime_UpperLimit);
            //Debug.Log(direction);
        }
        else
        {
            iter++;
        }

        if (roamDistance > 0)
        {
            while (direction == 0)
            {
                transform.Translate(Vector2.right * passiveSpeed * Time.deltaTime);

                //wait
                yield return new WaitForSeconds(roamDistance);
                //Debug.Log("right");
                yield break;
            }
            while (direction == 1)
            {
                transform.Translate(Vector2.left * passiveSpeed * Time.deltaTime);

                //wait
                yield return new WaitForSeconds(roamDistance);
                //Debug.Log("left");
                yield break;
            }
            while (direction == 2)
            {
                transform.Translate(Vector2.up * passiveSpeed * Time.deltaTime);

                //wait
                yield return new WaitForSeconds(roamDistance);
                //Debug.Log("up");
                yield break;
            }
            while (direction == 3)
            {
                transform.Translate(Vector2.down * passiveSpeed * Time.deltaTime);

                //wait
                yield return new WaitForSeconds(roamDistance);
                //Debug.Log("down");
                yield break;
            }
            //Original Script
            /*
            //Walk right for specified seconds
            if ((walkingDirection == "right") || (walkingDirection == "Right"))
            {
                transform.Translate(Vector2.right * passiveSpeed * Time.deltaTime);

                //wait
                yield return new WaitForSeconds(roamDistance);

                //change direction
                walkingDirection = "left";
                yield break;
            }

            //walk left for specified seconds
            if ((walkingDirection == "left") || (walkingDirection == "Left"))
            {
                transform.Translate(Vector2.left * passiveSpeed * Time.deltaTime);

                //wait
                yield return new WaitForSeconds(roamDistance);

                //change direction
                walkingDirection = "right";
                yield break;
            }
            */

            //Walk right for specified seconds
            if ((walkingDirection == "up") || (walkingDirection == "Up"))
            {
                transform.Translate(Vector2.up * passiveSpeed * Time.deltaTime);

                //wait
                yield return new WaitForSeconds(roamDistance);

                //change direction
                walkingDirection = "down";
                yield break;
            }

            //Walk right for specified seconds
            if ((walkingDirection == "down") || (walkingDirection == "Down"))
            {
                transform.Translate(Vector2.down * passiveSpeed * Time.deltaTime);

                //wait
                yield return new WaitForSeconds(roamDistance);

                //change direction
                walkingDirection = "up";
                yield break;
            }

            //pause
            yield return new WaitForSeconds(1);
        }
    }

    //Agressive enemy behaviour triggered after enemy notices player
    IEnumerator AggressiveBehaviour()
    {
        //pause
        yield return new WaitForSeconds(1);

        //walk towards target position at specified speed and stop if distance between is greater than certain amount
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, aggressiveSpeed * Time.smoothDeltaTime);
        }
    }
}
