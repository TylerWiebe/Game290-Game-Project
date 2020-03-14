using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Boss_Object : MonoBehaviour
{
    private int hitPoints = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //damage boss
    public void damageBoss(int damage)
    {
        hitPoints -= damage;
        //call destruction/dying animation
    }

    //called by destruction/dying animation upon completion of animation
    public void destroyBoss()
    {
        Destroy(gameObject);
    }

}
