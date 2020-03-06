using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_BossAI : MonoBehaviour
{
    //boss's rigidbody
    private Rigidbody2D rigidBody;

    //player game object
    GameObject target = null;

    //shotgun attack script
    Script_Shotgun_Attack shotgun_attack;

    //flame thrower attack script
    Script_Flame_Thrower_Attack flame_thrower_attack;


    //active weapon
    /*
     * Shotgun = 1
     * Flamethrower = 2
     * Laser = 3
     */
    private int weapon = 1;


    // Start is called before the first frame update
    void Start()
    {
        //set rigidbody variable 
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();

        //set enemy game object variable
        target = GameObject.FindGameObjectWithTag("Player");
        
        //check weapon change condition every 2.5 seconds
        InvokeRepeating("chooseWeapon", 0f, 2.5f);

        //set shotgun attack script
        shotgun_attack = this.gameObject.GetComponentInChildren<Script_Shotgun_Attack>();
        //set flame thrower attack script
        flame_thrower_attack = this.gameObject.GetComponentInChildren<Script_Flame_Thrower_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate boss
        rigidBody.rotation = calculateAngle();
    }

    //select which weapon to use based on player's distance from boss
    private void chooseWeapon()
    {
        //flamethrower for close range
        Vector3 targetPosition = target.transform.position;
        Vector3 bossPosition = this.gameObject.transform.position;
        if (Math.Pow(targetPosition.x - bossPosition.x, 2) + Math.Pow(targetPosition.y - bossPosition.y, 2) < 80) //range condition
        {
            weapon = 2;
            shotgun_attack.stopAttack(); //stop attacking with the shotgun
            flame_thrower_attack.startAttack(); //start attacking with the flame thrower
        }
        //shotgun for long range
        else
        {
            weapon = 1;
            flame_thrower_attack.stopAttack();  //stop attacking with the flame thrower
            shotgun_attack.startAttack(); //start attacking with the shotgun
        }

    }

    //calculate the angle of rotation paying respect to the offset shift
    private float calculateAngle()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.x = targetPosition.x - this.gameObject.transform.position.x;
        targetPosition.y = targetPosition.y - this.gameObject.transform.position.y;
        return (Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg) - (weapon * 90);
    }
    
}
