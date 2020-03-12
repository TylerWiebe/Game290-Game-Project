using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireProjectile : MonoBehaviour
{
    public Transform shootLocation;
    public GameObject projectile;
    public GameObject UI_Object;
    private GameObject chargeBar;
    private Script_ProjectileCharges myChargeScript;

    public float bulletForce;

    public Animator anim;

    private Alien_Object myAlienObjectScript;
    private void Start()
    {
        chargeBar = UI_Object.transform.Find("ProjectileCharges").gameObject;
        myAlienObjectScript = this.gameObject.GetComponent<Alien_Object>();
        myChargeScript = chargeBar.GetComponent<Script_ProjectileCharges>();
    }


    private void shoot()
    {
        if (myChargeScript.getRangedCharges() > 0)
        {
            GameObject myProjectile = (GameObject)Instantiate(projectile, shootLocation.position, shootLocation.rotation);
            Rigidbody2D rb = myProjectile.GetComponent<Rigidbody2D>();
            rb.AddForce(shootLocation.up * bulletForce, ForceMode2D.Impulse);
            anim.SetBool("is_attacking", false);
        }
    }
}
