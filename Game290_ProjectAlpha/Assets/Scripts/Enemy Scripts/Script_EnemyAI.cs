using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Controls Enemy behaviour for both passive and alerted phases.  
//Enemy walks back and forth.  If player appears in sight, walk towards and attack player.
//NOTE - Player game object must be tagged "Player"

public class Script_EnemyAI : MonoBehaviour
{
    //control condition in update function allowing only 1 enemy to be added to count
    private bool needToAddToEnemyCount = true;

    //Speed of walking after seeing player
    [SerializeField]
    private int aggressiveSpeed = 7;

    //Randomization of AI passive walk direction
    //dont need this if you are not using the randomization of walking direction
    private int iter = 0;
    private int direction = 0;
    private int walkTime = 100;
    public int walkTime_LowerLimit = 100;
    public int walkTime_UpperLimit = 250;

    private bool first_time = false; //used in aggro mode

    //rotate body when in aggro variables
    Vector3 enemy_position;
    public float angle = 0f;

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
    private float stoppingDistance;

    //Stop player from pushing enemy
    public Rigidbody2D rigidBody;

    //stop enemy when attacking
    public bool canMove = true;
    public bool canRotate = true;

    //script responsible for swapping music from between idle & combat
    private Script_SwapMusic swapMusicScript;

    void Start()
    {
        //assign the object with tag "Player" to be the enemy's target 
        target = GameObject.FindGameObjectWithTag("Player").transform;

        //Get rigidbody
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        rigidBody.bodyType = RigidbodyType2D.Kinematic;

        swapMusicScript = GameObject.Find("Music").GetComponent<Script_SwapMusic>();

        //set stopping distance
        if (this.gameObject.tag == "RangedEnemy")
            stoppingDistance = transform.GetComponentInChildren<Script_Ranged_Enemy_Attack>().stoppingDistance;
        else
            stoppingDistance = transform.GetComponentInChildren<Script_Melee_Enemy_Attack>().stoppingDistance;
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
            if (needToAddToEnemyCount)
            {
                swapMusicScript.alertedEnemiesCount += 1;
                needToAddToEnemyCount = false;
            }
            //change to combat music
            if ((swapMusicScript.isPlayingCombatMusic == false) & (swapMusicScript.alertedEnemiesCount >= 1))
            {
                swapMusicScript.isPlayingCombatMusic = true;
                swapMusicScript.PlayCombatMusic();
            }

            StartCoroutine(AggressiveBehaviour());
            //if (this.gameObject.tag == "RangedEnemy" && first_time == true)
            //{
            //this.gameObject.GetComponentInChildren<Script_Ranged_Enemy_Attack>().set_playerNotSeen(false);
            //    first_time = false;
        }
    }

    //Passive enemy behaviour (When enemy has not yet spotted player)
    IEnumerator PassiveBehaviour()
    {
        
        if (iter == walkTime)
        {
            iter = 0;
            direction = UnityEngine.Random.Range(0, 4);
            walkTime = UnityEngine.Random.Range(walkTime_LowerLimit, walkTime_UpperLimit);
        }
        else
        {
            iter++;
        }

        if (roamDistance > 0 && canMove)
        {
            while (direction == 0)
            {
                //rotate right
                if (rigidBody.rotation < (270 - 10) && rigidBody.rotation > (270 + 10))
                {
                    rigidBody.rotation = 270;
                }

                else if(rigidBody.rotation < 270)
                    rigidBody.rotation += 10f;
                else if(rigidBody.rotation > 270)
                    rigidBody.rotation += -10f;

                //move right
                rigidBody.velocity = Vector2.right * passiveSpeed;
                
                //wait
                yield return new WaitForSeconds(roamDistance);
                yield break;
            }
            while (direction == 1)
            {
                //rotate left
                if (rigidBody.rotation < (90 + 10) && rigidBody.rotation > (90 - 10))
                {
                    rigidBody.rotation = 90;
                }
                else if (rigidBody.rotation < 90)
                    rigidBody.rotation += 10f;
                else if (rigidBody.rotation > 90)
                    rigidBody.rotation += -10f;

                //move left
                rigidBody.velocity = Vector2.left * passiveSpeed;

                //wait
                yield return new WaitForSeconds(roamDistance);
                yield break;
            }
            while (direction == 2)
            {
                //rotate up
                if (rigidBody.rotation < (0 + 10) && rigidBody.rotation > (0 - 10))
                {
                    rigidBody.rotation = 0;
                }
                else if (rigidBody.rotation < 0)
                    rigidBody.rotation += 10;
                else if (rigidBody.rotation > 0)
                    rigidBody.rotation += -10f;

                //move up
                rigidBody.velocity = Vector2.up * passiveSpeed;
                
                //wait
                yield return new WaitForSeconds(roamDistance);
                yield break;
            }
            while (direction == 3)
            {
                //rotate down
                if (rigidBody.rotation < (180 + 10) && rigidBody.rotation > (180 - 10))
                {
                    rigidBody.rotation = 180;
                }
                else if(rigidBody.rotation < 180)
                    rigidBody.rotation += 10;
                else if (rigidBody.rotation > 180)
                    rigidBody.rotation += -10f;

                //move down
                rigidBody.velocity = Vector2.down * passiveSpeed;

                //wait
                yield return new WaitForSeconds(roamDistance);
                yield break;
            }

            /*
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
            */
            //pause
            yield return new WaitForSeconds(1);
        }
    }

    //Agressive enemy behaviour triggered after enemy notices player
    IEnumerator AggressiveBehaviour()
    {
        calculateAngle();
        rotate_body_aggro();

        //walk towards target position at specified speed and stop if distance between is greater than certain amount
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance && canMove)
        {
            float theta = (angle + 90) * Mathf.Deg2Rad;
            float v1 = (float)(aggressiveSpeed * Math.Cos(theta)); //find x velocity
            float v2 = (float)(aggressiveSpeed * Math.Sin(theta)); //find y velocity
            Vector3 vector = new Vector3(v1, v2, 0f); //create a vector of x and y velocities
            rigidBody.velocity = vector;
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
        }
        //pause
        yield return new WaitForSeconds(1);
    }

    private void calculateAngle()
    {
        enemy_position = target.transform.position;
        enemy_position.x = enemy_position.x - this.gameObject.transform.position.x;
        enemy_position.y = enemy_position.y - this.gameObject.transform.position.y;
        angle = (Mathf.Atan2(enemy_position.y, enemy_position.x) * Mathf.Rad2Deg) - 90;
    }

    //rotates the enemy to face the player whenst in aggro mode
    private void rotate_body_aggro()
    {
        if(canRotate)
            rigidBody.rotation = angle;
    }

    private void destroyEnemy()
    {
        if (this.gameObject.tag == "RangedEnemy")
        {
            this.transform.GetComponentInChildren<Script_Ranged_Enemy_Object>().destroy();
        }
        else
            this.transform.GetComponentInChildren<Script_Melee_Enemy_Object>().destroy();
    }

    //make enemy attack
    private void Attack()
    {
        if (this.gameObject.tag == "RangedEnemy")
        {
            this.transform.GetComponentInChildren<Script_Ranged_Enemy_Attack>().Attack();
        }
        else
            this.transform.GetComponentInChildren<Script_Melee_Enemy_Attack>().Attack();
    }
}