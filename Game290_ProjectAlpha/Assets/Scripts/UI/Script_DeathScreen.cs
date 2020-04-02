using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DeathScreenSprite : MonoBehaviour
{
    private GameObject backgroundEgg;

    private GameObject redEgg;
    private GameObject blueEgg;
    private GameObject yellowEgg;

    // Start is called before the first frame update
    void Start()
    {
        redEgg = gameObject.transform.Find("redEgg").gameObject;
        blueEgg = gameObject.transform.Find("blueEgg").gameObject;
        yellowEgg = gameObject.transform.Find("yellowEgg").gameObject;

        switch (Alien_Object.alienFormDuringDeath)
        {
            case 0:
                blueEgg.SetActive(true);
                break;
            case 1:
                redEgg.SetActive(true);
                break;
            case 2:
                yellowEgg.SetActive(true);
                break;
            default:
                yellowEgg.SetActive(true);
                break;
        }
    }
}
