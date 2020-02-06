using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Melee_Enemy_Object : MonoBehaviour
{
    private int attack_damage;
    private int hit_points;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void set_attack_damage(int attack_damage)
    {
        this.attack_damage = attack_damage;
    }

    public void set_hit_points(int hit_points)
    {
        this.hit_points = hit_points;
    }
}
