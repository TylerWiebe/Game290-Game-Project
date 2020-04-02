using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script must be attached to the attackRange game object and must also be a child of the "enemy" game object.
public class Script_Melee_Enemy_Attack : Script_Melee_Enemy_Object
{
    public int attack_damage;
    public float stoppingDistance = 1.5f;

    //gameobject's audio player
    AudioSource audioSource;

    //turret shoot sound
    public AudioClip slimeAttackSFX;

    //SFX volume
    public float sfxVolume;

    private int damageMultiplier = 2;

    // Start is called before the first frame update
    void Start()
    {
        //set audioSource to the gameobject's "audio controller"
        audioSource = GetComponent<AudioSource>();

        //seed attack damage
        attack_damage = 20 + damageMultiplier * GameObject.Find("GameManager").GetComponent<Script_Enemy_Controller>().levelNumber;
    }

    //when a collision with player occurs, trigger attacks
    IEnumerator OnTriggerStay2D(Collider2D other)
    {
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            //need this check such that multiple attacks are not queued making it so the boi never attacks
            if (!this.transform.GetComponentInParent<Animator>().GetBool("isAttacking"))
            {
                //stop the enemy from moving
                this.transform.parent.gameObject.GetComponent<Script_EnemyAI>().canMove = false;
                //wait time for attack
                yield return new WaitForSeconds(0.25f);
                //call animation
                this.transform.GetComponentInParent<Animator>().SetBool("isAttacking", true);
            }
        }
        yield break;
    }

    //attack function called by animation
    public void Attack()
    {
        //deal damage to the player if they are still in the attack hit box
        if (this.transform.GetComponent<Collider2D>().IsTouching(GameObject.Find("AlienBody").GetComponent<BoxCollider2D>()))
        {
            GameObject.Find("AlienHead").GetComponent<Alien_Object>().Deal_Damage_To_Alien(attack_damage);
        }
        //allow enemy to move
        this.transform.parent.gameObject.GetComponent<Script_EnemyAI>().canMove = true;
        //stop attacking, if the play does not leave the attack range, another attakc will be launched right away
        this.transform.GetComponentInParent<Animator>().SetBool("isAttacking", false);
        playSlimeAttackSFX();
    }

    //play slime attacking sound
    public void playSlimeAttackSFX()
    {
        audioSource.PlayOneShot(slimeAttackSFX, sfxVolume);
    }

    public void set_attack_damage(int attack_damage)
    {
        //this.attack_damage = attack_damage;
    }
}
