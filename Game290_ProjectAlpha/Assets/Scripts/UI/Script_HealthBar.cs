using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_HealthBar : MonoBehaviour
{
    //reference to slider
    [SerializeField]
    private Slider slider = null;


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
