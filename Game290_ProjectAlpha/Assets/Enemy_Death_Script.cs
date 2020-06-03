using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Death_Script : MonoBehaviour
{
    private float deathTime = 10;
    private bool timeToDie = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = new Quaternion(0, 0, Random.Range(0, 360), 0);
    }

    private void FixedUpdate()
    {
        if (timeToDie)
        {
            if (deathTime <= 0)
            {
                Destroy(gameObject);
            }
            deathTime -= Time.deltaTime;

        }
    }

    public void DestroyObject()
    {
        timeToDie = true;
    }


}
