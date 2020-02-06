using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemy_controller : MonoBehaviour
{
    /*
     * TODO// Prefab Ranged unit
     */

    //melee enemies' base stats
    public const int base_melee_attack_damage = 10;
    public const int base_melee_hit_points = 100;

    //melee enemies' level modifer values
    public const int melee_attack_damage_modifier = 2;
    public const int melee_hit_points_modifier = 5;

    //ranged enemies' base stats
    public const int base_ranged_attack_damage = 20;
    public const int base_ranged_hit_points = 50;

    //ranged enemies' level modifer values
    public const int ranged_attack_damage_modifier = 2;
    public const int ranged_hit_points_modifier = 5;

    private List<GameObject> all_enemies;

    // Start is called before the first frame update
    void Start()
    {
        //query world controller for level sequence number
        int level_sequence_number = 1;
        /*
        //find all melee enemies and put in the list "list_of_melee_enemies"
        List<GameObject> list_of_melee_enemies = find_all_melee_enemies();
        //seed melee enemies' stats based on the level multipler
        seed_melee_enemy_stats(list_of_melee_enemies, level_sequence_number);

        //find all ranged enemies and put in the list "list_of_ranged_enemies"
        List<GameObject> list_of_ranged_enemies = find_all_ranged_enemies();
        //seed ranged enemies' stats based on the level multipler
        seed_melee_enemy_stats(list_of_melee_enemies, level_sequence_number);

        all_enemies = new List<GameObject>(list_of_melee_enemies.Count + list_of_ranged_enemies.Count);
        all_enemies.AddRange(list_of_melee_enemies);
        all_enemies.AddRange(list_of_ranged_enemies);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //find all melee enemies and put in the list "list_of_melee_enemies"
    private List<GameObject> find_all_melee_enemies()
    {
        //find all melee enemies and put in the list "list_of_melee_enemies"
        List<GameObject> list_of_melee_enemies = new List<GameObject>();
        GameObject temp_melee_enemy_object = GameObject.Find("1_prefab_melee_enemy"); //variable that melee sprites will be assigned to
        int count = 0; //used to construct the name of the sprites
        while (temp_melee_enemy_object != null) //run until no more melee enemies can be found
        {
            count++;
            String query = count.ToString() + "_prefab_melee_enemy"; //construct query
            temp_melee_enemy_object = GameObject.Find(query); //query scene for sprite
            list_of_melee_enemies.Add(temp_melee_enemy_object);
        }
        if(list_of_melee_enemies.Count != 0)
            list_of_melee_enemies.RemoveAt(list_of_melee_enemies.Count - 1); //remove the last element, it will ALWAYS be null
        return list_of_melee_enemies;
    }

    //seed melee enemies' stats based on the level multipler
    private void seed_melee_enemy_stats(List<GameObject> list_of_melee_enemies, int level_sequence_number)
    {
        if (list_of_melee_enemies.Count != 0) //if empty no do
        {
            foreach (GameObject item in list_of_melee_enemies) //iterate through all members list_of_melee_enemies
            {
                melee_enemy_object temp_script = item.GetComponent<melee_enemy_object>(); //get melee_enemy_object script
                //set attack damage
                temp_script.set_attack_damage(base_melee_attack_damage + (melee_attack_damage_modifier * level_sequence_number));
                //set hit points
                temp_script.set_hit_points(base_melee_hit_points + (melee_hit_points_modifier * level_sequence_number));
            }
        }
    }

    //find all ranged enemies and put in the list "list_of_ranged_enemies"
    private List<GameObject> find_all_ranged_enemies()
    {
        //find all ranged enemies and put in the list "list_of_ranged_enemies"
        List<GameObject> list_of_ranged_enemies = new List<GameObject>();
        GameObject temp_ranged_enemy_object = GameObject.Find("1_prefab_ranged_enemy"); //variable that ranged sprites will be assigned to
        int count = 0; //used to construct the name of the sprites
        while (temp_ranged_enemy_object != null) //run until no more ranged enemies can be found
        {
            count++;
            String query = count.ToString() + "_prefab_ranged_enemy"; //construct query
            temp_ranged_enemy_object = GameObject.Find(query); //query scene for sprite
            list_of_ranged_enemies.Add(temp_ranged_enemy_object);
        }
        if(list_of_ranged_enemies.Count != 0)
            list_of_ranged_enemies.RemoveAt(list_of_ranged_enemies.Count - 1); //remove the last element, it will ALWAYS be null
        return list_of_ranged_enemies;
    }

    //seed ranged enemies' stats based on the level multipler
    private void seed_ranged_ranged_stats(List<GameObject> list_of_ranged_enemies, int level_sequence_number)
    {
        if (list_of_ranged_enemies.Count != 0) //if empty no do
        {
            foreach (GameObject item in list_of_ranged_enemies) //iterate through all members list_of_ranged_enemies
            {
                /*
                THIS CODE CAUSES AN ERROR, BUT IT IS NEEDED. DO NOT DELETE.
                ranged_enemy_object temp_script = item.GetComponent<ranged_enemy_object>(); //get ranged_enemy_object script
                //set attack damage
                temp_script.set_attack_damage(base_ranged_attack_damage + (ranged_attack_damage_modifier * level_sequence_number));
                //set hit points
                temp_script.set_hit_points(base_ranged_hit_points + (ranged_hit_points_modifier * level_sequence_number));
                */
            }
        }
    }

    //an enemy has been kilt by player
    public void destroy_enemy(GameObject temp_enemy)
    {
        all_enemies.Remove(temp_enemy);
    }

    //returns the "all_enemmies" list containing all the enemies that are alive
    public List<GameObject> get_all_enemies()
    {
        return all_enemies;
    }
}
