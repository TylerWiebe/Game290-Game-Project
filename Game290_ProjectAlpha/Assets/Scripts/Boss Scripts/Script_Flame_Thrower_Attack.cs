using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Flame_Thrower_Attack : MonoBehaviour
{
    //controls when the script can attack
    private bool canAttack = false;

    //player game object
    Alien_Object target = null;


    // Start is called before the first frame update
    void Start()
    {
        //set enemy game object variable
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Alien_Object>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAttack()
    {
        //use flame thrower
        canAttack = true;
    }

    public void stopAttack()
    {
        //no use flame thrower
        canAttack = false;
    }

    //attack enemy with the flame thrower if they get in range
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if collision with the player
        if (((other.tag == "player") || (other.tag == "Player")) && canAttack)
        {
            //do one point of damage very 0.05 seconds
            InvokeRepeating("attack", 0f, 0.05f);
        }
    }

    //trigger when player leaves enemy range (stop attacking)
    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.tag == "player") || (other.tag == "Player") && canAttack)
        {
            CancelInvoke();
        }
    }

    //deal one point of damage to the player every time called
    private void attack()
    {
        target.Deal_Damage_To_Alien(1);
    }

}
