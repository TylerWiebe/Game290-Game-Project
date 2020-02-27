using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Melee_Enemy_Object : MonoBehaviour
{
    public int hit_points;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void set_hit_points(int hit_points)
    {
        this.hit_points = hit_points;
    }

    public int get_hit_points()
    {
        return hit_points;
    }

    public void attacked(int damage)
    {
        Debug.Log("previous health"+ (damage).ToString());
        hit_points -= damage;
        Debug.Log("next health" + (hit_points).ToString());
        if (hit_points <= 0)
        {
            //son be deded
            Script_Enemy_Controller script_enemy_controller = GameObject.Find("GameManager").GetComponent<Script_Enemy_Controller>();
            script_enemy_controller.destroy_enemy(this.gameObject);
        }
    }
}
