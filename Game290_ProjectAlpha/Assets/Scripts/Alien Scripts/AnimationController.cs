using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private bool rangedMorphing;

    private GameObject alienHead;
    private GameObject alienBody;
    private Alien_Object myAlienObject;
    public Animator anim;
    private Animator animHead;
    // Start is called before the first frame update
    void Start()
    {
        rangedMorphing = false;
        alienHead = GameObject.Find("AlienBody");
        alienHead = GameObject.Find("AlienHead");
        animHead = alienHead.GetComponent<Animator>();
        myAlienObject = alienHead.GetComponent<Alien_Object>();
    }

    void update()
    {
        Debug.Log("should be fixed");
        if (rangedMorphing)
        {
            alienHead.transform.rotation = alienBody.transform.rotation;
        }
    }

    public void update_alien_morph_state()
    {
        int currentClass = myAlienObject.getCurrentClass();
        anim.SetBool("morph", false);
        anim.SetInteger("CurrentClass", currentClass);
        if (rangedMorphing)
        {
            rangedMorphing = false;
            alienHead.GetComponent<Alien_Object>().setDoMouseRotation(true);
        }
    }

    public void morph_ended()
    {
        bool temp = true;
        int currentClass = myAlienObject.getCurrentClass();

        myAlienObject.setCanMove(temp);
    }

    public void morphRanged()
    {
        int currentClass = myAlienObject.getCurrentClass();
        if (currentClass == 2)
        {
            animHead.SetBool("isRanged", true);
        }
        else
        {
            animHead.SetBool("isRanged", false);
        }
    }

    private void ranged_cannon_morph_anim_sync()
    {
        rangedMorphing = true;
        alienHead.GetComponent<Alien_Object>().setDoMouseRotation(false);
    }

    public void morph_started()
    {

        int currentClass = myAlienObject.getCurrentClass();
        if (currentClass != 2)
        {
            animHead.SetBool("isRanged", false);

        }
        Debug.Log("morphStarted");
        myAlienObject.setCanMove(false);
    }

    public void finishedAttack()
    {
        anim.SetBool("isAttacking", false);
    }
}
