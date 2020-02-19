using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Enemy_Controller : MonoBehaviour
{
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

    //list of all enemies on the scene
    private List<GameObject> all_enemies = new List<GameObject>();

    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        //Need to adjust level sequence nummber
        int level_sequence_number = 1;
        
        /*
         * Spawn Enemies
         * Randomize the number of melee and ranged enemies
         */
        int number_of_enemies = 5;
        int choose = UnityEngine.Random.Range(0,6);
        int number_of_ranged_enemies = choose;
        int number_of_melee_enemies = number_of_enemies - choose;

        //spawn number_of_ranged_enemies ranged enemies
        for (int i = 0; i < number_of_ranged_enemies; i++)
        {
            //get a ranged prefab enemy from the resource folder
            GameObject temp = Resources.Load("Prefab_Ranged_Enemy") as GameObject;
            int x_pos = UnityEngine.Random.Range(-15, 15); //random x pos (could be looked up from a table later)
            int y_pos = UnityEngine.Random.Range(-15, 15); //random y pos (could be looked up from a table later)
            temp.tag = "MeleeEnemy";
            Instantiate(temp, new Vector3(x_pos, y_pos, 0), Quaternion.identity); //load to scene
            seed_ranged_enemy_stats(temp, level_sequence_number); //seed ranged enemy stats
            all_enemies.Add(temp); //add enemy to list of all enemies
        }

        //spawn number_of_melee_enemies melee enemies
        for (int i = 0; i < number_of_melee_enemies; i++)
        {
            //get a ranged prefab enemy from the resource folder
            GameObject temp = Resources.Load("Prefab_Melee_Enemy") as GameObject;
            int x_pos = UnityEngine.Random.Range(-15, 15); //random x pos (could be looked up from a table later)
            int y_pos = UnityEngine.Random.Range(-15, 15); //random y pos (could be looked up from a table later)
            Instantiate(temp, new Vector3(x_pos, y_pos, 0), Quaternion.identity); //load to scene
            seed_melee_enemy_stats(temp, level_sequence_number); //seed melee enemy stats
            all_enemies.Add(temp); //add enemy to list of all enemies
        }
    }

    //Update is called once per frame
    void Update()
    {
        
    }

    //seed melee enemies' stats based on the level multipler
    private void seed_melee_enemy_stats(GameObject temp_gameObject, int level_sequence_number)
    {
        Script_Melee_Enemy_Object temp_script = temp_gameObject.GetComponent<Script_Melee_Enemy_Object>(); //get melee_enemy_object script
        //set attack damage
        temp_script.set_attack_damage(base_melee_attack_damage + (melee_attack_damage_modifier * level_sequence_number));
        //set hit points
        temp_script.set_hit_points(base_melee_hit_points + (melee_hit_points_modifier * level_sequence_number));
        //Debug.Log("Initial Hitpoints" + (base_melee_hit_points + (melee_hit_points_modifier * level_sequence_number)).ToString());
        //set the layer
        temp_gameObject.layer = LayerMask.NameToLayer("Enemy");
        //set the tag
        temp_gameObject.tag = "MeleeEnemy";
    }
    
    //seed ranged enemies' stats based on the level multipler
    private void seed_ranged_enemy_stats(GameObject temp_gameObject, int level_sequence_number)
    {
        Script_Ranged_Enemy_Object temp_script = temp_gameObject.GetComponent<Script_Ranged_Enemy_Object>(); //get ranged_enemy_object script
        //set attack damage
        temp_script.set_attack_damage(base_ranged_attack_damage + (ranged_attack_damage_modifier * level_sequence_number));
        //set hit points
        temp_script.set_hit_points(base_ranged_hit_points + (ranged_hit_points_modifier * level_sequence_number));
        //set the layer
        temp_gameObject.layer = LayerMask.NameToLayer("Enemy");
        //set the tag
        temp_gameObject.tag = "RangedEnemy";
    }

    //an enemy has been kilt by player
    public void destroy_enemy(GameObject temp_enemy)
    {
        //spawn a stat orb with percent chance (5)
        this.GetComponent<Script_SpawnStatOrb>().SpawnStatOrb(5);

        all_enemies.Remove(temp_enemy);
    }

    //returns the "all_enemmies" list containing all the enemies that are alive
    public List<GameObject> get_all_enemies()
    {
        return all_enemies;
    }
}
