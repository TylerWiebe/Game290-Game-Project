﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_StatOrb : MonoBehaviour
{
    private GameObject menu_Canvas;
    private GameObject statWindow;
    private GameObject player;

    void Start()
    {
        menu_Canvas = GameObject.Find("Menu_Canvas");
        statWindow = menu_Canvas.transform.Find("StatWindow").gameObject;
        player = GameObject.FindWithTag("Player");
    }

    //on collision with stat orb
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if the collision was with the player
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            Script_PauseMenu.gameIsPaused = true;

            //freeze time
            Time.timeScale = 0f;

            //pull up stat screen
            statWindow.SetActive(true);
        }
    }

    //call on button press (stat index determines stat to be leveled)
    public void IncreaseStat(int statIndex)
    {

        player.GetComponent<Alien_Object>().IncreaseStat(statIndex);

        Script_PauseMenu.gameIsPaused = false;

        statWindow.SetActive(false);

        //unfreeze time
        Time.timeScale = 1f;

        //remove stat orb
        DestroyImmediate(this.gameObject, true);
    }
}
