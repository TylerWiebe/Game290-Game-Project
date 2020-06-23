using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SpawnStatOrb : MonoBehaviour
{
    //reference to stat window
    [SerializeField]
    private GameObject statOrb = null;

    public GameObject cloneOrb;

    private static int orbsDropped = 0;

    //when called determine whether to spawn a stat orb
    public void SpawnStatOrb(int spawnChance, Vector3 position)
    {
        //5% chance of spawn
        if (Random.Range(0, 100) <= spawnChance - orbsDropped)
        {
            orbsDropped++;
            //instantiate statOrb
            GameObject cloneOrb = Instantiate(statOrb, position, this.transform.rotation);
        }
    }

    public void resetOrbsDropped()
    {
        orbsDropped = 0;
    }
}
