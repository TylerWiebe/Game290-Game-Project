using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_StatOrb : MonoBehaviour
{
    private GameObject menu_Canvas;
    private GameObject statWindow;

    void Start()
    {
        menu_Canvas = GameObject.Find("Menu_Canvas");
        statWindow = menu_Canvas.transform.Find("StatWindow").gameObject;
    }

    //on collision with stat orb
    private void OnTriggerEnter2D(Collider2D other)
    {
        //check if the collision was with the player
        if ((other.tag == "player") || (other.tag == "Player"))
        {
            Script_PauseMenu.gameIsPaused = true;

            //freeze time
            Time.timeScale = 0f;

            //pull up stat screen
            statWindow.SetActive(true);

            //remove stat orb
            Destroy(this.gameObject);
        }
    }
}