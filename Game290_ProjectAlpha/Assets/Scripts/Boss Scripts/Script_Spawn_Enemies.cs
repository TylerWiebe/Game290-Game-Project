using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Spawn_Enemies : MonoBehaviour
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

    //level number
    public int level_sequence_number = 4;
    
    //choose what enemy to spawn, equal distribution between melee, ranged, or none
    public void chooseEnemyToSpawn()
    {
        int choose = UnityEngine.Random.Range(0, 3);
        if (choose == 0)
            spawnMeleeEnemy();
        else if (choose == 1)
            spawnRangedEnemy();
        //else 
        //  dont spawn anything!
     }

    //spawn a melee enemy
    private void spawnMeleeEnemy()
    {
        //get a ranged prefab enemy from the resource folder
        GameObject temp = Resources.Load("Prefab_Melee_Enemy") as GameObject;

        float x_pos = this.gameObject.transform.position.x;
        float y_pos = this.gameObject.transform.position.y;

        //instanitate game object
        GameObject temp_instance = Instantiate(temp, new Vector3(x_pos, y_pos, 0), Quaternion.identity);

        //get Script_Ranged_Enemy_Object inorder to seed thje stats of the ranged enemy
        Script_Melee_Enemy_Object temp_script = temp_instance.GetComponent<Script_Melee_Enemy_Object>(); //get ranged_enemy_object script

        //aggro enemy right away
        temp_instance.GetComponent<Script_EnemyAI>().playerNotSeen = false;
        temp_instance.GetComponent<Script_EnemyAI>().reachedBounds = false;

        //set the layer
        //temp_instance.layer = LayerMask.NameToLayer("Enemy");

        //set the tag
        temp_instance.tag = "MeleeEnemy";
    }

    //spawn a ranged enemy
    private void spawnRangedEnemy()
    {
        //get a ranged prefab enemy from the resource folder
        GameObject temp = Resources.Load("Prefab_Ranged_Enemy") as GameObject;

        //choose x and y positions
        float x_pos = this.gameObject.transform.position.x;
        float y_pos = this.gameObject.transform.position.y;

        //instanitate game object
        GameObject temp_instance = Instantiate(temp, new Vector3(x_pos, y_pos, 0), Quaternion.identity);

        //get Script_Ranged_Enemy_Object inorder to seed thje stats of the ranged enemy
        Script_Ranged_Enemy_Object temp_script = temp_instance.GetComponent<Script_Ranged_Enemy_Object>();
        
        //aggro enemy right away
        temp_instance.GetComponent<Script_EnemyAI>().playerNotSeen = false;
        temp_instance.GetComponent<Script_EnemyAI>().reachedBounds = false;

        //set the layer
        //temp_instance.layer = LayerMask.NameToLayer("Enemy");

        //set the tag
        temp_instance.tag = "RangedEnemy";

        
    }
}
