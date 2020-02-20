using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_ProjectileCharges : MonoBehaviour
{
    //reference to slider
    [SerializeField]
    private Slider slider = null;


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
}
