using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerDeath : MonoBehaviour
{
    //Called on player Death by Alien_Object
    public void DeathScreen()
    {
        //Bring player to end screen
        Debug.Log("You have Died");
    }
}
