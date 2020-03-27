using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_HealthBar : MonoBehaviour
{
    //reference to slider
    [SerializeField]
    private Slider slider = null;

    //multiplier for time to regen health
    private int regen = 4;
    private float timer = 0;

    private GameObject player;
    public Alien_Object playerScript;

    void Start()
    {
        player = GameObject.Find("AlienHead");
        playerScript = player.GetComponent<Alien_Object>();

        int maxHP = (int)playerScript.get_max_health();
        int currHP = (int)playerScript.get_current_health();

        SetMaxHealth(maxHP);
        SetHealth(currHP);
    }

    void Update()
    {
        //regen health
        if ((playerScript.get_current_health()) < (playerScript.get_max_health()) && (Script_PauseMenu.gameIsPaused == false))
        {
            int maxHP = (int)playerScript.get_max_health();
            int currHP = (int)playerScript.get_current_health();

            SetMaxHealth(maxHP);
            SetHealth(currHP);

            //timer
            timer += regen * Time.deltaTime;

            //int currHP = (int) playerScript.Current_Health;
            if (timer >= 2)
            {

                playerScript.set_current_health(1);
                playerScript.setHPPercentage();
                SetHealth(currHP);

                timer = 0;
            }
        }
    }
    //sets maximumHealth for the Healthbar
    public void SetMaxHealth(int maxHealth)
    {
        //set max
        slider.maxValue = maxHealth;
    }



    //sets value of healthbar through slider
    public void SetHealth(int health)
    {
        //set current health
        slider.value = health;
    }
}
