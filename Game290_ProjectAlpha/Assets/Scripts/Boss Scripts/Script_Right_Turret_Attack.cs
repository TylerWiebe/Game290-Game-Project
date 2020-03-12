using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_Right_Turret_Attack : MonoBehaviour
{
    //player game object
    GameObject target = null;

    // Start is called before the first frame update
    void Start()
    {
        //set enemy game object variable
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //call shooting animation for the right turret attack
    public void startRightTurretAttackAnimation()
    {
        this.transform.GetComponent<Animator>().SetBool("rightTurretAttacking", true);
    }

    //shoot five projectiles in a line
    public void shootRightTurret()
    {
        spawnAndShootProject("boss_turret_right_barrel", -36);
        spawnAndShootProject("boss_turret_right_barrel", -30);
        spawnAndShootProject("boss_turret_right_barrel", -26);

        spawnAndShootProject("boss_turret_right_barrel", -4);
        spawnAndShootProject("boss_turret_right_barrel", 0);
        spawnAndShootProject("boss_turret_right_barrel", 4);

        spawnAndShootProject("boss_turret_right_barrel", 26);
        spawnAndShootProject("boss_turret_right_barrel", 30);
        spawnAndShootProject("boss_turret_right_barrel", 34);
        this.transform.GetComponent<Animator>().SetBool("rightTurretAttacking", false);
    }

    //spawn and shoot a projectile at the desingated barrel
    private void spawnAndShootProject(String barrel, int shift)
    {
        //get projectile from resource folder
        GameObject projectile = Resources.Load("boss_projectile") as GameObject;
        //get position of spawn point
        Vector3 barrelVector = GameObject.Find(barrel).transform.position;
        //instantiate proctile resource folder
        GameObject projectile_instance = Instantiate(projectile, new Vector3(barrelVector.x, barrelVector.y, 0), Quaternion.identity);
        //calculate angle to shoot projectile
        //float theta = calculateAngle() * Mathf.Deg2Rad;
        float theta = (this.gameObject.GetComponentInParent<Script_BossAI>().theta - 90 + shift) * Mathf.Deg2Rad;
        //calculate the velocity vector of the boi
        Vector3 vector = new Vector3((float)(10 * Math.Cos(theta)), (float)(10 * Math.Sin(theta)), 0f); //create a vector of x and y velocities
        //set damage of the projectile
        projectile_instance.GetComponent<Script_Ranged_Enemy_Projectile>().set_damage(10);
        //set rotatopm of the projectile
        projectile_instance.transform.rotation = Quaternion.Euler(0, 0, theta * Mathf.Rad2Deg + 90);
        //set velocity of the projectile
        projectile_instance.GetComponent<Rigidbody2D>().velocity = vector;
    }

    //calculate the angle of rotation paying respect to the offset shift
    private float calculateAngle()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.x = targetPosition.x - this.gameObject.transform.position.x;
        targetPosition.y = targetPosition.y - this.gameObject.transform.position.y;
        return (Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg) - 10;
    }
}
