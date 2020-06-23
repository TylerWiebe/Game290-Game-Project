using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Globalization;
using System.Diagnostics;

public class Alien_Object : MonoBehaviour
{
    //class of alien when he dies
    public static int alienFormDuringDeath = 0;

    // a boolean to state whether the alien is alive or dead.
    private bool AlienAlive = true;

    //hold reference to object holding Script_SceneTransition
    private GameObject sceneTransitionManager;
    private GameObject gameManager;
    private GameObject cutSceneManager;

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
    public static float Max_Health = 300;// actual maximum health`  1
    private static float HEALTH_SCALE_CONST = 300; //this is the health constant between the three classes, the max health will be scaled off of this value
    private static float Current_Health_Percentage = 1; //this is the amount of health the player has left in percentage
    public static float Current_Health = 300; //This is the amount of health, numeric value

    //Damage Stats
    private static int damage = 40;
    public static int num_ranged_charges = 25;
    public static float current_ranged_charges = 4;

    public static float ranged_charges_regen = 0.15f;

    //regen rate multiplier(scales with points put in "ranged_charges_regen")
    private static float charges = 1f;

    //Variable Stats
    private static int strength = 0;
    private static int vitality = 0;

    // Class status Variables
    //an array to keep the order of the morph
    // 0 = assassin
    // 1 = bruiser
    // 2 = sniper
    private static int[] Class_Order = new int[3];
    private static int Current_Class = 0;

    //control variables
    private bool alienCanMove = true;
    private bool doMouseRotate = true;
    
    //Camera Settings
    public int cameraSize;


    //rigidbody for movement
    public Rigidbody2D rigidBodyBody;
    private static bool gameJustStarted = true;

    //Animations
    public Animator animHead;
    public Animator animBody;
    private bool isNotMorphing = true;

    // Start is called before the first frame update
    public Text myText1;
    public Text myText2;
    public Text myText3;
    public Text myText4;

    public bool AlienCanMove { get => alienCanMove; set => alienCanMove = value; }

    void Start()
    {

        if (gameJustStarted)
        {
            resetAlien();
            animHead.SetInteger("IsRanged", 1);
            UnityEngine.Debug.Log("reset alien");
            gameJustStarted = false;
        }

        UnityEngine.Debug.Log(vitality);
        //Finding the desired GameObjects
        gameManager = GameObject.Find("GameManager");
        sceneTransitionManager = GameObject.Find("SceneTransitionManager");
        cutSceneManager = GameObject.Find("CutSceneManager");
        playerUICanvas = GameObject.Find("PlayerUI_Canvas");
        healthBar = playerUICanvas.transform.Find("HealthBar").gameObject;
        chargeBar = playerUICanvas.transform.Find("ProjectileCharges").gameObject;
        morphQueue = playerUICanvas.transform.Find("MorphQueue").gameObject;
        assassinAttackBox = playerUICanvas.transform.Find("assassinAttackBox").gameObject;
        bruiserAttackBox = playerUICanvas.transform.Find("bruiserAttackBox").gameObject;

        AlienHead = GameObject.Find("AlienHead"); //Need this to get alien object's sprite renderer
        alienBody = GameObject.Find("AlienBody"); //Need this to get alien object's sprite renderer
        myCamera = GameObject.Find("MainCamera");

        rigidBodyBody = alienBody.GetComponent<Rigidbody2D>();

        animHead = AlienHead.GetComponent<Animator>();
        animBody = alienBody.GetComponent<Animator>();


        AlienCanMove = true;

        updateAlienStats();

        if (Current_Class == 1)
        {
            morphQueue.GetComponent<Script_Morph_UI>().MorphLeft();

        }
        if (Current_Class == 0)
        {
            morphQueue.GetComponent<Script_Morph_UI>().MorphRight();

        }
    }


    // Update is called once per frame
    void Update()
    {
        Camera myCam = GameObject.Find("MainCamera").GetComponent<Camera>();
        myCam.orthographicSize = cameraSize;
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
        if (AlienCanMove)
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
        if (isNotMorphing)
        {
            isNotMorphing = false;

            int temp = Class_Order[2];
            Class_Order[2] = Class_Order[1];
            Class_Order[1] = Class_Order[0];
            Class_Order[0] = temp;

            Current_Class = Class_Order[1];

            morphQueue.GetComponent<Script_Morph_UI>().MorphRight();
            updateAlienStats();
            morph_animation();

        }
    }

