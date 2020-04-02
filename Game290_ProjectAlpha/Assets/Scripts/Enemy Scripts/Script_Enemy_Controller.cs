using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Enemy_Controller : MonoBehaviour
{
    private int spawnChance = 100;
    private Script_SwapMusic swapMusicScript;
    public int levelNumber;

    // Start is called before the first frame update
    void Start()
    {
        swapMusicScript = GameObject.Find("Music").GetComponent<Script_SwapMusic>();

    }

    //Update is called once per frame
    void Update()
    {
        
    }
}
