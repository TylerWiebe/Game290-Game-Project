using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private GameObject alienHead;
    private Alien_Object myAlienObject;
    public Animator anim;
    public Animator animHead;
    // Start is called before the first frame update
    void Start()
    {
        alienHead = GameObject.Find("AlienHead");
        myAlienObject = alienHead.GetComponent<Alien_Object>();
    }


    public void update_alien_morph_state()
    {
        int currentClass = myAlienObject.getCurrentClass();
        anim.SetBool("morph", false);
        anim.SetInteger("CurrentClass", currentClass);
    }

    public void morph_ended()
    {
        bool temp = true;
        int currentClass = myAlienObject.getCurrentClass();
        if (currentClass == 2)
        {
            animHead.SetBool("isRanged", true);
        }
        else
        {
            animHead.SetBool("isRanged", false);
        }
        myAlienObject.setCanMove(temp);
    }

    public void morph_started()
    {
        bool temp = false;
        myAlienObject.setCanMove(temp);
    }

    public void finishedAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
