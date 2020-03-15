using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Flame_Thrower_Hit_Box : MonoBehaviour
{
    //controls when the script can attack
    public bool canAttack = false;
    private int count = 0;

    //player game object
    Alien_Object target = null;

    // Start is called before the first frame update
    void Start()
    {
        //set enemy game object variable
        target = GameObject.Find("AlienBody").GetComponentInChildren<Alien_Object>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //attack enemy with the flame thrower if they get in range
    private void OnTriggerStay2D(Collider2D other)
    {
        //if collision with the player
        if (((other.tag == "player") || (other.tag == "Player")) && canAttack)
        {
            //do one point of damage very 0.05 seconds
            if (count == 4)
            {
                attack();
                count = 0;
            }
            else
                count++;
        }
    }

    //deal one point of damage to the player every time called
    private void attack()
    {
        target.Deal_Damage_To_Alien(1);
    }
}
