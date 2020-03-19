using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Alien_Object : MonoBehaviour
{
    //hold reference to object holding Script_SceneTransition
    private GameObject sceneTransitionManager;
    private GameObject gameManager;

    //PlayerUI_Canvas
    private GameObject playerUICanvas;

    //HealthBar
    private GameObject healthBar;

    //Projectile charges
    private GameObject chargeBar;

    //morpheQueue
    private GameObject morphQueue;

    //attack cooldownBox for assassin
    private GameObject assassinAttackBox;

    //Projectile charges
    private GameObject bruiserAttackBox;

    //Alien Sprite
    public Sprite alien_spriteq;

    //Alien Movement variables
    Vector3 mouse_position = new Vector3();
    Vector3 alien_sprite_position = new Vector3();
    float HeadAngle = 0f;
    float BodyAngle = 0f;

    //Attached objects
    private GameObject AlienHead;
    private GameObject alienBody;
    private GameObject myCamera;
    public float speed;

    public bool playerAlive = true;
    //ALIEN STATS
    //Health Stats
    public int Max_Health = 100;// actual maximum health
    private int HEALTH_SCALE_CONST = 100; //this is the health constant between the three classes, the max health will be scaled off of this value
    private int Current_Health_Percentage = 100; //this is the amount of health the player has left in percentage
    public int Current_Health = 100; //This is the amount of health, numeric value

    //Damage Stats
    private int damage = 10;
    public int num_ranged_charges = 4;
    public float current_ranged_charges = 4;

    private int charge_size = 25;

    //Variable Stats
    private int strength = 0;
    private int vitality = 0;

    // Class status Variables
    //an array to keep the order of the morph
    // 0 = assassin
    // 1 = bruiser
    // 2 = sniper
    private int[] Class_Order = new int[3];
    private int Current_Class = 0;

    //control variables
    private bool alienCanMove = true;
    private bool doMouseRotate = true;


    //rigidbody for movement
    public Rigidbody2D rigidBodyBody;

    //Animations
    public Animator animHead;
    public Animator animBody;
    // Start is called before the first frame update
    void Start()
    {
        //Finding the desired GameObjects
        gameManager = GameObject.Find("GameManager");
        sceneTransitionManager = GameObject.Find("SceneTransitionManager");
        playerUICanvas = GameObject.Find("PlayerUI_Canvas");
        healthBar = playerUICanvas.transform.Find("HealthBar").gameObject;
        chargeBar = playerUICanvas.transform.Find("ProjectileCharges").gameObject;
        morphQueue = playerUICanvas.transform.Find("MorphQueue").gameObject;
        assassinAttackBox = playerUICanvas.transform.Find("assassinAttackBox").gameObject;
        bruiserAttackBox = playerUICanvas.transform.Find("bruiserAttackBox").gameObject;

        AlienHead = GameObject.Find("AlienHead"); //Need this to get alien object's sprite renderer
        alienBody = GameObject.Find("AlienBody"); //Need this to get alien object's sprite renderer
        myCamera = GameObject.Find("Main Camera");

        rigidBodyBody = alienBody.GetComponent<Rigidbody2D>();

        animHead = AlienHead.GetComponent<Animator>();
        animBody = alienBody.GetComponent<Animator>();

        Class_Order = new int[] { 0, 2, 1 };
        Current_Class = 2;
        //morphQueue.GetComponent<Script_Morph_UI>().SetQueue(1);
        animHead.SetInteger("IsRanged", 1);
        updateAlienStats();

        //Initializing Character
        /*
        System.Random rand = new System.Random();
        switch (rand.Next(0, 6))
        {
            case 0:
                Class_Order = new int[] { 0, 1, 2 };
                Current_Class = 1;
                morphQueue.GetComponent<Script_Morph_UI>().SetQueue(0);
                animHead.SetInteger("IsRanged", 0);
                updateAlienStats();
                break;
            case 1:
                Class_Order = new int[] { 0, 2, 1 };
                Current_Class = 2;
                morphQueue.GetComponent<Script_Morph_UI>().SetQueue(1);
                animHead.SetInteger("IsRanged", 1);
                updateAlienStats();
                break;
            case 2:
                Class_Order = new int[] { 1, 0, 2 };
                Current_Class = 0;
                morphQueue.GetComponent<Script_Morph_UI>().SetQueue(2);
                animHead.SetInteger("IsRanged", 0);
                updateAlienStats();
                break;
            case 3:
                Class_Order = new int[] { 1, 2, 0 };
                Current_Class = 2;
                morphQueue.GetComponent<Script_Morph_UI>().SetQueue(3);
                animHead.SetInteger("IsRanged", 1);
                updateAlienStats();
                break;
            case 4:
                Class_Order = new int[] { 2, 1, 0 };
                Current_Class = 1;
                morphQueue.GetComponent<Script_Morph_UI>().SetQueue(4);
                animHead.SetInteger("IsRanged", 0);
                updateAlienStats();
                break;
            default:
                Class_Order = new int[] { 2, 0, 1 };
                Current_Class = 0;
                morphQueue.GetComponent<Script_Morph_UI>().SetQueue(5);
                animHead.SetInteger("IsRanged", 0);

                updateAlienStats();
                break;
        }
        */
    }


    // Update is called once per frame
    void Update()
    {

        if ((Current_Health <= 0) && (playerAlive))
        {
            playerAlive = false;
            StartCoroutine(Alien_Died());
        }

        if ((Input.GetKeyUp("q")) && (playerAlive))
        {
            morph_left();
        }
        if ((Input.GetKeyUp("e")) && (playerAlive))
        {
            morph_right();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            attack();
        }
    }

    void FixedUpdate()
    {
        if (playerAlive & Script_PauseMenu.gameIsPaused == false)
        {
            moveAlien();
        }
    }

    /// <summary>
    /// Movement and rotation of the alien body and head
    /// </summary>
    private void moveAlien()
    {
        if (alienCanMove)
        {
            //Alien Movement and Aiming
            mouse_position = Input.mousePosition;
            alien_sprite_position = Camera.main.WorldToScreenPoint(AlienHead.transform.position);
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float deltaX = horizontal * speed;
            float deltaY = vertical * speed;
            float nextX = transform.position.x + deltaX;
            float nextY = transform.position.y + deltaY;

            //move alien
            if (!(horizontal == 0 && vertical == 0))
            {
                rigidBodyBody.MovePosition(new Vector2(nextX, nextY));
                myCamera.transform.position = new Vector3(nextX, nextY, -10);
            }
            else
            {
                rigidBodyBody.velocity = Vector2.zero;
                Debug.Log("No input and position = " + transform.position + " and velocity = " + rigidBodyBody.velocity);
            }

            mouse_position.x = mouse_position.x - alien_sprite_position.x;
            mouse_position.y = mouse_position.y - alien_sprite_position.y;


            if (Math.Abs(deltaX) > 0 || Math.Abs(deltaY) > 0)
            {
                BodyAngle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg - 90;
                animBody.SetBool("isWalking", true);
            }
            else
            {
                animBody.SetBool("isWalking", false);
            }

            //rotate body

           
            alienBody.transform.rotation = Quaternion.Euler(0, 0, BodyAngle);
            HeadAngle = Mathf.Atan2(mouse_position.y, mouse_position.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(0, 0, HeadAngle);
            //meleeAttack.transform.position = new Vector3(nextX+deltaX, nextY+deltaY, 0);

        }
    }

    /// <summary>
    /// Rotates the morph queue to the right
    /// Calls the morph_animation function
    /// Updates the current_class
    /// </summary>
    private void morph_right()
    {
        int temp = Class_Order[2];
        Class_Order[2] = Class_Order[1];
        Class_Order[1] = Class_Order[0];
        Class_Order[0] = temp;

        Current_Class = Class_Order[1];

        morphQueue.GetComponent<Script_Morph_UI>().MorphRight();
        updateAlienStats();
        morph_animation();
    }

    private void RangedStartSetFalse()
    {
        animHead.SetBool("GameStartedAsRanged", false);
    }

    private void morph_left()
    {

        int temp = Class_Order[0];
        Class_Order[0] = Class_Order[1];
        Class_Order[1] = Class_Order[2];
        Class_Order[2] = temp;

        Current_Class = Class_Order[1];


        morphQueue.GetComponent<Script_Morph_UI>().MorphLeft();
        updateAlienStats();
        morph_animation();
    }

    /// <summary>
    /// This function is used to do a morph animation from one class into another
    /// </summary>
    /// <param name="prev_class">some value between 0-2 which is the previous class</param>
    /// <param name="current_class">some value between 0-2 which is the current class</param>
    private void morph_animation()
    {
        //set animation constraints such that the animaions play.
        animBody.SetBool("morph", true);
        animBody.SetInteger("CurrentClass", Current_Class);
    }


    private void updateAlienStats()
    {
        //assassin
        if (Current_Class == 0)
        {
            Max_Health = (int)Math.Round(HEALTH_SCALE_CONST * (vitality + 1) * 0.5);
            Current_Health = (int)(Max_Health * (Current_Health_Percentage * 0.01));
            speed = 0.075f * 2f; //0.075

            //update healthbar Max Health & current health
            healthBar.GetComponent<Script_HealthBar>().SetMaxHealth(Max_Health);
            healthBar.GetComponent<Script_HealthBar>().SetHealth(Current_Health);

            animHead.SetInteger("IsRanged", 0);

            //turn off charge bar
            chargeBar.SetActive(false);
            //turn off skill box
            //bruiserAttackBox.SetActive(false);

            //turn on skill box
            //assassinAttackBox.SetActive(true);

            //add skill

        }
        //bruiser
        else if (Current_Class == 1)
        {
            Max_Health = (int)Math.Round(HEALTH_SCALE_CONST * (vitality + 1) * 2.0);
            Current_Health = (int)(Max_Health * (Current_Health_Percentage * 0.01));
            speed = 0.025f * 2f; //0.025

            //update healthbar Max Health & current health
            healthBar.GetComponent<Script_HealthBar>().SetMaxHealth(Max_Health);
            healthBar.GetComponent<Script_HealthBar>().SetHealth(Current_Health);


            animHead.SetInteger("IsRanged", 0);

            //turn off charge bar
            chargeBar.SetActive(false);
            //turn off skill box
            //assassinAttackBox.SetActive(false);

            //turn on skill box
            //bruiserAttackBox.SetActive(true);

            //add skill
        }
        //ranged
        else
        {
            Max_Health = (int)Math.Round(HEALTH_SCALE_CONST * (vitality + 1.0));
            Current_Health = (int)(Max_Health * (Current_Health_Percentage * 0.01));
            speed = 0.050f * 2f;

            animHead.SetBool("isRanged", true);
            animHead.SetInteger("IsRanged", 1);

            //update healthbar Max Health & current health
            healthBar.GetComponent<Script_HealthBar>().SetMaxHealth(Max_Health);
            healthBar.GetComponent<Script_HealthBar>().SetHealth(Current_Health);

            //turn off skill box
            //assassinAttackBox.SetActive(false);
            //bruiserAttackBox.SetActive(false);

            //add projectile charge bar
            chargeBar.SetActive(true);
            chargeBar.GetComponent<Script_ProjectileCharges>().SetMaxCharge(num_ranged_charges);
            chargeBar.GetComponent<Script_ProjectileCharges>().SetCharge(current_ranged_charges);

            //add skill
        }
        this.gameObject.GetComponent<Animator>().SetInteger("CurrentClass", Current_Class);
        //Debug.Log(Current_Health);
    }

    /// <summary>
    /// On alien death, call coroutine: Plays fade out animation and load DeathScreen scene
    /// </summary>
    IEnumerator Alien_Died()
    {
        //Debug.Log("the alien has died");
        //play fade out animation
        sceneTransitionManager.GetComponent<Script_SceneTransition>().TransitionCall(1);

        //wait for animation(1 second)
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("DeathScreen");
    }

    ///
    ///Increase stat values on orb pickup
    ///
    public void IncreaseStat(int statIndex)
    {
        switch (statIndex)
        {
            //increase vitality
            case 0:
                vitality += 1;
                Debug.Log("Vitality Up");
                break;

            //increase strength
            case 1:
                strength += 1;
                Debug.Log("Strength Up");
                break;

            //increase number of ranged charges
            case 2:
                num_ranged_charges += 1;
                Debug.Log("Ranged Charges Up");
                break;

            //increase charge size
            case 3:
                charge_size += 1;
                Debug.Log("Charge Size Up");
                break;

            default:
                Debug.Log("Invalid StatIndex on stat pickup");
                break;
        }
    }

    public int getCurrentClass()
    {
        return Current_Class;
    }
    public void setCanMove(bool temp)
    {
        alienCanMove = temp;
    }

    /// <summary>
    /// On mouse1 down do some attack sequence
    /// </summary>
    public int getDamage()
    {
        return damage * ((strength + 1) * 10);
    }

    public void setDoMouseRotation(bool temp)
    {
        doMouseRotate = temp;
    }

    public void Deal_Damage_To_Alien(int damage)
    {
        //Debug.Log("oof");
        Current_Health -= damage;
        Current_Health_Percentage = Current_Health / Max_Health;

        healthBar.GetComponent<Script_HealthBar>().SetHealth(Current_Health);
    }

    public void attack()
    {
        if (Current_Class == 2)
        {
            animHead.SetBool("Is_attacking", true);
        }
        animBody.SetBool("isAttacking", true);

    }
    public void finishAttack()
    {
        animHead.SetBool("Is_attacking", false);
    }
    public float getBodyAngle()
    {
        return BodyAngle;
    }
}
