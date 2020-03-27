using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_ProjectileCharges : MonoBehaviour
{
    //reference to slider
    [SerializeField]
    private Slider slider = null;

    //public float currentRangedCharges;
    //public int maxRangedCharges;
    private int regen = 4;

    private float timer = 0;

    private GameObject player;
    private Alien_Object playerScript;

    void Start()
    {
        player = GameObject.Find("AlienHead");
        playerScript = player.GetComponent<Alien_Object>();
        Debug.Log(playerScript);
        SetCharge(playerScript.get_Current_ranged_charges());
        SetMaxCharge(playerScript.get_num_ranged_charges());
    }

    // Update is called once per frame
    void Update()
    {
        //regen shots
        if ((playerScript.get_Current_ranged_charges()) < (playerScript.get_num_ranged_charges()) && (Script_PauseMenu.gameIsPaused == false))
        {
            //timer
            timer += regen * Time.deltaTime;

            if (timer >= 0.1f)
            {
                playerScript.set_Current_ranged_charges(0.02f);
                SetCharge(playerScript.get_Current_ranged_charges());

                timer = 0;
            }
        }

        //use attack on click
        if ((Input.GetKeyUp(KeyCode.Mouse0)) && (playerScript.get_Current_ranged_charges() >= 1) && (Script_PauseMenu.gameIsPaused == false))
        {
            //decrease total charges
            playerScript.set_Current_ranged_charges(-1);
            SetCharge(playerScript.get_Current_ranged_charges());

            //useSkill
            //Debug.Log("Use Ranged Attack");
        }
    }

    //sets maximumCharges for the chargebar
    public void SetMaxCharge(int maxCharge)
    {
        //set max
        slider.maxValue = maxCharge;
    }

    //sets value of chargeBar through slider
    public void SetCharge(float charge)
    {
        //set current health
        slider.value = charge;
    }

    public float getRangedCharges()
    {
        player = GameObject.Find("AlienHead");
        playerScript = player.GetComponent<Alien_Object>();
        return playerScript.get_Current_ranged_charges();
    }
}
