using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_EnemyHPBar : MonoBehaviour
{
    float hp;
    float maxHP;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.tag == "MeleeEnemy")
        {
            hp = transform.parent.GetComponent<Script_Melee_Enemy_Object>().get_hit_points();
        }
        else if (transform.parent.tag == "RangedEnemy")
        {
            hp = transform.parent.GetComponent<Script_Ranged_Enemy_Object>().get_hit_points();
        }
        maxHP = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.tag == "MeleeEnemy")
        {
            hp = transform.parent.GetComponent<Script_Melee_Enemy_Object>().get_hit_points();
        }
        else if (transform.parent.tag == "RangedEnemy")
        {
            hp = transform.parent.GetComponent<Script_Ranged_Enemy_Object>().get_hit_points();
        }
        float hpPercent = ( hp / maxHP )* 100;
        float healthBarScalar = (hpPercent * 0.25f) / 100;
        transform.localScale = new Vector3(healthBarScalar, 0.05f, 1);
    }
}
