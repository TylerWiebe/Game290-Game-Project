using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_Shotgun_Attack : MonoBehaviour
{
    //controls when the script can attack
    private bool canAttack = false;

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

    public void startAttack()
    {
        //start an attack, starts in 0.5 seconds, shoots every 5 seconds
        InvokeRepeating("attack", 0.5f, 5f);
    }

    public void stopAttack()
    {
        //cancel attack
        CancelInvoke();
    }

    //shot five projectiles in a line
    private void attack()
    {
        spawnAndShootProject("Barrel1");
        spawnAndShootProject("Barrel2");
        spawnAndShootProject("Barrel3");
        spawnAndShootProject("Barrel4");
        spawnAndShootProject("Barrel5");
    }

    //spawn and shoot a projectile at the desingated barrel
    private void spawnAndShootProject(String barrel)
    {
        //get projectile from resource folder
        GameObject projectile = Resources.Load("Ranged_Enemy_Projectile") as GameObject;
        //get position of spawn point
        Vector3 barrelVector = GameObject.Find(barrel).transform.position;
        //instantiate proctile resource folder
        GameObject projectile_instance = Instantiate(projectile, new Vector3(barrelVector.x, barrelVector.y, 0), Quaternion.identity);
        //calculate angle to shoot projectile
        float theta = calculateAngle() * Mathf.Deg2Rad;
        //calculate the velocity vector of the boi
        Vector3 vector = new Vector3((float)(10 * Math.Cos(theta)), (float)(10 * Math.Sin(theta)), 0f); //create a vector of x and y velocities
        //set damage of the projectile
        projectile_instance.GetComponent<Script_Ranged_Enemy_Projectile>().set_damage(10);
        //set rotatopm of the projectile
        projectile_instance.transform.rotation = Quaternion.Euler(0, 0, theta * Mathf.Rad2Deg - 90);
        //set velocity of the projectile
        projectile_instance.GetComponent<Rigidbody2D>().velocity = vector;
    }

    //calculate the angle of rotation paying respect to the offset shift
    private float calculateAngle()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.x = targetPosition.x - this.gameObject.transform.position.x;
        targetPosition.y = targetPosition.y - this.gameObject.transform.position.y;
        return (Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg);
    }

}
