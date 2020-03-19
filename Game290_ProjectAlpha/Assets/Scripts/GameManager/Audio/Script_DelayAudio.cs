using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DelayAudio : MonoBehaviour
{
    //play audio after 2seconds
    void Start()
    {
        this.GetComponent<AudioSource>().Play(88200);
    }
}
