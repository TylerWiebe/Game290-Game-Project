using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Boss_Object : MonoBehaviour
{
    private GameObject doorPrefab;
    private int hitPoints = 1000;

    // Start is called before the first frame update
    void Start()
    {
        doorPrefab = Resources.Load("EndGameDoor") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //damage boss
    public void damageBoss(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
            destroyBoss();
    }

    //called by destruction/dying animation upon completion of animation
    public void destroyBoss()
    {
        //spawn door at position of boss at death
        Instantiate(doorPrefab, this.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

}
