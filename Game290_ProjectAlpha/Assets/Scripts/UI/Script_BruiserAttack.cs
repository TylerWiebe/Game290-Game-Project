﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_BruiserAttack : MonoBehaviour
{
    [SerializeField]
    private Image cooldownOverlay = null;

    [SerializeField]
    private Text text = null;

    [SerializeField]
    private float cooldown = 1f;
    private float timeLeft;
    private bool onCooldown = false;

    void Start()
    {
        timeLeft = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //use skill on spacebar press
        if (Input.GetKeyUp(KeyCode.Mouse0) && (onCooldown == false) && (Script_PauseMenu.gameIsPaused == false))
        {
            onCooldown = true;

            //use attack
        }

        //run skill cooldown timer
        if (onCooldown == true && (Script_PauseMenu.gameIsPaused == false))
        {
            //increase fill amount by 1 every second
            cooldownOverlay.fillAmount += 1 / cooldown * Time.deltaTime;

            //decrease count by 1 every seconds
            timeLeft -= Time.deltaTime;
            //show first digit of float
            text.text = timeLeft.ToString("f1");

            //turn off cooldown once fill is over 1
            if (cooldownOverlay.fillAmount >= 1)
            {
                cooldownOverlay.fillAmount = 0;

                //reset the text
                text.text = "";
                timeLeft = cooldown;

                onCooldown = false;
            }
        }
    }
}