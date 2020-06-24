using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ranged_Enemy_Object : MonoBehaviour
{
    public int attack_damage;
    public int damageMultiplier = 4;

    public float hit_points;
    public int healthMultiplier = 5;

    //stat orb spawn chance
    private int spawnChance = 5;
    private Script_SwapMusic swapMusicScript;

    private GameObject enemy_body;
    private GameObject enemy_attack_cone;

    //Death object Prefab
    //spawned on death of enemy
    public GameObject deathPrefab_Ranged_enemy;

    //gameobject's audio player
    AudioSource audioSource;

    //guard hurt sound
    public AudioClip rangedGuardDamagedSFX;

    //guard death sound 1
    public AudioClip rangedGuardDeath1SFX;

    //guard death sound 2
    public AudioClip rangedGuardDeath2SFX;

    //SFX volume
    public float sfxVolume;

    // Start is called before the first frame update
    void Start()
    {
        //set audioSource to the gameobject's "audio controller"
        audioSource = GetComponent<AudioSource>();

        swapMusicScript = GameObject.Find("Music").GetComponent<Script_SwapMusic>();

        //seed hit points
        hit_points = 50 + healthMultiplier * GameObject.Find("GameManager").GetComponent<Script_Enemy_Controller>().levelNumber;

        //seed attack damage
        attack_damage = 50 + damageMultiplier * GameObject.Find("GameManager").GetComponent<Script_Enemy_Controller>().levelNumber;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int get_attack_damage()
    {
        return attack_damage;
    }

    public float get_hit_points()
    {
        return hit_points;
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
        //play ranged guard damaged sound only when the guard is damaged and will not die, otherwise play death sound
        if (hit_points - damage > 0)
            playRangedGuardDamagedSFX();
        
        //remove hitpoints from the boss
        hit_points -= damage;

        //if ded
        if (hit_points <= 0)
        {

            //play the SFX when the guard dies
            int random = UnityEngine.Random.Range(1, 3);
            if (random == 1)
                playRangedGuardDeath1SFX();
            else
                playRangedGuardDeath2SFX();

            Instantiate(deathPrefab_Ranged_enemy, transform.position, transform.rotation);
            destroy();

        /*
                //stop enemy from moving when dead
                this.transform.GetComponentInParent<Script_EnemyAI>().canMove = false;

            //set the enemy's velocity to zero when it dies
            this.transform.GetComponentInParent<Script_EnemyAI>().rigidBody.velocity = Vector3.zero;

            //stop enemy from rotating
            this.transform.GetComponentInParent<Script_EnemyAI>().canRotate = false;

            //stop enemy from attacking if it is mid attack
            this.transform.GetComponentInParent<Animator>().SetBool("isAttacking", false);

            //call the death animation
            this.transform.GetComponentInParent<Animator>().SetBool("isDead", true);
            */
        }
    }

    //play ranged guard damaged sound
    public void playRangedGuardDamagedSFX()
    {
        audioSource.PlayOneShot(rangedGuardDamagedSFX, sfxVolume);
    }

    //play ranged guard death sound 1
    public void playRangedGuardDeath1SFX()
    {
        audioSource.PlayOneShot(rangedGuardDeath1SFX, sfxVolume);
    }

    //play ranged guard death sound 2
    public void playRangedGuardDeath2SFX()
    {
        audioSource.PlayOneShot(rangedGuardDeath2SFX, sfxVolume);
    }

    public void destroy()
    {
        gameObject.GetComponent<Script_SpawnStatOrb>().SpawnStatOrb(spawnChance, gameObject.transform.position);
        swapMusicScript.alertedEnemiesCount -= 1;

        Destroy(gameObject);
    }
}
