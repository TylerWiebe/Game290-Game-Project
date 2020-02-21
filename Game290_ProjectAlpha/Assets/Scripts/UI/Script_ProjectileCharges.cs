using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_ProjectileCharges : MonoBehaviour
{
    //reference to slider
    [SerializeField]
    private Slider slider = null;

    private int currentRangedCharges;
    private int maxRangedCharges;
    private int regen = 4;

    private float timer = 0;

    [SerializeField]
    private GameObject player = null;

    //sets maximumCharges for the Healthbar
    public void SetMaxCharge(int maxCharge)
    {
        //set max
        slider.maxValue = maxCharge;
    }

    //sets value of chargeBar through slider
    public void SetCharge(int charge)
    {
        //set current health
        slider.value = charge;
    }


    void Start()
    {
        currentRangedCharges = player.GetComponent<Alien_Object>().current_ranged_charges;
        maxRangedCharges = player.GetComponent<Alien_Object>().num_ranged_charges;

        SetCharge(currentRangedCharges);
        SetMaxCharge(maxRangedCharges);
    }

    // Update is called once per frame
    void Update()
    {
        //regen shots
        if (currentRangedCharges < maxRangedCharges)
        {
            //timer
            timer += regen * Time.deltaTime;

            if (timer >= 5)
            {
                currentRangedCharges += 1;
                SetCharge(currentRangedCharges);

                timer = 0;
            }
        }

        //use attack on click
        if (Input.GetKeyUp(KeyCode.Mouse0) && (currentRangedCharges >= 1))
        {
            //decrease total charges
            currentRangedCharges -= 1;
            SetCharge(currentRangedCharges);

            //useSkill
            Debug.Log("Use Ranged Attack");
        }
    }
}
