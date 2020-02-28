using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ranged_Enemy_Object : MonoBehaviour
{
    private int attack_damage;
    private int hit_points;

    private GameObject enemy_body;
    private GameObject enemy_attack_cone;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int get_attack_damage()
    {
        return attack_damage;
    }

    public int get_hit_points()
    {
        return hit_points;
    }


    public void set_attack_damage(int attack_damage)
    {
        this.attack_damage = attack_damage;
    }

    public void set_hit_points(int hit_points)
    {
        this.hit_points = hit_points;
    }

    public void attacked(int damage)
    {
        hit_points -= damage;
        if (hit_points <= 0)
        {
            //son be deded
            Script_Enemy_Controller script_enemy_controller = GameObject.Find("GameManager").GetComponent<Script_Enemy_Controller>();
            script_enemy_controller.destroy_enemy(this.gameObject);
        }
    }

}
