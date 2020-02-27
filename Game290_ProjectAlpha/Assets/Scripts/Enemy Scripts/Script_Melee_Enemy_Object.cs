using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Melee_Enemy_Object : MonoBehaviour
{
    private int attack_damage;
    public float hit_points;

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

    public void set_hit_points(float hit_points)
    {
        this.hit_points = hit_points;
    }

    public void attacked(float damage)
    {
        Debug.Log("previous health"+ (damage).ToString());
        hit_points -= damage;
        Debug.Log("next health" + (hit_points).ToString());
        if (hit_points <= 0)
        {
            //son be deded
            Script_Enemy_Controller script_enemy_controller = GameObject.Find("GameManager").GetComponent<Script_Enemy_Controller>();
            script_enemy_controller.destroy_enemy(this.gameObject);
            Destroy(gameObject);
        }
    }
}
