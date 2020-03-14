using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_IncreaseStat : MonoBehaviour
{
    private GameObject menu_Canvas;
    private GameObject statWindow;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        menu_Canvas = GameObject.Find("Menu_Canvas");
        statWindow = menu_Canvas.transform.Find("StatWindow").gameObject;
        player = GameObject.Find("AlienHead");
    }

    //call on button press (stat index determines stat to be leveled)
    public void IncreaseStat(int statIndex)
    {
        player.GetComponent<Alien_Object>().IncreaseStat(statIndex);

        Script_PauseMenu.gameIsPaused = false;

        statWindow.SetActive(false);

        //unfreeze time
        Time.timeScale = 1f;
    }
}