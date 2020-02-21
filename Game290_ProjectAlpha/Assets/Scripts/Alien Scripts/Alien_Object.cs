using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Alien_Object : MonoBehaviour
{
    //hold reference to object holding Script_SceneTransition
    [SerializeField]
    private GameObject sceneTransitionManager = null;

    //HealthBar
    [SerializeField]
    private GameObject healthBar = null;

    //Projectile charges
    [SerializeField]
    private GameObject chargeBar = null;

    //morpheQueue
    [SerializeField]
    private GameObject morphQueue = null;

    //Projectile charges
    [SerializeField]
    private GameObject assassinAttackBox = null;

    //Projectile charges
    [SerializeField]
    private GameObject bruiserAttackBox = null;

    //Alien Sprite
    public Sprite alien_sprite;

    //Alien Movement variables
    Vector3 mouse_position = new Vector3();
    Vector3 alien_sprite_position = new Vector3();
    float HeadAngle = 0f;
    float BodyAngle = 0f;

    //Attached objects
    private GameObject AlienHead;
    private GameObject alienBody;
    private GameObject myCamera;
    public GameObject meleeAttack;
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
    public int current_ranged_charges = 4;

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



    // Start is called before the first frame update
    void Start()
    {
        //Finding the desired GameObjects
        AlienHead = GameObject.Find("AlienHead"); //Need this to get alien object's sprite renderer
        alienBody = GameObject.Find("AlienBody"); //Need this to get alien object's sprite renderer
        myCamera = GameObject.Find("Main Camera");

        //Initializing Character
        System.Random rand = new System.Random();
        switch (rand.Next(0,6))
        {
            case 0:
                Class_Order = new int[] { 0, 1, 2 };
                Current_Class = 1;
                morphQueue.GetComponent<Script_MorphUI>().SetQueue(0);
                updateAlienStats();
                break;
            case 1:
                Class_Order = new int[] { 0, 2, 1 };
                Current_Class = 2;
                morphQueue.GetComponent<Script_MorphUI>().SetQueue(1);
                updateAlienStats();
                break;
            case 2:
                Class_Order = new int[] { 1, 0, 2 };
                Current_Class = 0;
                morphQueue.GetComponent<Script_MorphUI>().SetQueue(2);
                updateAlienStats();
                break;
            case 3:
                Class_Order = new int[] { 1, 2, 0 };
                Current_Class = 2;
                morphQueue.GetComponent<Script_MorphUI>().SetQueue(3);
                updateAlienStats();
                break;
            case 4:
                Class_Order = new int[] { 2, 1, 0 };
                Current_Class = 1;
                morphQueue.GetComponent<Script_MorphUI>().SetQueue(4);
                updateAlienStats();
                break;
            default:
                Class_Order = new int[] { 2, 0, 1 };
                Current_Class = 0;
                morphQueue.GetComponent<Script_MorphUI>().SetQueue(5);
                updateAlienStats();
                break;
        }
        meleeAttack.GetComponent<MeleeAttack>().setAttackForm(Current_Class);
    }

    // Update is called once per frame
    void Update()
    {

        if((Current_Health <= 0) && (playerAlive))
        {
            playerAlive = false;
            //Debug.Log("AlienDied");
            StartCoroutine(Alien_Died());
        }
        if((Input.GetKeyUp("q")) && (playerAlive))
        {
           //Debug.Log("MorphLeft");
            morph_left();
        }
        if ((Input.GetKeyUp("e")) && (playerAlive))
        {
            //Debug.Log("MorphRight");
            morph_right();
        }

        //if (Input.GetKeyUp(KeyCode.Mouse0))
        //{
        //   Debug.Log("AlienAttack");
        //    attack();
        //}
        if (playerAlive)
        {
            moveAlien();
        }
    }

    public float getBodyAngle()
    {
        return BodyAngle;
    }

    /// <summary>
    /// Movement and rotation of the alien body and head
    /// </summary>
    private void moveAlien()
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

        mouse_position.x = mouse_position.x - alien_sprite_position.x;
        mouse_position.y = mouse_position.y - alien_sprite_position.y;

        AlienHead.transform.position = new Vector3(nextX, nextY, 0);
        alienBody.transform.position = new Vector3(nextX, nextY, 0);
        myCamera.transform.position = new Vector3(nextX, nextY, -10);

        //Call the walking animation
        walking_Anim(horizontal, vertical);

        BodyAngle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
        alienBody.transform.rotation = Quaternion.Euler(0, 0, BodyAngle);
        HeadAngle = Mathf.Atan2(mouse_position.y, mouse_position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, HeadAngle);

        //meleeAttack.transform.position = new Vector3(nextX+deltaX, nextY+deltaY, 0);

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

        //Call the morph Animation Change
        int prev_class = Current_Class;
        Current_Class = Class_Order[1];
        morph_animation(prev_class, Current_Class);

        morphQueue.GetComponent<Script_MorphUI>().MorphRight();
        updateAlienStats();
    }
    private void morph_left()
    {
        int temp = Class_Order[0];
        Class_Order[0] = Class_Order[1];
        Class_Order[1] = Class_Order[2];
        Class_Order[2] = temp;

        //call the morph animation change
        int prev_class = Current_Class;
        Current_Class = Class_Order[1];
        morph_animation(prev_class, Current_Class);

        morphQueue.GetComponent<Script_MorphUI>().MorphLeft();
        updateAlienStats();
    }

    /// <summary>
    /// This function is used to do a morph animation from one class into another
    /// </summary>
    /// <param name="prev_class">some value between 0-2 which is the previous class</param>
    /// <param name="current_class">some value between 0-2 which is the current class</param>
    private void morph_animation(int prev_class, int current_class)
    {

    }

    /// <summary>
    /// This function will control which walking animations will be running
    /// </summary>
    /// <param name="Horizontal">a value between -1 and 1 that determines direction of horizontal movement</param>
    /// <param name="Vertical">a value between -1 and 1 that determines direction of vertical movement</param>
    private void walking_Anim(float Horizontal, float Vertical)
    {

    }

    private void updateAlienStats()
    {
        //assassin
        if (Current_Class == 0)
        {
            Max_Health = (int) Math.Round(HEALTH_SCALE_CONST * (vitality + 1) * 0.5);
            Current_Health = (int) (Max_Health * (Current_Health_Percentage * 0.01));
            speed = 0.075f;

            //update healthbar Max Health & current health
            healthBar.GetComponent<Script_HealthBar>().SetMaxHealth(Max_Health);
            healthBar.GetComponent<Script_HealthBar>().SetHealth(Current_Health);

            //turn off charge bar
            chargeBar.SetActive(false);
            //turn off skill box
            bruiserAttackBox.SetActive(false);

            //turn on skill box
            assassinAttackBox.SetActive(true);

            //add skill

        }
        //bruiser
        else if (Current_Class == 1)
        {
            Max_Health = (int)Math.Round(HEALTH_SCALE_CONST * (vitality + 1) * 2.0);
            Current_Health = (int)(Max_Health * (Current_Health_Percentage * 0.01));
            speed = 0.025f;

            //update healthbar Max Health & current health
            healthBar.GetComponent<Script_HealthBar>().SetMaxHealth(Max_Health);
            healthBar.GetComponent<Script_HealthBar>().SetHealth(Current_Health);

            //turn off charge bar
            chargeBar.SetActive(false);
            //turn off skill box
            assassinAttackBox.SetActive(false);

            //turn on skill box
            bruiserAttackBox.SetActive(true);

            //add skill
        }
        //ranged
        else
        {
            Max_Health = (int)Math.Round(HEALTH_SCALE_CONST * (vitality + 1.0));
            Current_Health = (int)(Max_Health * (Current_Health_Percentage * 0.01));
            speed = 0.05f;

            //update healthbar Max Health & current health
            healthBar.GetComponent<Script_HealthBar>().SetMaxHealth(Max_Health);
            healthBar.GetComponent<Script_HealthBar>().SetHealth(Current_Health);

            //turn off skill box
            assassinAttackBox.SetActive(false);
            bruiserAttackBox.SetActive(false);

            //add projectile charge bar
            chargeBar.SetActive(true);
            chargeBar.GetComponent<Script_ProjectileCharges>().SetMaxCharge(num_ranged_charges);
            chargeBar.GetComponent<Script_ProjectileCharges>().SetCharge(current_ranged_charges);

            //add skill
        }
        meleeAttack.GetComponent<MeleeAttack>().setAttackForm(Current_Class);
        //Debug.Log(Current_Health);
    }

    /// <summary>
    /// On alien death, call coroutine: Plays fade out animation and load DeathScreen scene
    /// </summary>
    IEnumerator Alien_Died()
    {
        //play fade out animation
        sceneTransitionManager.GetComponent<Script_SceneTransition>().TransitionCall(4);

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

    
    /// <summary>
    /// On mouse1 down do some attack sequence
    /// </summary>
    public int getDamage()
    {
        return damage * ((strength + 1) * 10);
    }

    public void Deal_Damage_To_Alien(int damage)
    {
        //Debug.Log("oof");
        Current_Health -= damage;
        Current_Health_Percentage = Current_Health / Max_Health;

        healthBar.GetComponent<Script_HealthBar>().SetHealth(Current_Health);
    }

}
