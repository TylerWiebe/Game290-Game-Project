using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DelayAudio : MonoBehaviour
{
    //play audio after .5 seconds
    void Start()
    {
        this.GetComponent<AudioSource>().Play(88200);
    }
}
