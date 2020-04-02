using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Flame_Thrower_Attack : MonoBehaviour
{
    //reference hit box script
    Script_Flame_Thrower_Hit_Box hit_box = null;

    //### State Variables ###
    //controls when the script can attack
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        hit_box = GameObject.Find("boss_flame_thrower_hit_box").GetComponent<Script_Flame_Thrower_Hit_Box>();
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

    //stop attack
    public void startAttack()
    {
        //use flame thrower
        this.transform.GetComponent<Animator>().SetBool("isAttacking", true);
        hit_box.canAttack = true;
    }

    //stop attack
    public void stopAttack()
    {
        //no use flame thrower
        hit_box.canAttack = false;
        this.transform.GetComponent<Animator>().SetBool("isAttacking", false);
    }
}
