using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    //filtering layer mask to find only enemies
    public LayerMask enemies;

    //a reference to the alien head to get alien damage
    private GameObject alienHead;

    public GameObject crack;

    //knockback Force
    public float knockbackForce;
    Vector3 myVector = new Vector3();


    //assassin attack variables
    public GameObject assassinHitPoint;
    private float AssassinDmgMultiplier = 3;
    public float assassinAttackRadius;

    //bruiser attack variables
    public GameObject bruiserHitPoint;
    public float bruiserAttackRadius;

    // Start is called before the first frame update
    void Start()
    {
        alienHead = GameObject.Find("AlienHead");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attackAssassin()
    {
        //Debug.Log("assassinAttackAttempt");
        Collider2D[] collisions = Physics2D.OverlapCircleAll(assassinHitPoint.transform.position, assassinAttackRadius, enemies);
        bool temp = true;
        foreach (Collider2D collider in collisions)
        {
            if (collider.gameObject.tag == "MeleeEnemy" && temp)
            {
                collider.gameObject.GetComponent<Script_Melee_Enemy_Object>().attacked(alienHead.GetComponent<Alien_Object>().getDamage()* AssassinDmgMultiplier);
                myVector = this.gameObject.transform.position - collider.gameObject.transform.position;
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(myVector * knockbackForce, ForceMode2D.Impulse);
                temp = false;
            }
            else if (collider.gameObject.tag == "RangedEnemy" && temp)
            {
                collider.gameObject.GetComponent<Script_Ranged_Enemy_Object>().attacked(alienHead.GetComponent<Alien_Object>().getDamage()* AssassinDmgMultiplier);
                myVector = this.gameObject.transform.position - collider.gameObject.transform.position;
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(myVector * knockbackForce, ForceMode2D.Impulse);
                temp = false;
            }
            else if (collider.gameObject.tag == "Boss" && temp)
            {
               
                Debug.Log("hitBoss");
                collider.gameObject.GetComponent<Script_Boss_Object>().damageBoss(alienHead.GetComponent<Alien_Object>().getDamage()* AssassinDmgMultiplier);
                temp = false;
            }
        }
    }


    //This function can be used to draw the hit box of the melee attacks
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(bruiserHitPoint.transform.position, bruiserAttackRadius);
    }
    

    public void attackBruiser()
    {
        Instantiate(crack, bruiserHitPoint.transform.position, bruiserHitPoint.transform.rotation);
        Debug.Log("triedAttacking");
        Collider2D[] collisions = Physics2D.OverlapCircleAll(bruiserHitPoint.transform.position, bruiserAttackRadius, enemies);
        foreach (Collider2D collider in collisions)
        {
            if (collider.gameObject.tag == "MeleeEnemy")
            {
                collider.gameObject.GetComponent<Script_Melee_Enemy_Object>().attacked(alienHead.GetComponent<Alien_Object>().getDamage());
            }
            else if (collider.gameObject.tag == "RangedEnemy")
            {
                collider.gameObject.GetComponent<Script_Ranged_Enemy_Object>().attacked(alienHead.GetComponent<Alien_Object>().getDamage());
            }
            else if (collider.gameObject.tag == "Boss")
            {
                collider.gameObject.GetComponent<Script_Boss_Object>().damageBoss(alienHead.GetComponent<Alien_Object>().getDamage());
            }
        }
    }
}
