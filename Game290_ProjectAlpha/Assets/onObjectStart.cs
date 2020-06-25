using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onObjectStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LocalPlayerStats.Instance.localPlayerData.timesWon++;
    }

}
