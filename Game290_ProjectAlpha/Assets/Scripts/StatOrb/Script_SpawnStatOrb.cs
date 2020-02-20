using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SpawnStatOrb : MonoBehaviour
{
    //reference to stat window
    [SerializeField]
    private GameObject statOrb = null;

    //when called determine whether to spawn a stat orb
    public void SpawnStatOrb(int spawnChance)
    {
        //5% chance of spawn
        if (Random.Range(0, 100) <= spawnChance)
        {
            //instantiate statOrb
            Instantiate(statOrb, transform.position, transform.rotation);
        }
    }
}
