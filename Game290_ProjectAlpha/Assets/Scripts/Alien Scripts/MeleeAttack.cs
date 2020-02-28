using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MeleeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private Script_Ranged_Enemy_Object myScript;
    private Alien_Object alienScript;

    private float attackTime;//countdown variable to know how much time is left in the attack
    public float attackLength;//how long the attack is going to take.

    public LayerMask enemy;
    //Collider2D[] enemies;

    private int attackForm;

    void Start()
    {
        alienScript = this.GetComponentInParent<Alien_Object>();
    }

    private void FixedUpdate()
    {
        if (attackTime <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Collider2D[] enemies = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(1, 5), alienScript.getBodyAngle());
                bool noEnemy = true;
                int i = 0;
                while (noEnemy && i < enemies.Length)
                {
                    if (enemies[i].tag == "RangedEnemy")
                    {
                        enemies[i].gameObject.GetComponent<Script_Ranged_Enemy_Object>().attacked(alienScript.getDamage());
                        noEnemy = false;
                        Debug.Log("ranged Enemy Hit");
                    }
                    else if (enemies[i].tag == "MeleeEnemy")
                    {
                        enemies[i].gameObject.GetComponent<Script_Melee_Enemy_Object>().attacked(alienScript.getDamage());
                        noEnemy = false;
                        Debug.Log("melee enemy Hit");
                    }
                    i++;
                }
                attackTime = attackLength;
                //Debug.Log("set Attack Length");

            }

        }
        else
        {
            attackTime -= Time.deltaTime;
            //Debug.Log(attackTime);
        }
    }

    public void setAttackForm(int i)
    {
        attackForm = i;
    }
}
