using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_StatOrb : MonoBehaviour
{
    //reference to stat window
    [SerializeField]
    private GameObject statWindow = null;

    //reference to alienBody object
    [SerializeField]
    private GameObject player = null;

    //on collision with stat orb
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if the collision was with the player
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            //freeze time
            Time.timeScale = 0f;

            //pull up stat screen
            statWindow.SetActive(true);
        }
    }

    //call on button press (stat index determines stat to be leveled)
    public void IncreaseStat(int statIndex)
    {
        player.GetComponent<Alien_Object>().IncreaseStat(statIndex);

        //unfreeze time
        Time.timeScale = 1f;

        statWindow.SetActive(false);

        //remove stat orb
        Destroy(this.gameObject);
    }
}
