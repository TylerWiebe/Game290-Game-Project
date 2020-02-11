using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MeleeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private bool haveEnemyCollision = false;
    private GameObject myobject;
    public GameObject gameManager;
    private Script_Ranged_Enemy_Object myScript;
    private Alien_Object alienScript;

    void Start()
    {
        myScript = gameManager.GetComponent<Script_Ranged_Enemy_Object>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            haveEnemyCollision = true;
            myobject = collision.gameObject;
        }
    }

    private void OnMouseDown()
    {
        if(haveEnemyCollision)
        {
            alienScript = this.gameObject.transform.parent.gameObject.GetComponent<Alien_Object>();
            myScript.attacked(alienScript.getDamage());
        }
    }
}
