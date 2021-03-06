﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Script_Left_Turret_Attack : MonoBehaviour
{
    //player game object
    GameObject target = null;

    //gameobject's audio player
    AudioSource audioSource;

    //turret shoot sound
    public AudioClip turretShootingSFX;

    //SFX volume
    public float sfxVolume;

    // Start is called before the first frame update
    void Start()
    {
        //set enemy game object variable
        target = GameObject.FindGameObjectWithTag("Player");
        //set audioSource to the gameobject's "audio controller"
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //fade in boss
        StartCoroutine("Fade");
    }

    //fade in boss
    IEnumerator Fade()
    {
        float alpha = 0;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 8f)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 1f, t));
            transform.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }

    //call shooting animation for the left turret attack
    public void startLeftTurretAttackAnimation()
    {
        this.transform.GetComponent<Animator>().SetBool("leftTurretAttacking", true);
    }

    //shoot five projectiles in a line (called from left turret shoot animation)
    private void shootLeftTurret()
    {
        int number = UnityEngine.Random.Range(1, 3);

        spawnAndShootProject("boss_turret_left_barrel", -38);
        spawnAndShootProject("boss_turret_left_barrel", -36);
        spawnAndShootProject("boss_turret_left_barrel", -34);
        spawnAndShootProject("boss_turret_left_barrel", -32);
        spawnAndShootProject("boss_turret_left_barrel", -30);
        spawnAndShootProject("boss_turret_left_barrel", -28);
        spawnAndShootProject("boss_turret_left_barrel", -26);
        spawnAndShootProject("boss_turret_left_barrel", -24);
        spawnAndShootProject("boss_turret_left_barrel", -22);
        spawnAndShootProject("boss_turret_left_barrel", -20);
        spawnAndShootProject("boss_turret_left_barrel", -18);
        spawnAndShootProject("boss_turret_left_barrel", -16);
        spawnAndShootProject("boss_turret_left_barrel", -14);
        if (number == 1)
        {
            spawnAndShootProject("boss_turret_left_barrel", -12);
            spawnAndShootProject("boss_turret_left_barrel", -10);
            spawnAndShootProject("boss_turret_left_barrel", -8);
            spawnAndShootProject("boss_turret_left_barrel", -6);
            spawnAndShootProject("boss_turret_left_barrel", -4);
        }

        spawnAndShootProject("boss_turret_left_barrel", -2);
        spawnAndShootProject("boss_turret_left_barrel", 0);
        spawnAndShootProject("boss_turret_left_barrel", 2);
        if (number == 2)
        {
            spawnAndShootProject("boss_turret_left_barrel", 4);
            spawnAndShootProject("boss_turret_left_barrel", 6);
            spawnAndShootProject("boss_turret_left_barrel", 8);
            spawnAndShootProject("boss_turret_left_barrel", 10);
            spawnAndShootProject("boss_turret_left_barrel", 12);
        }

        spawnAndShootProject("boss_turret_left_barrel", 14);
        spawnAndShootProject("boss_turret_left_barrel", 16);
        spawnAndShootProject("boss_turret_left_barrel", 18);
        spawnAndShootProject("boss_turret_left_barrel", 20);
        spawnAndShootProject("boss_turret_left_barrel", 22);
        spawnAndShootProject("boss_turret_left_barrel", 24);
        spawnAndShootProject("boss_turret_left_barrel", 26);
        spawnAndShootProject("boss_turret_left_barrel", 28);
        spawnAndShootProject("boss_turret_left_barrel", 30);
        spawnAndShootProject("boss_turret_left_barrel", 32);
        spawnAndShootProject("boss_turret_left_barrel", 34);
        spawnAndShootProject("boss_turret_left_barrel", 36);
        spawnAndShootProject("boss_turret_left_barrel", 38);

        this.transform.GetComponent<Animator>().SetBool("leftTurretAttacking", false);
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
        Vector3 vector = new Vector3((float)(8 * Math.Cos(theta)), (float)(8 * Math.Sin(theta)), 0f); //create a vector of x and y velocities
        //set damage of the projectile
        projectile_instance.GetComponent<Script_Boss_Projectile>().set_damage(10);
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

    //play turret shooting sound
    public void playTurretShootingSFX()
    {   
        audioSource.PlayOneShot(turretShootingSFX, sfxVolume);
    }
}
