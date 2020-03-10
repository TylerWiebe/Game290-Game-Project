using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_BossAI : MonoBehaviour
{
    //boss's rigidbody
    private Rigidbody2D rigidBody;

    //calculate the angle that the boss should be facing
    public float theta;

    //player game object
    GameObject target = null;

    //shotgun attack script
    Script_Gun_Attack gun_attack;

    //flame thrower attack script
    Script_Flame_Thrower_Attack flame_thrower_attack;


    // Start is called before the first frame update
    void Start()
    {
        //set rigidbody variable 
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();

        //set enemy game object variable
        target = GameObject.FindGameObjectWithTag("Player");
        
        //set shotgun attack script
        gun_attack = this.gameObject.GetComponentInChildren<Script_Gun_Attack>();
        //set flame thrower attack script
        flame_thrower_attack = this.gameObject.GetComponentInChildren<Script_Flame_Thrower_Attack>();

        //start left shotgun attack
        InvokeRepeating("leftGunAttack", 0f, 5f);

        //start right shotgun attack
        InvokeRepeating("rightGunAttack", 2.5f, 5f);

        //resume flame thrower attack
        InvokeRepeating("resumeFlameThrowerAttack", 0f, 20f);

        //suspend flame thrower attack
        InvokeRepeating("suspendFlameThrowerAttack", 5f, 25f);

    }

    //shoot left shotgun
    private void leftGunAttack()
    {
        gun_attack.shootLeftGun();
    }

    //shoot right shotgun
    private void rightGunAttack()
    {
        gun_attack.shootRightGun();
    }

    //resume flame thrower attack
    private void resumeFlameThrowerAttack()
    {
        flame_thrower_attack.startAttack();
    }

    //suspend flame thrower attack
    private void suspendFlameThrowerAttack()
    {
        flame_thrower_attack.stopAttack();
    }




    // Update is called once per frame
    void Update()
    {
        //rotate boss
        rigidBody.rotation = calculateAngle();
    }

    //calculate the angle of rotation paying respect to the offset shift
    private float calculateAngle()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.x = targetPosition.x - this.gameObject.transform.position.x;
        targetPosition.y = targetPosition.y - this.gameObject.transform.position.y;
        theta = (Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg) - (2 * 90);
        return theta;
    }
    
}