    /// <summary>
    /// Rotates the morph queue to the left
    /// Calls the morph_animation function
    /// Updates the current_class
    /// </summary>
    private void morph_left()
    {
        if (isNotMorphing)
        {
            isNotMorphing = false;
            int temp = Class_Order[0];
            Class_Order[0] = Class_Order[1];
            Class_Order[1] = Class_Order[2];
            Class_Order[2] = temp;

            Current_Class = Class_Order[1];


            morphQueue.GetComponent<Script_Morph_UI>().MorphLeft();
            updateAlienStats();
            morph_animation();
        }
       
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

    public void setIsNotMorphingTrue()
    {
        isNotMorphing = true;
    }

    private void updateAlienStats()
    {
        //If the alien is revived then we reset the alien stats
        if (cutSceneManager.GetComponent<Script_CutSceneManager>().getIsLevel1())
        {
            resetAlien();
            animHead.SetInteger("IsRanged", 1);
            UnityEngine.Debug.Log("reset alien");
            cutSceneManager.GetComponent<Script_CutSceneManager>().setIsLevel1False();
        }

        //Debug.Log(Current_Health_Percentage);
        //assassin
        if (Current_Class == 0)
        {
            Max_Health = (int)Math.Round(HEALTH_SCALE_CONST * (vitality + 1) * 0.5);
            Current_Health = (int)(Max_Health * (Current_Health_Percentage));
            speed = 0.075f * 2f; //0.075

            int maxHP = (int) Max_Health;
            int curHP = (int) Current_Health;

            //update healthbar Max Health & current health
            healthBar.GetComponent<Script_HealthBar>().SetMaxHealth(maxHP);
            healthBar.GetComponent<Script_HealthBar>().SetHealth(curHP);

            //animHead.SetInteger("IsRanged", 0);

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
            Current_Health = (int)(Max_Health * (Current_Health_Percentage));
            speed = 0.025f * 2f; //0.025

            int maxHP = (int)Max_Health;
            int curHP = (int)Current_Health;

            //update healthbar Max Health & current health
            healthBar.GetComponent<Script_HealthBar>().SetMaxHealth(maxHP);
            healthBar.GetComponent<Script_HealthBar>().SetHealth(curHP);


            //animHead.SetInteger("IsRanged", 0);

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
            Max_Health = (float)Math.Round(HEALTH_SCALE_CONST * (vitality + 1.0));
            Current_Health = (float)(Max_Health * (Current_Health_Percentage));
            speed = 0.050f * 2f;

            //animHead.SetBool("isRanged", true);
            //animHead.SetInteger("IsRanged", 1);

            int maxHP = (int)Max_Health;
            int curHP = (int)Current_Health;

            //update healthbar Max Health & current health
            healthBar.GetComponent<Script_HealthBar>().SetMaxHealth(maxHP);
            healthBar.GetComponent<Script_HealthBar>().SetHealth(curHP);

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


        //vitality text updates
        //Text myText1 = GameObject.Find("statup1").GetComponent<Text>();
        myText1.text = "Current Vitality: " + vitality.ToString();

        //Strength Text Updates
        //Text myText2 = GameObject.Find("statup2").GetComponent<Text>();
        myText2.text = "Current Strength: " + strength.ToString();

        //Num. Charges Text Updates
        //Text myText3 = GameObject.Find("statup3").GetComponent<Text>();
        myText3.text = "Charges Increased: " + (num_ranged_charges - 4).ToString();

        //Max. Charge stored Text Update
        //Text myText4 = GameObject.Find("statup4").GetComponent<Text>();
        myText4.text = "Increase Charge: " + charges.ToString();
    }
    private void RangedStartSetFalse()
    {
        animHead.SetBool("GameStartedAsRanged", false);
    }

    /// <summary>
    /// On alien death, call coroutine: Plays fade out animation and load DeathScreen scene
    /// </summary>
    IEnumerator Alien_Died()
    {
        //Debug.Log("current health: " + Current_Health.ToString());
        animHead.SetInteger("IsRanged", 0);
        animBody.SetBool("hasDied", true);
        animBody.SetBool("morph", true);

        

        //play fade out animation
        sceneTransitionManager.GetComponent<Script_SceneTransition>().TransitionCall(1);

        //store the form of the alien when he dies
        alienFormDuringDeath = getCurrentClass();

        //wait for animation(1 second)
        yield return new WaitForSeconds(5);

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
                UnityEngine.Debug.Log("Vitality Up");
                break;

            //increase strength
            case 1:
                strength += 1;

                UnityEngine.Debug.Log("Strength Up");
                break;

            //increase number of ranged charges
            case 2:
                num_ranged_charges += 1;
                UnityEngine.Debug.Log("Ranged Charges Up");
                break;

            //increase charge regen rate
            case 3:
                charges += 0.1f;
                //update variable in Script_ProjectileCharges
                Script_ProjectileCharges.regen += (0.15f * charges);

                //update local copy
                ranged_charges_regen += (0.15f * charges);


                UnityEngine.Debug.Log("Charge Size Up");
                break;

            default:
                UnityEngine.Debug.Log("Invalid StatIndex on stat pickup");
                break;
        }
        updateAlienStats();
    }


    public void Deal_Damage_To_Alien(int damage)
    {

        float FDamage = (float)damage;

        Current_Health -= FDamage;

        Current_Health_Percentage = (Current_Health / Max_Health);

        int curHP = (int)Current_Health;

        healthBar.GetComponent<Script_HealthBar>().SetHealth(curHP);
        //play the correct audio clip for death and taking damage which depends upon which alien the user is currently
        if (Current_Class == 0)
        {

            if (Current_Health <= 0 && AlienAlive)
            {
                transform.parent.GetComponent<SFX_Controller>().AssassinDied();
                AlienAlive = false;
            }
            else
            {
                transform.parent.GetComponent<SFX_Controller>().AssassinHurt();
            }
        }
        else if (Current_Class == 1)
        {
            if (Current_Health <= 0 && AlienAlive)
            {
                transform.parent.GetComponent<SFX_Controller>().BruiserDied();
                AlienAlive = false;
            }
            else
            {
                transform.parent.GetComponent<SFX_Controller>().BruiserHurt();
            }
        }
        else if (Current_Class == 2)
        {
            if (Current_Health <= 0 && AlienAlive)
            {
                transform.parent.GetComponent<SFX_Controller>().SniperDied();
                AlienAlive = false;
            }
            else
            {
                transform.parent.GetComponent<SFX_Controller>().SniperHurt();
            }
        }

    }

    public void attack()
    {
        if (Current_Class == 2)
        {
            animHead.SetBool("Is_attacking", true);
            transform.parent.GetComponent<SFX_Controller>().SniperAttack();        }
        animBody.SetBool("isAttacking", true);

    }
    
    public void finishAttack()
    {
        animHead.SetBool("Is_attacking", false);
    }

    public int getCurrentClass()
    {
        return Current_Class;
    }
    public void setCanMove(bool temp)
    {
        AlienCanMove = temp;
    }

    /// <summary>
    /// On mouse1 down do some attack sequence
    /// </summary>

    public void setDoMouseRotation(bool temp)
    {
        doMouseRotate = temp;
    }

    public int getDamage()
    {
        return damage + ((strength) * 10);
    }

    public bool isNotDead()
    {
        return AlienAlive;
    }

    public float getBodyAngle()
    {
        return BodyAngle;
    }

    public void setHPPercentage()
    {
        Current_Health_Percentage = Current_Health / Max_Health;
    }
    
    public float get_Current_ranged_charges()
    {
        return current_ranged_charges;
    }
    public void set_Current_ranged_charges(float x)
    {
        current_ranged_charges += x;
    }

    public int get_num_ranged_charges()
    {
        return num_ranged_charges;
    }

    public float get_max_health()
    {
        return Max_Health;
    }

    public float get_current_health()
    {
        return Current_Health;
    }
    public void set_current_health(float x)
    {
        Current_Health += x;
    }


    public void resetAlien()
    {
        //ALIEN STATS RESET
        //Health Stats
        Max_Health = 300;
        HEALTH_SCALE_CONST = 300; 
        Current_Health_Percentage = 1; 
        Current_Health = 300;

        UnityEngine.
        Debug.Log("Reset Alien Called");
        
        //Damage Stats
        damage = 10;
        num_ranged_charges = 4;
        current_ranged_charges = 4;

        ranged_charges_regen = 4;

        //Variable Stats
        strength = 0;
        vitality = 0;

        //Class reordering
        Class_Order = new int[] { 0, 2, 1 };
        Current_Class = 2;

        gameObject.GetComponent<Script_SpawnStatOrb>().resetOrbsDropped();
    }
}
